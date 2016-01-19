using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Karamem0.Kanpuchi.Interactivity {

    /// <summary>
    /// ウェブ ブラウザーで開くときのパラメーターを表します。
    /// </summary>
    public class WebBrowserTaskContext : DependencyObject {

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.WebBrowserTaskContext.Uri"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty UriProperty =
            DependencyProperty.Register(
                "Uri",
                typeof(string),
                typeof(WebBrowserTaskContext),
                new PropertyMetadata(null));

        /// <summary>
        /// URI を取得または設定します。
        /// </summary>
        public string Uri {
            get { return (string)this.GetValue(UriProperty); }
            set { this.SetValue(UriProperty, value); }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.WebBrowserTaskContext.UseAppInBrowser"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty UseAppInBrowserProperty =
            DependencyProperty.Register(
                "UseAppInBrowser",
                typeof(bool?),
                typeof(WebBrowserTaskContext),
                new PropertyMetadata(null));

        /// <summary>
        /// アプリ内ブラウザーを使用するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool? UseAppInBrowser {
            get { return (bool?)this.GetValue(UseAppInBrowserProperty); }
            set { this.SetValue(UseAppInBrowserProperty, value); }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.WebBrowserTaskContext"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public WebBrowserTaskContext() { }

    }

}
