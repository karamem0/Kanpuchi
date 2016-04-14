using Karamem0.Kanpuchi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.ViewModels {

    /// <summary>
    /// ツイートのビュー モデルを表します。
    /// </summary>
    public sealed class TweetViewModel : ViewModel {

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
                    this.RaisePropertyChanged(() => this.StatusId);
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
                    this.RaisePropertyChanged(() => this.UserId);
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
                    this.RaisePropertyChanged(() => this.UserName);
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
                    this.RaisePropertyChanged(() => this.ScreenName);
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
                    this.RaisePropertyChanged(() => this.ProfileImageUrl);
                }
            }
        }

        /// <summary>
        /// プロフィール画像の URL を表します。
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
                    this.RaisePropertyChanged(() => this.Permalink);
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
                    this.RaisePropertyChanged(() => this.Text);
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
        /// <see cref="Karamem0.Kanpuchi.ViewModels.TweetViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TweetViewModel() { }

    }

}
