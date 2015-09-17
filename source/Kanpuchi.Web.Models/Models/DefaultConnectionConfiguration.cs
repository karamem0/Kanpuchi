using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

namespace Kanpuchi.Models {

    /// <summary>
    /// データベースを移行するための構成を表します。
    /// </summary>
    public class DefaultConnectionConfiguration : DbMigrationsConfiguration<DefaultConnection> {

        /// <summary>
        /// <see cref="Kanpuchi.Models.DefaultConnectionConfiguration"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public DefaultConnectionConfiguration() {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
        }

        /// <summary>
        /// データベースをシードします。
        /// </summary>
        /// <param name="context"><see cref="Kanpuchi.Models.DefaultConnection"/>。</param>
        protected override void Seed(DefaultConnection context) {
            context.MatomeSites.AddOrUpdate(x => x.SiteUrl,
                new[] {
                    new MatomeSite() {
                        SiteId = 1,
                        SiteName = "艦これ速報 艦隊これくしょんまとめ",
                        SiteUrl = "http://kancolle.doorblog.jp/",
                        FeedUrl = "http://kancolle.doorblog.jp/atom.xml",
                        FeedFormat = "Atom0.3",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    },
                    new MatomeSite() {
                        SiteId = 2,
                        SiteName = "あ艦これ ～艦隊これくしょんまとめブログ～",
                        SiteUrl = "http://akankore.doorblog.jp/",
                        FeedUrl = "http://akankore.doorblog.jp/atom.xml",
                        FeedFormat = "Atom0.3",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    },
                    new MatomeSite() {
                        SiteId = 3,
                        SiteName = "艦これまとめ主義-攻略ネタサイト",
                        SiteUrl = "http://kancolle-news.com/",
                        FeedUrl = "http://kancolle-news.com/atom.xml",
                        FeedFormat = "Atom0.3",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    },
                    new MatomeSite() {
                        SiteId = 4,
                        SiteName = "艦これまとめ魂-艦隊これくしょん攻略-",
                        SiteUrl = "http://kantama.net/",
                        FeedUrl = "http://kantama.net/atom.xml",
                        FeedFormat = "Atom0.3",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    },
                    new MatomeSite() {
                        SiteId = 5,
                        SiteName = "艦これ攻略速報！～艦隊これくしょん情報まとめ～",
                        SiteUrl = "http://kannkorekouryaku.doorblog.jp/",
                        FeedUrl = "http://kannkorekouryaku.doorblog.jp/atom.xml",
                        FeedFormat = "Atom0.3",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    },
                    new MatomeSite() {
                        SiteId = 6,
                        SiteName = "電これ ～艦これまとめブログ～",
                        SiteUrl = "http://kantai-collection.com/",
                        FeedUrl = "http://kantai-collection.com/atom.xml",
                        FeedFormat = "Atom0.3",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    },
                }
            );
        }

    }

}
