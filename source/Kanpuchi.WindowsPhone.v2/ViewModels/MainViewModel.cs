using Karamem0.Kanpuchi.Infrastructure;
using Karamem0.Kanpuchi.Models;
using Karamem0.Kanpuchi.Services;
using Karamem0.Kanpuchi.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.ViewModels {

    /// <summary>
    /// メイン ページのビュー モデルを表します。
    /// </summary>
    public sealed class MainViewModel : ViewModel {

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Views.MainPage"/> のピボットの項目のインデックスを指定します。   
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
                var tweetService = new TweetService(this);
                tweetService.AsyncStarted += this.OnTweetServiceAsyncStarted;
                tweetService.AsyncCompleted += this.OnTweetServiceAsyncCompleted;
                tweetService.LoadLatestAsync();
            }
            if (index == PivotIndex.MatomeEntry) {
                var matomeEntryService = new MatomeEntryService(this);
                matomeEntryService.AsyncStarted += this.OnMatomeEntryServiceAsyncStarted;
                matomeEntryService.AsyncCompleted += this.OnMatomeEntryServiceAsyncCompleted;
                matomeEntryService.LoadLatestAsync();
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.MainViewModel.LoadLatest"/> を実行できるかどうかを判断します。
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
                var tweetService = new TweetService(this);
                tweetService.AsyncStarted += this.OnTweetServiceAsyncStarted;
                tweetService.AsyncCompleted += this.OnTweetServiceAsyncCompleted;
                tweetService.LoadPreviousAsync();
            }
            if (index == PivotIndex.MatomeEntry) {
                var matomeEntryService = new MatomeEntryService(this);
                matomeEntryService.AsyncStarted += this.OnMatomeEntryServiceAsyncStarted;
                matomeEntryService.AsyncCompleted += this.OnMatomeEntryServiceAsyncCompleted;
                matomeEntryService.LoadPreviousAsync();
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.MainViewModel.LoadPrevious"/> を実行できるかどうかを判断します。
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
        /// ツイートのコレクションを表します。
        /// </summary>
        private ObservableCollection<Tweet> tweets;

        /// <summary>
        /// ツイートのコレクションを取得します。
        /// </summary>
        public ObservableCollection<Tweet> Tweets {
            get { return this.tweets; }
            private set {
                if (this.tweets != value) {
                    this.tweets = value;
                    this.RaisePropertyChanged(() => this.Tweets);
                }
            }
        }

        /// <summary>
        /// まとめ記事のコレクションを表します。
        /// </summary>
        private ObservableCollection<MatomeEntry> matomeEntries;

        /// <summary>
        /// まとめ記事のコレクションを取得します。
        /// </summary>
        public ObservableCollection<MatomeEntry> MatomeEntries {
            get { return this.matomeEntries; }
            private set {
                if (this.matomeEntries != value) {
                    this.matomeEntries = value;
                    this.RaisePropertyChanged(() => this.MatomeEntries);
                }
            }
        }

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
        /// <see cref="Karamem0.Kanpuchi.ViewModels.MainViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MainViewModel() {
            this.Tweets = new ObservableCollection<Tweet>();
            this.MatomeEntries = new ObservableCollection<MatomeEntry>();
            this.SelectedIndex = (int)PivotIndex.Tweet;
            this.LoadLatestCommand = new DelegateCommand(this.LoadLatest, this.CanLoadLatest);
            this.LoadPreviousCommand = new DelegateCommand(this.LoadPrevious, this.CanLoadPrevious);
            this.GoToSettingsPageCommand = new DelegateCommand(this.GoToSettingsPage);
            this.GoToAboutPageCommand = new DelegateCommand(this.GoToAboutPage);
        }

        /// <summary>
        /// ビュー モデルがロードされると呼び出されます。
        /// </summary>
        public override void OnLoaded() {
            base.OnLoaded();
            this.LoadLatest();
        }

        /// <summary>
        /// ビュー モデルがアンロードされる直前に呼び出されます。
        /// </summary>
        public override void OnUnloaded() {
            base.OnUnloaded();
        }

        /// <summary>
        /// コレクションをの要素をすべて削除します。
        /// </summary>
        public void Clear() {
            this.Tweets.Clear();
            this.MatomeEntries.Clear();
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.ViewModel.BusyStateChanged"/> イベントで追加の処理を実行します。
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
        /// イベントのデータを格納する <see cref="Karamem0.Kanpuchi.Infrastructure.AsyncStartedEventArgs"/>。
        /// </param>
        private void OnTweetServiceAsyncStarted(object sender, AsyncStartedEventArgs e) {
            var tweetService = sender as TweetService;
            if (tweetService != null) {
                tweetService.AsyncStarted -= this.OnTweetServiceAsyncStarted;
            }
            this.BeginBusy("LoadTweet");
        }

        /// <summary>
        /// ツイートの取得の非同期操作が完了するときに追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="Karamem0.Kanpuchi.Infrastructure.AsyncCompletedEventArgs"/>。</param>
        private void OnTweetServiceAsyncCompleted(object sender, AsyncCompletedEventArgs e) {
            var tweetService = sender as TweetService;
            if (tweetService != null) {
                tweetService.AsyncCompleted -= this.OnTweetServiceAsyncCompleted;
            }
            this.EndBusy();
            if (e.Exception != null) {
                this.RaiseError("LoadError");
            }
        }

        /// <summary>
        /// まとめ記事の取得の非同期操作が開始するときに追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="Karamem0.Kanpuchi.Infrastructure.AsyncStartedEventArgs"/>。</param>
        private void OnMatomeEntryServiceAsyncStarted(object sender, AsyncStartedEventArgs e) {
            var matomeEntryService = sender as MatomeEntryService;
            if (matomeEntryService != null) {
                matomeEntryService.AsyncStarted -= this.OnMatomeEntryServiceAsyncStarted;
            }
            this.BeginBusy("LoadMatomeEntry");
        }

        /// <summary>
        /// まとめ記事の取得の非同期操作が完了するときに追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="Karamem0.Kanpuchi.Infrastructure.AsyncCompletedEventArgs"/>。</param>
        private void OnMatomeEntryServiceAsyncCompleted(object sender, AsyncCompletedEventArgs e) {
            var matomeEntryService = sender as MatomeEntryService;
            if (matomeEntryService != null) {
                matomeEntryService.AsyncCompleted -= this.OnMatomeEntryServiceAsyncCompleted;
            }
            this.EndBusy();
            if (e.Exception != null) {
                this.RaiseError("LoadError");
            }
        }

    }

}
