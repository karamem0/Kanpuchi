using Kanpuchi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Kanpuchi.Models {

    /// <summary>
    /// 設定情報のデータを格納します。
    /// </summary>
    [DataContract(Name = "Settings")]
    public class Settings : Model {

        /// <summary>
        /// アプリ内ブラウザーを使用するかどうかを示す値を取得または設定します。
        /// </summary>
        [DataMember()]
        public bool UseInAppBrowser { get; set; }

        /// <summary>
        /// 有効なイト ID の配列を取得または設定します。
        /// </summary>
        [DataMember()]
        public int[] EnableSiteIds { get; set; }

        /// <summary>
        /// <see cref="Kanpuchi.Models.Settings"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public Settings() { }

    }

}
