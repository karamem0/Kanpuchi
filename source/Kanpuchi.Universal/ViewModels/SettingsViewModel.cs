using Karamem0.Kanpuchi.Infrastructure;
using Karamem0.Kanpuchi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.ViewModels {

    /// <summary>
    /// 設定ページのビュー モデルを表します。
    /// </summary>
    public class SettingsViewModel : ViewModel {

        /// <summary>
        /// まとめサイトのコレクションを表します。
        /// </summary>
        private ObservableCollection<MatomeSite> matomeSites;

        /// <summary>
        /// まとめサイトのコレクションを取得します。
        /// </summary>
        public ObservableCollection<MatomeSite> MatomeSites {
            get { return this.matomeSites; }
            private set {
                if (this.matomeSites != value) {
                    this.matomeSites = value;
                    this.RaisePropertyChanged(() => this.MatomeSites);
                }
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.SettingsViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public SettingsViewModel() { }

    }

}
