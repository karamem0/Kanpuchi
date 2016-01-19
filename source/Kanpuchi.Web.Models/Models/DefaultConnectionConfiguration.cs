using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

namespace Karamem0.Kanpuchi.Models {

    /// <summary>
    /// データベースを移行するための構成を表します。
    /// </summary>
    public class DefaultConnectionConfiguration : DbMigrationsConfiguration<DefaultConnectionContext> {

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Models.DefaultConnectionConfiguration"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public DefaultConnectionConfiguration() {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
        }

        /// <summary>
        /// データベースをシードします。
        /// </summary>
        /// <param name="context"><see cref="Karamem0.Kanpuchi.Models.DefaultConnectionContext"/>。</param>
        protected override void Seed(DefaultConnectionContext context) { }

    }

}
