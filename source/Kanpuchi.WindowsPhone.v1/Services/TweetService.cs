using Kanpuchi.Extensions;
using Kanpuchi.Infrastructures;
using Kanpuchi.Models;
using Kanpuchi.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanpuchi.Services {

    /// <summary>
    /// ツイートを管理するためのサービスを表します。
    /// </summary>
    public class TweetService : Service {

        /// <summary>
        /// ツイートのコレクションを取得します。
        /// </summary>
        public ObservableCollection<Tweet> Items { get; private set; }

        /// <summary>
        /// <see cref="Kanpuchi.Services.TweetService"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TweetService() {
            this.Items = new ObservableCollection<Tweet>();
        }

        /// <summary>
        /// 最新のツイートを読み込みます。
        /// </summary>
        /// <returns>非同期操作を示す <see cref="System.Threading.Tasks.Task"/>。</returns>
        public Task LoadLatestAsync() {
            this.Dispatcher.BeginInvoke(() => this.RaiseAsyncStarted());
            var repository = new TweetRepository();
            return repository.GetAsync()
                .ContinueWith(
                    task => this.Dispatcher.BeginInvoke(
                        parameter => {
                            if (parameter != null) {
                                this.Items.InsertRangeIf(0, parameter.OrderByDescending(x => x.StatusId), x => x.StatusId);
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
        /// 現在読み込まれているツイートより前のツイートを読み込みます。
        /// </summary>
        /// <returns>非同期操作を示す <see cref="System.Threading.Tasks.Task"/>。</returns>
        public Task LoadPreviousAsync() {
            this.Dispatcher.BeginInvoke(() => this.RaiseAsyncStarted());
            var repository = new TweetRepository();
            var max = this.Items.LastOrDefault();
            return repository.GetAsync(maxId: max == null ? null : max.StatusId)
                .ContinueWith(
                    task => this.Dispatcher.BeginInvoke(
                        parameter => {
                            if (parameter != null) {
                                this.Items.AddRangeIf(parameter.OrderByDescending(x => x.StatusId), x => x.StatusId);
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
