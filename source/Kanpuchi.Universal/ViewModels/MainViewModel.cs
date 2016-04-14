using Karamem0.Kanpuchi.Infrastructure;
using Karamem0.Kanpuchi.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.ViewModels {

    /// <summary>
    /// メイン ページのビュー モデルを表します。
    /// </summary>
    public sealed class MainViewModel : ViewModel {

        /// <summary>
        /// コンテンツ領域に表示されるページを表します。
        /// </summary>
        private Type displayPage;

        /// <summary>
        /// コンテンツ領域に表示されるページを取得または設定します。
        /// </summary>
        public Type DisplayType {
            get { return this.displayPage; }
            set {
                if (this.displayPage != value) {
                    this.displayPage = value;
                    this.RaisePropertyChanged(() => this.DisplayType);
                    this.GoToPage(value);
                }
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.MainViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MainViewModel() { }

        /// <summary>
        /// ビュー モデルがロードされると呼び出されます。
        /// </summary>
        public override void OnLoaded() {
            this.DisplayType = typeof(HomePage);
        }

    }

}
