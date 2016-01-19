using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Karamem0.Kanpuchi.Models {

    /// <summary>
    /// Twitter のステータスのデータを格納します。
    /// </summary>
    [Table("TwitterStatus")]
    public class TwitterStatus {

        /// <summary>
        /// ステータス ID を取得または設定します。
        /// </summary>
        [Key()]
        [StringLength(40)]
        public virtual string StatusId { get; set; }

        /// <summary>
        /// ユーザー ID を取得または設定します。
        /// </summary>
        [StringLength(40)]
        public virtual string UserId { get; set; }

        /// <summary>
        /// テキストを取得または設定します。
        /// </summary>
        [StringLength(200)]
        public virtual string Text { get; set; }

        /// <summary>
        /// 作成日時を取得または設定します。
        /// </summary>
        public virtual DateTime CreatedAt { get; set; }

        /// <summary>
        /// 更新日時を取得または設定します。
        /// </summary>
        public virtual DateTime UpdatedAt { get; set; }

        /// <summary>
        /// 現在のインスタンスに関連付けられたユーザーを取得または設定します。
        /// </summary>
        [ForeignKey("UserId")]
        public virtual TwitterUser User { get; set; }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Models.TwitterStatus"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TwitterStatus() { }

    }

}
