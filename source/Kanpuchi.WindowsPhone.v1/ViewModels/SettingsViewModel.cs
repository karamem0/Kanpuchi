using Karamem0.Kanpuchi.Configuration;
using Karamem0.Kanpuchi.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Karamem0.Kanpuchi.ViewModels {

    /// <summary>
    /// 設定ページのビュー モデルを表します。
    /// </summary>
    public class SettingsViewModel : ViewModel {

        /// <summary>
        /// アプリ内ブラウザを使用するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool UseAppInBrowser {
            get { return AppSettings.Current.UseAppInBrowser; }
            set {
                AppSettings.Current.UseAppInBrowser = value;
                AppSettings.Current.Save();
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.SettingsViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public SettingsViewModel() { }

    }

}
