using Karamem0.Kanpuchi.Extensions;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Karamem0.Kanpuchi.Interactivity {

    /// <summary>
    /// アプリケーション バーのメニューをクリックしたときにコマンドを実行するためのビヘイビアーを表します。
    /// </summary>
    public class ApplicationBarCommandBindingBehavior : Behavior<PhoneApplicationPage> {

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.PivotApplicationBarCommandBinding.Text"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(ApplicationBarCommandBindingBehavior),
                new PropertyMetadata(null));

        /// <summary>
        /// テキストを取得または設定します。
        /// </summary>
        public string Text {
            get { return (string)this.GetValue(TextProperty); }
            set { this.SetValue(TextProperty, value); }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.PivotApplicationBarCommandBinding.Command"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                "Command",
                typeof(ICommand),
                typeof(ApplicationBarCommandBindingBehavior),
                new PropertyMetadata(null));

        /// <summary>
        /// コマンドを取得または設定します。
        /// </summary>
        public ICommand Command {
            get { return (ICommand)this.GetValue(CommandProperty); }
            set { this.SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.PivotApplicationBarCommandBinding.CommandParameter"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(
                "CommandParameter",
                typeof(object),
                typeof(ApplicationBarCommandBindingBehavior),
                new PropertyMetadata(null));

        /// <summary>
        /// コマンドのパラメーターを取得または設定します。
        /// </summary>
        public object CommandParameter {
            get { return (object)this.GetValue(CommandParameterProperty); }
            set { this.SetValue(CommandParameterProperty, value); }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.PivotApplicationBarCommandBinding.DisplayText"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty DisplayTextProperty =
            DependencyProperty.Register(
                "DisplayText",
                typeof(string),
                typeof(ApplicationBarCommandBindingBehavior),
                new PropertyMetadata(null));

        /// <summary>
        /// 表示テキストを取得または設定します。
        /// </summary>
        public string DisplayText {
            get { return (string)this.GetValue(DisplayTextProperty); }
            set { this.SetValue(DisplayTextProperty, value); }
        }

        /// <summary>
        /// ビヘイビアーがアタッチされると呼び出されます。
        /// </summary>
        protected override void OnAttached() {
            base.OnAttached();
            if (this.AssociatedObject.ApplicationBar != null) {
                Enumerable.Concat(
                    this.AssociatedObject.ApplicationBar.Buttons.OfType<IApplicationBarMenuItem>(),
                    this.AssociatedObject.ApplicationBar.MenuItems.OfType<IApplicationBarMenuItem>())
                    .ForEach(menuItem => {
                        if (this.Text == menuItem.Text) {
                            if (string.IsNullOrEmpty(this.DisplayText) != true) {
                                menuItem.Text = this.DisplayText;
                                this.Text = this.DisplayText;
                            }
                        }
                        menuItem.Click += this.OnApplicationBarMenuItemClick;
                    });
            }
        }

        /// <summary>
        /// ビヘイビアーがデタッチされるときに呼び出されます。
        /// </summary>
        protected override void OnDetaching() {
            base.OnDetaching();
            if (this.AssociatedObject.ApplicationBar != null) {
                Enumerable.Concat(
                    this.AssociatedObject.ApplicationBar.Buttons.OfType<IApplicationBarMenuItem>(),
                    this.AssociatedObject.ApplicationBar.MenuItems.OfType<IApplicationBarMenuItem>())
                    .ForEach(menuItem => menuItem.Click -= this.OnApplicationBarMenuItemClick);
            }
        }

        /// <summary>
        /// <see cref="Microsoft.Phone.Shell.ApplicationBarMenuItem.Click"/> イベントで追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="System.EventArgs"/>。</param>
        private void OnApplicationBarMenuItemClick(object sender, EventArgs e) {
            var source = (IApplicationBarMenuItem)sender;
            if (source.Text == this.Text) {
                if (this.Command != null &&
                    this.Command.CanExecute(this.CommandParameter) == true) {
                    this.Command.Execute(this.CommandParameter);
                }
            }
        }

    }

}
