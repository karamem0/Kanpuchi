using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.Extensions {

    public static class ObservableCollectionExtensions {

        public sealed class DelegateComparer<T> : IComparer<T> {

            private Func<T, T, int> selector;

            public DelegateComparer(Func<T, T, int> selector) {
                if (selector == null) {
                    throw new ArgumentNullException(nameof(selector));
                }
                this.selector = selector;
            }

            public int Compare(T x, T y) {
                return this.selector(x, y);
            }

        }

        public sealed class DelegateEqualityComparer<T> : IEqualityComparer<T> {

            private Func<T, T, bool> selector;

            public DelegateEqualityComparer(Func<T, T, bool> selector) {
                if (selector == null) {
                    throw new ArgumentNullException(nameof(selector));
                }
                this.selector = selector;
            }

            public bool Equals(T x, T y) {
                return this.selector(x, y);
            }

            public int GetHashCode(T obj) {
                return obj.GetHashCode();
            }

        }

        public static void AddRange<T, TSort, TEquality>(
            this ObservableCollection<T> target,
            IEnumerable<T> collection,
            Func<T, TSort> sortSelector,
            Func<T, TEquality> equalitySelector) 
            where TSort : IComparable {
            var sortComparer = new DelegateComparer<T>(
                (x, y) => sortSelector(x).CompareTo(sortSelector(y)));
            var equalityComparer = new DelegateEqualityComparer<T>(
                (x, y) => equalitySelector(x).Equals(equalitySelector(y)));
            var index = target.Count - 1;
            foreach (var addItem in collection.OrderBy(sortSelector)) {
                if (index < 0) {
                    target.Insert(0, addItem);
                } else {
                    while (index >= 0) {
                        var currentItem = target[index];
                        if (sortComparer.Compare(currentItem, addItem) >= 0) {
                            if (equalityComparer.Equals(currentItem, addItem) != true) {
                                target.Insert(index + 1, addItem);
                            }
                            break;
                        }
                        index -= 1;
                    }
                }
            }
        }

    }

}
