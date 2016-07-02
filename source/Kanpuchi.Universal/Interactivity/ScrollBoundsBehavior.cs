using Karamem0.Kanpuchi.Extensions;
using Karamem0.Kanpuchi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Karamem0.Kanpuchi.Interactivity {

    /// <summary>
    /// リスト ボックスのスクロール バウンズをトリガーするビヘイビアーを表します。
    /// </summary>
    public sealed class ScrollBoundsBehavior : Behavior<ListBox> {

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.ScrollBoundsBehavior.ScrollTopCommand"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty ScrollTopCommandProperty =
            DependencyProperty.Register(
                "ScrollTopCommand",
                typeof(ICommand),
                typeof(ScrollBoundsBehavior),
                new PropertyMetadata(null));

        /// <summary>
        /// スクロールがリスト ボックスの上端に移動したときに実行されるコマンドを取得または設定します。
        /// </summary>
        public ICommand ScrollTopCommand {
            get { return (ICommand)this.GetValue(ScrollTopCommandProperty); }
            set { this.SetValue(ScrollTopCommandProperty, value); }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.ScrollBoundsBehavior.ScrollTopOffset"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty ScrollTopOffsetProperty =
            DependencyProperty.Register(
                "ScrollTopOffset",
                typeof(double),
                typeof(ScrollBoundsBehavior),
                new PropertyMetadata(null));

        /// <summary>
        /// スクロールがリスト ボックスの上端に移動したときのオフセットを取得または設定します。
        /// </summary>
        public double ScrollTopOffset {
            get { return (double)this.GetValue(ScrollTopOffsetProperty); }
            set { this.SetValue(ScrollTopOffsetProperty, value); }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.ScrollBoundsBehavior.ScrollBottomCommand"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty ScrollBottomCommandProperty =
            DependencyProperty.Register(
                "ScrollBottomCommand",
                typeof(ICommand),
                typeof(ScrollBoundsBehavior),
                null);

        /// <summary>
        /// スクロールがリスト ボックスの下端に移動したときに実行されるコマンドを表します。
        /// </summary>
        public ICommand ScrollBottomCommand {
            get { return (ICommand)this.GetValue(ScrollBottomCommandProperty); }
            set { this.SetValue(ScrollBottomCommandProperty, value); }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.ScrollBoundsBehavior.ScrollBottomOffset"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty ScrollButtomOffsetProperty =
            DependencyProperty.Register(
                "ScrollBottom",
                typeof(double),
                typeof(ScrollBoundsBehavior),
                new PropertyMetadata(null));

        /// <summary>
        /// スクロールがリスト ボックスの下端に移動したときのオフセットを取得または設定します。
        /// </summary>
        public double ScrollBottomOffset {
            get { return (double)this.GetValue(ScrollButtomOffsetProperty); }
            set { this.SetValue(ScrollButtomOffsetProperty, value); }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.ScrollBoundsBehavior"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ScrollBoundsBehavior() { }

        /// <summary>
        /// ビヘイビアーがアタッチされると呼び出されます。
        /// </summary>
        protected override void OnAttached() {
            this.AssociatedObject.Loaded += this.OnListBoxLoaded;
        }

        /// <summary>
        /// ビヘイビアーがデタッチされると呼び出されます。
        /// </summary>
        protected override void OnDetached() {
            var scrollViewer = this.AssociatedObject.FindVisualChildren<ScrollViewer>().FirstOrDefault();
            if (scrollViewer != null) {
                scrollViewer.LayoutUpdated -= this.OnScrollViewerLayoutUpdated;
            }
        }

        /// <summary>
        /// <see cref="Windows.UI.Xaml.FrameworkElement.Loaded"/> イベントで追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="Windows.UI.Xaml.RoutedEventArgs"/>。
        /// </param>
        private void OnListBoxLoaded(object sender, RoutedEventArgs e) {
            this.AssociatedObject.Loaded -= this.OnListBoxLoaded;
            var scrollViewer = this.AssociatedObject.FindVisualChildren<ScrollViewer>().FirstOrDefault();
            if (scrollViewer != null) {
                scrollViewer.LayoutUpdated += this.OnScrollViewerLayoutUpdated;
            }
        }

        /// <summary>
        /// <see cref="Windows.UI.Xaml.FrameworkElement.LayoutUpdated"/> イベントで追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="Windows.UI.Xaml.RoutedEventArgs"/>。
        /// </param>
        private void OnScrollViewerLayoutUpdated(object sender, object e) {
            var scrollViewer = this.AssociatedObject.FindVisualChildren<ScrollViewer>().FirstOrDefault();
            if (scrollViewer == null) {
                return;
            }
            var grid = scrollViewer.FindName("Content") as Grid;
            if (grid == null) {
                return;
            }
            var transform = grid.TransformToVisual(scrollViewer);
            var point = transform.TransformPoint(new Point(0, 0));
            var topOffset = Math.Round(point.Y);
            if (topOffset > this.ScrollTopOffset) {
                if (this.ScrollTopCommand != null &&
                    this.ScrollTopCommand.CanExecute(topOffset) == true) {
                    this.ScrollTopCommand.Execute(topOffset);
                }
            }
            var bottomOffset = (Math.Round(scrollViewer.ScrollableHeight) + Math.Round(point.Y)) * -1;
            if (bottomOffset > this.ScrollBottomOffset) {
                if (this.ScrollBottomCommand != null &&
                    this.ScrollBottomCommand.CanExecute(bottomOffset) == true) {
                    this.ScrollBottomCommand.Execute(bottomOffset);
                }
            }
        }

    }

}
