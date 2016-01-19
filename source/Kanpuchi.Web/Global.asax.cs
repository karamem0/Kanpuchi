﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Karamem0.Kanpuchi {

    /// <summary>
    /// Web API アプリケーションを表します。
    /// </summary>
    public class WebApiApplication : HttpApplication {

        /// <summary>
        /// アプリケーションが開始されるときに呼び出されます。
        /// </summary>
        protected void Application_Start() {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
        }

    }

}
