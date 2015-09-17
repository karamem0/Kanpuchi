using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Kanpuchi.Extensions {

    /// <summary>
    /// <see cref="Windows.UI.Xaml.DependencyObject"/> クラスの拡張メソッドを定義します。
    /// </summary>
    public static class DependencyObjectExtensions {

        /// <summary>
        /// 指定した依存関係オブジェクトの子孫要素で指定した型に一致するオブジェクトのコレクションを列挙します。
        /// </summary>
        /// <typeparam name="T">検索する依存関係オブジェクトの型。</typeparam>
        /// <param name="target">対象の <see cref="Windows.UI.Xaml.DependencyObject"/>。</param>
        /// <returns><see cref="T:System.Collections.Generic.IEnumerable`1"/>。</returns>
        public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject target) where T : DependencyObject {
            for (int index = 0; index < VisualTreeHelper.GetChildrenCount(target); index++) {
                var child = VisualTreeHelper.GetChild(target, index);
                var typed = child as T;
                if (typed != null) {
                    yield return typed;
                }
                foreach (var item in child.FindVisualChildren<T>()) {
                    yield return item;
                }
            }
        }
    }

}
