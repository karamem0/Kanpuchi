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
        protected override void Seed(DefaultConnection context) { }

    }

}
