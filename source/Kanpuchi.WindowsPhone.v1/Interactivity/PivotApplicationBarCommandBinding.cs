using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Kanpuchi.Interactivity {

    /// <summary>
    /// <see cref="Microsoft.Phone.Shell.ApplicationBar"/>
    /// のメニューまたはボタンをクリックしたときに関連付けられたコマンドを実行します。
    /// </summary>
    public class PivotApplicationBarCommandBinding : DependencyObject {

        /// <summary>
        /// <see cref="Kanpuchi.Interactivity.PivotApplicationBarCommandBinding.Text"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(PivotApplicationBarCommandBinding),
                new PropertyMetadata(null));

        /// <summary>
        /// テキストを取得または設定します。
        /// </summary>
        public string Text {
            get { return (string)this.GetValue(TextProperty); }
            set { this.SetValue(TextProperty, value); }
        }

        /// <summary>
        /// <see cref="Kanpuchi.Interactivity.PivotApplicationBarCommandBinding.Command"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                "Command",
                typeof(ICommand),
                typeof(PivotApplicationBarCommandBinding),
                new PropertyMetadata(null));

        /// <summary>
        /// コマンドを取得または設定します。
        /// </summary>
        public ICommand Command {
            get { return (ICommand)this.GetValue(CommandProperty); }
            set { this.SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// <see cref="Kanpuchi.Interactivity.PivotApplicationBarCommandBinding.CommandParameter"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(
                "CommandParameter",
                typeof(object),
                typeof(PivotApplicationBarCommandBinding),
                new PropertyMetadata(null));

        /// <summary>
        /// コマンドのパラメーターを取得または設定します。
        /// </summary>
        public object CommandParameter {
            get { return (object)this.GetValue(CommandParameterProperty); }
            set { this.SetValue(CommandParameterProperty, value); }
        }

        /// <summary>
        /// <see cref="Kanpuchi.Interactivity.PivotApplicationBarCommandBinding.DisplayText"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty DisplayTextProperty =
            DependencyProperty.Register(
                "DisplayText",
                typeof(string),
                typeof(PivotApplicationBarCommandBinding),
                new PropertyMetadata(null));

        /// <summary>
        /// 表示テキストを取得または設定します。
        /// </summary>
        public string DisplayText {
            get { return (string)this.GetValue(DisplayTextProperty); }
            set { this.SetValue(DisplayTextProperty, value); }
        }

        /// <summary>
        /// <see cref="Kanpuchi.Interactivity.PivotApplicationBarCommandBinding"/>
        /// クラスの新しいインスタンスを初期化します。
        /// </summary>
        public PivotApplicationBarCommandBinding() { }

    }

}
