using Karamem0.Kanpuchi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Karamem0.Kanpuchi.Filters {

    /// <summary>
    /// アクセス ログを取得するアクション フィルターを表します。
    /// </summary>
    public class AccessLogAttribute : ActionFilterAttribute {

        /// <summary>
        /// アクションが実行される直前に呼び出されます。
        /// </summary>
        /// <param name="actionContext">
        /// アクションに関する情報を格納する <see cref="System.Web.Http.Controllers.HttpActionContext"/>。
        /// </param>
        public override void OnActionExecuting(HttpActionContext actionContext) {
            var deviceId = default(Guid?);
            var auth = actionContext.Request.Headers.Authorization;
            try {
                if (auth != null && auth.Scheme == "Basic") {
                    var stringDeviceId = Encoding.UTF8.GetString(Convert.FromBase64String(auth.Parameter)).Split(':').FirstOrDefault();
                    var parseDeviceId = Guid.Empty;
                    if (Guid.TryParse(stringDeviceId, out parseDeviceId) == true) {
                        deviceId = parseDeviceId;
                    }
                }
            } catch { }
            using (var dbContext = new DefaultConnectionContext()) {
                dbContext.AccessLogs.Add(new AccessLog() {
                    LogId = Guid.NewGuid(),
                    DeviceId = deviceId,
                    Url = actionContext.Request.RequestUri.AbsolutePath.ToString(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                });
                dbContext.SaveChanges();
            }
        }

    }

}