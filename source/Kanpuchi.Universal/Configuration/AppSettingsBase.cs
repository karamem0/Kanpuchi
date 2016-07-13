using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;

namespace Karamem0.Kanpuchi.Configuration {

    /// <summary>
    /// アプリケーションの設定情報の基本機能を提供します。
    /// </summary>
    public abstract class AppSettingsBase {

        private static object syncObject = new object();

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
            set {
                try {
                    Monitor.Enter(syncObject);
                    this.workSettings[name] = value;
                } finally {
                    Monitor.Exit(syncObject);
                }
            }
        }

        /// <summary>
        /// 設定情報を既定値にリセットします。
        /// </summary>
        public void Reset() {
            try {
                Monitor.Enter(syncObject);
                this.workSettings = new Dictionary<string, object>();
                var properties = this.GetType().GetTypeInfo().DeclaredProperties;
                foreach (var property in properties.Where(x => x.GetMethod.IsStatic != true)) {
                    var attribute = property.GetCustomAttributes(false)
                        .Cast<DefaultValueAttribute>()
                        .FirstOrDefault();
                    if (attribute == null) {
                        this.workSettings.Add(property.Name, null);
                    } else {
                        this.workSettings.Add(property.Name, attribute.Value);
                    }
                }
            } finally {
                Monitor.Exit(syncObject);
            }
        }

        /// <summary>
        /// 設定情報をストレージから読み込みます。
        /// </summary>
        public void Reload() {
            try {
                Monitor.Enter(syncObject);
                var appSettings = ApplicationData.Current.LocalSettings.Values;
                foreach (var pair in appSettings) {
                    if (this.workSettings.ContainsKey(pair.Key) == true) {
                        this.workSettings[pair.Key] = pair.Value;
                    }
                }
            } finally {
                Monitor.Exit(syncObject);
            }
        }

        /// <summary>
        /// 設定情報をストレージに保存します。
        /// </summary>
        public void Save() {
            try {
                Monitor.Enter(syncObject);
                var appSettings = ApplicationData.Current.LocalSettings.Values;
                appSettings.Clear();
                foreach (var pair in this.workSettings) {
                    appSettings.Add(pair.Key, pair.Value);
                }
            } finally {
                Monitor.Exit(syncObject);
            }
        }

    }

}
