using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kanpuchi.Extensions {

    /// <summary>
    /// <see cref="System.Threading.SynchronizationContext"/> クラスの拡張メソッドを定義します。
    /// </summary>
    public static class SynchronizationContextExtensions {

        /// <summary>
        /// 指定したメソッドを同期コンテキストにディスパッチします。
        /// </summary>
        /// <param name="target">非同期コンテキストを示す <see cref="System.Threading.SynchronizationContext"/>。</param>
        /// <param name="action">実行されるメソッドを示す <see cref="System.Action"/>。</param>
        public static void Post(this SynchronizationContext target, Action action) {
            if (target == null) {
                action.Invoke();
            } else {
                target.Post(state => action.Invoke(), null);
            }
        }

        /// <summary>
        /// 指定したメソッドを同期コンテキストにディスパッチします。メソッドは指定したパラメーターを受け取ります。
        /// </summary>
        /// <typeparam name="T">メソッドのパラメーターの型。</typeparam>
        /// <param name="target">非同期コンテキストを示す <see cref="System.Threading.SynchronizationContext"/>。</param>
        /// <param name="action">実行されるメソッドを示す <see cref="T:System.Action`1"/>。</param>
        /// <param name="parameter">メソッドのパラメーターを示すオブジェクト。</param>
        public static void Post<T>(this SynchronizationContext target, Action<T> action, T parameter) {
            if (target == null) {
                action.Invoke(parameter);
            } else {
                target.Post(state => action.Invoke((T)state), parameter);
            }
        }

    }

}
