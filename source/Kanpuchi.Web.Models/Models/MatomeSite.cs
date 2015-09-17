using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Kanpuchi.Models {

    /// <summary>
    /// まとめサイトのデータを格納します。
    /// </summary>
    [Table("MatomeSite")]
    public class MatomeSite {

        /// <summary>
        /// サイト ID を取得または設定します。
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key()]
        public virtual int SiteId { get; set; }

        /// <summary>
        /// サイト名を取得または設定します。
        /// </summary>
        [StringLength(200)]
        public virtual string SiteName { get; set; }

        /// <summary>
        /// サイトの URL を取得または設定します。
        /// </summary>
        [StringLength(1000)]
        public virtual string SiteUrl { get; set; }

        /// <summary>
        /// フィードの URL を取得または設定します。
        /// </summary>
        [StringLength(1000)]
        public virtual string FeedUrl { get; set; }

        /// <summary>
        /// フィードの形式を取得または設定します。
        /// </summary>
        [StringLength(20)]
        public virtual string FeedFormat { get; set; }

        /// <summary>
        /// 作成日時を取得または設定します。
        /// </summary>
        public virtual DateTime CreatedAt { get; set; }

        /// <summary>
        /// 更新日時を取得または設定します。
        /// </summary>
        public virtual DateTime UpdatedAt { get; set; }

        /// <summary>
        /// 現在のインスタンスに関連付けられたまとめ記事のコレクションを取得または設定します。
        /// </summary>
        public virtual ICollection<MatomeEntry> Entries { get; set; }

        /// <summary>
        /// <see cref="Kanpuchi.Models.MatomeSite"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MatomeSite() { }

    }

}
