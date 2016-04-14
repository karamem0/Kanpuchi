using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;

namespace Karamem0.Kanpuchi.Infrastructure {

    /// <summary>
    /// ビュー モデルを表します。
    /// </summary>
    public abstract class ViewModel : NotifyPropertyChanged {

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.ViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ViewModel() { }

        /// <summary>
        /// ビュー モデルがロードされると呼び出されます。
        /// </summary>
        public virtual void OnLoaded() { }

        /// <summary>
        /// ビュー モデルがアンロードされると呼び出されます。
        /// </summary>
        public virtual void OnUnloaded() { }

        /// <summary>
        /// 指定した型で表されるページに移動します。
        /// </summary>
        /// <param name="pageType">ナビゲーションするページを示す <see cref="System.Type"/>。</param>
        /// <param name="parameter">パラメーターを示す <see cref="System.Object"/>。</param>
        protected void GoToPage(Type pageType, object parameter = null) {
            if (pageType != null) {
                var contentFrame = ((App)Application.Current).ContentFrame;
                if (contentFrame != null) {
                    contentFrame.Navigate(pageType, parameter);
                }
            }
        }

    }

}
