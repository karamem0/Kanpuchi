using Kanpuchi.Models;
using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Kanpuchi.Services {

    /// <summary>
    /// Twitter データを操作するオブジェクトを表します。
    /// </summary>
    public class TweetService : IDisposable {

        /// <summary>
        /// Twitter コンテキストを表します。
        /// </summary>
        private TwitterContext twitterContext;

        /// <summary>
        /// データベース コンテキストを表します。
        /// </summary>
        private DefaultConnection dbContext;

        /// <summary>
        /// <see cref="Kanpuchi.Services.TweetService"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TweetService() {
            var consumerKey = ConfigurationManager.AppSettings["TwitterConsumerKey"];
            var consumerSecret = ConfigurationManager.AppSettings["TwitterConsumerSecret"];
            var accessToken = ConfigurationManager.AppSettings["TwitterAccessToken"];
            var accessTokenSecret = ConfigurationManager.AppSettings["TwitterAccessTokenSecret"];
            var authorizer = new SingleUserAuthorizer() {
                CredentialStore = new SingleUserInMemoryCredentialStore() {
                    ConsumerKey = consumerKey,
                    ConsumerSecret = consumerSecret,
                    AccessToken = accessToken,
                    AccessTokenSecret = accessTokenSecret,
                }
            };
            this.twitterContext = new TwitterContext(authorizer);
            this.dbContext = new DefaultConnection();
            this.dbContext.Database.Log = str => Debug.WriteLine(str);
        }

        /// <summary>
        /// API を読み込んで Twitter ステータスを追加します。
        /// </summary>
        public void AddTwitterStatus() {
            this.dbContext.TwitterStatuses.AddOrUpdate(
                this.dbContext.TwitterUsers.ToList()
                    .Select(dbUser => {
                        var twitterUser = this.twitterContext.User
                            .Where(x => x.Type == UserType.Show)
                            .Where(x => x.UserID == ulong.Parse(dbUser.UserId))
                            .FirstOrDefault();
                        if (twitterUser != null) {
                            dbUser.UserName = twitterUser.Name;
                            dbUser.ScreenName = twitterUser.ScreenNameResponse;
                            dbUser.ProfileImageUrl = twitterUser.ProfileImageUrl;
                            dbUser.UpdatedAt = DateTime.UtcNow;
                        }
                        return dbUser;
                    })
                    .SelectMany(dbUser => this.twitterContext.Status
                            .Where(x => x.Type == StatusType.User)
                            .Where(x => x.Count == 100)
                            .Where(x => x.UserID == ulong.Parse(dbUser.UserId))
                            .ToList()
                            .Where(x => x.InReplyToScreenName == null)
                            .Where(x => x.IncludeRetweets != true)
                            .Select(x => new TwitterStatus() {
                                StatusId = x.StatusID.ToString(),
                                UserId = x.UserID.ToString(),
                                Text = x.Text,
                                CreatedAt = x.CreatedAt.ToUniversalTime(),
                                UpdatedAt = DateTime.UtcNow,
                            })
                    )
                    .ToArray()
            );
            this.dbContext.SaveChanges();
        }

        /// <summary>
        /// 古くなった Twitter ステータスを削除します。
        /// </summary>
        public void RemoveTwitterStatus() {
            var storeCount = default(int);
            var storeCountString = ConfigurationManager.AppSettings["TwitterStatusStoreCount"];
            if (int.TryParse(storeCountString, out storeCount) == true) {
                this.dbContext.Database.ExecuteSqlCommand(
                    " DELETE T FROM (" +
                    " SELECT * FROM [dbo].[TwitterStatus]" +
                    " ORDER BY [CreatedAt] DESC" +
                    " OFFSET @offset ROWS" +
                    " ) AS T",
                    new SqlParameter("@offset", storeCount)
                );
            }
            this.dbContext.SaveChanges();
        }

        /// <summary>
        /// 現在のオブジェクトで使用されているリソースを解放します。
        /// </summary>
        public void Dispose() {
            if (this.dbContext != null) {
                this.dbContext.Dispose();
            }
            if (this.twitterContext != null) {
                this.twitterContext.Dispose();
            }
        }

    }

}