using Prism.Mvvm;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Windows.Navigation;
using Karamem0.Kanpuchi.Services;
using Karamem0.Kanpuchi.Infrastructure;
using Prism.Commands;

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
            this.MatomeSites = new ObservableCollection<MatomeSiteViewModel>();
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState) {
            this.Load();
            Messanger.Current.AfterSend += this.OnMessangerAfterSend;
            base.OnNavigatedTo(e, viewModelState);
        }

        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending) {
            Messanger.Current.AfterSend -= this.OnMessangerAfterSend;
            base.OnNavigatingFrom(e, viewModelState, suspending);
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

        private void OnMessangerAfterSend(object sender, MessageEventArgs e) {
            if (e.Key == "SaveSettings") {
                this.Save();
            }
        }

        /// <summary>
        /// まとめサイトの取得の非同期操作が開始するときに追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="Karamem0.Kanpuchi.Infrastructure.AsyncStartedEventArgs"/>。
        /// </param>
        private void OnSettingsServiceAsyncStarted(object sender, AsyncStartedEventArgs e) {
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
        /// イベントのデータを格納する <see cref="Karamem0.Kanpuchi.Infrastructure.AsyncCompletedEventArgs"/>。
        /// </param>
        private void OnSettingsServiceAsyncCompleted(object sender, AsyncCompletedEventArgs e) {
            var settingsService = sender as SettingsService;
            if (settingsService != null) {
                settingsService.AsyncCompleted -= this.OnSettingsServiceAsyncCompleted;
                settingsService.Dispose();
            }
            this.IsBusy = false;
            if (e.Exception != null) {
                Messanger.Current.Send("Error", "LoadError");
            }
        }

    }

}
