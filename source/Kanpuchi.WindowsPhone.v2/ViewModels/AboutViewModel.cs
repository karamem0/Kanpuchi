using Karamem0.Kanpuchi.Extensions;
using Karamem0.Kanpuchi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace Karamem0.Kanpuchi.ViewModels {

    /// <summary>
    /// アプリの情報を表示するページのビュー モデルを表します。
    /// </summary>
    public sealed class AboutViewModel : ViewModel {

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
                    this.RaisePropertyChanged(() => this.Version);
                }
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.AboutViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public AboutViewModel() {
            this.Version = Package.Current.Id.Version.ToFormattedString();
        }

    }

}
