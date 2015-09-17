using Kanpuchi.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Kanpuchi {

    /// <summary>
    /// Windows Phone アプリケーションを表します。
    /// </summary>
    public sealed partial class App : Application {

        /// <summary>
        /// アプリケーションのルート フレームを取得します。
        /// </summary>
        public Frame RootFrame { get; private set; }

        /// <summary>
        /// <see cref="Kanpuchi.App"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public App() {
            this.InitializeComponent();
        }

        /// <summary>
        /// アプリケーションがアクティブになるとき呼び出されます。
        /// </summary>
        /// <param name="e">イベントのデータを格納する <see cref="Windows.ApplicationModel.Activation.LaunchActivatedEventArgs"/>。</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e) {
            if (this.RootFrame == null) {
                this.RootFrame = new Frame();
            }
            this.RootFrame.Navigate(typeof(MainPage), e.Arguments);
            Window.Current.Content = this.RootFrame;
            Window.Current.Activate();
        }

    }

}