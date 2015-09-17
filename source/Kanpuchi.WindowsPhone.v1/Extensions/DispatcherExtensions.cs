using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace Kanpuchi.Extensions {

    /// <summary>
    /// <see cref="System.Windows.Threading.Dispatcher"/> クラスの拡張メソッドを定義します。
    /// </summary>
    public static class DispatcherExtensions {

        /// <summary>
        /// <see cref="System.Windows.Threading.Dispatcher"/> が関連付けられたスレッドで指定したメソッドを非同期的に実行します。
        /// </summary>
        /// <typeparam name="T">パラメーターの型。</typeparam>
        /// <param name="target"><see cref="System.Windows.Threading.Dispatcher"/>。</param>
        /// <param name="action">実行するメソッドを示す <see cref="T:System.Action`1"/>。</param>
        /// <param name="parameter">実行するメソッドのパラメーターの値。</param>
        /// <returns><see cref="System.Windows.Threading.DispatcherOperation"/>。</returns>
        public static DispatcherOperation BeginInvoke<T>(this Dispatcher target, Action<T> action, T parameter) {
            return target.BeginInvoke(action, parameter);
        }

    }

}
