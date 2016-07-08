using Karamem0.Kanpuchi.ViewModels;
using Karamem0.Kanpuchi.Views;
using Prism.Mvvm;
using Prism.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Karamem0.Kanpuchi {

    public sealed partial class Application : PrismApplication {

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Application"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public Application() {
            this.InitializeComponent();
        }

        /// <summary>
        /// アプリケーションのシェルを示すビジュアル要素を作成します。
        /// </summary>
        /// <param name="rootFrame">基底のフレームを示す <see cref="Windows.UI.Xaml.Controls.Frame"/>。</param>
        /// <returns>シェルを示す <see cref="Windows.UI.Xaml.UIElement"/>。</returns>
        protected override UIElement CreateShell(Frame rootFrame) {
            var shell = new Shell();
            shell.SplitView.Content = rootFrame;
            return shell;
        }

        /// <summary>
        /// アプリケーションが初期化されるときのイベントで追加の処理を実行します。
        /// </summary>
        /// <param name="args">
        /// イベントのデータを格納する
        /// <see cref="Windows.ApplicationModel.Activation.IActivatedEventArgs"/>。
        /// </param>
        /// <returns><see cref="System.Threading.Tasks.Task"/>。</returns>
        protected override Task OnInitializeAsync(IActivatedEventArgs args) {
            ViewModelLocationProvider.Register(
                typeof(Shell).FullName,
                () => new ShellViewModel(this.NavigationService));
            var homePageViewModel = new HomePageViewModel();
            ViewModelLocationProvider.Register(
                typeof(HomePage).FullName,
                () => homePageViewModel);
            var settingsPageViewModel = new SettingsPageViewModel();
            ViewModelLocationProvider.Register(
                typeof(SettingsPage).FullName,
                () => settingsPageViewModel);
            return base.OnInitializeAsync(args);
        }

        /// <summary>
        /// アプリケーションが起動されるときのイベントで追加の処理を実行します。
        /// </summary>
        /// <param name="args">
        /// イベントのデータを格納する
        /// <see cref="Windows.ApplicationModel.Activation.LaunchActivatedEventArgs"/>。
        /// </param>
        /// <returns><see cref="System.Threading.Tasks.Task"/>。</returns>
        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args) {
            this.NavigationService.Navigate("Home", null);
            return Task.FromResult<object>(null);
        }

    }

}
