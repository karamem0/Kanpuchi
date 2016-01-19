using Karamem0.Kanpuchi.Infrastructures;
using Karamem0.Kanpuchi.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Karamem0.Kanpuchi.ViewModels {

    /// <summary>
    /// アプリの情報を表示するページのビュー モデルを表します。
    /// </summary>
    public class AboutViewModel : ViewModel {

        /// <summary>
        /// バージョン情報を取得します。
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.AboutViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public AboutViewModel() {
            this.Version = AssemblyVersion.Current.Version;
        }

    }

}
