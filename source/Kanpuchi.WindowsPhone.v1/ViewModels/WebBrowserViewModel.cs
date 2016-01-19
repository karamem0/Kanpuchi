using Karamem0.Kanpuchi.Infrastructures;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Karamem0.Kanpuchi.ViewModels {

    /// <summary>
    /// ウェブ ブラウザーを表示するページのビュー モデルを表します。
    /// </summary>
    public class WebBrowserViewModel : ViewModel {

        /// <summary>
        /// URI を表します。
        /// </summary>
        private string uri;

        /// <summary>
        /// URI を取得または設定します。
        /// </summary>
        public string Uri {
            get { return this.uri; }
            set {
                if (this.uri != value) {
                    this.uri = value;
                    this.RaisePropertyChanged(() => this.Uri);
                }
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.WebBrowserViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public WebBrowserViewModel() { }

    }

}
