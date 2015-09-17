using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kanpuchi.Infrastructures {

    /// <summary>
    /// 非同期操作が開始したときに発生するイベントのデータを格納します。
    /// </summary>
    public class AsyncStartedEventArgs : EventArgs {

        /// <summary>
        /// <see cref="Kanpuchi.Infrastructures.AsyncStartedEventArgs"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public AsyncStartedEventArgs() { }

    }

}
