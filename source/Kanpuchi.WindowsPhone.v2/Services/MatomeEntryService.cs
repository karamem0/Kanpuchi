using Kanpuchi.Extensions;
using Kanpuchi.Infrastructure;
using Kanpuchi.Models;
using Kanpuchi.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kanpuchi.Services {

    /// <summary>
    /// まとめ記事を管理するためのサービスを表します。
    /// </summary>
    public class MatomeEntryService : Service {

        /// <summary>
        /// まとめ記事のコレクションを取得します。
        /// </summary>
        public ObservableCollection<MatomeEntry> Items { get; private set; }

        /// <summary>
        /// <see cref="Kanpuchi.Services.MatomeEntryService"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MatomeEntryService() {
            this.Items = new ObservableCollection<MatomeEntry>();
        }

        /// <summary>
        /// 最新のまとめ記事を読み込みます。
        /// </summary>
        public async void LoadLatestAsync() {
            var context = SynchronizationContext.Current;
            try {
                context.Post(() => this.RaiseAsyncStarted());
                var deviceRepository = new DeviceRepository();
                var device = deviceRepository.Load();
                if (device == null) {
                    device = await deviceRepository.PostAsync(deviceRepository.Create());
                    if (device != null) {
                        deviceRepository.Save(device);
                    }
                }
                var matomeEntryRepository = new MatomeEntryRepository();
                var matomeEntries = await matomeEntryRepository.GetAsync();
                if (matomeEntries != null) {
                    this.Items.InsertRangeIf(0, matomeEntries.OrderByDescending(x => x.CreatedAt), x => x.CreatedAt);
                    this.RaiseAsyncCompleted();
                }
            } catch (Exception ex) {
                context.Post(() => this.RaiseAsyncCompleted(ex));
            }
        }

        /// <summary>
        /// 最新のまとめ記事より前のまとめ記事を読み込みます。
        /// </summary>
        /// <returns>非同期操作を示す <see cref="System.Threading.Tasks.Task"/>。</returns>
        public async void LoadPreviousAsync() {
            var context = SynchronizationContext.Current;
            try {
                context.Post(() => this.RaiseAsyncStarted());
                var deviceRepository = new DeviceRepository();
                var device = deviceRepository.Load();
                if (device == null) {
                    device = await deviceRepository.PostAsync(deviceRepository.Create());
                    if (device != null) {
                        deviceRepository.Save(device);
                    }
                }
                var maxEntry = this.Items.LastOrDefault();
                var maxId = (maxEntry == null) ? (Guid?)null : maxEntry.EntryId;
                var matomeEntryRepository = new MatomeEntryRepository();
                var matomeEntries = await matomeEntryRepository.GetAsync(maxId: maxId);
                if (matomeEntries != null) {
                    this.Items.AddRangeIf(matomeEntries.OrderByDescending(x => x.CreatedAt), x => x.CreatedAt);
                    this.RaiseAsyncCompleted();
                }
            } catch (Exception ex) {
                context.Post(() => this.RaiseAsyncCompleted(ex));
            }
        }
    }

}
