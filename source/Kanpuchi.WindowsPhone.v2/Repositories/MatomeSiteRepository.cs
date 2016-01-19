using Karamem0.Kanpuchi.Infrastructure;
using Karamem0.Kanpuchi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.Repositories {

    /// <summary>
    /// まとめサイトのコレクションを格納するリポジトリを表します。
    /// </summary>
    public sealed class MatomeSiteRepository : Repository<MatomeSite> {

#if DEBUG
        /// <summary>
        /// GET リクエスト URI を表します。
        /// </summary>
        private static readonly string GetUri = "https://kanpuchidev.azurewebsites.net/api/matomesite";
#else
        /// <summary>
        /// GET リクエスト URI を表します。
        /// </summary>
        private static readonly string GetUri = "https://kanpuchi.azurewebsites.net/api/matomesite";
#endif

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Repositories.MatomeSiteRepository"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MatomeSiteRepository() { }

        /// <summary>
        /// まとめサイトのコレクションを返します。
        /// </summary>
        /// <param name="deviceId">デバイス ID を示す <see cref="System.String"/>。</param>
        /// <param name="deviceKey">デバイス キーを示す <see cref="System.String"/>。</param>
        /// <returns>非同期操作を示す <see cref="T:System.Threading.Tasks.Task`1"/>。</returns>
        public async Task<IEnumerable<MatomeSite>> SearchAsync(string deviceId, string deviceKey) {
            var requestUri = new Uri(GetUri, UriKind.Absolute);
            var requestAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes(deviceId + ":" + deviceKey));
            var requestMessage = new HttpRequestMessage();
            requestMessage.RequestUri = requestUri;
            requestMessage.Method = HttpMethod.Get;
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", requestAuth);
            var responseMessage = await new HttpClient().SendAsync(requestMessage);
            using (var stream = await responseMessage.Content.ReadAsStreamAsync()) {
                var serializer = new JsonSerializer();
                using (var reader = new JsonTextReader(new StreamReader(stream))) {
                    return serializer.Deserialize<IEnumerable<MatomeSite>>(reader);
                }
            }
        }

    }

}
