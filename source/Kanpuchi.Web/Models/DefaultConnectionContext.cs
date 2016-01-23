using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Karamem0.Kanpuchi.Models {

    /// <summary>
    /// データベースに格納されたエンティティに対するリポジトリを表します。
    /// </summary>
    public class DefaultConnectionContext : DbContext {

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Models.AccessLog"/> クラスのコレクションを取得または設定します。
        /// </summary>
        public DbSet<AccessLog> AccessLogs { get; set; }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Models.Device"/> クラスのコレクションを取得または設定します。
        /// </summary>
        public DbSet<Device> Devices { get; set; }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Models.MatomeSite"/> クラスのコレクションを取得または設定します。
        /// </summary>
        public DbSet<MatomeSite> MatomeSites { get; set; }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Models.MatomeEntry"/> クラスのコレクションを取得または設定します。
        /// </summary>
        public DbSet<MatomeEntry> MatomeEntries { get; set; }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Models.TwitterStatus"/> クラスのコレクションを取得または設定します。
        /// </summary>
        public DbSet<TwitterStatus> TwitterStatuses { get; set; }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Models.TwitterUser"/> クラスのコレクションを取得または設定します。
        /// </summary>
        public DbSet<TwitterUser> TwitterUsers { get; set; }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Models.DefaultConnectionContext"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public DefaultConnectionContext() {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

    }

}
