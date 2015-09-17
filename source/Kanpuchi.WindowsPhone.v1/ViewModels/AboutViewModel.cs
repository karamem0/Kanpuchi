using Kanpuchi.Infrastructures;
using Kanpuchi.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kanpuchi.ViewModels {

    /// <summary>
    /// アプリの情報を表示するページのビュー モデルを表します。
    /// </summary>
    public class AboutViewModel : ViewModel {

        /// <summary>
        /// バージョン情報を取得します。
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// <see cref="Kanpuchi.ViewModels.AboutViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public AboutViewModel() {
            this.Version = AssemblyVersion.Current.Version;
        }

    }

}
