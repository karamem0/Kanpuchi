using Kanpuchi.Extensions;
using Kanpuchi.Infrastructure;
using Kanpuchi.Models;
using Kanpuchi.Repositories;
using Kanpuchi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kanpuchi.Services {

    /// <summary>
    /// まとめサイトを管理するためのサービスを表します。
    /// </summary>
    public class SettingsService : Service {

        /// <summary>
        /// ビュー モデルを表します。
        /// </summary>
        private SettingsViewModel viewModel;

        private SettingsRepository settingsRepository;

        /// <summary>
        /// デバイスを格納するリポジトリを表します。
        /// </summary>
        private DeviceRepository deviceRepository;

        /// <summary>
        /// まとめサイトのコレクションを格納するリポジトリを表します。
        /// </summary>
        private MatomeSiteRepository matomeSiteRepository;

        /// <summary>
        /// <see cref="Kanpuchi.Services.SettingsService"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        private SettingsService() {
            this.settingsRepository = new SettingsRepository();
            this.deviceRepository = new DeviceRepository();
            this.matomeSiteRepository = new MatomeSiteRepository();
        }

        /// <summary>
        /// <see cref="Kanpuchi.Services.SettingsService"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="viewModel"><see cref="Kanpuchi.ViewModels.SettingsViewModel"/>。</param>
        public SettingsService(SettingsViewModel viewModel)
            : this() {
            this.viewModel = viewModel;
        }

        /// <summary>
        /// 設定情報を読み込みます。
        /// </summary>
        public async void LoadAsync() {
            try {
                this.RaiseAsyncStarted();
                var settings = await this.settingsRepository.LoadAsync();
                this.viewModel.UseInAppBrowser = settings.UseInAppBrowser;
                var device = await this.deviceRepository.RegisterAsync();
                var matomeSites = await this.matomeSiteRepository.SearchAsync(
                    deviceId: device.DeviceId.ToString(),
                    deviceKey: device.DeviceKey);
                if (matomeSites != null) {
                    this.viewModel.MatomeSites.Clear();
                    this.viewModel.MatomeSites.AddRange(matomeSites
                        .OrderBy(x => x.SiteId)
                        .Select(x => {
                            if (settings.EnableSiteIds == null ||
                                settings.EnableSiteIds.Length == 0 ||
                                settings.EnableSiteIds.Contains(x.SiteId) == true) {
                                x.Enabled = true;
                            } else {
                                x.Enabled = false;
                            }
                            return x;
                        }));
                }
                this.RaiseAsyncCompleted();
            } catch (Exception ex) {
                this.RaiseAsyncCompleted(ex);
            }
        }

        /// <summary>
        /// 設定情報を保存します。
        /// </summary>
        public async void SaveAsync() {
            try {
                this.RaiseAsyncStarted();
                var settings = new Settings();
                settings.UseInAppBrowser = this.viewModel.UseInAppBrowser;
                settings.EnableSiteIds = this.viewModel.MatomeSites
                    .Where(x => x.Enabled)
                    .Select(x => x.SiteId)
                    .ToArray();
                await this.settingsRepository.SaveAsync(settings);
                this.RaiseAsyncCompleted();
            } catch (Exception ex) {
                this.RaiseAsyncCompleted(ex);
            }
        }

    }

}
