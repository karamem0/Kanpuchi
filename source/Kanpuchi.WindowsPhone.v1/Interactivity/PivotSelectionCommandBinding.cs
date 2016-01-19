using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Karamem0.Kanpuchi.Interactivity {

    /// <summary>
    /// <see cref="Microsoft.Phone.Controls.Pivot"/> のイベントに関連付けられたコマンドを実行します。
    /// </summary>
    public class PivotSelectionCommandBinding : DependencyObject {

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.PivotSelectionCommandBinding.Command"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                "Command",
                typeof(ICommand),
                typeof(PivotSelectionCommandBinding),
                new PropertyMetadata(null));

        /// <summary>
        /// コマンドを取得または設定します。
        /// </summary>
        public ICommand Command {
            get { return (ICommand)this.GetValue(CommandProperty); }
            set { this.SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.PivotSelectionCommandBinding.CommandParameter"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(
                "CommandParameter",
                typeof(object),
                typeof(PivotSelectionCommandBinding),
                new PropertyMetadata(null));

        /// <summary>
        /// コマンドのパラメーターを取得または設定します。
        /// </summary>
        public object CommandParameter {
            get { return (object)this.GetValue(CommandParameterProperty); }
            set { this.SetValue(CommandParameterProperty, value); }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.PivotSelectionCommandBinding"/>
        /// クラスの新しいインスタンスを初期化します。
        /// </summary>
        public PivotSelectionCommandBinding() { }

    }

}
