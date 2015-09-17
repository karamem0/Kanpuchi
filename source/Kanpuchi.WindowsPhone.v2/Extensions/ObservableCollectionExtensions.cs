using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanpuchi.Extensions {

    /// <summary>
    /// <see cref="T:System.Collections.ObjectModel.ObservableCollection`1"/> クラスの拡張メソッドを定義します。
    /// </summary>
    public static class ObservableCollectionExtensions {

        /// <summary>
        /// <see cref="T:System.Collections.ObjectModel.ObservableCollection`1"/>
        /// の末尾に指定したコレクションを追加します。
        /// </summary>
        /// <typeparam name="T">コレクションの要素の型。</typeparam>
        /// <param name="target"><see cref="T:System.Collections.ObjectModel.ObservableCollection`1"/>。</param>
        /// <param name="collection">
        /// 追加するコレクションを示す <see cref="T:System.Collections.Generic.IEnumerable`1"/>。
        /// </param>
        public static void AddRange<T>(this ObservableCollection<T> target, IEnumerable<T> collection) {
            foreach (var item in collection) {
                target.Add(item);
            }
        }

        /// <summary>
        /// <see cref="T:System.Collections.ObjectModel.ObservableCollection`1"/>
        /// の末尾に指定したコレクションを追加します。
        /// </summary>
        /// <typeparam name="T">コレクションの要素の型。</typeparam>
        /// <typeparam name="TProperty">プロパティの型。</typeparam>
        /// <param name="target"><see cref="T:System.Collections.ObjectModel.ObservableCollection`1"/>。</param>
        /// <param name="collection">
        /// 追加するコレクションを示す <see cref="T:System.Collections.Generic.IEnumerable`1"/>。
        /// </param>
        /// <param name="selector">比較に使用するプロパティを取得する <see cref="T:System.Func`2"/>。</param>
        public static void AddRangeIf<T, TProperty>(
            this ObservableCollection<T> target, IEnumerable<T> collection, Func<T, TProperty> selector) {
            foreach (var item in collection) {
                if (target.Any(x => selector.Invoke(x).Equals(selector.Invoke(item))) != true) {
                    target.Add(item);
                }
            }
        }

        /// <summary>
        /// <see cref="T:System.Collections.ObjectModel.ObservableCollection`1"/>
        /// の指定した位置に指定したコレクションを追加します。
        /// </summary>
        /// <typeparam name="T">コレクションの要素の型。</typeparam>
        /// <param name="target"><see cref="T:System.Collections.ObjectModel.ObservableCollection`1"/>。</param>
        /// <param name="index">追加する位置を示す <see cref="System.Int32"/>。</param>
        /// <param name="collection">
        /// 追加するコレクションを示す <see cref="T:System.Collections.Generic.IEnumerable`1"/>。
        /// </param>
        public static void InsertRange<T>(this ObservableCollection<T> target, int index, IEnumerable<T> collection) {
            foreach (var item in collection) {
                target.Insert(index, item);
            }
        }

        /// <summary>
        /// <see cref="T:System.Collections.ObjectModel.ObservableCollection`1"/>
        /// の指定した位置に指定したコレクションを追加します。
        /// </summary>
        /// <typeparam name="T">コレクションの要素の型。</typeparam>
        /// <typeparam name="TProperty">プロパティの型。</typeparam>
        /// <param name="target"><see cref="T:System.Collections.ObjectModel.ObservableCollection`1"/>。</param>
        /// <param name="collection">
        /// 追加するコレクションを示す <see cref="T:System.Collections.Generic.IEnumerable`1"/>。
        /// </param>
        /// <param name="index">追加する位置を示す <see cref="System.Int32"/>。</param>
        /// <param name="selector">比較に使用するプロパティを取得する <see cref="T:System.Func`2"/>。</param>
        public static void InsertRangeIf<T, TProperty>(
            this ObservableCollection<T> target, int index, IEnumerable<T> collection, Func<T, TProperty> selector) {
            foreach (var item in collection) {
                if (target.Any(x => selector.Invoke(x).Equals(selector.Invoke(item))) != true) {
                    target.Insert(index++, item);
                }
            }
        }

    }

}
