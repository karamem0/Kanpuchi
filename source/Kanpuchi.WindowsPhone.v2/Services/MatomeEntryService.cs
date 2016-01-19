using Karamem0.Kanpuchi.Extensions;
using Karamem0.Kanpuchi.Infrastructure;
using Karamem0.Kanpuchi.Repositories;
using Karamem0.Kanpuchi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.Services {

    /// <summary>
    /// まとめ記事を管理するためのサービスを表します。
    /// </summary>
    public class MatomeEntryService : Service {

        /// <summary>
        /// ビュー モデルを表します。
        /// </summary>
        private MainViewModel viewModel;

        /// <summary>
        /// デバイスを格納するリポジトリを表します。
        /// </summary>
        private DeviceRepository deviceRepository;

        /// <summary>
        /// 設定情報を格納するリポジトリを表します。
        /// </summary>
        private SettingsRepository settingsRepository;

        /// <summary>
        /// まとめ記事のコレクションを格納するリポジトリを表します。
        /// </summary>
        private MatomeEntryRepository matomeEntryRepository;

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Services.MatomeEntryService"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        private MatomeEntryService() {
            this.deviceRepository = new DeviceRepository();
            this.settingsRepository = new SettingsRepository();
            this.matomeEntryRepository = new MatomeEntryRepository();
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Services.MatomeEntryService"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="viewModel"><see cref="Karamem0.Kanpuchi.ViewModels.MainViewModel"/>。</param>
        public MatomeEntryService(MainViewModel viewModel)
            : this() {
            this.viewModel = viewModel;
        }

        /// <summary>
        /// 最新のまとめ記事を読み込みます。
        /// </summary>
        public async void LoadLatestAsync() {
            try {
                this.RaiseAsyncStarted();
                var device = await this.deviceRepository.RegisterAsync();
                var settings = await this.settingsRepository.LoadAsync();
                var matomeEntries = await this.matomeEntryRepository.SearchAsync(
                    deviceId: device.DeviceId.ToString(),
                    deviceKey: device.DeviceKey,
                    siteIds: settings.EnableSiteIds);
                if (matomeEntries != null) {
                    this.viewModel.MatomeEntries.InsertRangeIf(0, matomeEntries.OrderByDescending(x => x.CreatedAt), x => x.CreatedAt);
                }
                this.RaiseAsyncCompleted();
            } catch (Exception ex) {
                this.RaiseAsyncCompleted(ex);
            }
        }

        /// <summary>
        /// 最新のまとめ記事より前のまとめ記事を読み込みます。
        /// </summary>
        public async void LoadPreviousAsync() {
            var context = SynchronizationContext.Current;
            try {
                this.RaiseAsyncStarted();
                var device = await this.deviceRepository.RegisterAsync();
                var settings = await this.settingsRepository.LoadAsync();
                var maxEntry = this.viewModel.MatomeEntries.LastOrDefault();
                var maxId = (maxEntry == null) ? (Guid?)null : maxEntry.EntryId;
                var matomeEntries = await this.matomeEntryRepository.SearchAsync(
                    deviceId: device.DeviceId.ToString(),
                    deviceKey: device.DeviceKey,
                    maxId: maxId,
                    siteIds: settings.EnableSiteIds);
                if (matomeEntries != null) {
                    this.viewModel.MatomeEntries.AddRangeIf(matomeEntries.OrderByDescending(x => x.CreatedAt), x => x.CreatedAt);
                }
                this.RaiseAsyncCompleted();
            } catch (Exception ex) {
                this.RaiseAsyncCompleted(ex);
            }
        }
    }

}
