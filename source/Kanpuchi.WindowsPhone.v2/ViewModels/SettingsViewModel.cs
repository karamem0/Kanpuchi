using Kanpuchi.Configuration;
using Kanpuchi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanpuchi.ViewModels {

    /// <summary>
    /// 設定ページのビュー モデルを表します。
    /// </summary>
    public sealed class SettingsViewModel : ViewModel {

        /// <summary>
        /// アプリ内ブラウザーを使用するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool UseInAppBrowser {
            get { return AppSettings.Current.UseInAppBrowser; }
            set { AppSettings.Current.UseInAppBrowser = value; }
        }

        /// <summary>
        /// <see cref="Kanpuchi.ViewModels.SettingsViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public SettingsViewModel() { }

        /// <summary>
        /// ビュー モデルがロードされると呼び出されます。
        /// </summary>
        public override void OnLoaded() {
            base.OnLoaded();
            AppSettings.Current.Reload();
        }

        /// <summary>
        /// ビュー モデルがアンロードされると呼び出されます。
        /// </summary>
        public override void OnUnloaded() {
            base.OnUnloaded();
            AppSettings.Current.Save();
        }

    }

}
