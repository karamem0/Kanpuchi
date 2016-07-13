using Karamem0.Kanpuchi.Infrastructure;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.ViewModels {

    public sealed class MatomeSiteViewModel : BindableBase {

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
        /// サイトの URL を表します。
        /// </summary>
        private string siteUrl;

        /// <summary>
        /// サイトの URL を取得または設定します。
        /// </summary>
        public string SiteUrl {
            get { return this.siteUrl; }
            set {
                if (this.siteUrl != value) {
                    this.siteUrl = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// フィードの URL を表します。
        /// </summary>
        private string feedUrl;

        /// <summary>
        /// フィードの URL を取得または設定します。
        /// </summary>
        public string FeedUrl {
            get { return this.feedUrl; }
            set {
                if (this.feedUrl != value) {
                    this.feedUrl = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 使用するかどうかを示す値を表します。
        /// </summary>
        private bool enabled;

        /// <summary>
        /// 使用するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool Enabled {
            get { return this.enabled; }
            set {
                if (this.enabled != value) {
                    this.enabled = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.MatomeSiteViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MatomeSiteViewModel() { }

    }

}
