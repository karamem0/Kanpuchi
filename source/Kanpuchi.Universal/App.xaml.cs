using Karamem0.Kanpuchi.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Karamem0.Kanpuchi {

    /// <summary>
    /// Windows Phone アプリケーションを表します。
    /// </summary>
    public sealed partial class App : Application {

        /// <summary>
        /// アプリケーションのルート フレームを取得します。
        /// </summary>
        public Frame RootFrame { get; private set; }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.App"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public App() {
            this.InitializeComponent();
        }

        /// <summary>
        /// アプリケーションがアクティブになるとき呼び出されます。
        /// </summary>
        /// <param name="e">イベントのデータを格納する <see cref="Windows.ApplicationModel.Activation.LaunchActivatedEventArgs"/>。</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e) {
            if (this.RootFrame == null) {
                this.RootFrame = new Frame();
            }
            var manager = SystemNavigationManager.GetForCurrentView();
            manager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            manager.BackRequested += this.OnSystemNavigationManagerBackRequested;
            this.RootFrame.Navigate(typeof(MainPage), e.Arguments);
            Window.Current.Content = this.RootFrame;
            Window.Current.Activate();
        }

        private void OnSystemNavigationManagerBackRequested(object sender, BackRequestedEventArgs e) {
            if (this.RootFrame != null) {
                if (this.RootFrame.CanGoBack == true) {
                    this.RootFrame.GoBack();
                }
            }
            e.Handled = true;
        }

    }

}
