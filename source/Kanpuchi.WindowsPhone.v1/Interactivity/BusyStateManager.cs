using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Karamem0.Kanpuchi.Interactivity {

    /// <summary>
    /// ビジー状態を管理します。
    /// </summary>
    public class BusyStateManager : NotificationObject {

        /// <summary>
        /// アプリケーションの <see cref="Karamem0.Kanpuchi.Interactivity.BusyStateManager"/>
        /// クラスのインスタンスを取得します。
        /// </summary>
        public static BusyStateManager Current {
            get { return (BusyStateManager)Application.Current.Resources["BusyStateManager"]; }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.BusyStateManager.IsBusy"/> プロパティが変更されると発生します。
        /// </summary>
        public event EventHandler<BusyStateEventArgs> BusyStateChanged;

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.BusyStateManager.BusyStateChanged"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベントのデータを格納する <see cref="System.EventArgs"/>。</param>
        protected virtual void OnBusyStateChanged(BusyStateEventArgs e) {
            var handler = this.BusyStateChanged;
            if (handler != null) {
                handler.Invoke(this, e);
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.BusyStateManager.BusyStateChanged"/> イベントを発生させます。
        /// </summary>
        protected void RaiseBusyStateChanged() {
            this.OnBusyStateChanged(new BusyStateEventArgs(this.IsBusy, this.Text));
        }

        /// <summary>
        /// ビジーかどうかを示す値を表します。
        /// </summary>
        private bool isBusy;

        /// <summary>
        /// ビジーかどうかを示す値を取得します。
        /// </summary>
        public bool IsBusy {
            get { return this.isBusy; }
            private set {
                if (this.isBusy != value) {
                    this.isBusy = value;
                    this.RaisePropertyChanged(() => this.IsBusy);
                }
            }
        }

        /// <summary>
        /// ビジー状態のときに表示するテキストを表します。
        /// </summary>
        private string text;

        /// <summary>
        /// ビジー状態のときに表示するテキストを取得します。
        /// </summary>
        public string Text {
            get { return this.text; }
            private set {
                if (this.text != value) {
                    this.text = value;
                    this.RaisePropertyChanged(() => this.Text);
                }
            }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.BusyStateManager"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public BusyStateManager() { }

        /// <summary>
        /// ビジー状態を開始します。
        /// </summary>
        /// <param name="text">テキストを示す <see cref="System.String"/>。</param>
        public void Begin(string text) {
            this.IsBusy = true;
            this.Text = text;
            this.RaiseBusyStateChanged();
        }

        /// <summary>
        /// ビジー状態を終了します。
        /// </summary>
        public void End() {
            this.IsBusy = false;
            this.Text = null;
            this.RaiseBusyStateChanged();
        }

    }

}
