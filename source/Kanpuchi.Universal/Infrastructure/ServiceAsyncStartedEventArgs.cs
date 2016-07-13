using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.Infrastructure {

    /// <summary>
    /// 非同期操作が開始したときに発生するイベントのデータを格納します。
    /// </summary>
    public sealed class ServiceAsyncStartedEventArgs : EventArgs {

        /// <summary>
        /// 非同期操作のメソッド名を取得します。
        /// </summary>
        public string MethodName { get; private set; }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.ServiceAsyncStartedEventArgs"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="methodName">非同期操作のメソッド名を示す <see cref="System.String"/>。</param>
        public ServiceAsyncStartedEventArgs(string methodName) {
            this.MethodName = methodName;
        }

    }

}
