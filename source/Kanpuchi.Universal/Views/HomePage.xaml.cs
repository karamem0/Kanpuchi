using Karamem0.Kanpuchi.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Karamem0.Kanpuchi.Views {

    public sealed partial class HomePage : Page {

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Views.HomePage"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public HomePage() {
            this.InitializeComponent();
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Views.HomePage.ScrollToTopButton"/> をクリックしたときに追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.EventArgs"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="Windows.UI.Xaml.RoutedEventArgs"/>。</param>
        private void OnScrollToTopButtonClick(object sender, RoutedEventArgs e) {
            var pivotItem = this.Pivot.SelectedItem as PivotItem;
            var listBox = pivotItem.FindVisualChildren<ListBox>().FirstOrDefault();
            var scrollViewer = listBox.FindVisualChildren<ScrollViewer>().FirstOrDefault();
            var source = scrollViewer.VerticalOffset;
            var destination = 0.0;
            for (int index = 1; index <= 10; index++) {
                var offset = source + ((destination - source) * index * 0.1);
                scrollViewer.ChangeView(null, offset, null);
                Task.Delay(10);
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Views.HomePage.ScrollToBottomButton"/> をクリックしたときに追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.EventArgs"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="Windows.UI.Xaml.RoutedEventArgs"/>。</param>
        private void OnScrollToBottomButtonClick(object sender, RoutedEventArgs e) {
            var pivotItem = this.Pivot.SelectedItem as PivotItem;
            var listBox = pivotItem.FindVisualChildren<ListBox>().FirstOrDefault();
            var scrollViewer = listBox.FindVisualChildren<ScrollViewer>().FirstOrDefault();
            var source = scrollViewer.VerticalOffset;
            var destination = scrollViewer.ScrollableHeight;
            for (int index = 1; index <= 10; index++) {
                var offset = source + ((destination - source) * index * 0.1);
                scrollViewer.ChangeView(null, offset, null);
                Task.Delay(10);
            }
        }

    }

}
