using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kanpuchi.Extensions {

    /// <summary>
    /// <see cref="T:System.Collections.Generic.IEnumerable`1"/> インターフェースの拡張メソッドを定義します。
    /// </summary>
    public static class EnumerableExtensions {

        /// <summary>
        /// <see cref="T:System.Collections.Generic.IEnumerable`1"/> の要素に対して指定した処理を実行します。
        /// </summary>
        /// <typeparam name="T">コレクションの要素の型。</typeparam>
        /// <param name="target"><see cref="T:System.Collections.Generic.IEnumerable`1"/>。</param>
        /// <param name="action">実行する処理を示す <see cref="T:System.Action`1"/>。</param>
        public static void ForEach<T>(this IEnumerable<T> target, Action<T> action) {
            foreach (var item in target) {
                action.Invoke(item);
            }
        }

    }

}
