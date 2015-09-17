using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kanpuchi.Infrastructure {

    /// <summary>
    /// 厳密に型指定されたパラメーターを受け取るコマンドを表します。
    /// </summary>
    public class DelegateCommand<T> : ICommand {

        /// <summary>
        /// コマンドを実行できるかどうかを示す値が変更されたときに発生します。
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// <see cref="T:Kanpuchi.Infrastructure.DelegateCommand`1.CanExecuteChanged"/> イベントを発生させます。
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
        private Action<T> onExecute;

        /// <summary>
        /// コマンドを実行できるかどうかを判断するときに呼び出されるメソッドを表します。
        /// </summary>
        private Func<T, bool> onCanExecute;

        /// <summary>
        /// <see cref="T:Kanpuchi.Infrastructure.DelegateCommand`1"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="onExecute">コマンドが実行されたときに呼び出される <see cref="T:System.Action`1"/>。</param>
        public DelegateCommand(Action<T> onExecute) {
            this.onExecute = onExecute;
            this.onCanExecute = delegate { return true; };
        }

        /// <summary>
        /// <see cref="T:Kanpuchi.Infrastructure.DelegateCommand`1"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="onExecute">コマンドが実行されたときに呼び出される <see cref="T:System.Action`1"/>。</param>
        /// <param name="onCanExecute">コマンドを実行できるかどうかを判断するときに呼び出される <see cref="T:System.Func`2"/>。</param>
        public DelegateCommand(Action<T> onExecute, Func<T, bool> onCanExecute) {
            this.onExecute = onExecute;
            this.onCanExecute = onCanExecute;
        }

        /// <summary>
        /// コマンドが実行されたときに呼び出されます。
        /// </summary>
        /// <param name="parameter">パラメーターを示す <see cref="System.Object"/>。</param>
        void ICommand.Execute(object parameter) {
            if (this.onExecute != null) {
                this.onExecute.Invoke((T)parameter);
            }
        }

        /// <summary>
        /// コマンドを実行できるかどうかを判断するときに呼び出されます。
        /// </summary>
        /// <param name="parameter">パラメーターを示す <see cref="System.Object"/>。</param>
        /// <returns>コマンドを実行できる場合は true、それ以外の場合は false。</returns>
        bool ICommand.CanExecute(object parameter) {
            if (this.onCanExecute != null) {
                return this.onCanExecute.Invoke((T)parameter);
            }
            return false;
        }

    }

}
