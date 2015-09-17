using Kanpuchi.Configuration;
using Kanpuchi.Infrastructures;
using Kanpuchi.Models;
using Kanpuchi.Reflection;
using Microsoft.Phone.Info;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
        /// <see cref="Kanpuchi.Repositories.DeviceRepository"/>
        /// クラスの新しいインスタンスを初期化します。
        /// </summary>
        public DeviceRepository() { }

        /// <summary>
        /// デバイスを作成して返します。
        /// </summary>
        /// <returns>デバイスを示す <see cref="Kanpuchi.Models.Device"/>。</returns>
        public Device Create() {
            return new Device() {
                DeviceId = AppSettings.Current.DeviceId,
                FirmwareVersion = DeviceStatus.DeviceFirmwareVersion,
                HardwareVersion = DeviceStatus.DeviceHardwareVersion,
                Manufacturer = DeviceStatus.DeviceManufacturer,
                Name = DeviceStatus.DeviceName,
                AppVersion = AssemblyVersion.Current.Version,
            };
        }

        /// <summary>
        /// 指定したデバイスのデバイス ID およびデバイス キーをストレージに保存します。
        /// </summary>
        /// <param name="device">デバイスを示す <see cref="Kanpuchi.Models.Device"/>。</param>
        public void Save(Device device) {
            if (device != null) {
                AppSettings.Current.DeviceId = device.DeviceId;
                AppSettings.Current.DeviceKey = device.DeviceKey;
                AppSettings.Current.Save();
            }
        }

        /// <summary>
        /// 指定したデバイスを追加または更新します。
        /// </summary>
        /// <param name="device">デバイスを示す <see cref="Kanpuchi.Models.Device"/>。</param>
        /// <returns>非同期操作を示す <see cref="T:System.Threading.Tasks.Task`1"/>。</returns>
        public Task<Device> PostAsync(Device device) {
            var requestUri = new Uri(PostUri, UriKind.Absolute);
            var message = new HttpRequestMessage();
            message.RequestUri = requestUri;
            message.Method = HttpMethod.Post;
            message.Content = new StringContent(device.ToString(), Encoding.UTF8, "application/json");
            return new HttpClient().SendAsync(message)
                .ContinueWith(
                    task => task.Result.Content.ReadAsStreamAsync(),
                    TaskContinuationOptions.OnlyOnRanToCompletion).Unwrap()
                .ContinueWith(
                    task => {
                        var reader = new JsonTextReader(new StreamReader(task.Result));
                        var serializer = new JsonSerializer();
                        return serializer.Deserialize<Device>(reader);
                    },
                    TaskContinuationOptions.OnlyOnRanToCompletion);
        }

    }

}
