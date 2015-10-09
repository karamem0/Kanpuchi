using Kanpuchi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Kanpuchi.Services {

    /// <summary>
    /// まとめデータを操作するオブジェクトを表します。
    /// </summary>
    public class MatomeService : IDisposable {

        /// <summary>
        /// データベース コンテキストを表します。
        /// </summary>
        private DefaultConnection dbContext;

        /// <summary>
        /// <see cref="Kanpuchi.Services.MatomeService"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MatomeService() {
            this.dbContext = new DefaultConnection();
            this.dbContext.Database.Log = str => Debug.WriteLine(str);
        }

        /// <summary>
        /// フィードを読み込んでまとめ記事を追加します。
        /// </summary>
        public void AddMatomeEntry() {
            this.dbContext.MatomeEntries.AddOrUpdate(
                matomeEntry => matomeEntry.Url,
                this.dbContext.MatomeSites
                    .ToList()
                    .SelectMany(matomeSite => {
                        try {
                            var element = XElement.Load(matomeSite.FeedUrl);
                            if (matomeSite.FeedFormat == "Atom0.3") {
                                var ns = XNamespace.Get("http://purl.org/atom/ns#");
                                return element.Elements(ns + "entry")
                                    .Select(x => new MatomeEntry() {
                                        EntryId = Guid.NewGuid(),
                                        SiteId = matomeSite.SiteId,
                                        Title = (string)x.Element(ns + "title"),
                                        Url = (string)x.Element(ns + "link").Attribute("href"),
                                        Content = (string)x.Element(ns + "content"),
                                        CreatedAt = (DateTime)x.Element(ns + "issued"),
                                        UpdatedAt = DateTime.UtcNow,
                                    });
                            }
                        } catch { }
                        return Enumerable.Empty<MatomeEntry>();
                    })
                    .ToArray()
            );
            this.dbContext.SaveChanges();
        }

        /// <summary>
        /// 古くなったまとめ記事を削除します。
        /// </summary>
        public void RemoveMatomeEntry() {
            var storeCount = default(int);
            var storeCountString = ConfigurationManager.AppSettings["MatomeEntryStoreCount"];
            if (int.TryParse(storeCountString, out storeCount) == true) {
                this.dbContext.Database.ExecuteSqlCommand(
                    " DELETE T FROM (" +
                    " SELECT * FROM [dbo].[MatomeEntry]" +
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
        }

    }

}
