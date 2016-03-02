using Karamem0.Kanpuchi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Karamem0.Kanpuchi.Filters {

    /// <summary>
    /// Basic 認証を処理するアクション フィルターを表します。
    /// </summary>
    public class BasicAutheticationAttribute : ActionFilterAttribute {

        /// <summary>
        /// アクションが実行される直前に呼び出されます。
        /// </summary>
        /// <param name="actionContext">
        /// アクションに関する情報を格納する <see cref="System.Web.Http.Controllers.HttpActionContext"/>。
        /// </param>
        public override void OnActionExecuting(HttpActionContext actionContext) {
            try {
                var value = actionContext.Request.Headers.Authorization;
                if (value == null) {
                    throw new UnauthorizedAccessException();
                }
                if (string.Equals(value.Scheme, "Basic", StringComparison.OrdinalIgnoreCase) != true) {
                    throw new UnauthorizedAccessException();
                }
                var parameter = value.Parameter;
                if (string.IsNullOrEmpty(parameter) == true) {
                    throw new UnauthorizedAccessException();
                }
                var bytes = Convert.FromBase64String(parameter);
                var encoding = Encoding.GetEncoding("iso-8859-1");
                var credentials = encoding.GetString(bytes);
                var separator = credentials.IndexOf(':');
                var deviceId = Guid.Parse(credentials.Substring(0, separator));
                var deviceKey = credentials.Substring(separator + 1);
                using (var dbContext = new DefaultConnectionContext()) {
                    var device = dbContext.Devices
                        .Where(x => x.DeviceId == deviceId)
                        .Where(x => x.DeviceKey == deviceKey)
                        .SingleOrDefault();
                    if (device == null) {
                        throw new UnauthorizedAccessException();
                    }
                    dbContext.AccessLogs.Add(new AccessLog() {
                        LogId = Guid.NewGuid(),
                        DeviceId = deviceId,
                        Url = actionContext.Request.RequestUri.AbsolutePath.ToString(),
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    });
                    dbContext.SaveChanges();
                }
            } catch (UnauthorizedAccessException) {
                var response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.Unauthorized;
                response.Headers.Add("WWW-Authenticate", "Basic Realm=Kanpuchi");
                actionContext.Response = response;
            } catch (Exception ex) {
                var response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Content = new StringContent(ex.Message);
                actionContext.Response = response;
            }
        }

    }

}