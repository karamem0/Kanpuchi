using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Karamem0.Kanpuchi.Views {

    public sealed partial class MainPage : Page {

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Views.MainPage"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MainPage() {
            this.InitializeComponent();
        }
        private void OnLoaded(object sender, RoutedEventArgs e) {
            this.HomeButton.IsChecked = true;
        }

        /// <summary>
        /// <see cref="Windows.UI.Xaml.Controls.RadioButton.Checked"/> イベントで追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="Windows.UI.Xaml.RoutedEventArgs"/>。</param>
        private void OnRadioButtonChecked(object sender, RoutedEventArgs e) {
            var radioButton = sender as RadioButton;
            if (radioButton != null) {
                if (radioButton.IsChecked == true) {
                    if (radioButton == this.HomeButton) {
                        this.ContentFrame.Navigate(typeof(HomePage));
                    }
                    if (radioButton == this.SettingsButton) {
                        this.ContentFrame.Navigate(typeof(SettingsPage));
                    }
                }
                this.ContentTitleTextBlock.Text = radioButton.Content.ToString();
            }
        }

    }

}
