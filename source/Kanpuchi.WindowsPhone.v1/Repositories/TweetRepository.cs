﻿using Karamem0.Kanpuchi.Configuration;
using Karamem0.Kanpuchi.Infrastructures;
using Karamem0.Kanpuchi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class TweetRepository : Repository<Tweet> {

#if DEBUG
        /// <summary>
        /// GET リクエスト URI を表します。
        /// </summary>
        private static readonly string GetUri = "https://kanpuchidev.azurewebsites.net/api/tweet?minid={0}&maxid={1}&itemcount={2}";
#else
        /// <summary>
        /// GET リクエスト URI を表します。
        /// </summary>
        private static readonly string GetUri = "https://kanpuchi.azurewebsites.net/api/tweet?minid={0}&maxid={1}&itemcount={2}";
#endif

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Repositories.TweetRepository"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TweetRepository() { }

        /// <summary>
        /// ツイートのコレクションを返します。
        /// </summary>
        /// <param name="minId">ステータス ID の最小値を示す <see cref="System.String"/>。</param>
        /// <param name="maxId">ステータス ID の最大値を示す <see cref="System.String"/>。</param>
        /// <param name="itemCount">取得する件数を示す <see cref="System.Int32"/>。</param>
        /// <returns>非同期操作を示す <see cref="T:System.Threading.Tasks.Task`1"/>。</returns>
        public Task<IEnumerable<Tweet>> GetAsync(string minId = null, string maxId = null, int itemCount = 20) {
            var requestUri = new Uri(string.Format(GetUri,
                minId == null ? null : Uri.EscapeUriString(minId),
                maxId == null ? null : Uri.EscapeUriString(maxId),
                itemCount), UriKind.Absolute);
            var message = new HttpRequestMessage();
            message.RequestUri = requestUri;
            message.Method = HttpMethod.Get;
            message.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format(
                    "{0}:{1}",
                    AppSettings.Current.DeviceId,
                    AppSettings.Current.DeviceKey))
            ));
            return new HttpClient().SendAsync(message)
                .ContinueWith(
                    task => task.Result.Content.ReadAsStreamAsync(),
                    TaskContinuationOptions.OnlyOnRanToCompletion).Unwrap()
                .ContinueWith(
                    task => {
                        var reader = new JsonTextReader(new StreamReader(task.Result));
                        var serializer = new JsonSerializer();
                        return serializer.Deserialize<IEnumerable<Tweet>>(reader);
                    },
                    TaskContinuationOptions.OnlyOnRanToCompletion);
        }

    }

}
