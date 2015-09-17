using Kanpuchi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

namespace Kanpuchi {

    /// <summary>
    /// アプリケーションのデータベースの設定を構成します。
    /// </summary>
    public static class DatabaseConfig {

        /// <summary>
        /// アプリケーションのデータベースの設定を登録します。
        /// </summary>
        public static void Register() {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DefaultConnection, DefaultConnectionConfiguration>());
        }

    }

}