using Karamem0.Kanpuchi.Infrastructures;
using Karamem0.Kanpuchi.Interactivity;
using Karamem0.Kanpuchi.Resources;
using Karamem0.Kanpuchi.Services;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Karamem0.Kanpuchi.ViewModels {

    /// <summary>
    /// ツイートを表示するピボット ページのビュー モデルを表します。
    /// </summary>
    public class TimelineViewModel : ViewModel {

        /// <summary>
        /// ツイートを管理するサービスを取得します。
        /// </summary>
        public TweetService Service { get; private set; }

        /// <summary>
        /// 最新のツイートを読み込むコマンドを取得します。
        /// </summary>
        public DelegateCommand LoadLatestCommand { get; private set; }

        /// <summary>
        /// 前のツイートを読み込むコマンドを取得します。
        /// </summary>
        public DelegateCommand LoadPreviousCommand { get; private set; }

        /// <summary>
        /// 最上部にスクロールするコマンドを取得します。
        /// </summary>
        public DelegateCommand ScrollToTopCommand { get; private set; }

        /// <summary>
        /// 最下部にスクロールするコマンドを取得します。
        /// </summary>
        public DelegateCommand ScrollToBottomCommand { get; private set; }

        /// <summary>
        /// 最上部にスクロールするリクエストを取得します。
        /// </summary>
        public InteractionRequest<Notification> ScrollToTopRequest { get; private set; }

        /// <summary>
        /// 最下部にスクロールするリクエストを取得します。
        /// </summary>
        public InteractionRequest<Notification> ScrollToBottomRequest { get; private set; }

        /// <summary>
        /// ツイートが読み込まれたかどうかを示す値を取得します。
        /// </summary>
        public bool IsLoaded {
            get { return this.Service.Items.Any(); }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.TimelineViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TimelineViewModel() {
            this.Service = new TweetService();
            this.LoadLatestCommand = new DelegateCommand(
                () => this.Service.LoadLatestAsync(),
                () => BusyStateManager.Current.IsBusy != true);
            this.LoadPreviousCommand = new DelegateCommand(
                () => this.Service.LoadPreviousAsync(),
                () => BusyStateManager.Current.IsBusy != true);
            this.ScrollToTopCommand = new DelegateCommand(
                () => this.ScrollToTopRequest.Raise(null));
            this.ScrollToBottomCommand = new DelegateCommand(
                () => this.ScrollToBottomRequest.Raise(null));
            this.ScrollToTopRequest = new InteractionRequest<Notification>();
            this.ScrollToBottomRequest = new InteractionRequest<Notification>();
        }

        /// <summary>
        /// ビュー モデルがロードされると呼び出されます。
        /// </summary>
        public override void OnLoaded() {
            base.OnLoaded();
            this.Service.AsyncStarted += this.OnAsyncStarted;
            this.Service.AsyncCompleted += this.OnAsyncCompleted;
            this.Service.AsyncError += this.OnAsyncError;
            BusyStateManager.Current.BusyStateChanged += this.OnBusyStateChanged;
        }

        /// <summary>
        /// ビュー モデルがアンロードされると呼び出されます。
        /// </summary>
        public override void OnUnloaded() {
            base.OnUnloaded();
            this.Service.AsyncStarted -= this.OnAsyncStarted;
            this.Service.AsyncCompleted -= this.OnAsyncCompleted;
            this.Service.AsyncError -= this.OnAsyncError;
            BusyStateManager.Current.BusyStateChanged -= this.OnBusyStateChanged;
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructures.Service.AsyncStarted"/> イベントで追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="System.EventArgs"/>。</param>
        private void OnAsyncStarted(object sender, EventArgs e) {
            BusyStateManager.Current.Begin(StringResource.LoadProgress);
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructures.Service.AsyncCompleted"/> イベントで追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="System.EventArgs"/>。</param>
        private void OnAsyncCompleted(object sender, EventArgs e) {
            BusyStateManager.Current.End();
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructures.Service.AsyncError"/> イベントで追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="Karamem0.Kanpuchi.Infrastructures.AsyncErrorEventArgs"/>。
        /// </param>
        private void OnAsyncError(object sender, AsyncErrorEventArgs e) {
            BusyStateManager.Current.End();
            this.NotificationRequest.Raise(new Notification() {
                Title = StringResource.LoadErrorTitle,
                Content = StringResource.LoadErrorMessage,
            });
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.BusyStateManager.BusyStateChanged"/> イベントで追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="System.EventArgs"/>。</param>
        private void OnBusyStateChanged(object sender, EventArgs e) {
            this.LoadLatestCommand.RaiseCanExecuteChanged();
            this.LoadPreviousCommand.RaiseCanExecuteChanged();
            this.RaisePropertyChanged(() => this.IsLoaded);
        }

    }

}
