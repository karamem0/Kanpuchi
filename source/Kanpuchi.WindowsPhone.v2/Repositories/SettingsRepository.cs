using Kanpuchi.Configuration;
using Kanpuchi.Infrastructure;
using Kanpuchi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanpuchi.Repositories {

    /// <summary>
    /// 設定情報を格納するリポジトリを表します。
    /// </summary>
    public sealed class SettingsRepository : Repository<Settings> {

        /// <summary>
        /// <see cref="Kanpuchi.Repositories.SettingsRepository"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public SettingsRepository() { }

        /// <summary>
        /// 保尊されている設定情報を取得します。
        /// </summary>
        /// <returns>非同期操作を示す <see cref="T:System.Threading.Tasks.Task`1"/>。</returns>
        public async Task<Settings> LoadAsync() {
            return await Task.Factory.StartNew(() => {
                AppSettings.Current.Reload();
                return new Settings() {
                    UseInAppBrowser = AppSettings.Current.UseInAppBrowser,
                    EnableSiteIds = AppSettings.Current.EnableSiteIds,
                };
            });
        }

        /// <summary>
        /// 指定した設定情報を保存します。
        /// </summary>
        /// <returns>非同期操作を示す <see cref="T:System.Threading.Tasks.Task"/>。</returns>
        public async Task SaveAsync(Settings value) {
            await Task.Factory.StartNew(() => {
                if (value != null) {
                    AppSettings.Current.UseInAppBrowser = value.UseInAppBrowser;
                    AppSettings.Current.EnableSiteIds = value.EnableSiteIds;
                    AppSettings.Current.Save();
                }
            });
        }

    }

}
