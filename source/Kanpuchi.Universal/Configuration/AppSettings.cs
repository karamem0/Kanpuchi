using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.Configuration {

    /// <summary>
    /// アプリケーションの設定情報を表します。
    /// </summary>
    public class AppSettings : AppSettingsBase {

        /// <summary>
        /// アプリケーションの <see cref="Karamem0.Kanpuchi.Configuration.AppSettings"/>
        /// クラスのインスタンスを取得します。
        /// </summary>
        public static AppSettings Current { get; private set; }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Configuration.AppSettings"/> クラスの静的メンバーを初期化します。
        /// </summary>
        static AppSettings() {
            AppSettings.Current = new AppSettings();
        }

        /// <summary>
        /// デバイス ID を取得または設定します。
        /// </summary>
        public Guid? DeviceId {
            get { return (Guid?)this[nameof(this.DeviceId)]; }
            set { this[nameof(this.DeviceId)] = value; }
        }

        /// <summary>
        /// デバイス キーを取得または設定します。
        /// </summary>
        public string DeviceKey {
            get { return (string)this[nameof(this.DeviceKey)]; }
            set { this[nameof(this.DeviceKey)] = value; }
        }

        /// <summary>
        /// 有効なサイト ID の配列を取得または設定します。
        /// </summary>
        public int[] EnableSiteIds {
            get { return (int[])this[nameof(this.EnableSiteIds)]; }
            set { this[nameof(this.EnableSiteIds)] = value; }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Configuration.AppSettings"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        private AppSettings() { }

    }

}
