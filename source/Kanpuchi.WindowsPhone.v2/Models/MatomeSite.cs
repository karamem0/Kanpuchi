using Karamem0.Kanpuchi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.Models {

    /// <summary>
    /// まとめサイトのデータを格納します。
    /// </summary>
    [DataContract(Name = "MatomeSite")]
    public class MatomeSite : Model {

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
        /// 使用するかどうかを示す値を取得または設定します。
        /// </summary>
        public virtual bool Enabled { get; set; }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Models.MatomeSite"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MatomeSite() { }

    }

}
