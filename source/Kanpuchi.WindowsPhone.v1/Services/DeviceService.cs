using Karamem0.Kanpuchi.Extensions;
using Karamem0.Kanpuchi.Infrastructures;
using Karamem0.Kanpuchi.Models;
using Karamem0.Kanpuchi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.Services {

    /// <summary>
    /// デバイスを管理するためのサービスを表します。
    /// </summary>
    public class DeviceService : Service {

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Services.DeviceService"/> クラスの新しいインスタンスを初期化します。
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
