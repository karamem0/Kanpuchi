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
    /// ホーム ページのビュー モデルを表します。
    /// </summary>
    public class HomeViewModel : ViewModel {

        /// <summary>
        /// ツイートのコレクションを表します。
        /// </summary>
        private ObservableCollection<Tweet> tweets;

        /// <summary>
        /// ツイートのコレクションを取得します。
        /// </summary>
        public ObservableCollection<Tweet> Tweets {
            get { return this.tweets; }
            private set {
                if (this.tweets != value) {
                    this.tweets = value;
                    this.RaisePropertyChanged(() => this.Tweets);
                }
            }
        }

        /// <summary>
        /// まとめ記事のコレクションを表します。
        /// </summary>
        private ObservableCollection<MatomeEntry> matomeEntries;

        /// <summary>
        /// まとめ記事のコレクションを取得します。
        /// </summary>
        public ObservableCollection<MatomeEntry> MatomeEntries {
            get { return this.matomeEntries; }
            private set {
                if (this.matomeEntries != value) {
                    this.matomeEntries = value;
                    this.RaisePropertyChanged(() => this.MatomeEntries);
                }
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.ViewModels.HomeViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public HomeViewModel() { }

    }

}
