using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Karamem0.Kanpuchi.Infrastructure {

    /// <summary>
    /// パラメーターを受け取らないコマンドを表します。
    /// </summary>
    public class DelegateCommand : ICommand {

        /// <summary>
        /// コマンドを実行できるかどうかを示す値が変更されたときに発生します。
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.DelegateCommand.CanExecuteChanged"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベントのデータを格納する <see cref="System.EventArgs"/>。</param>
        protected virtual void OnCanExecuteChanged(EventArgs e) {
            var handler = this.CanExecuteChanged;
            if (handler != null) {
                handler.Invoke(this, e);
            }
        }

        /// <summary>
        /// コマンドを実行できるかどうかを示す値が変更されたことを通知します。
        /// </summary>
        public void RaiseCanExecuteChanged() {
            this.OnCanExecuteChanged(new EventArgs());
        }

        /// <summary>
        /// コマンドが実行されたときに呼び出されるメソッドを表します。
        /// </summary>
        private Action onExecute;

        /// <summary>
        /// コマンドを実行できるかどうかを判断するときに呼び出されるメソッドを表します。
        /// </summary>
        private Func<bool> onCanExecute;

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.DelegateCommand"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="onExecute">コマンドが実行されたときに呼び出される <see cref="System.Action"/>。</param>
        public DelegateCommand(Action onExecute) {
            this.onExecute = onExecute;
            this.onCanExecute = delegate { return true; };
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.DelegateCommand"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="onExecute">コマンドが実行されたときに呼び出される <see cref="System.Action"/>。</param>
        /// <param name="onCanExecute">コマンドを実行できるかどうかを判断するときに呼び出される <see cref="T:System.Func`1"/>。</param>
        public DelegateCommand(Action onExecute, Func<bool> onCanExecute) {
            this.onExecute = onExecute;
            this.onCanExecute = onCanExecute;
        }

        /// <summary>
        /// コマンドが実行されたときに呼び出されます。
        /// </summary>
        /// <param name="parameter">パラメーターを示す <see cref="System.Object"/>。</param>
        void ICommand.Execute(object parameter) {
            if (this.onExecute != null) {
                this.onExecute.Invoke();
            }
        }

        /// <summary>
        /// コマンドを実行できるかどうかを判断するときに呼び出されます。
        /// </summary>
        /// <param name="parameter">パラメーターを示す <see cref="System.Object"/>。</param>
        /// <returns>コマンドを実行できる場合は true、それ以外の場合は false。</returns>
        bool ICommand.CanExecute(object parameter) {
            if (this.onCanExecute != null) {
                return this.onCanExecute.Invoke();
            }
            return false;
        }

    }

}
