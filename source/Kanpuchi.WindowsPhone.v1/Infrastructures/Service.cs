using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Karamem0.Kanpuchi.Infrastructures {

    /// <summary>
    /// サービスの基本機能を提供します。
    /// </summary>
    public abstract class Service {

        /// <summary>
        /// 非同期操作が開始されると発生します。
        /// </summary>
        public event EventHandler<AsyncStartedEventArgs> AsyncStarted;

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructures.Service.AsyncStarted"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="Karamem0.Kanpuchi.Infrastructures.AsyncStartedEventArgs"/>。
        /// </param>
        protected virtual void OnAsyncStarted(AsyncStartedEventArgs e) {
            var handler = this.AsyncStarted;
            if (handler != null) {
                handler.Invoke(this, e);
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructures.Service.AsyncStarted"/> イベントを発生させます。
        /// </summary>
        protected void RaiseAsyncStarted() {
            this.OnAsyncStarted(new AsyncStartedEventArgs());
        }

        /// <summary>
        /// 非同期操作が完了すると発生します。
        /// </summary>
        public event EventHandler<AsyncCompletedEventArgs> AsyncCompleted;

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructures.Service.AsyncCompleted"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="Karamem0.Kanpuchi.Infrastructures.AsyncCompletedEventArgs"/>。
        /// </param>
        protected virtual void OnAsyncCompleted(AsyncCompletedEventArgs e) {
            var handler = this.AsyncCompleted;
            if (handler != null) {
                handler.Invoke(this, e);
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructures.Service.AsyncCompleted"/> イベントを発生させます。
        /// </summary>
        protected void RaiseAsyncCompleted() {
            this.OnAsyncCompleted(new AsyncCompletedEventArgs());
        }

        /// <summary>
        /// 非同期操作中にエラーが発生すると発生します。
        /// </summary>
        public event EventHandler<AsyncErrorEventArgs> AsyncError;

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructures.Service.AsyncError"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="Karamem0.Kanpuchi.Infrastructures.AsyncErrorEventArgs"/>。
        /// </param>
        protected virtual void OnAsyncError(AsyncErrorEventArgs e) {
            var handler = this.AsyncError;
            if (handler != null) {
                handler.Invoke(this, e);
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructures.Service.AsyncError"/> イベントを発生させます。
        /// </summary>
        /// <param name="exception">発生した例外を示す <see cref="System.Exception"/>。</param>
        protected void RaiseAsyncError(Exception exception) {
            this.OnAsyncError(new AsyncErrorEventArgs(exception));
        }

        /// <summary>
        /// UI スレッドのディスパッチャーを取得します。
        /// </summary>
        protected Dispatcher Dispatcher {
            get { return Deployment.Current.Dispatcher; }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructures.Service"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        protected Service() { }

    }

}
