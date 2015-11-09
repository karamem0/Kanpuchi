using Kanpuchi.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kanpuchi.Infrastructure {

    /// <summary>
    /// サービスの基本機能を提供します。
    /// </summary>
    public abstract class Service {

        /// <summary>
        /// 非同期操作が開始されると発生します。
        /// </summary>
        public event EventHandler<AsyncStartedEventArgs> AsyncStarted;

        /// <summary>
        /// <see cref="Kanpuchi.Infrastructure.Service.AsyncStarted"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="Kanpuchi.Infrastructure.AsyncStartedEventArgs"/>。
        /// </param>
        protected virtual void OnAsyncStarted(AsyncStartedEventArgs e) {
            var handler = this.AsyncStarted;
            if (handler != null) {
                handler.Invoke(this, e);
            }
        }

        /// <summary>
        /// <see cref="Kanpuchi.Infrastructure.Service.AsyncStarted"/> イベントを発生させます。
        /// </summary>
        protected void RaiseAsyncStarted() {
            SynchronizationContext.Current.Post(param => this.OnAsyncStarted(param), new AsyncStartedEventArgs());
        }

        /// <summary>
        /// 非同期操作が完了すると発生します。
        /// </summary>
        public event EventHandler<AsyncCompletedEventArgs> AsyncCompleted;

        /// <summary>
        /// <see cref="Kanpuchi.Infrastructure.Service.AsyncCompleted"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="Kanpuchi.Infrastructure.AsyncCompletedEventArgs"/>。
        /// </param>
        protected virtual void OnAsyncCompleted(AsyncCompletedEventArgs e) {
            var handler = this.AsyncCompleted;
            if (handler != null) {
                handler.Invoke(this, e);
            }
        }

        /// <summary>
        /// <see cref="Kanpuchi.Infrastructure.Service.AsyncCompleted"/> イベントを発生させます。
        /// </summary>\
        /// <param name="ex">発生した例外を示す <see cref="System.Exception"/>。</param>
        protected void RaiseAsyncCompleted(Exception ex = null) {
            SynchronizationContext.Current.Post(param => this.OnAsyncCompleted(param), new AsyncCompletedEventArgs(ex));
        }

        /// <summary>
        /// <see cref="Kanpuchi.Infrastructure.Service"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        protected Service() { }

    }

}
