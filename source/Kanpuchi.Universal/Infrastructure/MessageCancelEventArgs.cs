using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.Infrastructure {

    /// <summary>
    /// メッセージが送信される直前に発生するイベントのデータを格納します。
    /// </summary>
    public sealed class MessageCancelEventArgs : CancelEventArgs {

        /// <summary>
        /// キーを取得します。
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// コンテンツを取得します。
        /// </summary>
        public object Content { get; private set; }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.MessageCancelEventArgs"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="key">キーを示す <see cref="System.String"/>。</param>
        /// <param name="content">コンテンツを示す <see cref="System.Object"/>。</param>
        public MessageCancelEventArgs(string key, object content) {
            this.Key = key;
            this.Content = content;
        }

    }

}
