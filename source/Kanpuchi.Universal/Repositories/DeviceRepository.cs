using Karamem0.Kanpuchi.Configuration;
using Karamem0.Kanpuchi.Extensions;
using Karamem0.Kanpuchi.Infrastructure;
using Karamem0.Kanpuchi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Security.ExchangeActiveSyncProvisioning;

namespace Karamem0.Kanpuchi.Repositories {

    /// <summary>
    /// デバイスを格納するリポジトリを表します。
    /// </summary>
    public sealed class DeviceRepository : Repository<Device> {

        /// <summary>
        /// POST リクエスト URI を表します。
        /// </summary>
        private static readonly string PostUri = "https://kanpuchi.azurewebsites.net/api/device";

        /// <summary>
        /// アプリケーションの <see cref=".Kanpuchi.Repositories.Device"/> クラスのインスタンスを表します。
        /// </summary>
        private static Device Instance;

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Repositories.DeviceRepository"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public DeviceRepository() { }

        /// <summary>
        /// 指定したデバイスを登録します。
        /// </summary>
        /// <returns>非同期操作を示す <see cref="System.Threading.Tasks.Task{TResult}"/>。</returns>
        public async Task<Device> RegisterAsync() {
            if (DeviceRepository.Instance != null) {
                return DeviceRepository.Instance;
            }
            var eas = new EasClientDeviceInformation();
            var device = new Device() {
                DeviceId = AppSettings.Current.DeviceId,
                FirmwareVersion = eas.SystemFirmwareVersion,
                HardwareVersion = eas.SystemHardwareVersion,
                Manufacturer = eas.SystemManufacturer,
                AppVersion = Package.Current.Id.Version.ToFormattedString(),
                Name = eas.SystemProductName,
            };
            var requestUri = new Uri(PostUri, UriKind.Absolute);
            var requestMessage = new HttpRequestMessage();
            requestMessage.RequestUri = requestUri;
            requestMessage.Method = HttpMethod.Post;
            requestMessage.Content = new StringContent(device.ToString(), Encoding.UTF8, "application/json");
            using (var client = new HttpClient()) {
                var responseMessage = await client.SendAsync(requestMessage);
                using (var stream = await responseMessage.Content.ReadAsStreamAsync()) {
                    var serializer = new JsonSerializer();
                    using (var reader = new JsonTextReader(new StreamReader(stream))) {
                        device = serializer.Deserialize<Device>(reader);
                    }
                }
                AppSettings.Current.DeviceId = device.DeviceId;
                AppSettings.Current.DeviceKey = device.DeviceKey;
                AppSettings.Current.Save();
                DeviceRepository.Instance = device;
                return device;
            }
        }

    }

}
