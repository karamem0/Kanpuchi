using Kanpuchi.Infrastructure;
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

namespace Kanpuchi.Interactivity {

    /// <summary>
    /// <see cref="Kanpuchi.Infrastructure.ViewModel"/> をサポートするビヘイビアーを表します。
    /// </summary>
    public sealed class ViewModelBehavior : Behavior<Page> {

        /// <summary>
        /// ナビゲーションを管理する <see cref="Windows.UI.Xaml.Controls.Frame"/> を取得します。
        /// </summary>
        private Frame Frame {
            get { return ((App)App.Current).RootFrame; }
        }

        /// <summary>
        /// <see cref="Kanpuchi.Interactivity.ViewModelBehavior"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ViewModelBehavior() { }

        /// <summary>
        /// ビヘイビアーがアタッチされると呼び出されます。
        /// </summary>
        protected override void OnAttached() {
            this.AssociatedObject.Loaded += this.OnAssociatedObjectLoaded;
            this.Frame.Navigating += this.OnFrameNavigating;
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
            this.Frame.Navigating -= this.OnFrameNavigating;
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
            HardwareButtons.BackPressed += this.OnHardwareButtonsBackPressed;
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
            HardwareButtons.BackPressed -= this.OnHardwareButtonsBackPressed;
        }

        private async void OnViewModelError(object sender, ErrorEventArgs e) {
            await new MessageDialog(e.Message).ShowAsync();
        }

        /// <summary>
        /// <see cref="Windows.UI.Xaml.Input.HardwareButtons.BackPressed"/> イベントで追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="Windows.UI.Xaml.Input.BackPressedEventArgs"/>。</param>
        private void OnHardwareButtonsBackPressed(object sender, BackPressedEventArgs e) {
            var viewModel = this.AssociatedObject.DataContext as ViewModel;
            if (viewModel != null) {
                var command = viewModel.GoBackCommand as ICommand;
                if (command.CanExecute(null) == true) {
                    command.Execute(null);
                    e.Handled = true;
                }
            }
        }

    }

}
