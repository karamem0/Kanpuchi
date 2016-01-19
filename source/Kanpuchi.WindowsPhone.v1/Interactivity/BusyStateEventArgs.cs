using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Karamem0.Kanpuchi.Interactivity {

    /// <summary>
    /// <see cref="Karamem0.Kanpuchi.Interactivity.BusyStateManager.BusyStateChanged"/> イベントのデータを格納します。
    /// </summary>
    public class BusyStateEventArgs : EventArgs {

        /// <summary>
        /// ビジーかどうかを示す値を取得します。
        /// </summary>
        public bool IsBusy { get; private set; }

        /// <summary>
        /// ビジー状態のときに表示するテキストを取得します。
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.BusyStateEventArgs"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="isBusy">ビジーかどうかを示す <see cref="System.Boolean"/>。</param>
        /// <param name="text">ビジー状態のときに表示するテキストを示す <see cref="System.String"/>。</param>
        public BusyStateEventArgs(bool isBusy, string text) {
            this.IsBusy = isBusy;
            this.Text = text;
        }

    }

}
