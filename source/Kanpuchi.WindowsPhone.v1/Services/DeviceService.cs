using Kanpuchi.Extensions;
using Kanpuchi.Infrastructures;
using Kanpuchi.Models;
using Kanpuchi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanpuchi.Services {

    /// <summary>
    /// デバイスを管理するためのサービスを表します。
    /// </summary>
    public class DeviceService : Service {

        /// <summary>
        /// <see cref="Kanpuchi.Services.DeviceService"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public DeviceService() { }

        /// <summary>
        /// デバイスを登録します。
        /// </summary>
        /// <returns>非同期操作を示す <see cref="System.Threading.Tasks.Task"/>。</returns>
        public Task RegisterAsync() {
            this.Dispatcher.BeginInvoke(() => this.RaiseAsyncStarted());
            var repository = new DeviceRepository();
            return repository.PostAsync(repository.Create())
                .ContinueWith(
                    task => {
                        this.Dispatcher.BeginInvoke(
                            parameter => {
                                repository.Save(parameter);
                                this.RaiseAsyncCompleted();
                            },
                            task.Result);
                    },
                    TaskContinuationOptions.OnlyOnRanToCompletion)
                .ContinueWith(
                    task => this.Dispatcher.BeginInvoke(() => this.RaiseAsyncError(task.Exception)),
                    TaskContinuationOptions.NotOnRanToCompletion);
        }

    }

}
