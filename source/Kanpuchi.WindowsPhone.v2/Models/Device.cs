using Kanpuchi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Kanpuchi.Models {

    /// <summary>
    /// デバイスのデータを格納します。
    /// </summary>
    [DataContract(Name = "Device")]
    public class Device : Model {

        /// <summary>
        /// デバイス ID を取得または設定します。
        /// </summary>
        [DataMember()]
        public Guid? DeviceId { get; set; }

        /// <summary>
        /// デバイス キーを取得または設定します。
        /// </summary>
        [DataMember()]
        public string DeviceKey { get; set; }

        /// <summary>
        /// ファームウェア バージョンを取得または設定します。
        /// </summary>
        [DataMember()]
        public string FirmwareVersion { get; set; }

        /// <summary>
        /// ハードウェア バージョンを取得または設定します。
        /// </summary>
        [DataMember()]
        public string HardwareVersion { get; set; }

        /// <summary>
        /// 製造元を取得または設定します。
        /// </summary>
        [DataMember()]
        public string Manufacturer { get; set; }

        /// <summary>
        /// 名前を取得または設定します。
        /// </summary>
        [DataMember()]
        public string Name { get; set; }

        /// <summary>
        /// アプリケーション バージョンを取得または設定します。
        /// </summary>
        [DataMember()]
        public string AppVersion { get; set; }

        /// <summary>
        /// <see cref="Kanpuchi.Models.Device"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public Device() { }

    }

}