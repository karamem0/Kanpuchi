using Kanpuchi.Configuration;
using Kanpuchi.Infrastructure;
using Kanpuchi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.ExchangeActiveSyncProvisioning;

namespace Kanpuchi.Repositories {

    /// <summary>
    /// デバイス トークンを格納するリポジトリを表します。
    /// </summary>
    public class DeviceRepository : Repository<Device> {

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
        /// 現在のデバイス情報を取得します。
        /// </summary>
        public static Device Current { get; private set; }

        /// <summary>
        /// <see cref="Kanpuchi.Repositories.DeviceRepository"/>
        /// クラスの新しいインスタンスを初期化します。
        /// </summary>
        public DeviceRepository() { }

        /// <summary>
        /// デバイスを作成して返します。
        /// </summary>
        /// <returns>デバイスを示す <see cref="Kanpuchi.Models.Device"/>。</returns>
        public Device Create() {
            var device = new EasClientDeviceInformation();
            return new Device() {
                DeviceId = AppSettings.Current.DeviceId,
                FirmwareVersion = device.SystemFirmwareVersion,
                HardwareVersion = device.SystemHardwareVersion,
                Manufacturer = device.SystemManufacturer,
                Name = device.SystemProductName,
            };
        }

        /// <summary>
        /// デバイスを返します。
        /// </summary>
        /// <returns>デバイスを示す <see cref="Kanpuchi.Models.Device"/>。</returns>
        public Device Load() {
            return DeviceRepository.Current;
        }

        /// <summary>
        /// 指定したデバイスのデバイス ID およびデバイス キーを保存します。
        /// </summary>
        /// <param name="device">デバイスを示す <see cref="Kanpuchi.Models.Device"/>。</param>
        public void Save(Device device) {
            if (device != null) {
                AppSettings.Current.DeviceId = device.DeviceId;
                AppSettings.Current.DeviceKey = device.DeviceKey;
                AppSettings.Current.Save();
                DeviceRepository.Current = device;
            }
        }

        /// <summary>
        /// 指定したデバイスを追加または更新します。
        /// </summary>
        /// <param name="device">デバイスを示す <see cref="Kanpuchi.Models.Device"/>。</param>
        /// <returns>非同期操作を示す <see cref="T:System.Threading.Tasks.Task`1"/>。</returns>
        public async Task<Device> PostAsync(Device device) {
            var requestUri = new Uri(PostUri, UriKind.Absolute);
            var requestMessage = new HttpRequestMessage();
            requestMessage.RequestUri = requestUri;
            requestMessage.Method = HttpMethod.Post;
            requestMessage.Content = new StringContent(device.ToString(), Encoding.UTF8, "application/json");
            var responseMessage = await new HttpClient().SendAsync(requestMessage);
            using (var stream = await responseMessage.Content.ReadAsStreamAsync()) {
                var serializer = new JsonSerializer();
                using (var reader = new JsonTextReader(new StreamReader(stream))) {
                    return serializer.Deserialize<Device>(reader);
                }
            }
        }

    }

}
