using Kanpuchi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Kanpuchi.Models {

    /// <summary>
    /// ツイートのデータを格納します。
    /// </summary>
    [DataContract()]
    public class Tweet : Model {

        /// <summary>
        /// ID を取得または設定します。
        /// </summary>
        [DataMember()]
        public string StatusId { get; set; }

        /// <summary>
        /// ユーザー ID を取得または設定します。
        /// </summary>
        [DataMember()]
        public string UserId { get; set; }

        /// <summary>
        /// ユーザー名を取得または設定します。
        /// </summary>
        [DataMember()]
        public string UserName { get; set; }

        /// <summary>
        /// 表示名を取得または設定します。
        /// </summary>
        [DataMember()]
        public string ScreenName { get; set; }

        /// <summary>
        /// プロフィール画像の URL を取得または設定します。
        /// </summary>
        [DataMember()]
        public string ProfileImageUrl { get; set; }

        /// <summary>
        /// 固定リンクの URL を取得または設定します。
        /// </summary>
        [DataMember()]
        public string Permalink { get; set; }

        /// <summary>
        /// テキストを取得または設定します。
        /// </summary>
        [DataMember()]
        public string Text { get; set; }

        /// <summary>
        /// 作成日時を取得または設定します。
        /// </summary>
        [DataMember()]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// <see cref="Kanpuchi.Models.Tweet"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public Tweet() { }

    }

}
