using Karamem0.Kanpuchi.Infrastructure;
using Karamem0.Kanpuchi.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.ViewModels {

    public sealed class HomePageViewModel : ViewModelBase {

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
        private async void LoadLatest() {
            var index = (PivotIndex)this.SelectedIndex;
            if (index == PivotIndex.Tweet) {
                var tweetService = new TweetService(this);
                tweetService.AsyncStarted += this.OnTweetServiceAsyncStarted;
                tweetService.AsyncCompleted += this.OnTweetServiceAsyncCompleted;
                await tweetService.LoadLatestAsync();
            }
            if (index == PivotIndex.MatomeEntry) {
                var matomeEntryService = new MatomeEntryService(this);
                matomeEntryService.AsyncStarted += this.OnMatomeEntryServiceAsyncStarted;
                matomeEntryService.AsyncCompleted += this.OnMatomeEntryServiceAsyncCompleted;
                await matomeEntryService.LoadLatestAsync();
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.HomePageViewModel.LoadLatest"/> を実行できるかどうかを判断します。
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
        private async void LoadPrevious() {
            var index = (PivotIndex)this.SelectedIndex;
            if (index == PivotIndex.Tweet) {
                var tweetService = new TweetService(this);
                tweetService.AsyncStarted += this.OnTweetServiceAsyncStarted;
                tweetService.AsyncCompleted += this.OnTweetServiceAsyncCompleted;
                await tweetService.LoadPreviousAsync();
            }
            if (index == PivotIndex.MatomeEntry) {
                var matomeEntryService = new MatomeEntryService(this);
                matomeEntryService.AsyncStarted += this.OnMatomeEntryServiceAsyncStarted;
                matomeEntryService.AsyncCompleted += this.OnMatomeEntryServiceAsyncCompleted;
                await matomeEntryService.LoadPreviousAsync();
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.HomePageViewModel.LoadPrevious"/> を実行できるかどうかを判断します。
        /// </summary>
        /// <returns>コマンドを実行できるか場合は true。それ以外の場合は false。</returns>
        private bool CanLoadPrevious() {
            if (this.IsBusy == true) {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ツイートのコレクションを表します。
        /// </summary>
        private ObservableCollection<TweetViewModel> tweets;

        /// <summary>
        /// ツイートのコレクションを取得します。
        /// </summary>
        public ObservableCollection<TweetViewModel> Tweets {
            get { return this.tweets; }
            private set {
                if (this.tweets != value) {
                    this.tweets = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// まとめ記事のコレクションを表します。
        /// </summary>
        private ObservableCollection<MatomeEntryViewModel> matomeEntries;

        /// <summary>
        /// まとめ記事のコレクションを取得します。
        /// </summary>
        public ObservableCollection<MatomeEntryViewModel> MatomeEntries {
            get { return this.matomeEntries; }
            private set {
                if (this.matomeEntries != value) {
                    this.matomeEntries = value;
                    this.OnPropertyChanged();
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
                    this.OnPropertyChanged();
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
                    this.OnPropertyChanged();
                    this.LoadLatestCommand.RaiseCanExecuteChanged();
                    this.LoadPreviousCommand.RaiseCanExecuteChanged();
                }
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.HomePageViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public HomePageViewModel() {
            this.Tweets = new ObservableCollection<TweetViewModel>();
            this.MatomeEntries = new ObservableCollection<MatomeEntryViewModel>();
            this.LoadLatestCommand = new DelegateCommand(this.LoadLatest, this.CanLoadLatest);
            this.LoadPreviousCommand = new DelegateCommand(this.LoadPrevious, this.CanLoadPrevious);
        }

        /// <summary>
        /// 現在のページにナビゲーションが移動するときに追加の処理を実行します。
        /// </summary>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="Prism.Windows.Navigation.NavigatedToEventArgs"/>。
        /// </param>
        /// <param name="viewModelState">
        /// ビュー モデルの状態を格納する <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"/>。
        /// </param>
        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState) {
            base.OnNavigatedTo(e, viewModelState);
            this.LoadLatest();
        }

        /// <summary>
        /// 現在のページからナビゲーションが移動するときに追加の処理を実行します。
        /// </summary>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="Prism.Windows.Navigation.NavigatedToEventArgs"/>。
        /// </param>
        /// <param name="viewModelState">
        /// ビュー モデルの状態を格納する <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"/>。
        /// </param>
        /// <param name="suspending">
        /// アプリが中断される場合は true。それ以外の場合は false。
        /// </param>
        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending) {
            base.OnNavigatingFrom(e, viewModelState, suspending);
        }

        /// <summary>
        /// ツイートの取得の非同期操作が開始するときに追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="Karamem0.Kanpuchi.Infrastructure.ServiceAsyncStartedEventArgs"/>。
        /// </param>
        private void OnTweetServiceAsyncStarted(object sender, ServiceAsyncStartedEventArgs e) {
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
        /// <param name="e">イベントのデータを格納する <see cref="Karamem0.Kanpuchi.Infrastructure.ServiceAsyncCompletedEventArgs"/>。</param>
        private void OnTweetServiceAsyncCompleted(object sender, ServiceAsyncCompletedEventArgs e) {
            var tweetService = sender as TweetService;
            if (tweetService != null) {
                tweetService.AsyncCompleted -= this.OnTweetServiceAsyncCompleted;
                tweetService.Dispose();
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
        /// <param name="e">イベントのデータを格納する <see cref="Karamem0.Kanpuchi.Infrastructure.ServiceAsyncStartedEventArgs"/>。</param>
        private void OnMatomeEntryServiceAsyncStarted(object sender, ServiceAsyncStartedEventArgs e) {
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
        /// <param name="e">イベントのデータを格納する <see cref="Karamem0.Kanpuchi.Infrastructure.ServiceAsyncCompletedEventArgs"/>。</param>
        private void OnMatomeEntryServiceAsyncCompleted(object sender, ServiceAsyncCompletedEventArgs e) {
            var matomeEntryService = sender as MatomeEntryService;
            if (matomeEntryService != null) {
                matomeEntryService.AsyncCompleted -= this.OnMatomeEntryServiceAsyncCompleted;
                matomeEntryService.Dispose();
            }
            this.IsBusy = false;
            if (e.Exception != null) {
                Messanger.Current.Send("Error", "LoadError");
            }
        }

    }

}
