using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Karamem0.Kanpuchi.Models {

    /// <summary>
    /// Twitter のユーザーのデータを格納します。
    /// </summary>
    [Table("TwitterUser")]
    public class TwitterUser {

        /// <summary>
        /// ユーザー ID を取得または設定します。
        /// </summary>
        [Key()]
        [StringLength(40)]
        public virtual string UserId { get; set; }

        /// <summary>
        /// ユーザー名を取得または設定します。
        /// </summary>
        [StringLength(20)]
        public virtual string UserName { get; set; }

        /// <summary>
        /// 表示名を取得または設定します。
        /// </summary>
        [StringLength(40)]
        public virtual string ScreenName { get; set; }

        /// <summary>
        /// プロフィール画像の URL を取得または設定します。
        /// </summary>
        [StringLength(1000)]
        public virtual string ProfileImageUrl { get; set; }

        /// <summary>
        /// 作成日時を取得または設定します。
        /// </summary>
        public virtual DateTime CreatedAt { get; set; }

        /// <summary>
        /// 更新日時を取得または設定します。
        /// </summary>
        public virtual DateTime UpdatedAt { get; set; }

        /// <summary>
        /// 現在のインスタンスに関連付けられたステータスのコレクションを取得または設定します。
        /// </summary>
        public virtual ICollection<TwitterStatus> Statuses { get; set; }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Models.TwitterUser"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TwitterUser() { }

    }

}
