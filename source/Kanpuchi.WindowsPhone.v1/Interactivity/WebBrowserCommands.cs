using Microsoft.Phone.Controls;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Kanpuchi.Interactivity {

    /// <summary>
    /// ウェブ ブラウザーに関するコマンドを定義します。
    /// </summary>
    public class WebBrowserCommands : DependencyObject {

        /// <summary>
        /// ウェブ ブラウザー コンポーネントを取得または設定します。
        /// </summary>
        public WebBrowser WebBrowser {
            get { return (WebBrowser)GetValue(WebBrowserProperty); }
            set { SetValue(WebBrowserProperty, value); }
        }

        /// <summary>
        /// <see cref="Kanpuchi.Interactivity.WebBrowserCommands.WebBrowser"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty WebBrowserProperty =
            DependencyProperty.Register(
                "WebBrowser",
                typeof(WebBrowser),
                typeof(WebBrowserCommands),
                new PropertyMetadata((d, de) => {
                    var webBrowser = de.NewValue as WebBrowser;
                    if (webBrowser != null) {
                        webBrowser.IsScriptEnabled = true;
                    }
                }));

        /// <summary>
        /// 前に戻るコマンドを取得します。
        /// </summary>
        public DelegateCommand GoBackCommand { get; private set; }

        /// <summary>
        /// 次に進むコマンドを取得します。
        /// </summary>
        public DelegateCommand GoForwardCommand { get; private set; }

        /// <summary>
        /// 現在のページを最新の状態に更新するコマンドを取得します。
        /// </summary>
        public DelegateCommand RefreshCommand { get; private set; }

        /// <summary>
        /// <see cref="Kanpuchi.Interactivity.WebBrowserCommands"/>
        /// クラスの新しいインスタンスを初期化します。
        /// </summary>
        public WebBrowserCommands() {
            this.GoBackCommand = new DelegateCommand(() => this.GoBack());
            this.GoForwardCommand = new DelegateCommand(() => this.GoForward());
            this.RefreshCommand = new DelegateCommand(() => this.Refresh());
        }

        /// <summary>
        /// 前のページに戻ります。
        /// </summary>
        private void GoBack() {
            if (this.WebBrowser != null) {
                this.WebBrowser.InvokeScript("eval", "window.history.back()");
            }
        }

        /// <summary>
        /// 次のページに進みます。
        /// </summary>
        private void GoForward() {
            if (this.WebBrowser != null) {
                this.WebBrowser.InvokeScript("eval", "window.history.forward()");
            }
        }

        /// <summary>
        /// 現在のページを最新の状態に更新します。
        /// </summary>
        private void Refresh() {
            if (this.WebBrowser != null) {
                this.WebBrowser.InvokeScript("eval", "window.location.reload()");
            }
        }

    }

}
