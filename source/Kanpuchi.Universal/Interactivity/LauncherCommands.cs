using Karamem0.Kanpuchi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace Karamem0.Kanpuchi.Interactivity {

    /// <summary>
    /// ランチャーに関連するコマンドを定義します。
    /// </summary>
    public sealed class LauncherCommands {

        /// <summary>
        /// URI を指定して Web ブラウザーを起動するコマンドを取得します。
        /// </summary>
        public DelegateCommand<string> LaunchBrowserCommand { get; private set; }

        /// <summary>
        /// URI を指定して Web ブラウザーを起動します。
        /// </summary>
        /// <param name="parameter">URI を示す <see cref="System.String"/>。</param>
        private async void LaunchBrowser(string parameter) {
            await Launcher.LaunchUriAsync(new Uri(parameter));
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.ViewModel.LaunchBrowser"/> を実行できるかどうかを判断します。
        /// </summary>
        /// <param name="parameter">URI を示す <see cref="System.String"/>。</param>
        /// <returns>コマンドを実行できるか場合は true。それ以外の場合は false。</returns>
        private bool CanLaunchBrowser(string parameter) {
            if (string.IsNullOrEmpty(parameter) == true) {
                return false;
            }
            return true;
        }

        /// <summary>
        /// アプリのレビューを起動するコマンドを取得します。
        /// </summary>
        public DelegateCommand LaunchReviewAppCommand { get; private set; }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.ViewModel.LaunchReviewApp"/> を実行できるかどうかを判断します。
        /// </summary>
        /// <param name="parameter">URI を示す <see cref="System.String"/>。</param>
        /// <returns>コマンドを実行できるか場合は true。それ以外の場合は false。</returns>
        private bool CanLaunchReviewApp() {
            return true;
        }

        /// <summary>
        /// アプリのレビューを起動します。
        /// </summary>
        private async void LaunchReviewApp() {
            try {
                await Launcher.LaunchUriAsync(new Uri("ms-windows-store:Review?ProductId=9wzdncrdcr55"));
            } catch {
                Messanger.Current.Send("Error", "LaunchAppError");
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.LauncherCommands"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public LauncherCommands() {
            this.LaunchBrowserCommand = new DelegateCommand<string>(this.LaunchBrowser, this.CanLaunchBrowser);
            this.LaunchReviewAppCommand = new DelegateCommand(this.LaunchReviewApp, this.CanLaunchReviewApp);
        }

    }

}
