using Kanpuchi.Extensions;
using Kanpuchi.Resources;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Kanpuchi.Interactivity {

    /// <summary>
    /// ピボットの選択項目が変更されたときにアプリケーション バーを切り替えるビヘイビアーを表します。
    /// </summary>
    public class PivotApplicationBarBehavior : Behavior<Pivot> {

        /// <summary>
        /// <see cref="Kanpuchi.Interactivity.PivotApplicationBarBehavior"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public PivotApplicationBarBehavior() { }

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
            var appPage = this.AssociatedObject.GetSelfAndAncestors().OfType<PhoneApplicationPage>().First();
            if (appPage.ApplicationBar != null) {
                Enumerable.Concat(
                    appPage.ApplicationBar.Buttons.OfType<IApplicationBarMenuItem>(),
                    appPage.ApplicationBar.MenuItems.OfType<IApplicationBarMenuItem>())
                    .ForEach(menuItem => menuItem.Click -= this.OnApplicationBarMenuItemClick);
            }
            var pivotItem = (PivotItem)this.AssociatedObject.SelectedItem;
            var bindings = PivotApplicationBar.GetCommandBindings(pivotItem);
            appPage.ApplicationBar = PivotApplicationBar.GetApplicationBar(pivotItem);
            if (appPage.ApplicationBar != null) {
                Enumerable.Concat(
                    appPage.ApplicationBar.Buttons.OfType<IApplicationBarMenuItem>(),
                    appPage.ApplicationBar.MenuItems.OfType<IApplicationBarMenuItem>())
                    .ForEach(menuItem => {
                        menuItem.Click += this.OnApplicationBarMenuItemClick;
                        var binding = bindings.FirstOrDefault(y => y.Text == menuItem.Text);
                        if (binding != null) {
                            if (string.IsNullOrEmpty(binding.DisplayText) != true) {
                                menuItem.Text = binding.DisplayText;
                                binding.Text = binding.DisplayText;
                            }
                            menuItem.IsEnabled = binding.Command.CanExecute(binding.CommandParameter);
                        }
                    });
            }
        }

        /// <summary>
        /// <see cref="Microsoft.Phone.Shell.ApplicationBarMenuItem.Click"/> イベントで追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="System.EventArgs"/>。</param>
        private void OnApplicationBarMenuItemClick(object sender, EventArgs e) {
            var source = (IApplicationBarMenuItem)sender;
            var pivotItem = (PivotItem)this.AssociatedObject.SelectedItem;
            var bindings = PivotApplicationBar.GetCommandBindings(pivotItem);
            if (bindings != null) {
                var binding = bindings.FirstOrDefault(x => x.Text == source.Text);
                if (binding != null) {
                    if (binding.Command != null &&
                        binding.Command.CanExecute(binding.CommandParameter) == true) {
                        binding.Command.Execute(binding.CommandParameter);
                    }
                }
            }
        }

    }

}
