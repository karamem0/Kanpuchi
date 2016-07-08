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
    /// まとめ記事のビュー モデルを表します。
    /// </summary>
    public sealed class MatomeEntryViewModel : BindableBase {

        /// <summary>
        /// 記事 ID を表します。
        /// </summary>
        private Guid entryId;

        /// <summary>
        /// 記事 ID を取得または設定します。
        /// </summary>
        public Guid EntryId {
            get { return this.entryId; }
            set {
                if (this.entryId != value) {
                    this.entryId = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// サイト ID を表します。
        /// </summary>
        private int siteId;

        /// <summary>
        /// サイト ID を取得または設定します。
        /// </summary>
        public int SiteId {
            get { return this.siteId; }
            set {
                if (this.siteId != value) {
                    this.siteId = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// サイト名を表します。
        /// </summary>
        private string siteName;

        /// <summary>
        /// サイト名を取得または設定します。
        /// </summary>
        public string SiteName {
            get { return this.siteName; }
            set {
                if (this.siteName != value) {
                    this.siteName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// タイトルを表します。
        /// </summary>
        private string title;

        /// <summary>
        /// タイトルを取得または設定します。
        /// </summary>
        public string Title {
            get { return this.title; }
            set {
                if (this.title != value) {
                    this.title = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// URL を表します。
        /// </summary>
        private string url;

        /// <summary>
        /// URL を取得または設定します。
        /// </summary>
        public string Url {
            get { return this.url; }
            set {
                if (this.url != value) {
                    this.url = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// サムネイル画像の URL を表します。
        /// </summary>
        private string thumbnailUrl;

        /// <summary>
        /// サムネイル画像の URL を取得または設定します。
        /// </summary>
        public string ThumbnailUrl {
            get { return this.thumbnailUrl; }
            set {
                if (this.thumbnailUrl != value) {
                    this.thumbnailUrl = value;
                    this.OnPropertyChanged();
                    this.OnPropertyChanged(() => this.IsThumbnailUrlEnable);
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
        /// サムネイル画像が有効かどうかを示す値を取得します。
        /// </summary>
        public bool IsThumbnailUrlEnable {
            get { return (string.IsNullOrEmpty(this.thumbnailUrl) != true); }
        }

        /// <summary>
        /// ツイートの URL を Web ブラウザーで表示するコマンドを取得します。
        /// </summary>
        public DelegateCommand LaunchBrowserCommand { get; private set; }

        /// <summary>
        /// ツイートの URL を Web ブラウザーで表示します。
        /// </summary>
        private async void LaunchBrowser() {
            await Launcher.LaunchUriAsync(new Uri(this.Url));
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.TweetViewModel.LaunchBrowser"/> を実行できるかどうかを判断します。
        /// </summary>
        /// <returns>コマンドを実行できるか場合は true。それ以外の場合は false。</returns>
        private bool CanLaunchBrowser() {
            if (string.IsNullOrEmpty(this.Url) == true) {
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
            if (string.IsNullOrEmpty(this.Url) == true) {
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
            dataPackage.SetText(this.Url);
            Clipboard.SetContent(dataPackage);
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.TweetViewModel.CopyClipboard"/> を実行できるかどうかを判断します。
        /// </summary>
        /// <returns>コマンドを実行できるか場合は true。それ以外の場合は false。</returns>
        private bool CanCopyClipboard() {
            if (string.IsNullOrEmpty(this.Url) == true) {
                return false;
            }
            return true;
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.MatomeEntryViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MatomeEntryViewModel() {
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
            e.Request.Data.SetWebLink(new Uri(this.Url));
            sender.DataRequested += this.OnDataTransferManagerDataRequested;
        }

    }

}
