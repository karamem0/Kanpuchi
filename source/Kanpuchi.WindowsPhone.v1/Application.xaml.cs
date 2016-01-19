using Karamem0.Kanpuchi.Infrastructures;
using Karamem0.Kanpuchi.Services;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Navigation;

namespace Karamem0.Kanpuchi {

    /// <summary>
    /// Phone アプリケーションを表します。
    /// </summary>
    public partial class Application : System.Windows.Application {

        /// <summary>
        /// アプリケーションのルート フレームを取得します。
        /// </summary>
        public PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Application"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public Application() {
            this.UnhandledException += this.OnApplicationUnhandledException;
            this.InitializeComponent();
            this.InitializePhoneApplication();
            if (Debugger.IsAttached == true) {
                //Application.Current.Host.Settings.EnableFrameRateCounter = true;
                //Application.Current.Host.Settings.EnableRedrawRegions = true;
                //Application.Current.Host.Settings.EnableCacheVisualization = true;
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }
        }

        /// <summary>
        /// Phone アプリケーションを初期化します。
        /// </summary>
        private void InitializePhoneApplication() {
            this.RootFrame = new TransitionFrame();
            this.RootFrame.Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.Name);
            this.RootFrame.Navigated += this.OnRootFrameNavigated;
            this.RootFrame.NavigationFailed += this.OnRootFrameNavigationFailed;
        }

        /// <summary>
        /// <see cref="System.Windows.Application.UnhandledException"/> イベントで追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="System.Windows.ApplicationUnhandledExceptionEventArgs"/>。
        /// </param>
        private void OnApplicationUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e) {
            if (Debugger.IsAttached == true) {
                Debugger.Break();
            }
        }

        /// <summary>
        /// <see cref="System.Windows.Controls.Frame.Navigated"/> イベントで追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="System.Windows.Navigation.NavigationEventArgs"/>。
        /// </param>
        private void OnRootFrameNavigated(object sender, NavigationEventArgs e) {
            if (this.RootVisual != this.RootFrame) {
                this.RootVisual = this.RootFrame;
            }
            this.RootFrame.Navigated -= this.OnRootFrameNavigated;
        }

        /// <summary>
        /// <see cref="System.Windows.Controls.Frame.NavigationFailed"/> イベントで追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="System.Windows.Navigation.NavigationFailedEventArgs"/>。
        /// </param>
        private void OnRootFrameNavigationFailed(object sender, NavigationFailedEventArgs e) {
            if (Debugger.IsAttached == true) {
                Debugger.Break();
            }
        }

    }

}
