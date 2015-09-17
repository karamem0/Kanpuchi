using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Kanpuchi.Interactivity {

    /// <summary>
    /// ソーシャル ネットワークに共有するときのパラメーターを表します。
    /// </summary>
    public class ShareStatusTaskContext : DependencyObject {

        /// <summary>
        /// <see cref="Kanpuchi.Interactivity.ShareStatusTaskContext.Uri"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty UriProperty =
            DependencyProperty.Register(
                "Uri",
                typeof(string),
                typeof(ShareStatusTaskContext),
                new PropertyMetadata(null));

        /// <summary>
        /// URI を取得または設定します。
        /// </summary>
        public string Uri {
            get { return (string)this.GetValue(UriProperty); }
            set { this.SetValue(UriProperty, value); }
        }

        /// <summary>
        /// <see cref="Kanpuchi.Interactivity.ShareStatusTaskContext.Message"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register(
                "Message",
                typeof(string),
                typeof(ShareStatusTaskContext),
                new PropertyMetadata(null));

        /// <summary>
        /// メッセージを取得または設定します。
        /// </summary>
        public string Message {
            get { return (string)this.GetValue(MessageProperty); }
            set { this.SetValue(MessageProperty, value); }
        }

        /// <summary>
        /// <see cref="Kanpuchi.Interactivity.ShareStatusTaskContext"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ShareStatusTaskContext() { }

    }

}
