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
    public sealed class SettingsViewModel : ViewModel {

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
        /// ピボットの選択項目のインデックス番号を表します。
        /// </summary>
        private int selectedIndex;

        /// <summary>
        /// ピボットの選択項目のインデックス番号を取得または設定します。
        /// </summary>
        public int SelectedIndex {
            get { return this.selectedIndex; }
            set {
                if (this.selectedIndex != value) {
                    this.selectedIndex = value;
                    this.RaisePropertyChanged(() => this.SelectedIndex);
                }
            }
        }

        /// <summary>
        /// ビジー状態にあるかどうかを示す値を表します。
        /// </summary>
        private bool isBusy;

        /// <summary>
        /// ビジー状態にあるかどうかを示す値を取得します。
        /// </summary>
        public bool IsBusy {
            get { return this.isBusy; }
            private set {
                if (this.isBusy != value) {
                    this.isBusy = value;
                    this.RaisePropertyChanged(() => this.IsBusy);
                }
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.SettingsViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public SettingsViewModel() { }

    }

}
