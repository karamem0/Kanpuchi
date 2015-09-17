using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Kanpuchi.Models {

    /// <summary>
    /// アクセス ログのデータを格納します。
    /// </summary>
    [Table("AccessLog")]
    public class AccessLog {

        /// <summary>
        /// ログ ID を取得または設定します。
        /// </summary>
        [Key()]
        public virtual Guid LogId { get; set; }

        /// <summary>
        /// デバイス ID を取得または設定します。
        /// </summary>
        public virtual Guid? DeviceId { get; set; }

        /// <summary>
        /// URL を取得または設定します。
        /// </summary>
        [StringLength(1000)]
        public virtual string Url { get; set; }

        /// <summary>
        /// 作成日時を取得または設定します。
        /// </summary>
        public virtual DateTime CreatedAt { get; set; }

        /// <summary>
        /// 更新日時を取得または設定します。
        /// </summary>
        public virtual DateTime UpdatedAt { get; set; }

        /// <summary>
        /// 現在のインスタンスに関連付けられたデバイスを取得または設定します。
        /// </summary>
        [ForeignKey("DeviceId")]
        public virtual Device Device { get; set; }
        
        /// <summary>
        /// <see cref="Kanpuchi.Models.AccessLog"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public AccessLog() { }

    }

}