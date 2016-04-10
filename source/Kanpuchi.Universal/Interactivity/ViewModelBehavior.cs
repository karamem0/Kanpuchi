using Karamem0.Kanpuchi.Infrastructure;
using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Karamem0.Kanpuchi.Interactivity {

    /// <summary>
    /// <see cref="Karamem0.Kanpuchi.Infrastructure.ViewModel"/> をサポートするビヘイビアーを表します。
    /// </summary>
    public sealed class ViewModelBehavior : Behavior<Page> {

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.ViewModelBehavior"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ViewModelBehavior() { }

        /// <summary>
        /// ビヘイビアーがアタッチされると呼び出されます。
        /// </summary>
        protected override void OnAttached() {
            this.AssociatedObject.Loaded += this.OnAssociatedObjectLoaded;
            var contentFrame = ((App)Application.Current).ContentFrame;
            if (contentFrame != null) {
                contentFrame.Navigating += this.OnFrameNavigating;
            }
            var viewModel = this.AssociatedObject.DataContext as ViewModel;
            if (viewModel != null) {
                viewModel.Error += this.OnViewModelError;
            }
        }

        /// <summary>
        /// ビヘイビアーがデタッチされると呼び出されます。
        /// </summary>
        protected override void OnDetached() {
            this.AssociatedObject.Loaded -= this.OnAssociatedObjectLoaded;
            var contentFrame = ((App)Application.Current).ContentFrame;
            if (contentFrame != null) {
                contentFrame.Navigating -= this.OnFrameNavigating;
            }
            var viewModel = this.AssociatedObject.DataContext as ViewModel;
            if (viewModel != null) {
                viewModel.Error -= this.OnViewModelError;
            }
        }

        /// <summary>
        /// <see cref="Windows.UI.Xaml.FrameworkElement.Loaded"/> イベントで追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="Windows.UI.Xaml.RoutedEventArgs"/>。</param>
        private void OnAssociatedObjectLoaded(object sender, RoutedEventArgs e) {
            var viewModel = this.AssociatedObject.DataContext as ViewModel;
            if (viewModel != null) {
                viewModel.OnLoaded();
            }
        }

        /// <summary>
        /// <see cref="Windows.UI.Xaml.Controls.Frame.Navigating"/> イベントで追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="Windows.UI.Xaml.Navigation.NavigatingCancelEventArgs"/>。</param>
        private void OnFrameNavigating(object sender, NavigatingCancelEventArgs e) {
            var viewModel = this.AssociatedObject.DataContext as ViewModel;
            if (viewModel != null) {
                viewModel.OnUnloaded();
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.ViewModel.Error"/> イベントで追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="Karamem0.Kanpuchi.Infrastructure.ErrorEventArgs"/>。</param>
        private async void OnViewModelError(object sender, ErrorEventArgs e) {
            await new MessageDialog(e.Message).ShowAsync();
        }

    }

}
