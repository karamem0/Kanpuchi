using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Kanpuchi.Interactivity {

    /// <summary>
    /// <see cref="Kanpuchi.Interactivity.PivotSelectionBehavior"/> で使用される添付プロパティを定義します。
    /// </summary>
    public static class PivotSelection {

        /// <summary>
        /// <see cref="P:Kanpuchi.Interactivity.PivotSelection.SelectedCommandBinding"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty SelectedCommandBindingProperty =
            DependencyProperty.RegisterAttached(
                "SelectedCommandBinding",
                typeof(PivotSelectionCommandBinding),
                typeof(PivotSelection),
                new PropertyMetadata(null));

        /// <summary>
        /// 指定した <see cref="Microsoft.Phone.Controls.PivotItem"/> から
        /// <see cref="P:Kanpuchi.Interactivity.PivotSelection.SelectedCommandBinding"/>
        /// プロパティの値を取得します。
        /// </summary>
        /// <param name="element"><see cref="Microsoft.Phone.Controls.PivotItem"/>。</param>
        /// <returns><see cref="Kanpuchi.Interactivity.PivotSelectionCommandBinding"/>。</returns>
        public static PivotSelectionCommandBinding GetSelectedCommandBinding(PivotItem element) {
            return (PivotSelectionCommandBinding)element.GetValue(SelectedCommandBindingProperty);
        }

        /// <summary>
        /// <see cref="P:Kanpuchi.Interactivity.PivotSelection.SelectedCommandBinding"/>
        /// プロパティの値を指定した <see cref="Microsoft.Phone.Controls.PivotItem"/> に設定します。
        /// </summary>
        /// <param name="element"><see cref="Microsoft.Phone.Controls.PivotItem"/>。</param>
        /// <param name="value"><see cref="Kanpuchi.Interactivity.PivotSelectionCommandBinding"/>。</param>
        public static void SetSelectedCommandBinding(PivotItem element, PivotSelectionCommandBinding value) {
            element.SetValue(SelectedCommandBindingProperty, value);
        }

        /// <summary>
        /// <see cref="P:Kanpuchi.Interactivity.PivotSelection.UnselectedCommandBinding"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty UnselectedCommandBindingProperty =
            DependencyProperty.RegisterAttached(
                "UnselectedCommandBinding",
                typeof(PivotSelectionCommandBinding),
                typeof(PivotSelection),
                new PropertyMetadata(null));

        /// <summary>
        /// 指定した <see cref="Microsoft.Phone.Controls.PivotItem"/> から
        /// <see cref="P:Kanpuchi.Interactivity.PivotSelection.UnselectedCommandBinding"/>
        /// プロパティの値を取得します。
        /// </summary>
        /// <param name="element"><see cref="Microsoft.Phone.Controls.PivotItem"/>。</param>
        /// <returns><see cref="Kanpuchi.Interactivity.PivotSelectionCommandBinding"/>。</returns>
        public static PivotSelectionCommandBinding GetUnselectedCommandBinding(PivotItem element) {
            return (PivotSelectionCommandBinding)element.GetValue(UnselectedCommandBindingProperty);
        }

        /// <summary>
        /// <see cref="P:Kanpuchi.Interactivity.PivotSelection.UnselectedCommandBinding"/>
        /// プロパティの値を指定した <see cref="Microsoft.Phone.Controls.PivotItem"/> に設定します。
        /// </summary>
        /// <param name="element"><see cref="Microsoft.Phone.Controls.PivotItem"/>。</param>
        /// <param name="value"><see cref="Kanpuchi.Interactivity.PivotSelectionCommandBinding"/>。</param>
        public static void SetUnselectedCommandBinding(PivotItem element, PivotSelectionCommandBinding value) {
            element.SetValue(UnselectedCommandBindingProperty, value);
        }

    }

}
