using Kanpuchi.Infrastructures;
using Kanpuchi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Kanpuchi.Interactivity {

    /// <summary>
    /// ビュー モデルを格納します。
    /// </summary>
    public class ViewModelLocator : DependencyObject {

        /// <summary>
        /// <see cref="Kanpuchi.Interactivity.ViewModelLocator.SplashViewModel"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty SplashViewModelProperty =
            DependencyProperty.Register(
                "SplashViewModel",
                typeof(ViewModel),
                typeof(ViewModelLocator),
                new PropertyMetadata(new SplashViewModel()));

        /// <summary>
        /// <see cref="Kanpuchi.ViewModels.SplashViewModel"/> を取得または設定します。
        /// </summary>
        public ViewModel SplashViewModel {
            get { return (ViewModel)this.GetValue(SplashViewModelProperty); }
            set { this.SetValue(SplashViewModelProperty, value); }
        }

        /// <summary>
        /// <see cref="Kanpuchi.Interactivity.ViewModelLocator.TimelineViewModel"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty TimelineViewModelProperty =
            DependencyProperty.Register(
                "TimelineViewModel",
                typeof(ViewModel),
                typeof(ViewModelLocator),
                new PropertyMetadata(new TimelineViewModel()));

        /// <summary>
        /// <see cref="Kanpuchi.ViewModels.TimelineViewModel"/> を取得または設定します。
        /// </summary>
        public ViewModel TimelineViewModel {
            get { return (ViewModel)this.GetValue(TimelineViewModelProperty); }
            set { this.SetValue(TimelineViewModelProperty, value); }
        }

        /// <summary>
        /// <see cref="Kanpuchi.Interactivity.ViewModelLocator.MatomeViewModel"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty MatomeViewModelProperty =
            DependencyProperty.Register(
                "MatomeViewModel",
                typeof(ViewModel),
                typeof(ViewModelLocator),
                new PropertyMetadata(new MatomeViewModel()));

        /// <summary>
        /// <see cref="Kanpuchi.ViewModels.MatomeViewModel"/> を取得または設定します。
        /// </summary>
        public ViewModel MatomeViewModel {
            get { return (ViewModel)this.GetValue(MatomeViewModelProperty); }
            set { this.SetValue(MatomeViewModelProperty, value); }
        }

        /// <summary>
        /// <see cref="Kanpuchi.Interactivity.ViewModelLocator.WebBrowserViewModel"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty WebBrowserViewModelProperty =
            DependencyProperty.Register(
                "WebBrowserViewModel",
                typeof(ViewModel),
                typeof(ViewModelLocator),
                new PropertyMetadata(new WebBrowserViewModel()));

        /// <summary>
        /// <see cref="Kanpuchi.ViewModels.WebBrowserViewModel"/> を取得または設定します。
        /// </summary>
        public ViewModel WebBrowserViewModel {
            get { return (ViewModel)this.GetValue(WebBrowserViewModelProperty); }
            set { this.SetValue(WebBrowserViewModelProperty, value); }
        }

        /// <summary>
        /// <see cref="Kanpuchi.Interactivity.ViewModelLocator.SettingsViewModel"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty SettingsViewModelProperty =
            DependencyProperty.Register(
                "SettingsViewModel",
                typeof(ViewModel),
                typeof(ViewModelLocator),
                new PropertyMetadata(new SettingsViewModel()));

        /// <summary>
        /// <see cref="Kanpuchi.ViewModels.SettingsViewModel"/> を取得または設定します。
        /// </summary>
        public ViewModel SettingsViewModel {
            get { return (ViewModel)this.GetValue(SettingsViewModelProperty); }
            set { this.SetValue(SettingsViewModelProperty, value); }
        }

        /// <summary>
        /// <see cref="Kanpuchi.Interactivity.ViewModelLocator.AboutViewModel"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty AboutViewModelProperty =
            DependencyProperty.Register(
                "AboutViewModel",
                typeof(ViewModel),
                typeof(ViewModelLocator),
                new PropertyMetadata(new AboutViewModel()));

        /// <summary>
        /// <see cref="Kanpuchi.ViewModels.AboutViewModel"/> を取得または設定します。
        /// </summary>
        public ViewModel AboutViewModel {
            get { return (ViewModel)this.GetValue(AboutViewModelProperty); }
            set { this.SetValue(AboutViewModelProperty, value); }
        }

        /// <summary>
        /// <see cref="Kanpuchi.Interactivity.ViewModelLocator"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ViewModelLocator() { }

    }

}
