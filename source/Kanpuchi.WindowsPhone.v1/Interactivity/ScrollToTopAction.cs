using Karamem0.Kanpuchi.Extensions;
using Microsoft.Phone.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace Karamem0.Kanpuchi.Interactivity {

    /// <summary>
    /// リスト ボックスを最上部にスクロールするアクションを表します。
    /// </summary>
    public class ScrollToTopAction : TriggerAction<ListBox> {

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.ScrollToTopAction"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ScrollToTopAction() { }

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
                        var dst = 0.0;
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
