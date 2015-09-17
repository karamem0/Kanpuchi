using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kanpuchi.Infrastructures {

    /// <summary>
    /// 非同期操作が完了したときに発生するイベントのデータを格納します。
    /// </summary>
    public class AsyncCompletedEventArgs : EventArgs {

        /// <summary>
        /// <see cref="Kanpuchi.Infrastructures.AsyncCompletedEventArgs"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public AsyncCompletedEventArgs() { }

    }

}
