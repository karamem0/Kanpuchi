using Kanpuchi.Extensions;
using Kanpuchi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace Kanpuchi.ViewModels {

    /// <summary>
    /// アプリの情報を表示するページのビュー モデルを表します。
    /// </summary>
    public sealed class AboutViewModel : ViewModel {

        /// <summary>
        /// バージョン情報を取得します。
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// <see cref="Kanpuchi.ViewModels.AboutViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public AboutViewModel() {
            this.Version = Package.Current.Id.Version.ToFormattedString();
        }

    }

}
