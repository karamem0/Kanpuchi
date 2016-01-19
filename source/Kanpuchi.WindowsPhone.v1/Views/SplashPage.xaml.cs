using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Karamem0.Kanpuchi.Views {
  
    /// <summary>
    /// スプラッシュ ページを表します。
    /// </summary>
    public partial class SplashPage : PhoneApplicationPage {

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Views.SplashPage"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public SplashPage() {
            this.InitializeComponent();
        }

        /// <summary>
        /// ページがフレームでアクティブでなくなったときに呼び出されます。
        /// </summary>
        /// <param name="e">イベントのデータを格納する <see cref="System.Windows.Navigation.NavigationEventArgs"/>。</param>
        protected override void OnNavigatedFrom(NavigationEventArgs e) {
            this.NavigationService.RemoveBackEntry();
            base.OnNavigatedFrom(e);
        }
        
    }

}