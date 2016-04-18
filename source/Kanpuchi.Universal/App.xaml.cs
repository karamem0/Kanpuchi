using Karamem0.Kanpuchi.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Karamem0.Kanpuchi {

    /// <summary>
    /// Windows Phone アプリケーションを表します。
    /// </summary>
    public sealed partial class App : Application {

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.App"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public App() {
            this.InitializeComponent();
        }

        /// <summary>
        /// アプリケーションがアクティブになるとき呼び出されます。
        /// </summary>
        /// <param name="e">イベントのデータを格納する <see cref="Windows.ApplicationModel.Activation.LaunchActivatedEventArgs"/>。</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e) {
            var rootFrame = new Frame();
            rootFrame.Navigate(typeof(MainPage), e.Arguments);
            Window.Current.Content = rootFrame;
            Window.Current.Activate();
        }

    }

}
