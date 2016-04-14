using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.Infrastructure {

    /// <summary>
    /// 非同期操作が完了したときに発生するイベントのデータを格納します。
    /// </summary>
    public sealed class AsyncCompletedEventArgs : EventArgs {

        /// <summary>
        /// 発生した例外を取得します。
        /// </summary>
        public Exception Exception { get; private set; }
        
        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.AsyncCompletedEventArgs"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="exception">発生した例外を示す <see cref="System.Exception"/>。</param>
        public AsyncCompletedEventArgs(Exception exception = null) {
            this.Exception = exception;
        }

    }

}
