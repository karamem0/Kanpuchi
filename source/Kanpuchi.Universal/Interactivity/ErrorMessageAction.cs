using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace Karamem0.Kanpuchi.Interactivity {

    /// <summary>
    /// エラー メッセージ ダイアログを表示するアクションを表します。
    /// </summary>
    public sealed class ErrorMessageAction : DependencyObject, IAction {

        /// <summary>
        /// アクションを実行します。
        /// </summary>
        /// <param name="sender">ビヘイビアーからパラメーターに渡される <see cref="System.Object"/>。</param>
        /// <param name="parameter">アクションのパラメーターを示す <see cref="System.Object"/>。</param>
        /// <returns>実行結果を示す <see cref="System.Object"/>。</returns>
        public object Execute(object sender, object parameter) {
            return this.ExecuteCore(parameter.ToString());
        }

        /// <summary>
        /// アクションを実行します。
        /// </summary>
        /// <param name="resourceKey">メッセージ リソースのキー文字列を示す <see cref="System.String"/>。</param>
        /// <returns>実行結果を示す <see cref="Windows.UI.Popups.IUICommand"/>。</returns>
        private async Task<IUICommand> ExecuteCore(string resourceKey) {
            var resourceLoader = ResourceLoader.GetForCurrentView("Strings");
            var title = resourceLoader.GetString("ErrorTitle");
            var message = resourceLoader.GetString(resourceKey);
            var dialog = new MessageDialog(message, title);
            return await dialog.ShowAsync();
        }

    }

}
