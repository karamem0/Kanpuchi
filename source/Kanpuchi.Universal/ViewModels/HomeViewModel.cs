using Karamem0.Kanpuchi.Infrastructure;
using Karamem0.Kanpuchi.Models;
using Karamem0.Kanpuchi.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.ViewModels {

    /// <summary>
    /// ホーム ページのビュー モデルを表します。
    /// </summary>
    public sealed class HomeViewModel : ViewModel {

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Views.HomePage"/> のピボットの項目のインデックスを指定します。   
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
                }
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.HomeViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public HomeViewModel() {
            this.Tweets = new ObservableCollection<Tweet>();
            this.MatomeEntries = new ObservableCollection<MatomeEntry>();
            this.LoadLatestCommand = new DelegateCommand(this.LoadLatest, this.CanLoadLatest);
        }

        /// <summary>
        /// ビュー モデルがロードされると呼び出されます。
        /// </summary>
        public override void OnLoaded() {
            this.LoadLatest();
        }

        /// <summary>
        /// ビュー モデルがアンロードされる直前に呼び出されます。
        /// </summary>
        public override void OnUnloaded() { }

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
            this.IsBusy = true;
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
            this.IsBusy = false;
            if (e.Exception != null) {
                Messanger.Current.Send("Error", "LoadError");
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
            this.IsBusy = true;
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
            this.IsBusy = false;
            if (e.Exception != null) {
                Messanger.Current.Send("Error", "LoadError");
            }
        }


    }

}
