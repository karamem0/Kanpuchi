using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Karamem0.Kanpuchi.Views {

    /// <summary>
    /// アプリ内ブラウザーを表示するページを表します。
    /// </summary>
    public sealed partial class WebBrowserPage : Page {

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Views.WebBrowserPage"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public WebBrowserPage() {
            this.InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e">イベントのデータを格納する <see cref="Windows.UI.Xaml.Navigation.NavigationEventArgs"/>。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e) {
            var manager = DataTransferManager.GetForCurrentView();
            if (manager != null) {
                manager.DataRequested += this.OnDataTransferManagerDataRequested;
            }
            this.WebView.Source = new Uri((string)e.Parameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e">イベントのデータを格納する <see cref="Windows.UI.Xaml.Navigation.NavigationEventArgs"/>。</param>
        protected override void OnNavigatedFrom(NavigationEventArgs e) {
            var manager = DataTransferManager.GetForCurrentView();
            if (manager != null) {
                manager.DataRequested -= this.OnDataTransferManagerDataRequested;
            }
        }

        /// <summary>
        /// <see cref="Windows.ApplicationModel.DataTransfer.DataTransferManager.DataRequested"/> イベントで追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="Windows.ApplicationModel.DataTransfer.DataRequestedEventArgs"/>。</param>
        private void OnDataTransferManagerDataRequested(object sender, DataRequestedEventArgs e) {
            e.Request.Data.Properties.Title = this.WebView.DocumentTitle;
            e.Request.Data.Properties.Description = this.WebView.DocumentTitle;
            e.Request.Data.SetWebLink(this.WebView.Source);
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Views.WebBrowserPage.WebView"/> でナビゲーションを完了したときに追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="Windows.UI.Xaml.Controls.WebViewNavigationCompletedEventArgs"/>。</param>
        private void OnWebViewNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs e) {
            this.BackButton.IsEnabled = this.WebView.CanGoBack;
            this.ForwardButton.IsEnabled = this.WebView.CanGoForward;
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Views.WebBrowserPage.BackButton"/> をクリックしたときに追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="Windows.UI.Xaml.RoutedEventArgs"/>。</param>
        private void OnBackButtonClick(object sender, RoutedEventArgs e) {
            if (this.WebView.CanGoBack == true) {
                this.WebView.GoBack();
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Views.WebBrowserPage.FormardButton"/> をクリックしたときに追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="Windows.UI.Xaml.RoutedEventArgs"/>。</param>
        private void OnForwardButtonClick(object sender, RoutedEventArgs e) {
            if (this.WebView.CanGoForward == true) {
                this.WebView.GoForward();
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Views.WebBrowserPage.ShareButton"/> をクリックしたときに追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="Windows.UI.Xaml.RoutedEventArgs"/>。</param>
        private void OnShareButtonClick(object sender, RoutedEventArgs e) {
            DataTransferManager.ShowShareUI();
        }

    }

}
