using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Karamem0.Kanpuchi.Models {

    /// <summary>
    /// デバイスのデータを格納します。
    /// </summary>
    [Table("Device")]
    public class Device {

        /// <summary>
        /// デバイス ID を取得または設定します。
        /// </summary>
        [Key()]
        public virtual Guid DeviceId { get; set; }

        /// <summary>
        /// デバイス キーを取得または設定します。
        /// </summary>
        [StringLength(80)]
        public string DeviceKey { get; set; }

        /// <summary>
        /// ファームウェア バージョンを取得または設定します。
        /// </summary>
        [StringLength(20)]
        public virtual string FirmwareVersion { get; set; }

        /// <summary>
        /// ハードウェア バージョンを取得または設定します。
        /// </summary>
        [StringLength(20)]
        public virtual string HardwareVersion { get; set; }

        /// <summary>
        /// 製造元を取得または設定します。
        /// </summary>
        [StringLength(40)]
        public virtual string Manufacturer { get; set; }

        /// <summary>
        /// 名前を取得または設定します。
        /// </summary>
        [StringLength(40)]
        public virtual string Name { get; set; }

        /// <summary>
        /// アプリケーション バージョンを取得または設定します。
        /// </summary>
        [StringLength(20)]
        public string AppVersion { get; set; }

        /// <summary>
        /// 作成日時を取得または設定します。
        /// </summary>
        public virtual DateTime CreatedAt { get; set; }

        /// <summary>
        /// 更新日時を取得または設定します。
        /// </summary>
        public virtual DateTime UpdatedAt { get; set; }

        /// <summary>
        /// 現在のインスタンスに関連付けられたアクセスログのコレクションを取得または設定します。
        /// </summary>
        public virtual ICollection<AccessLog> AccessLogs { get; set; }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Models.Device"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public Device() { }

    }

}
