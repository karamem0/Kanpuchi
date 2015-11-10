using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Kanpuchi.ViewModels {

    /// <summary>
    /// まとめ記事のデータを格納します。
    /// </summary>
    [DataContract(Name = "MatomeEntry")]
    public class MatomeEntryViewModel {

        /// <summary>
        /// 記事 ID を取得または設定します。
        /// </summary>
        [DataMember()]
        public virtual Guid EntryId { get; set; }

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
        /// タイトルを取得または設定します。
        /// </summary>
        [DataMember()]
        public virtual string Title { get; set; }

        /// <summary>
        /// URL を取得または設定します。
        /// </summary>
        [DataMember()]
        public virtual string Url { get; set; }

        /// <summary>
        /// URL を取得または設定します。
        /// </summary>
        [DataMember()]
        public virtual string ThumbnailUrl { get; set; }

        /// <summary>
        /// 作成日時を取得または設定します。
        /// </summary>
        [DataMember()]
        public virtual DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// <see cref="Kanpuchi.ViewModels.MatomeEntryViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MatomeEntryViewModel() { }

    }

}
