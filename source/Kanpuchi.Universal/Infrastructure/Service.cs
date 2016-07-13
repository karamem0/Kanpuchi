using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.Infrastructure {

    /// <summary>
    /// サービスの基本機能を提供します。
    /// </summary>
    public abstract class Service : IDisposable {

        /// <summary>
        /// 非同期操作が開始されると発生します。
        /// </summary>
        public event EventHandler<ServiceAsyncStartedEventArgs> AsyncStarted;

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.Service.AsyncStarted"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="Karamem0.Kanpuchi.Infrastructure.ServiceAsyncStartedEventArgs"/>。
        /// </param>
        protected virtual void OnAsyncStarted(ServiceAsyncStartedEventArgs e) {
            var handler = this.AsyncStarted;
            if (handler != null) {
                handler.Invoke(this, e);
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.Service.AsyncStarted"/> イベントを発生させます。
        /// </summary>
        /// <param name="methodName">非同期操作のメソッド名を示す <see cref="System.String"/>。</param>
        protected void RaiseAsyncStarted(string methodName) {
            this.OnAsyncStarted(new ServiceAsyncStartedEventArgs(methodName));
        }

        /// <summary>
        /// 非同期操作が完了すると発生します。
        /// </summary>
        public event EventHandler<ServiceAsyncCompletedEventArgs> AsyncCompleted;

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.Service.AsyncCompleted"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="Karamem0.Kanpuchi.Infrastructure.ServiceAsyncCompletedEventArgs"/>。
        /// </param>
        protected virtual void OnAsyncCompleted(ServiceAsyncCompletedEventArgs e) {
            var handler = this.AsyncCompleted;
            if (handler != null) {
                handler.Invoke(this, e);
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.Service.AsyncCompleted"/> イベントを発生させます。
        /// </summary>
        /// <param name="methodName">非同期操作のメソッド名を示す <see cref="System.String"/>。</param>
        /// <param name="ex">発生した例外を示す <see cref="System.Exception"/>。</param>
        protected void RaiseAsyncCompleted(string methodName, Exception ex = null) {
            this.OnAsyncCompleted(new ServiceAsyncCompletedEventArgs(methodName, ex));
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.Service"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        protected Service() { }

        /// <summary>
        /// オブジェクトが破棄されるときにクリーン アップを実行します。
        /// </summary>
        ~Service() {
            this.Dispose(false);
        }

        /// <summary>
        /// 現在のインスタンスで使用されているリソースを解放します。
        /// </summary>
        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 現在のインスタンスで使用されているリソースを解放します。
        /// </summary>
        /// <param name="disposing">
        /// アンマネージ リソースとマネージ リソースの両方を解放する場合は true。アンマネージ
        /// リソースのみ解放する場合は false。
        /// </param>
        protected abstract void Dispose(bool disposing);

    }

}
