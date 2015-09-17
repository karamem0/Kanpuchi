using Kanpuchi.Infrastructure;
using Kanpuchi.Interactivity;
using Kanpuchi.Services;
using Kanpuchi.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.Popups;

namespace Kanpuchi.ViewModels {

    /// <summary>
    /// メイン ページのビュー モデルを表します。
    /// </summary>
    public sealed class MainViewModel : ViewModel {

        /// <summary>
        /// <see cref="Kanpuchi.Views.MainPage"/> のピボットの項目のインデックスを指定します。   
        /// </summary>
        private enum PivotIndex {

            /// <summary>
            /// ツイートを表します。
            /// </summary>
            Tweet = 0,

            /// <summary>
            /// まとめ記事を表します。
            /// </summary>
            MatomeEntry = 1,

        }

        /// <summary>
        /// 選択されているピボット項目の最新のデータを読み込むコマンドを取得します。
        /// </summary>
        public DelegateCommand LoadLatestCommand { get; private set; }

        /// <summary>
        /// 選択されているピボット項目の最新のデータを読み込みます。
        /// </summary>
        private void LoadLatest() {
            var index = (PivotIndex)this.SelectedIndex;
            if (index == PivotIndex.Tweet) {
                this.TweetService.LoadLatestAsync();
            }
            if (index == PivotIndex.MatomeEntry) {
                this.MatomeEntryService.LoadLatestAsync();
            }
        }

        /// <summary>
        /// <see cref="Kanpuchi.ViewModels.MainViewModel.LoadLatest"/> を実行できるかどうかを判断します。
        /// </summary>
        /// <returns>コマンドを実行できるか場合は true。それ以外の場合は false。</returns>
        private bool CanLoadLatest() {
            if (this.IsBusy == true) {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 選択されているピボット項目の直前のデータを読み込むコマンドを取得します。
        /// </summary>
        public DelegateCommand LoadPreviousCommand { get; private set; }

        /// <summary>
        /// 選択されているピボット項目の直前のデータを読み込みます。
        /// </summary>
        private void LoadPrevious() {
            var index = (PivotIndex)this.SelectedIndex;
            if (index == PivotIndex.Tweet) {
                this.TweetService.LoadPreviousAsync();
            }
            if (index == PivotIndex.MatomeEntry) {
                this.MatomeEntryService.LoadPreviousAsync();
            }
        }

        /// <summary>
        /// <see cref="Kanpuchi.ViewModels.MainViewModel.LoadPrevious"/> を実行できるかどうかを判断します。
        /// </summary>
        /// <returns>コマンドを実行できるか場合は true。それ以外の場合は false。</returns>
        private bool CanLoadPrevious() {
            if (this.IsBusy == true) {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 設定ページに移動するコマンドを取得します。
        /// </summary>
        public DelegateCommand GoToSettingsPageCommand { get; private set; }

        /// <summary>
        /// 設定ページに移動します。
        /// </summary>
        private void GoToSettingsPage() {
            this.GoToPage(typeof(SettingsPage));
        }

        /// <summary>
        /// アプリ情報ページに移動するコマンドを取得します。
        /// </summary>
        public DelegateCommand GoToAboutPageCommand { get; private set; }

        /// <summary>
        /// アプリ情報ページに移動します。
        /// </summary>
        private void GoToAboutPage() {
            this.GoToPage(typeof(AboutPage));
        }

        /// <summary>
        /// ツイートを取得するサービスを取得します。
        /// </summary>
        public TweetService TweetService { get; private set; }

        /// <summary>
        /// まとめ記事を取得するサービスを取得します。
        /// </summary>
        public MatomeEntryService MatomeEntryService { get; private set; }

        /// <summary>
        /// ピボットの選択項目のインデックス番号を表します。
        /// </summary>
        private int selectedIndex;

        /// <summary>
        /// ピボットの選択項目のインデックス番号を取得または設定します。
        /// </summary>
        public int SelectedIndex {
            get { return this.selectedIndex; }
            set {
                if (this.selectedIndex != value) {
                    this.selectedIndex = value;
                    this.RaisePropertyChanged(() => this.SelectedIndex);
                    this.LoadLatest();
                }
            }
        }

        /// <summary>
        /// <see cref="Kanpuchi.ViewModels.MainViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MainViewModel() {
            this.LoadLatestCommand = new DelegateCommand(this.LoadLatest, this.CanLoadLatest);
            this.LoadPreviousCommand = new DelegateCommand(this.LoadPrevious, this.CanLoadPrevious);
            this.GoToSettingsPageCommand = new DelegateCommand(this.GoToSettingsPage);
            this.GoToAboutPageCommand = new DelegateCommand(this.GoToAboutPage);
            this.TweetService = new TweetService();
            this.MatomeEntryService = new MatomeEntryService();
        }

        /// <summary>
        /// ビュー モデルがロードされると呼び出されます。
        /// </summary>
        public override void OnLoaded() {
            base.OnLoaded();
            this.SubscribeService();
            this.LoadLatest();
        }

        /// <summary>
        /// ビュー モデルがアンロードされる直前に呼び出されます。
        /// </summary>
        public override void OnUnloaded() {
            base.OnUnloaded();
            this.UnsubscribeService();
            this.EndBusy();
        }

        /// <summary>
        /// サービスのイベントの購読を開始します。
        /// </summary>
        private void SubscribeService() {
            this.TweetService.AsyncStarted += this.OnTweetServiceAsyncStarted;
            this.TweetService.AsyncCompleted += this.OnTweetServiceAsyncCompleted;
            this.MatomeEntryService.AsyncStarted += this.OnMatomeEntryServiceAsyncStarted;
            this.MatomeEntryService.AsyncCompleted += this.OnMatomeEntryServiceAsyncCompleted;
        }

        /// <summary>
        /// サービスのイベントの購読を解除します。
        /// </summary>
        private void UnsubscribeService() {
            this.TweetService.AsyncStarted -= this.OnTweetServiceAsyncStarted;
            this.TweetService.AsyncCompleted -= this.OnTweetServiceAsyncCompleted;
            this.MatomeEntryService.AsyncStarted -= this.OnMatomeEntryServiceAsyncStarted;
            this.MatomeEntryService.AsyncCompleted -= this.OnMatomeEntryServiceAsyncCompleted;
        }

        /// <summary>
        /// <see cref="Kanpuchi.Infrastructure.ViewModel.BusyStateChanged"/> イベントで追加の処理を実行します。
        /// </summary>
        /// <param name="e">イベントのデータを格納する <see cref="System.EventArgs"/>。</param>
        protected override void OnBusyStateChanged(EventArgs e) {
            base.OnBusyStateChanged(e);
            this.LoadLatestCommand.RaiseCanExecuteChanged();
            this.LoadPreviousCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// ツイートの取得の非同期操作が開始するときに追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="Kanpuchi.Infrastructure.AsyncStartedEventArgs"/>。
        /// </param>
        private void OnTweetServiceAsyncStarted(object sender, AsyncStartedEventArgs e) {
            this.BeginBusy("LoadTweet");
        }

        /// <summary>
        /// ツイートの取得の非同期操作が完了するときに追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="Kanpuchi.Infrastructure.AsyncCompletedEventArgs"/>。</param>
        private void OnTweetServiceAsyncCompleted(object sender, AsyncCompletedEventArgs e) {
            this.EndBusy();
            if (e.Exception != null) {
                this.RaiseError("LoadError");
            }
        }

        /// <summary>
        /// まとめ記事の取得の非同期操作が開始するときに追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="Kanpuchi.Infrastructure.AsyncStartedEventArgs"/>。</param>
        private void OnMatomeEntryServiceAsyncStarted(object sender, AsyncStartedEventArgs e) {
            this.BeginBusy("LoadMatomeEntry");
        }

        /// <summary>
        /// まとめ記事の取得の非同期操作が完了するときに追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="Kanpuchi.Infrastructure.AsyncCompletedEventArgs"/>。</param>
        private void OnMatomeEntryServiceAsyncCompleted(object sender, AsyncCompletedEventArgs e) {
            this.EndBusy();
            if (e.Exception != null) {
                this.RaiseError("LoadError");
            }
        }

    }

}
