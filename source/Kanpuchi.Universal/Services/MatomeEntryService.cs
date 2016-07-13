using AutoMapper;
using Karamem0.Kanpuchi.Extensions;
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
    /// まとめ記事を管理するためのサービスを表します。
    /// </summary>
    public class MatomeEntryService : Service {

        /// <summary>
        /// オブジェクトが解放されたかどうかを示す値を表します。
        /// </summary>
        private bool disposed;

        /// <summary>
        /// ビュー モデルを表します。
        /// </summary>
        private HomePageViewModel viewModel;

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
        /// <param name="viewModel"><see cref="Karamem0.Kanpuchi.ViewModels.HomeViewModel"/>。</param>
        public MatomeEntryService(HomePageViewModel viewModel)
            : this() {
            this.viewModel = viewModel;
        }

        /// <summary>
        /// 最新のまとめ記事を読み込みます。
        /// </summary>
        public async Task LoadLatestAsync() {
            if (this.disposed == true) {
                throw new ObjectDisposedException(nameof(MatomeEntryService));
            }
            try {
                this.RaiseAsyncStarted(nameof(this.LoadLatestAsync));
                var device = await this.deviceRepository.RegisterAsync();
                var settings = await this.settingsRepository.LoadAsync();
                for (int index = this.viewModel.MatomeEntries.Count - 1; index >= 0; index--) {
                    var matomeEntry = this.viewModel.MatomeEntries[index];
                    if (settings.EnableSiteIds.Contains(matomeEntry.SiteId) != true) {
                        this.viewModel.MatomeEntries.Remove(matomeEntry);
                    }
                }
                var matomeEntries = await this.matomeEntryRepository.SearchAsync(
                    deviceId: device.DeviceId.ToString(),
                    deviceKey: device.DeviceKey,
                    siteIds: settings.EnableSiteIds);
                if (matomeEntries != null) {
                    var config = new MapperConfiguration(x => x.CreateMap<MatomeEntry, MatomeEntryViewModel>());
                    var mapper = config.CreateMapper();
                    this.viewModel.MatomeEntries.AddRange(
                        matomeEntries.Select(x => mapper.Map<MatomeEntryViewModel>(x)),
                        x => x.CreatedAt,
                        x => x.EntryId);
                }
                this.RaiseAsyncCompleted(nameof(this.LoadLatestAsync));
            } catch (Exception ex) {
                this.RaiseAsyncCompleted(nameof(this.LoadLatestAsync), ex);
            }
        }

        /// <summary>
        /// 最新のまとめ記事より前のまとめ記事を読み込みます。
        /// </summary>
        public async Task LoadPreviousAsync() {
            if (this.disposed == true) {
                throw new ObjectDisposedException(nameof(MatomeEntryService));
            }
            try {
                this.RaiseAsyncStarted(nameof(this.LoadPreviousAsync));
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
                    var config = new MapperConfiguration(x => x.CreateMap<MatomeEntry, MatomeEntryViewModel>());
                    var mapper = config.CreateMapper();
                    this.viewModel.MatomeEntries.AddRange(
                        matomeEntries.Select(x => mapper.Map<MatomeEntryViewModel>(x)),
                        x => x.CreatedAt,
                        x => x.EntryId);
                }
                this.RaiseAsyncCompleted(nameof(this.LoadPreviousAsync));
            } catch (Exception ex) {
                this.RaiseAsyncCompleted(nameof(this.LoadPreviousAsync), ex);
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
