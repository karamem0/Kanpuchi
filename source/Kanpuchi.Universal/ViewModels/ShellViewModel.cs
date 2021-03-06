﻿using Karamem0.Kanpuchi.Infrastructure;
using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.System;

namespace Karamem0.Kanpuchi.ViewModels {

    public sealed class ShellViewModel : ViewModelBase {

        /// <summary>
        /// ナビゲーションを管理するサービスを表します。
        /// </summary>
        private INavigationService navigationService;

        /// <summary>
        /// 指定したページに移動するコマンドを表します。
        /// </summary>
        public DelegateCommand<string> NavigateCommand { get; private set; }

        /// <summary>
        /// 指定したページに移動します。
        /// </summary>
        private void Navigate(string parameter) {
            this.IsPaneOpen = false;
            var resourceLoader = ResourceLoader.GetForCurrentView("Strings");
            var title = resourceLoader.GetString(parameter);
            this.ContentTitle = title;
            this.navigationService.Navigate(parameter, null);
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.ShellViewModel.Navigate"/> を実行できるかどうかを判断します。
        /// </summary>
        /// <returns>コマンドを実行できるか場合は true。それ以外の場合は false。</returns>
        private bool CanNavigate(string parameter) {
            if (string.IsNullOrEmpty(parameter) == true) {
                return false;
            }
            return true;
        }

        /// <summary>
        /// アプリのレビューを起動するコマンドを取得します。
        /// </summary>
        public DelegateCommand LaunchReviewAppCommand { get; private set; }

        /// <summary>
        /// アプリのレビューを起動します。
        /// </summary>
        private async void LaunchReviewApp() {
            this.IsPaneOpen = false;
            try {
                await Launcher.LaunchUriAsync(new Uri("ms-windows-store:Review?ProductId=9wzdncrdcr55"));
            } catch {
                Messanger.Current.Send("Error", "LaunchAppError");
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.LauncherCommands.LaunchBrowser"/> を実行できるかどうかを判断します。
        /// </summary>
        /// <returns>コマンドを実行できるか場合は true。それ以外の場合は false。</returns>
        private bool CanLaunchReviewApp() {
            return true;
        }

        /// <summary>
        /// メニューを開いているかどうかを示す値を表します。
        /// </summary>
        private bool isPaneOpen;

        /// <summary>
        /// メニューを開いているかどうかを示す値を取得または設定します。
        /// </summary>
        public bool IsPaneOpen {
            get { return this.isPaneOpen; }
            set {
                if (this.isPaneOpen != value) {
                    this.isPaneOpen = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// コンテンツのタイトルを表します。
        /// </summary>
        private string contentTitle;

        /// <summary>
        /// コンテンツのタイトル取得または設定します。
        /// </summary>
        public string ContentTitle {
            get { return this.contentTitle; }
            set {
                if (this.contentTitle != value) {
                    this.contentTitle = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.ShellViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        private ShellViewModel() {
            this.NavigateCommand = new DelegateCommand<string>(this.Navigate, this.CanNavigate);
            this.LaunchReviewAppCommand = new DelegateCommand(this.LaunchReviewApp, this.CanLaunchReviewApp);
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.ShellViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="navigationService">
        /// ナビゲーションを管理する <see cref="Prism.Windows.Navigation.INavigationService"/>。
        /// </param>
        public ShellViewModel(INavigationService navigationService) : this() {
            this.navigationService = navigationService;
            var resourceLoader = ResourceLoader.GetForCurrentView("Strings");
            var title = resourceLoader.GetString("Home");
            this.ContentTitle = title;
        }

    }

}
