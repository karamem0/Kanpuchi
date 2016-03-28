using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Karamem0.Kanpuchi.Views {

    public sealed partial class MainPage : Page {

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Views.MainPage"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MainPage() {
            this.InitializeComponent();
        }

        private void OnRadioButtonChecked(object sender, RoutedEventArgs e) {
            var radioButton = sender as RadioButton;
            if (radioButton != null) {
                this.ContentTitleTextBlock.Text = radioButton.Content.ToString();
            }
        }

    }

}
