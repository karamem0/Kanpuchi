using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kanpuchi.Infrastructures {

    /// <summary>
    /// 非同期操作でエラーが発生したときに発生するイベントのデータを格納します。
    /// </summary>
    public class AsyncErrorEventArgs : EventArgs {

        /// <summary>
        /// 発生した例外を取得します。
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// <see cref="Kanpuchi.Infrastructures.AsyncErrorEventArgs"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="exception">発生した例外を示す <see cref="System.Exception"/>。</param>
        public AsyncErrorEventArgs(Exception exception) {
            this.Exception = exception;
        }

    }

}
