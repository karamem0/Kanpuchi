using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Kanpuchi.Views {

    /// <summary>
    /// ツイートを表示するピボット項目を表します。
    /// </summary>
    public sealed partial class TweetPivotItem : PivotItem {

        /// <summary>
        /// <see cref="Kanpuchi.Views.TweetPivotItem"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TweetPivotItem() {
            this.InitializeComponent();
        }

    }

}
