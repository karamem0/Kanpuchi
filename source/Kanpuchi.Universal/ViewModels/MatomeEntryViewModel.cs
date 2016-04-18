using Karamem0.Kanpuchi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.ViewModels {

    /// <summary>
    /// まとめ記事のビュー モデルを表します。
    /// </summary>
    public sealed class MatomeEntryViewModel : ViewModel {

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
                    this.RaisePropertyChanged(() => this.EntryId);
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
                    this.RaisePropertyChanged(() => this.SiteId);
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
                    this.RaisePropertyChanged(() => this.SiteName);
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
                    this.RaisePropertyChanged(() => this.Title);
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
                    this.RaisePropertyChanged(() => this.Url);
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
                    this.RaisePropertyChanged(() => this.ThumbnailUrl);
                    this.RaisePropertyChanged(() => this.IsThumbnailUrlEnable);
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
                    this.RaisePropertyChanged(() => this.CreatedAt);
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
        /// <see cref="Karamem0.Kanpuchi.ViewModels.MatomeEntryViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MatomeEntryViewModel() { }

        /// <summary>
        /// ビュー モデルがロードされると呼び出されます。
        /// </summary>
        public override void OnLoaded() { }

        /// <summary>
        /// ビュー モデルがアンロードされると呼び出されます。
        /// </summary>
        public override void OnUnloaded() { }

    }

}
