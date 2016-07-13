using Karamem0.Kanpuchi.Extensions;
using Karamem0.Kanpuchi.Infrastructure;
using Karamem0.Kanpuchi.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.System;

namespace Karamem0.Kanpuchi.ViewModels {

    public sealed class SettingsPageViewModel : ViewModelBase {

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Views.SettingsPage"/> のピボットの項目のインデックスを指定します。   
        /// </summary>
        private enum PivotIndex {

            /// <summary>
            /// まとめサイトを表します。
            /// </summary>
            MatomeSite = 0,

            About = 1,

        }

        /// <summary>
        /// 指定した URL を Web ブラウザーで表示するコマンドを取得します。
        /// </summary>
        public DelegateCommand<string> LaunchBrowserCommand { get; private set; }

        /// <summary>
        /// 指定した URL を Web ブラウザーで表示します。
        /// </summary>
        private async void LaunchBrowser(string parameter) {
            await Launcher.LaunchUriAsync(new Uri(parameter));
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.SettingsPageViewModel.LaunchBrowser"/> を実行できるかどうかを判断します。
        /// </summary>
        /// <returns>コマンドを実行できるか場合は true。それ以外の場合は false。</returns>
        private bool CanLaunchBrowser(string parameter) {
            if (string.IsNullOrEmpty(parameter) == true) {
                return false;
            }
            return true;
        }

        /// <summary>
        /// まとめサイトのコレクションを表します。
        /// </summary>
        private ObservableCollection<MatomeSiteViewModel> matomeSites;

        /// <summary>
        /// まとめサイトのコレクションを取得します。
        /// </summary>
        public ObservableCollection<MatomeSiteViewModel> MatomeSites {
            get { return this.matomeSites; }
            private set {
                if (this.matomeSites != value) {
                    this.matomeSites = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// バージョン情報を表します。
        /// </summary>
        private string version;

        /// <summary>
        /// バージョン情報を取得します。
        /// </summary>
        public string Version {
            get { return this.version; }
            private set {
                if (this.version != value) {
                    this.version = value;
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
                }
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.SettingsPageViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public SettingsPageViewModel() {
            this.LaunchBrowserCommand = new DelegateCommand<string>(this.LaunchBrowser, this.CanLaunchBrowser);
            this.MatomeSites = new ObservableCollection<MatomeSiteViewModel>();
            this.Version = Package.Current.Id.Version.ToFormattedString();
        }

        /// <summary>
        /// 選択されているピボット項目の最新のデータを読み込みます。
        /// </summary>
        private async void Load() {
            var settingsService = new SettingsService(this);
            settingsService.AsyncStarted += this.OnSettingsServiceAsyncStarted;
            settingsService.AsyncCompleted += this.OnSettingsServiceAsyncCompleted;
            await settingsService.LoadAsync();
        }

        /// <summary>
        /// 選択されているピボット項目の最新のデータを読み込みます。
        /// </summary>
        private async void Save() {
            var settingsService = new SettingsService(this);
            settingsService.AsyncStarted += this.OnSettingsServiceAsyncStarted;
            settingsService.AsyncCompleted += this.OnSettingsServiceAsyncCompleted;
            await settingsService.SaveAsync();
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
            this.Load();
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
            foreach (var matomeSite in this.MatomeSites) {
                matomeSite.PropertyChanged -= this.OnMatomeSitePropertyChanged;
            }
        }

        /// <summary>
        /// まとめサイトのプロパティが変更されたときに追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="System.ComponentModel.PropertyChangedEventArgs"/>。
        /// </param>
        private void OnMatomeSitePropertyChanged(object sender, PropertyChangedEventArgs e) {
            var matomeSite = sender as MatomeSiteViewModel;
            if (e.PropertyName == nameof(matomeSite.Enabled)) {
                this.Save();
            }
        }

        /// <summary>
        /// まとめサイトの取得の非同期操作が開始するときに追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="Karamem0.Kanpuchi.Infrastructure.ServiceAsyncStartedEventArgs"/>。
        /// </param>
        private void OnSettingsServiceAsyncStarted(object sender, ServiceAsyncStartedEventArgs e) {
            var settingsService = sender as SettingsService;
            if (settingsService != null) {
                settingsService.AsyncStarted -= this.OnSettingsServiceAsyncStarted;
            }
            this.IsBusy = true;
        }

        /// <summary>
        /// まとめサイトの取得の非同期操作が完了するときに追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="Karamem0.Kanpuchi.Infrastructure.ServiceAsyncCompletedEventArgs"/>。
        /// </param>
        private void OnSettingsServiceAsyncCompleted(object sender, ServiceAsyncCompletedEventArgs e) {
            var settingsService = sender as SettingsService;
            if (settingsService != null) {
                settingsService.AsyncCompleted -= this.OnSettingsServiceAsyncCompleted;
                settingsService.Dispose();
                if (e.MethodName == nameof(settingsService.LoadAsync)) {
                    foreach (var matomeSite in this.MatomeSites) {
                        matomeSite.PropertyChanged += this.OnMatomeSitePropertyChanged;
                    }
                }
            }
            this.IsBusy = false;
            if (e.Exception != null) {
                Messanger.Current.Send("Error", "LoadError");
            }
        }

    }

}
