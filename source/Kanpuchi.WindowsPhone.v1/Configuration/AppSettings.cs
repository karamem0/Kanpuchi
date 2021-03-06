﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Karamem0.Kanpuchi.Configuration {

    /// <summary>
    /// アプリケーションの設定情報を表します。
    /// </summary>
    public class AppSettings : AppSettingsBase {

        /// <summary>
        /// アプリケーションの <see cref="Karamem0.Kanpuchi.Configuration.AppSettings"/>
        /// クラスのインスタンスを取得します。
        /// </summary>
        public static readonly AppSettings Current = new AppSettings();

        /// <summary>
        /// デバイス ID を取得または設定します。
        /// </summary>
        public Guid? DeviceId {
            get { return (Guid?)this["DeviceId"]; }
            set { this["DeviceId"] = value; }
        }

        /// <summary>
        /// デバイス キーを取得または設定します。
        /// </summary>
        public string DeviceKey {
            get { return (string)this["DeviceKey"]; }
            set { this["DeviceKey"] = value; }
        }

        /// <summary>
        /// アプリ内ブラウザーを使用するかどうかを示す値を取得または設定します。
        /// </summary>
        [DefaultValue(false)]
        public bool UseAppInBrowser {
            get { return (bool)this["UseAppInBrowser"]; }
            set { this["UseAppInBrowser"] = value; }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Configuration.AppSettings"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        private AppSettings() { }

    }

}
