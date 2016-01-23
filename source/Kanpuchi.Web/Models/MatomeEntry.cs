using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Karamem0.Kanpuchi.Models {

    /// <summary>
    /// まとめ記事のデータを格納します。
    /// </summary>
    [Table("MatomeEntry")]
    public class MatomeEntry {

        /// <summary>
        /// 記事 ID を取得または設定します。
        /// </summary>
        [Key()]
        public virtual Guid EntryId { get; set; }

        /// <summary>
        /// サイト ID を取得または設定します。
        /// </summary>
        public virtual int SiteId { get; set; }

        /// <summary>
        /// URL を取得または設定します。
        /// </summary>
        [StringLength(1000)]
        public virtual string Url { get; set; }

        /// <summary>
        /// タイトルを取得または設定します。
        /// </summary>
        [StringLength(200)]
        public virtual string Title { get; set; }

        /// <summary>
        /// 内容を取得または設定します。
        /// </summary>
        public virtual string Content { get; set; }

        /// <summary>
        /// サムネイル画像を更新したかどうかを示す値を取得または設定します。
        /// </summary>
        public virtual bool ThumbnailUpdated { get; set; }

        /// <summary>
        /// サムネイル画像の URL を取得または設定します。
        /// </summary>
        [StringLength(1000)]
        public virtual string ThumbnailUrl { get; set; }

        /// <summary>
        /// 作成日時を取得または設定します。
        /// </summary>
        public virtual DateTime CreatedAt { get; set; }

        /// <summary>
        /// 更新日時を取得または設定します。
        /// </summary>
        public virtual DateTime UpdatedAt { get; set; }

        /// <summary>
        /// 現在のインスタンスに関連付けられたサイトを取得または設定します。
        /// </summary>
        [ForeignKey("SiteId")]
        public virtual MatomeSite Site { get; set; }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Models.MatomeEntry"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MatomeEntry() { }

    }

}
