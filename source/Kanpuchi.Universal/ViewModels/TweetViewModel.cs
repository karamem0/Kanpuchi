using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Resources;
using Windows.System;

namespace Karamem0.Kanpuchi.ViewModels {

    /// <summary>
    /// ツイートのビュー モデルを表します。
    /// </summary>
    public sealed class TweetViewModel : BindableBase {

        /// <summary>
        /// ID を表します。
        /// </summary>
        private string statusId;

        /// <summary>
        /// ID を取得または設定します。
        /// </summary>
        public string StatusId {
            get { return this.statusId; }
            set {
                if (this.statusId != value) {
                    this.statusId = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// ユーザー ID を表します。
        /// </summary>
        private string userId;

        /// <summary>
        /// ユーザー ID を取得または設定します。
        /// </summary>
        public string UserId {
            get { return this.userId; }
            set {
                if (this.userId != value) {
                    this.userId = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// ユーザー名を表します。
        /// </summary>
        private string userName;

        /// <summary>
        /// ユーザー名を取得または設定します。
        /// </summary>
        public string UserName {
            get { return this.userName; }
            set {
                if (this.userName != value) {
                    this.userName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 表示名を表します。
        /// </summary>
        private string screenName;

        /// <summary>
        /// 表示名を取得または設定します。
        /// </summary>
        public string ScreenName {
            get { return this.screenName; }
            set {
                if (this.screenName != value) {
                    this.screenName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// プロフィール画像の URL を表します。
        /// </summary>
        private string profileImageUrl;

        /// <summary>
        /// プロフィール画像の URL を取得または設定します。
        /// </summary>
        public string ProfileImageUrl {
            get { return this.profileImageUrl; }
            set {
                if (this.profileImageUrl != value) {
                    this.profileImageUrl = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 固定リンクの URL を表します。
        /// </summary>
        private string permalink;

        /// <summary>
        /// 固定リンクの URL を取得または設定します。
        /// </summary>
        public string Permalink {
            get { return this.permalink; }
            set {
                if (this.permalink != value) {
                    this.permalink = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// テキストを表します。
        /// </summary>
        private string text;

        /// <summary>
        /// テキストを取得または設定します。
        /// </summary>
        public string Text {
            get { return this.text; }
            set {
                if (this.text != value) {
                    this.text = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 作成日時を表します。
        /// </summary>
        private DateTime createdAt;

        /// <summary>
        /// 作成日時を取得または設定します。
        /// </summary>
        public DateTime CreatedAt {
            get { return this.createdAt; }
            set {
                if (this.createdAt != value) {
                    this.createdAt = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// ツイートの URL を Web ブラウザーで表示するコマンドを取得します。
        /// </summary>
        public DelegateCommand LaunchBrowserCommand { get; private set; }

        /// <summary>
        /// ツイートの URL を Web ブラウザーで表示します。
        /// </summary>
        private async void LaunchBrowser() {
            await Launcher.LaunchUriAsync(new Uri(this.Permalink));
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.TweetViewModel.LaunchBrowser"/> を実行できるかどうかを判断します。
        /// </summary>
        /// <returns>コマンドを実行できるか場合は true。それ以外の場合は false。</returns>
        private bool CanLaunchBrowser() {
            if (string.IsNullOrEmpty(this.Permalink) == true) {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ツイートを共有するコマンドを取得します。
        /// </summary>
        public DelegateCommand DataTransferCommand { get; private set; }

        /// <summary>
        /// ツイートを共有します。
        /// </summary>
        private void DataTransfer() {
            var manager = DataTransferManager.GetForCurrentView();
            manager.DataRequested += this.OnDataTransferManagerDataRequested;
            DataTransferManager.ShowShareUI();
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.TweetViewModel.DataTransfer"/> を実行できるかどうかを判断します。
        /// </summary>
        /// <returns>コマンドを実行できるか場合は true。それ以外の場合は false。</returns>
        private bool CanDataTransfer() {
            if (string.IsNullOrEmpty(this.Permalink) == true) {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ツイートをクリップ ボードにコピーするコマンドを取得します。
        /// </summary>
        public DelegateCommand CopyClipboardCommand { get; private set; }

        /// <summary>
        /// ツイートをクリップ ボードにコピーします。
        /// </summary>
        private void CopyClipboard() {
            var dataPackage = new DataPackage();
            dataPackage.RequestedOperation = DataPackageOperation.Copy;
            dataPackage.SetText(this.Permalink);
            Clipboard.SetContent(dataPackage);
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.TweetViewModel.CopyClipboard"/> を実行できるかどうかを判断します。
        /// </summary>
        /// <returns>コマンドを実行できるか場合は true。それ以外の場合は false。</returns>
        private bool CanCopyClipboard() {
            if (string.IsNullOrEmpty(this.Permalink) == true) {
                return false;
            }
            return true;
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.TweetViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TweetViewModel() {
            this.LaunchBrowserCommand = new DelegateCommand(this.LaunchBrowser, this.CanLaunchBrowser);
            this.DataTransferCommand = new DelegateCommand(this.DataTransfer, this.CanDataTransfer);
            this.CopyClipboardCommand = new DelegateCommand(this.CopyClipboard, this.CanCopyClipboard);
        }

        /// <summary>
        /// <see cref="Windows.ApplicationModel.DataTransfer.DataTransferManager.DataRequested"/>
        /// イベントで追加の処理を実行します。
        /// </summary>
        /// <param name="sender">
        /// イベントを発生させた <see cref="Windows.ApplicationModel.DataTransfer.DataTransferManager"/>。
        /// </param>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="Windows.ApplicationModel.DataTransfer.DataRequestedEventArgs"/>。
        /// </param>
        private void OnDataTransferManagerDataRequested(DataTransferManager sender, DataRequestedEventArgs e) {
            var resourceLoader = ResourceLoader.GetForCurrentView("Strings");
            e.Request.Data.Properties.Title = resourceLoader.GetString("ShareTitle");
            e.Request.Data.Properties.Description = resourceLoader.GetString("ShareDescription");
            e.Request.Data.SetWebLink(new Uri(this.Permalink));
            sender.DataRequested += this.OnDataTransferManagerDataRequested;
        }

    }

}
