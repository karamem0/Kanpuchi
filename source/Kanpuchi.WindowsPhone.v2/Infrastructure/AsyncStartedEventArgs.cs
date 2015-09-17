using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanpuchi.Infrastructure {

    /// <summary>
    /// 非同期操作が開始したときに発生するイベントのデータを格納します。
    /// </summary>
    public class AsyncStartedEventArgs : EventArgs {

        /// <summary>
        /// <see cref="Kanpuchi.Infrastructure.AsyncStartedEventArgs"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public AsyncStartedEventArgs() { }

    }

}
