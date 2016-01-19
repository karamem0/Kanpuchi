using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;

namespace Karamem0.Kanpuchi.Configuration {

    /// <summary>
    /// アプリケーションの設定情報の基本機能を提供します。
    /// </summary>
    public abstract class AppSettingsBase {

        /// <summary>
        /// 内部で使用されるディクショナリーを表します。
        /// </summary>
        private Dictionary<string, object> workSettings;

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Configuration.AppSettingsBase"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        protected AppSettingsBase() {
            this.Reset();
            this.Reload();
        }

        /// <summary>
        /// 指定したキーに一致する値を取得または設定します。
        /// </summary>
        /// <param name="name">キーを示す <see cref="System.String"/>。</param>
        /// <returns>キーに一致する <see cref="System.Object"/>。</returns>
        protected object this[string name] {
            get { return this.workSettings[name]; }
            set { this.workSettings[name] = value; }
        }

        /// <summary>
        /// 設定情報を既定値にリセットします。
        /// </summary>
        public void Reset() {
            this.workSettings = new Dictionary<string, object>();
            foreach (var property in this.GetType().GetProperties()) {
                var attribute = property.GetCustomAttributes(false)
                    .Cast<DefaultValueAttribute>()
                    .FirstOrDefault();
                this.workSettings.Add(property.Name, (attribute != null) ? attribute.Value : null);
            }
        }

        /// <summary>
        /// 設定情報をストレージから読み込みます。
        /// </summary>
        public void Reload() {
            var appSettings = IsolatedStorageSettings.ApplicationSettings;
            foreach (var pair in appSettings) {
                if (this.workSettings.ContainsKey(pair.Key) == true) {
                    this.workSettings[pair.Key] = pair.Value;
                }
            }
        }

        /// <summary>
        /// 設定情報をストレージに保存します。
        /// </summary>
        public void Save() {
            var appSettings = IsolatedStorageSettings.ApplicationSettings;
            appSettings.Clear();
            foreach (var pair in this.workSettings) {
                appSettings.Add(pair.Key, pair.Value);
            }
            appSettings.Save();
        }

    }

}
