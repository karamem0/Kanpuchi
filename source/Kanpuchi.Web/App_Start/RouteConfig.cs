using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Kanpuchi {

    /// <summary>
    /// アプリケーションのルーティングを構成します。
    /// </summary>
    public static class RouteConfig {
        
        /// <summary>
        /// ルーティングの構成を登録します。
        /// </summary>
        /// <param name="routes">
        /// ルーティングのコレクションを示す <see cref="System.Web.Routing.RouteCollection"/>。
        /// </param>
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{Resource}.axd/{*PathInfo}");
        }

    }

}
