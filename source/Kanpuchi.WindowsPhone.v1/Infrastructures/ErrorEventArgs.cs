using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kanpuchi.Infrastructures {

    /// <summary>
    /// <see cref="E:Kanpuchi.Infrastructures.Repository`1.Error"/> イベントのデータを格納します。
    /// </summary>
    public class ErrorEventArgs : EventArgs {

        /// <summary>
        /// 発生した例外を取得します。
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// <see cref="Kanpuchi.Infrastructures.ErrorEventArgs"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="exception">発生した例外を示す <see cref="System.Exception"/>。</param>
        public ErrorEventArgs(Exception exception) {
            this.Exception = exception;
        }

    }

}
