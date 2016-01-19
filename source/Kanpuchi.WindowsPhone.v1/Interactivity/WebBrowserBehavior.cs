using Karamem0.Kanpuchi.ViewModels;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Navigation;

namespace Karamem0.Kanpuchi.Interactivity {

    /// <summary>
    /// ウェブ ブラウザーの URI が変更されたときにビュー モデルに変更を通知するためのビヘイビアーを表します。
    /// </summary>
    public class WebBrowserBehavior : Behavior<WebBrowser> {

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.WebBrowserBehavior.ViewModel"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                "ViewModel",
                typeof(WebBrowserViewModel),
                typeof(WebBrowserBehavior),
                new PropertyMetadata(null));

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.WebBrowserViewModel"/> を取得または設定します。
        /// </summary>
        public WebBrowserViewModel ViewModel {
            get { return (WebBrowserViewModel)this.GetValue(ViewModelProperty); }
            set { this.SetValue(ViewModelProperty, value); }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.WebBrowserBehavior"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public WebBrowserBehavior() { }

        /// <summary>
        /// ビヘイビアーがアタッチされると呼び出されます。
        /// </summary>
        protected override void OnAttached() {
            base.OnAttached();
            this.AssociatedObject.Navigated += this.OnAssociatedObjectNavigated;
        }

        /// <summary>
        /// ビヘイビアーがデタッチされるときに呼び出されます。
        /// </summary>
        protected override void OnDetaching() {
            base.OnDetaching();
            this.AssociatedObject.Navigated -= this.OnAssociatedObjectNavigated;
        }

        /// <summary>
        /// <see cref="Microsoft.Phone.Controls.WebBrowser.Navigated"/> イベントで追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="System.Windows.Navigation.NavigationEventArgs"/>。
        /// </param>
        private void OnAssociatedObjectNavigated(object sender, NavigationEventArgs e) {
            if (this.ViewModel != null) {
                this.ViewModel.Uri = e.Uri.AbsoluteUri;
            }
        }

    }

}
