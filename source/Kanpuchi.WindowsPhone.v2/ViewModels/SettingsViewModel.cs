using Kanpuchi.Configuration;
using Kanpuchi.Infrastructure;
using Kanpuchi.Interactivity;
using Kanpuchi.Models;
using Kanpuchi.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanpuchi.ViewModels {

    /// <summary>
    /// 設定ページのビュー モデルを表します。
    /// </summary>
    public sealed class SettingsViewModel : ViewModel {

        /// <summary>
        /// 設定を保存するコマンドを取得します。
        /// </summary>
        public DelegateCommand SaveCommand { get; private set; }

        /// <summary>
        /// 設定を保存します。
        /// </summary>
        private void Save() {
            var settingsService = new SettingsService(this);
            settingsService.SaveAsync();
            var mainViewModel = new ViewModelLocator().MainViewModel as MainViewModel;
            if (mainViewModel != null) {
                mainViewModel.Clear();
            }
            var frame = ((App)App.Current).RootFrame;
            if (frame.CanGoBack == true) {
                frame.GoBack();
            }
        }

        /// <summary>
        /// <see cref="Kanpuchi.ViewModels.SettingsViewModel.CanSave"/> を実行できるか判断します。
        /// </summary>
        /// <returns></returns>
        private bool CanSave() {
            return true;
        }

        /// <summary>
        /// アプリ内ブラウザーを使用するかどうかを示す値を表します。
        /// </summary>
        private bool useInAppBrowser;

        /// <summary>
        /// アプリ内ブラウザーを使用するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool UseInAppBrowser {
            get { return this.useInAppBrowser; }
            set {
                if (this.useInAppBrowser != value) {
                    this.useInAppBrowser = value;
                    this.RaisePropertyChanged(() => this.UseInAppBrowser);
                }
            }
        }

        /// <summary>
        /// まとめサイトのコレクションを表します。
        /// </summary>
        private ObservableCollection<MatomeSite> matomeSites;

        /// <summary>
        /// まとめサイトのコレクションを取得します。
        /// </summary>
        public ObservableCollection<MatomeSite> MatomeSites {
            get { return this.matomeSites; }
            private set {
                if (this.matomeSites != value) {
                    this.matomeSites = value;
                    this.RaisePropertyChanged(() => this.MatomeSites);
                }
            }
        }

        /// <summary>
        /// <see cref="Kanpuchi.ViewModels.SettingsViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public SettingsViewModel() {
            this.MatomeSites = new ObservableCollection<MatomeSite>();
            this.SaveCommand = new DelegateCommand(this.Save, this.CanSave);
        }

        /// <summary>
        /// ビュー モデルがロードされると呼び出されます。
        /// </summary>
        public override void OnLoaded() {
            base.OnLoaded();
            var settingsService = new SettingsService(this);
            settingsService.AsyncStarted += this.OnSettingsServiceAsyncStarted;
            settingsService.AsyncCompleted += this.OnSettingsServiceAsyncCompleted;
            settingsService.LoadAsync();
        }

        /// <summary>
        /// ビュー モデルがアンロードされると呼び出されます。
        /// </summary>
        public override void OnUnloaded() {
            base.OnUnloaded();
        }

        /// <summary>
        /// まとめ記事の取得の非同期操作が開始するときに追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="Kanpuchi.Infrastructure.AsyncStartedEventArgs"/>。</param>
        private void OnSettingsServiceAsyncStarted(object sender, AsyncStartedEventArgs e) {
            var settingsService = sender as SettingsService;
            if (settingsService != null) {
                settingsService.AsyncStarted -= this.OnSettingsServiceAsyncStarted;
            }
            this.BeginBusy("LoadSettings");
        }

        /// <summary>
        /// まとめ記事の取得の非同期操作が完了するときに追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="Kanpuchi.Infrastructure.AsyncCompletedEventArgs"/>。</param>
        private void OnSettingsServiceAsyncCompleted(object sender, AsyncCompletedEventArgs e) {
            var settingsService = sender as SettingsService;
            if (settingsService != null) {
                settingsService.AsyncCompleted -= this.OnSettingsServiceAsyncCompleted;
            }
            this.EndBusy();
            if (e.Exception != null) {
                this.RaiseError("LoadError");
            }
        }

    }

}
