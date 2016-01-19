using Karamem0.Kanpuchi.Models;
using Karamem0.Kanpuchi.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web.Http;

namespace Karamem0.Kanpuchi.Controllers {

    /// <summary>
    /// ツイートを取得する API コントローラーを表します。
    /// </summary>
    public class TweetController : ApiController {

        /// <summary>
        /// ツイートのパーマリンクを表します。
        /// </summary>
        private static readonly string Permalink = "https://twitter.com/{0}/status/{1}";

        /// <summary>
        /// データベース コンテキストを表します。
        /// </summary>
        private DefaultConnectionContext dbContext;

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Controllers.TweetController"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TweetController() {
            this.dbContext = new DefaultConnectionContext();
        }

        /// <summary>
        /// 範囲および件数を指定してツイートのコレクションを返します。
        /// </summary>
        /// <param name="minId">最小値を示す <see cref="System.String"/>。</param>
        /// <param name="maxId">最大値を示す <see cref="System.String"/>。</param>
        /// <param name="itemCount">取得する件数を示す <see cref="System.Int32"/>。</param>
        /// <returns>検索結果を示す <see cref="T:System.Collections.Generic.IEnumerable`1"/>。</returns>
        public IEnumerable<TweetViewModel> GetTweet(string minId = null, string maxId = null, int itemCount = 20) {
            return this.dbContext.TwitterStatuses
                .Include(x => x.User)
                .OrderByDescending(x => x.StatusId)
                .Where(x => minId == null || x.StatusId.CompareTo(minId) > 0)
                .Where(x => maxId == null || x.StatusId.CompareTo(maxId) < 0)
                .Take(itemCount)
                .ToList()
                .Select(x => new TweetViewModel() {
                    StatusId = x.StatusId,
                    UserId = x.User.UserId,
                    UserName = x.User.UserName,
                    ScreenName = x.User.ScreenName,
                    ProfileImageUrl = x.User.ProfileImageUrl,
                    Permalink = string.Format(Permalink, x.User.ScreenName, x.StatusId),
                    CreatedAt = x.CreatedAt,
                    Text = x.Text,
                })
                .ToArray();
        }

        /// <summary>
        /// 指定したステータス ID のツイートを返します。
        /// </summary>
        /// <param name="statusId">ステータス ID を示す <see cref="System.String"/>。</param>
        /// <returns>検索結果を示す <see cref="Karamem0.Kanpuchi.ViewModels.TweetViewModel"/>。</returns>
        public TweetViewModel GetTweet(string statusId) {
            return this.dbContext.TwitterStatuses
                .Where(x => x.StatusId == statusId)
                .ToList()
                .Select(x => new TweetViewModel() {
                    StatusId = x.StatusId,
                    UserId = x.User.UserId,
                    UserName = x.User.UserName,
                    ScreenName = x.User.ScreenName,
                    ProfileImageUrl = x.User.ProfileImageUrl,
                    CreatedAt = x.CreatedAt,
                    Text = x.Text,
                })
                .FirstOrDefault();
        }

        /// <summary>
        /// 現在のオブジェクトで使用されているリソースを解放します。
        /// </summary>
        /// <param name="disposing">
        /// マネージ オブジェクトとアンマネージ オブジェクトの両方を解放する場合は
        /// true。アンマネージ オブジェクトのみ解放する場合は false。
        /// </param>
        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);
            if (disposing == true) {
                if (this.dbContext != null) {
                    this.dbContext.Dispose();
                }
            }
        }

    }

}
