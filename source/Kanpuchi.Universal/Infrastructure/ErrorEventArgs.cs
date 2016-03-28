using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Karamem0.Kanpuchi.Infrastructure {

    /// <summary>
    /// エラーが発生したときに発生するイベントのデータを格納します。
    /// </summary>
    public sealed class ErrorEventArgs : EventArgs {

        /// <summary>
        /// エラー メッセージを取得します。
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// <see cref="ErrorEventArgs"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="message">エラー メッセージを示す <see cref="System.String"/>。</param>
        public ErrorEventArgs(string message) {
            this.Message = message;
        }

    }

}
