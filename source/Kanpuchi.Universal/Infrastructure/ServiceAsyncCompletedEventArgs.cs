using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.Infrastructure {

    /// <summary>
    /// 非同期操作が完了したときに発生するイベントのデータを格納します。
    /// </summary>
    public sealed class ServiceAsyncCompletedEventArgs : EventArgs {

        /// <summary>
        /// 非同期操作のメソッド名を取得します。
        /// </summary>
        public string MethodName { get; private set; }
        
        /// <summary>
        /// 発生した例外を取得します。
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.ServiceAsyncCompletedEventArgs"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="methodName">非同期操作のメソッド名を示す <see cref="System.String"/>。</param>
        /// <param name="exception">発生した例外を示す <see cref="System.Exception"/>。</param>
        public ServiceAsyncCompletedEventArgs(string methodName, Exception exception = null) {
            this.MethodName = methodName;
            this.Exception = exception;
        }

    }

}
