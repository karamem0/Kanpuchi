using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.Infrastructure {

    /// <summary>
    /// メッセンジャーを表します。
    /// </summary>
    public sealed class Messanger {

        /// <summary>
        /// アプリケーションの <see cref="Karamem0.Kanpuchi.Infrastructure.Messanger"/>
        /// クラスのインスタンスを取得します。
        /// </summary>
        public static Messanger Current { get; private set; } = new Messanger();

        /// <summary>
        /// メッセージが送信される直前に発生します。
        /// </summary>
        public event EventHandler<MessageCancelEventArgs> BeforeSend;

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.Messanger.BeforeSend"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="Karamem0.Kanpuchi.Infrastructure.MessageCancelEventArgs"/>。
        /// </param>
        private void OnBeforeSend(MessageCancelEventArgs e) {
            var handler = this.BeforeSend;
            if (handler != null) {
                handler.Invoke(this, e);
            }
        }

        /// <summary>
        /// メッセージが送信された直後に発生します。
        /// </summary>
        public event EventHandler<MessageEventArgs> AfterSend;

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.Messanger.AfterSend"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="Karamem0.Kanpuchi.Infrastructure.MessageEventArgs"/>。
        /// </param>
        private void OnAfterSend(MessageEventArgs e) {
            var handler = this.AfterSend;
            if (handler != null) {
                handler.Invoke(this, e);
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.Messanger"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public Messanger() { }

        /// <summary>
        /// メッセージを送信します。
        /// </summary>
        /// <param name="key">キーを示す <see cref="System.String"/>。</param>
        /// <param name="content">コンテンツを示す <see cref="System.Object"/>。</param>
        public void Send(string key, object content) {
            var before = new MessageCancelEventArgs(key, content);
            this.OnBeforeSend(before);
            if (before.Cancel == true) {
                return;
            }
            var after = new MessageEventArgs(key, content);
            this.OnAfterSend(after);
        }

    }

}
