using Karamem0.Kanpuchi.Infrastructure;
using Karamem0.Kanpuchi.Models;
using Karamem0.Kanpuchi.Repositories;
using Karamem0.Kanpuchi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.Services {

    /// <summary>
    /// まとめサイトを管理するためのサービスを表します。
    /// </summary>
    public class SettingsService : Service {

        /// <summary>
        /// オブジェクトが解放されたかどうかを示す値を表します。
        /// </summary>
        private bool disposed;

        /// <summary>
        /// ビュー モデルを表します。
        /// </summary>
        private SettingsViewModel viewModel;

        /// <summary>
        /// 設定情報を格納するリポジトリを表します。
        /// </summary>
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
        /// <see cref="Karamem0.Kanpuchi.Services.SettingsService"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        private SettingsService() {
            this.settingsRepository = new SettingsRepository();
            this.deviceRepository = new DeviceRepository();
            this.matomeSiteRepository = new MatomeSiteRepository();
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Services.SettingsService"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="viewModel"><see cref="Karamem0.Kanpuchi.ViewModels.SettingsViewModel"/>。</param>
        public SettingsService(SettingsViewModel viewModel)
            : this() {
            this.viewModel = viewModel;
        }

        /// <summary>
        /// 設定情報を読み込みます。
        /// </summary>
        public async void LoadAsync() {
            if (this.disposed == true) {
                throw new ObjectDisposedException(nameof(SettingsService));
            }
            try {
                this.RaiseAsyncStarted();
                var settings = await this.settingsRepository.LoadAsync();
                var device = await this.deviceRepository.RegisterAsync();
                var matomeSites = await this.matomeSiteRepository.SearchAsync(
                    deviceId: device.DeviceId.ToString(),
                    deviceKey: device.DeviceKey);
                if (matomeSites != null) {
                    this.viewModel.MatomeSites.Clear();
                    foreach (var matomeSite in matomeSites.OrderBy(x => x.SiteId)) {
                        if (settings.EnableSiteIds == null ||
                            settings.EnableSiteIds.Length == 0 ||
                            settings.EnableSiteIds.Contains(matomeSite.SiteId) == true) {
                            matomeSite.Enabled = true;
                        } else {
                            matomeSite.Enabled = false;
                        }
                        this.viewModel.MatomeSites.Add(matomeSite);
                    }
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
            if (this.disposed == true) {
                throw new ObjectDisposedException(nameof(SettingsService));
            }
            try {
                this.RaiseAsyncStarted();
                var settings = new Settings();
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

        /// <summary>
        /// 現在のインスタンスで使用されているリソースを解放します。
        /// </summary>
        /// <param name="disposing">
        /// アンマネージ リソースとマネージ リソースの両方を解放する場合は true。アンマネージ
        /// リソースのみ解放する場合は false。
        /// </param>
        protected override void Dispose(bool disposing) {
            if (this.disposed != true) {
                if (disposing == true) {
                    this.viewModel = null;
                }
            }
            this.disposed = true;
        }

    }

}
