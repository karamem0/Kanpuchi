using Karamem0.Kanpuchi.Infrastructure;
using Karamem0.Kanpuchi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Karamem0.Kanpuchi.Interactivity {
    
    /// <summary>
    /// ビュー モデルを格納します。
    /// </summary>
    public class ViewModelLocator : DependencyObject {

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.ViewModelLocator.MainViewModel"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty MainViewModelProperty =
            DependencyProperty.Register(
                "MainViewModel",
                typeof(ViewModel),
                typeof(ViewModelLocator),
                new PropertyMetadata(new MainViewModel()));

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.MainViewModel"/> を取得または設定します。
        /// </summary>
        public ViewModel MainViewModel {
            get { return (ViewModel)this.GetValue(MainViewModelProperty); }
            set { this.SetValue(MainViewModelProperty, value); }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.ViewModelLocator.SettingsViewModel"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty SettingsViewModelProperty =
            DependencyProperty.Register(
                "SettingsViewModel",
                typeof(ViewModel),
                typeof(ViewModelLocator),
                new PropertyMetadata(new SettingsViewModel()));

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.SettingsViewModel"/> を取得または設定します。
        /// </summary>
        public ViewModel SettingsViewModel {
            get { return (ViewModel)this.GetValue(SettingsViewModelProperty); }
            set { this.SetValue(SettingsViewModelProperty, value); }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.ViewModelLocator.AboutViewModel"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty AboutViewModelProperty =
            DependencyProperty.Register(
                "AboutViewModel",
                typeof(ViewModel),
                typeof(ViewModelLocator),
                new PropertyMetadata(new AboutViewModel()));

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.AboutViewModel"/> を取得または設定します。
        /// </summary>
        public ViewModel AboutViewModel {
            get { return (ViewModel)this.GetValue(AboutViewModelProperty); }
            set { this.SetValue(AboutViewModelProperty, value); }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.ViewModelLocator.WebBrowserViewModel"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty WebBrowserViewModelProperty =
            DependencyProperty.Register(
                "WebBrowserViewModel",
                typeof(ViewModel),
                typeof(ViewModelLocator),
                new PropertyMetadata(new WebBrowserViewModel()));

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.WebBrowserViewModel"/> を取得または設定します。
        /// </summary>
        public ViewModel WebBrowserViewModel {
            get { return (ViewModel)this.GetValue(WebBrowserViewModelProperty); }
            set { this.SetValue(WebBrowserViewModelProperty, value); }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.ViewModelLocator"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ViewModelLocator() { }

    }

}
