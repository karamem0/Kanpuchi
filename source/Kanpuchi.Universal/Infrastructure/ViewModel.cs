using Karamem0.Kanpuchi.Configuration;
using Karamem0.Kanpuchi.Extensions;
using Karamem0.Kanpuchi.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.ApplicationModel.Store;
using Windows.System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace Karamem0.Kanpuchi.Infrastructure {

    /// <summary>
    /// ビュー モデルを表します。
    /// </summary>
    public abstract class ViewModel : NotifyPropertyChanged {

        /// <summary>
        /// エラーが発生すると発生します。
        /// </summary>
        public event EventHandler<ErrorEventArgs> Error;

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.ViewModel.Error"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベントのデータを格納する <see cref="Karamem0.Kanpuchi.Infrastructure.ErrorEventArgs"/>。</param>
        protected virtual void OnError(ErrorEventArgs e) {
            var handler = this.Error;
            if (handler != null) {
                handler.Invoke(this, e);
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.ViewModel.Error"/> イベントを発生させます。
        /// </summary>
        /// <param name="resourceKey">メッセージ リソースのキー文字列を示す <see cref="System.String"/>。</param>
        protected void RaiseError(string resourceKey) {
            var resourceLoader = ResourceLoader.GetForCurrentView("Errors");
            var message = resourceLoader.GetString(resourceKey);
            this.OnError(new ErrorEventArgs(message));
        }

        /// <summary>
        /// ビジー状態が変更されると発生します。
        /// </summary>
        public event EventHandler BusyStateChanged;

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.ViewModel.BusyStateChanged"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベントのデータを格納する <see cref="System.EventArgs"/>。</param>
        protected virtual void OnBusyStateChanged(EventArgs e) {
            var handler = this.BusyStateChanged;
            if (handler != null) {
                handler.Invoke(this, e);
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.ViewModel.BusyStateChanged"/> イベントを発生させます。
        /// </summary>
        protected void RaiseBusyStateChanged() {
            this.OnBusyStateChanged(new EventArgs());
        }

        /// <summary>
        /// 戻るナビゲーション履歴の最新ページに移動するコマンドを取得します。
        /// </summary>
        public DelegateCommand GoBackCommand { get; private set; }

        /// <summary>
        /// 戻るナビゲーション履歴の最新ページに移動します。
        /// </summary>
        protected void GoBack() {
            var contentFrame = ((App)Application.Current).ContentFrame;
            if (contentFrame.CanGoBack == true) {
                contentFrame.GoBack();
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.ViewModel.GoBack"/> を実行できるかどうかを判断します。
        /// </summary>
        /// <returns>コマンドを実行できるか場合は true。それ以外の場合は false。</returns>
        protected bool CanGoBack() {
            return ((App)Application.Current).ContentFrame.CanGoBack;
        }

        /// <summary>
        /// 進むナビゲーション履歴の最新ページに移動するコマンドを取得します。
        /// </summary>
        public DelegateCommand GoForwardCommand { get; private set; }

        /// <summary>
        /// 進むナビゲーション履歴の最新ページに移動します。
        /// </summary>
        protected void GoForward() {
            var contentFrame = ((App)Application.Current).ContentFrame;
            if (contentFrame.CanGoForward == true) {
                contentFrame.GoForward();
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.ViewModel.GoForward"/> を実行できるかどうかを判断します。
        /// </summary>
        /// <returns>コマンドを実行できるか場合は true。それ以外の場合は false。</returns>
        protected bool CanGoForward() {
            return ((App)Application.Current).ContentFrame.CanGoBack;
        }

        /// <summary>
        /// URI を指定して Web ブラウザーを起動するコマンドを取得します。
        /// </summary>
        public DelegateCommand<string> LaunchBrowserCommand { get; private set; }

        /// <summary>
        /// URI を指定して Web ブラウザーを起動します。
        /// </summary>
        /// <param name="parameter">URI を示す <see cref="System.String"/>。</param>
        protected async void LaunchBrowser(string parameter) {
            await Launcher.LaunchUriAsync(new Uri(parameter));
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.ViewModel.LaunchBrowser"/> を実行できるかどうかを判断します。
        /// </summary>
        /// <param name="parameter">URI を示す <see cref="System.String"/>。</param>
        /// <returns>コマンドを実行できるか場合は true。それ以外の場合は false。</returns>
        protected bool CanLaunchBrowser(string parameter) {
            if (parameter.IsNullOrEmpty() == true) {
                return false;
            }
            return true;
        }

        /// <summary>
        /// アプリのレビューを起動するコマンドを取得します。
        /// </summary>
        public DelegateCommand LaunchReviewAppCommand { get; private set; }

        /// <summary>
        /// アプリのレビューを起動します。
        /// </summary>
        private async void LaunchReviewApp() {
            await Launcher.LaunchUriAsync(new Uri("ms-windows-store:reviewapp?appid=" + CurrentApp.AppId));
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.ViewModel.LaunchReviewApp"/> を実行できるかどうかを判断します。
        /// </summary>
        /// <param name="parameter">URI を示す <see cref="System.String"/>。</param>
        /// <returns>コマンドを実行できるか場合は true。それ以外の場合は false。</returns>
        private bool CanLaunchReviewApp() {
            return true;
        }

        /// <summary>
        /// ビジー状態にあるかどうかを示す値を表します。
        /// </summary>
        private bool isBusy;

        /// <summary>
        /// ビジー状態にあるかどうかを示す値を取得します。
        /// </summary>
        public bool IsBusy {
            get { return this.isBusy; }
            private set {
                if (this.isBusy != value) {
                    this.isBusy = value;
                    this.RaisePropertyChanged(() => this.IsBusy);
                    this.RaiseBusyStateChanged();
                }
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.ViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ViewModel() {
            this.GoBackCommand = new DelegateCommand(this.GoBack, this.CanGoBack);
            this.GoForwardCommand = new DelegateCommand(this.GoForward, this.CanGoForward);
            this.LaunchBrowserCommand = new DelegateCommand<string>(this.LaunchBrowser, this.CanLaunchBrowser);
            this.LaunchReviewAppCommand = new DelegateCommand(this.LaunchReviewApp, this.CanLaunchReviewApp);
        }

        /// <summary>
        /// ビュー モデルがロードされると呼び出されます。
        /// </summary>
        public virtual void OnLoaded() { }

        /// <summary>
        /// ビュー モデルがアンロードされると呼び出されます。
        /// </summary>
        public virtual void OnUnloaded() {
            this.EndBusy();
        }

        /// <summary>
        /// 指定した型で表されるページに移動します。
        /// </summary>
        /// <param name="pageType">ナビゲーションするページを示す <see cref="System.Type"/>。</param>
        /// <param name="parameter">パラメーターを示す <see cref="System.Object"/>。</param>
        protected void GoToPage(Type pageType, object parameter = null) {
            if (pageType != null) {
                var contentFrame = ((App)Application.Current).ContentFrame;
                if (contentFrame != null) {
                    contentFrame.Navigate(pageType, parameter);
                }
            }
        }

        /// <summary>
        /// ビジー状態を開始します。
        /// </summary>
        /// <param name="resourceKey">メッセージ リソースのキー文字列を示す <see cref="System.String"/>。</param>
        protected void BeginBusy(string resourceKey) {
            this.IsBusy = true;
        }

        /// <summary>
        /// ビジー状態を終了します。
        /// </summary>
        protected void EndBusy() {
            this.IsBusy = false;
        }

    }

}
