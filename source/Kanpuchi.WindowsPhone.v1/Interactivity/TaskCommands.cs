using Kanpuchi.Configuration;
using Kanpuchi.ViewModels;
using Microsoft.Phone.Tasks;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kanpuchi.Interactivity {

    /// <summary>
    /// タスクに関するコマンドを定義します。
    /// </summary>
    public class TaskCommands {

        /// <summary>
        /// 製品のレビュー ページを表示するコマンドを取得します。
        /// </summary>
        public DelegateCommand MarketplaceReviewTaskCommand { get; private set; }

        /// <summary>
        /// 指定した URI をブラウザーで表示するコマンドを取得します。
        /// </summary>
        public DelegateCommand<WebBrowserTaskContext> WebBrowserTaskCommand { get; private set; }

        /// <summary>
        /// ソーシャル ネットワークに共有するコマンドを取得します。
        /// </summary>
        public DelegateCommand<ShareStatusTaskContext> ShareStatusTaskCommand { get; private set; }

        /// <summary>
        /// <see cref="Kanpuchi.Interactivity.TaskCommands"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TaskCommands() {
            this.MarketplaceReviewTaskCommand = new DelegateCommand(
                () => this.MarketplaceReviewTask());
            this.WebBrowserTaskCommand = new DelegateCommand<WebBrowserTaskContext>(
                context => this.WebBrowserTask(context),
                context => context != null);
            this.ShareStatusTaskCommand = new DelegateCommand<ShareStatusTaskContext>(
                context => this.ShareStatusTask(context),
                context => context != null);
        }

        /// <summary>
        /// 製品のレビュー ページを表示するタスクを起動します。
        /// </summary>
        private void MarketplaceReviewTask() {
            var task = new MarketplaceReviewTask();
            task.Show();
        }

        /// <summary>
        /// 指定した URI をブラウザーで表示するタスクを起動します。
        /// </summary>
        /// <param name="context">コンテキストを示す <see cref="Kanpuchi.Interactivity.WebBrowserTaskContext"/>。</param>
        private void WebBrowserTask(WebBrowserTaskContext context) {
            var useAppInBrowser = context.UseAppInBrowser.GetValueOrDefault(AppSettings.Current.UseAppInBrowser);
            if (useAppInBrowser == true) {
                var locator = (ViewModelLocator)ApplicationContext.Current.Resources["ViewModelLocator"];
                var viewModel = (WebBrowserViewModel)locator.WebBrowserViewModel;
                viewModel.Uri = context.Uri;
                ApplicationContext.Current.RootFrame.Navigate(new Uri("/Views/WebBrowserPage.xaml", UriKind.Relative));
            } else {
                var task = new WebBrowserTask();
                task.Uri = new Uri(context.Uri, UriKind.Absolute);
                task.Show();
            }
        }


        /// <summary>
        /// ソーシャル ネットワークに共有するタスクを起動します。
        /// </summary>
        /// <param name="parameter"><see cref="Kanpuchi.Interactivity.ShareStatusTaskContext"/>。</param>
        private void ShareStatusTask(ShareStatusTaskContext parameter) {
            var task = new ShareLinkTask();
            task.LinkUri = new Uri(parameter.Uri, UriKind.Absolute);
            task.Message = parameter.Message;
            task.Title = parameter.Message;
            task.Show();
        }

    }

}
