using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kanpuchi.Interactivity {

    /// <summary>
    /// ナビゲーションに関するコマンドを定義します。
    /// </summary>
    public class NavigationCommands {

        /// <summary>
        /// 指定した URI のビューに移動するコマンドを表します。
        /// </summary>
        public DelegateCommand<string> GoToPageCommand { get; private set; }

        /// <summary>
        /// 戻るナビゲーションの履歴の最新エントリーに移動するコマンドを表します。
        /// </summary>
        public DelegateCommand GoBackCommand { get; private set; }

        /// <summary>
        /// <see cref="Kanpuchi.Interactivity.NavigationCommands"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public NavigationCommands() {
            this.GoToPageCommand = new DelegateCommand<string>(
                 uri => ApplicationContext.Current.RootFrame.Navigate(new Uri(uri, UriKind.Relative)),
                 uri => string.IsNullOrEmpty(uri) != true);
            this.GoBackCommand = new DelegateCommand(
                () => ApplicationContext.Current.RootFrame.GoBack(),
                () => ApplicationContext.Current.RootFrame.BackStack.Any());

        }

    }

}
