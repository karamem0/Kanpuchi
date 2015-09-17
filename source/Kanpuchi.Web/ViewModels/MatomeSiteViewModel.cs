using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Kanpuchi.ViewModels {

    /// <summary>
    /// まとめサイトのデータを格納します。
    /// </summary>
    [DataContract(Name = "MatomeSite")]
    public class MatomeSiteViewModel {

        /// <summary>
        /// サイト ID を取得または設定します。
        /// </summary>
        [DataMember()]
        public virtual int SiteId { get; set; }

        /// <summary>
        /// サイト名を取得または設定します。
        /// </summary>
        [DataMember()]
        public virtual string SiteName { get; set; }

        /// <summary>
        /// サイトの URL を取得または設定します。
        /// </summary>
        [DataMember()]
        public virtual string SiteUrl { get; set; }

        /// <summary>
        /// フィードの URL を取得または設定します。
        /// </summary>
        [DataMember()]
        public virtual string FeedUrl { get; set; }

        /// <summary>
        /// <see cref="Kanpuchi.ViewModels.MatomeSiteViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MatomeSiteViewModel() { }

    }

}
