using Kanpuchi.Extensions;
using Microsoft.Phone.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace Kanpuchi.Interactivity {

    /// <summary>
    /// リスト ボックスを最下部にスクロールするアクションを表します。
    /// </summary>
    public class ScrollToBottomAction : TriggerAction<ListBox> {

        /// <summary>
        /// <see cref="Kanpuchi.Interactivity.ScrollToBottomAction"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ScrollToBottomAction() { }

        /// <summary>
        /// アクションが起動されると呼び出されます。
        /// </summary>
        /// <param name="parameter">パラメーターを示す <see cref="System.Object"/>。</param>
        protected override void Invoke(object parameter) {
            if (this.AssociatedObject != null) {
                for (int index = 0; index < VisualTreeHelper.GetChildrenCount(this.AssociatedObject); index++) {
                    var scrollViewer = VisualTreeHelper.GetChild(this.AssociatedObject, index) as ScrollViewer;
                    if (scrollViewer != null) {
                        var src = scrollViewer.VerticalOffset;
                        var dst = scrollViewer.ScrollableHeight;
                        var disposable = default(IDisposable);
                        disposable = Observable.Timer(TimeSpan.Zero, TimeSpan.FromMilliseconds(10))
                            .Subscribe(i => {
                                if (i <= 10) {
                                    var dispatcher = Deployment.Current.Dispatcher;
                                    var offset = src + ((dst - src) * i * 0.1);
                                    dispatcher.BeginInvoke(x => scrollViewer.ScrollToVerticalOffset(x), offset);
                                } else {
                                    if (disposable != null) {
                                        disposable.Dispose();
                                    }
                                }
                            });
                    }
                }
            }
        }

    }

}
