using Karamem0.Kanpuchi.Extensions;
using Karamem0.Kanpuchi.Infrastructures;
using Karamem0.Kanpuchi.Models;
using Karamem0.Kanpuchi.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.Services {

    /// <summary>
    /// まとめ記事を管理するためのサービスを表します。
    /// </summary>
    public class MatomeEntryService : Service {

        /// <summary>
        /// まとめ記事のコレクションを取得します。
        /// </summary>
        public ObservableCollection<MatomeEntry> Items { get; private set; }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Services.MatomeEntryService"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MatomeEntryService() {
            this.Items = new ObservableCollection<MatomeEntry>();
        }

        /// <summary>
        /// 最新のまとめ記事を読み込みます。
        /// </summary>
        /// <returns>非同期操作を示す <see cref="System.Threading.Tasks.Task"/>。</returns>
        public Task LoadLatestAsync() {
            this.Dispatcher.BeginInvoke(() => this.RaiseAsyncStarted());
            var repository = new MatomeEntryRepository();
            return repository.GetAsync()
                .ContinueWith(
                    task => this.Dispatcher.BeginInvoke(
                        parameter => {
                            if (parameter != null) {
                                this.Items.InsertRangeIf(0, parameter.OrderByDescending(x => x.CreatedAt), x => x.CreatedAt);
                                this.RaiseAsyncCompleted();
                            } else {
                                this.RaiseAsyncError(null);
                            }
                        },
                        task.Result),
                    TaskContinuationOptions.OnlyOnRanToCompletion)
                .ContinueWith(
                    task => this.Dispatcher.BeginInvoke(() => this.RaiseAsyncError(task.Exception)),
                    TaskContinuationOptions.NotOnRanToCompletion);
        }

        /// <summary>
        /// 最新のまとめ記事より前のまとめ記事を読み込みます。
        /// </summary>
        /// <returns>非同期操作を示す <see cref="System.Threading.Tasks.Task"/>。</returns>
        public Task LoadPreviousAsync() {
            this.Dispatcher.BeginInvoke(() => this.RaiseAsyncStarted());
            var repository = new MatomeEntryRepository();
            var max = this.Items.LastOrDefault();
            return repository.GetAsync(maxId: max == null ? (Guid?)null : max.EntryId)
                .ContinueWith(
                    task => this.Dispatcher.BeginInvoke(
                        parameter => {
                            if (parameter != null) {
                                this.Items.AddRangeIf(parameter.OrderByDescending(x => x.CreatedAt), x => x.CreatedAt);
                                this.RaiseAsyncCompleted();
                            } else {
                                this.RaiseAsyncError(null);
                            }
                        },
                        task.Result),
                    TaskContinuationOptions.OnlyOnRanToCompletion)
                .ContinueWith(
                    task => this.Dispatcher.BeginInvoke(() => this.RaiseAsyncError(task.Exception)),
                    TaskContinuationOptions.NotOnRanToCompletion);
        }

    }

}
