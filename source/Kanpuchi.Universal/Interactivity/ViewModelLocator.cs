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
        /// <see cref="Karamem0.Kanpuchi.Interactivity.ViewModelLocator.HomeViewModel"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty HomeViewModelProperty =
            DependencyProperty.Register(
                "HomeViewModel",
                typeof(ViewModel),
                typeof(ViewModelLocator),
                new PropertyMetadata(new HomeViewModel()));

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.HomeViewModel"/> を取得または設定します。
        /// </summary>
        public ViewModel HomeViewModel {
            get { return (ViewModel)this.GetValue(HomeViewModelProperty); }
            set { this.SetValue(HomeViewModelProperty, value); }
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
        /// <see cref="Karamem0.Kanpuchi.Interactivity.ViewModelLocator"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ViewModelLocator() { }

    }

}
