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
    /// ツイートのコレクションを格納するリポジトリを表します。
    /// </summary>
    public sealed class TweetRepository : Repository<Tweet> {

#if DEBUG
        /// <summary>
        /// GET リクエスト URI を表します。
        /// </summary>
        private static readonly string GetUri = "https://kanpuchidev.azurewebsites.net/api/tweet";
#else
        /// <summary>
        /// GET リクエスト URI を表します。
        /// </summary>
        private static readonly string GetUri = "https://kanpuchi.azurewebsites.net/api/tweet";
#endif

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Repositories.TweetRepository"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TweetRepository() { }

        /// <summary>
        /// ツイートのコレクションを返します。
        /// </summary>
        /// <param name="deviceId">デバイス ID を示す <see cref="System.String"/>。</param>
        /// <param name="deviceKey">デバイス キーを示す <see cref="System.String"/>。</param>
        /// <param name="minId">ステータス ID の最小値を示す <see cref="System.String"/>。</param>
        /// <param name="maxId">ステータス ID の最大値を示す <see cref="System.String"/>。</param>
        /// <param name="itemCount">取得する件数を示す <see cref="System.Int32"/>。</param>
        /// <returns>非同期操作を示す <see cref="System.Threading.Tasks.Task{TResult}"/>。</returns>
        public async Task<IEnumerable<Tweet>> SearchAsync(
            string deviceId, string deviceKey, string minId = null, string maxId = null, int? itemCount = null) {
            var requestUri = new Uri(
                GetUri + "?" + string.Join("&", new[] {
                    minId == null ? null : "minid=" + Uri.EscapeUriString(minId.ToString()),
                    maxId == null ? null : "maxid=" + Uri.EscapeUriString(maxId.ToString()),
                    itemCount == null ? null : "itemcount=" + Uri.EscapeUriString(itemCount.ToString()),
                }.Where(x => x != null)),
                UriKind.Absolute);
            var requestAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes(deviceId + ":" + deviceKey));
            var requestMessage = new HttpRequestMessage();
            requestMessage.RequestUri = requestUri;
            requestMessage.Method = HttpMethod.Get;
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", requestAuth);
            var responseMessage = await new HttpClient().SendAsync(requestMessage);
            using (var stream = await responseMessage.Content.ReadAsStreamAsync()) {
                var serializer = new JsonSerializer();
                using (var reader = new JsonTextReader(new StreamReader(stream))) {
                    return serializer.Deserialize<IEnumerable<Tweet>>(reader);
                }
            }
        }

    }

}
