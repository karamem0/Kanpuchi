using Karamem0.Kanpuchi.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interactivity;

namespace Karamem0.Kanpuchi.Interactivity {

    /// <summary>
    /// <see cref="Karamem0.Kanpuchi.Infrastructures.ViewModel"/> をサポートするビヘイビアーを表します。
    /// </summary>
    public class ViewModelBehavior : Behavior<FrameworkElement> {

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.ViewModelBehavior"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ViewModelBehavior() { }

        /// <summary>
        /// ビヘイビアーがアタッチされると呼び出されます。
        /// </summary>
        protected override void OnAttached() {
            base.OnAttached();
            this.AssociatedObject.Loaded += this.OnAssociatedObjectLoaded;
            this.AssociatedObject.Unloaded += this.OnAssociatedObjectUnloaded;
        }

        /// <summary>
        /// ビヘイビアーがデタッチされるときに呼び出されます。
        /// </summary>
        protected override void OnDetaching() {
            base.OnDetaching();
            this.AssociatedObject.Loaded -= this.OnAssociatedObjectLoaded;
            this.AssociatedObject.Unloaded -= this.OnAssociatedObjectUnloaded;
        }

        /// <summary>
        /// <see cref="System.Windows.FrameworkElement.Loaded"/> イベントで追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="System.Windows.RoutedEventArgs"/>。</param>
        private void OnAssociatedObjectLoaded(object sender, RoutedEventArgs e) {
            var viewModel = this.AssociatedObject.DataContext as ViewModel;
            if (viewModel != null) {
                viewModel.OnLoaded();
            }
        }

        /// <summary>
        /// <see cref="System.Windows.FrameworkElement.Unloaded"/> イベントで追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="System.Windows.RoutedEventArgs"/>。</param>
        private void OnAssociatedObjectUnloaded(object sender, RoutedEventArgs e) {
            var viewModel = this.AssociatedObject.DataContext as ViewModel;
            if (viewModel != null) {
                viewModel.OnUnloaded();
            }
        }

    }

}
