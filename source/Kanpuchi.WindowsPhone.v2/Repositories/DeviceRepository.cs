using Kanpuchi.Configuration;
using Kanpuchi.Infrastructure;
using Kanpuchi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.ExchangeActiveSyncProvisioning;

namespace Kanpuchi.Repositories {

    /// <summary>
    /// デバイスを格納するリポジトリを表します。
    /// </summary>
    public sealed class DeviceRepository : Repository<Device> {

#if DEBUG
        /// <summary>
        /// POST リクエスト URI を表します。
        /// </summary>
        private static readonly string PostUri = "https://kanpuchidev.azurewebsites.net/api/device";
#else
        /// <summary>
        /// POST リクエスト URI を表します。
        /// </summary>
        private static readonly string PostUri = "https://kanpuchi.azurewebsites.net/api/device";
#endif
        /// <summary>
        /// <see cref="Kanpuchi.Repositories.DeviceRepository"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public DeviceRepository() { }

        /// <summary>
        /// 指定したデバイスを登録します。
        /// </summary>
        /// <returns>非同期操作を示す <see cref="T:System.Threading.Tasks.Task`1"/>。</returns>
        public async Task<Device> RegisterAsync() {
            if (this.GetCurrent() != null) {
                return this.GetCurrent();
            }
            var eas = new EasClientDeviceInformation();
            var device = new Device() {
                DeviceId = AppSettings.Current.DeviceId,
                FirmwareVersion = eas.SystemFirmwareVersion,
                HardwareVersion = eas.SystemHardwareVersion,
                Manufacturer = eas.SystemManufacturer,
                Name = eas.SystemProductName,
            };
            var requestUri = new Uri(PostUri, UriKind.Absolute);
            var requestMessage = new HttpRequestMessage();
            requestMessage.RequestUri = requestUri;
            requestMessage.Method = HttpMethod.Post;
            requestMessage.Content = new StringContent(device.ToString(), Encoding.UTF8, "application/json");
            var responseMessage = await new HttpClient().SendAsync(requestMessage);
            using (var stream = await responseMessage.Content.ReadAsStreamAsync()) {
                var serializer = new JsonSerializer();
                using (var reader = new JsonTextReader(new StreamReader(stream))) {
                    device = serializer.Deserialize<Device>(reader);
                }
            }
            AppSettings.Current.DeviceId = device.DeviceId;
            AppSettings.Current.DeviceKey = device.DeviceKey;
            AppSettings.Current.Save();
            this.SetCurrent(device);
            return device;
        }

        /// <summary>
        /// 現在のデバイス情報を返します。
        /// </summary>
        /// <returns>デバイスのデータを格納する <see cref="Kanpuchi.Models.Device"/>。</returns>
        private Device GetCurrent() {
            if (App.Current.Resources.ContainsKey("Device") == true) {
                return (Device)App.Current.Resources["Device"];
            }
            return null;
        }

        /// <summary>
        /// 指定したデバイスを現在のデバイス情報として設定します。
        /// </summary>
        /// <param name="value">デバイスのデータを格納する <see cref="Kanpuchi.Models.Device"/>。</param>
        private void SetCurrent(Device value) {
            App.Current.Resources["Device"] = value;
        }

    }

}
