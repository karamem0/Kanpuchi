using Kanpuchi.Extensions;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Kanpuchi.Interactivity {

    /// <summary>
    /// ピボットの選択項目が変更されたときにコマンドを実行するビヘイビアーを表します。
    /// </summary>
    public class PivotSelectionBehavior : Behavior<Pivot> {

        /// <summary>
        /// <see cref="Kanpuchi.Interactivity.PivotSelectionBehavior"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public PivotSelectionBehavior() { }

        /// <summary>
        /// ビヘイビアーがアタッチされると呼び出されます。
        /// </summary>
        protected override void OnAttached() {
            base.OnAttached();
            this.AssociatedObject.SelectionChanged += this.OnAssociatedObjectSelectionChanged;
        }

        /// <summary>
        /// ビヘイビアーがデタッチされるときに呼び出されます。
        /// </summary>
        protected override void OnDetaching() {
            base.OnDetaching();
            this.AssociatedObject.SelectionChanged -= this.OnAssociatedObjectSelectionChanged;
        }

        /// <summary>
        /// <see cref="Microsoft.Phone.Controls.Pivot.SelectionChanged"/> イベントで追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="System.Windows.Controls.SelectionChangedEventArgs"/>。
        /// </param>
        private void OnAssociatedObjectSelectionChanged(object sender, SelectionChangedEventArgs e) {
            e.AddedItems.OfType<PivotItem>().ForEach(x => {
                var binding = PivotSelection.GetSelectedCommandBinding(x);
                if (binding != null) {
                    if (binding.Command != null &&
                        binding.Command.CanExecute(binding.CommandParameter) == true) {
                        binding.Command.Execute(binding.CommandParameter);
                    }
                }
            });
            e.RemovedItems.OfType<PivotItem>().ForEach(x => {
                var binding = PivotSelection.GetUnselectedCommandBinding(x);
                if (binding != null) {
                    if (binding.Command != null &&
                        binding.Command.CanExecute(binding.CommandParameter) == true) {
                        binding.Command.Execute(binding.CommandParameter);
                    }
                }
            });
        }

    }

}
