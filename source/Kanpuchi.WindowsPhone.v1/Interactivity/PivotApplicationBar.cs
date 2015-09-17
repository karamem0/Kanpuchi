using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Kanpuchi.Interactivity {

    /// <summary>
    /// <see cref="Kanpuchi.Interactivity.PivotApplicationBarBehavior"/>
    /// で使用される添付プロパティを定義します。
    /// </summary>
    public static class PivotApplicationBar {

        /// <summary>
        /// <see cref="P:Kanpuchi.Interactivity.PivotApplicationBar.ApplicationBar"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty ApplicationBarProperty =
            DependencyProperty.RegisterAttached(
                "ApplicationBar",
                typeof(ApplicationBar),
                typeof(PivotApplicationBarBehavior),
                new PropertyMetadata(null));

        /// <summary>
        /// 指定した <see cref="Microsoft.Phone.Controls.PivotItem"/> から
        /// <see cref="P:Kanpuchi.Interactivity.PivotApplicationBar.ApplicationBar"/>
        /// プロパティの値を取得します。
        /// </summary>
        /// <param name="element"><see cref="Microsoft.Phone.Controls.PivotItem"/>。</param>
        /// <returns><see cref="Microsoft.Phone.Shell.ApplicationBar"/>。</returns>
        public static ApplicationBar GetApplicationBar(PivotItem element) {
            return (ApplicationBar)element.GetValue(ApplicationBarProperty);
        }

        /// <summary>
        /// <see cref="P:Kanpuchi.Interactivity.PivotApplicationBar.ApplicationBar"/>
        /// プロパティの値を指定した <see cref="Microsoft.Phone.Controls.PivotItem"/> に設定します。
        /// </summary>
        /// <param name="element"><see cref="Microsoft.Phone.Controls.PivotItem"/>。</param>
        /// <param name="value"><see cref="Microsoft.Phone.Shell.ApplicationBar"/>。</param>
        public static void SetApplicationBar(PivotItem element, ApplicationBar value) {
            element.SetValue(ApplicationBarProperty, value);
        }

        /// <summary>
        /// <see cref="P:Kanpuchi.Interactivity.PivotApplicationBar.CommandBindings"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty CommandBindingsProperty =
            DependencyProperty.RegisterAttached(
                "CommandBindings",
                typeof(PivotApplicationBarCommandBindingCollection),
                typeof(PivotApplicationBarBehavior),
                new PropertyMetadata(new PivotApplicationBarCommandBindingCollection()));

        /// <summary>
        /// 指定した <see cref="Microsoft.Phone.Controls.PivotItem"/> から
        /// <see cref="P:Kanpuchi.Interactivity.PivotApplicationBar.CommandBindings"/>
        /// プロパティの値を取得します。
        /// </summary>
        /// <param name="element"><see cref="Microsoft.Phone.Controls.PivotItem"/>。</param>
        /// <returns>
        /// <see cref="Kanpuchi.Interactivity.PivotApplicationBarCommandBindingCollection"/>。
        /// </returns>
        public static PivotApplicationBarCommandBindingCollection GetCommandBindings(PivotItem element) {
            return (PivotApplicationBarCommandBindingCollection)element.GetValue(CommandBindingsProperty);
        }

        /// <summary>
        /// <see cref="P:Kanpuchi.Interactivity.PivotApplicationBar.CommandBindings"/>
        /// プロパティの値を指定した <see cref="Microsoft.Phone.Controls.PivotItem"/> に設定します。
        /// </summary>
        /// <param name="element"><see cref="Microsoft.Phone.Controls.PivotItem"/>。</param>
        /// <param name="value">
        /// <see cref="Kanpuchi.Interactivity.PivotApplicationBarCommandBindingCollection"/>。
        /// </param>
        public static void SetCommandBindings(PivotItem element, PivotApplicationBarCommandBindingCollection value) {
            element.SetValue(CommandBindingsProperty, value);
        }

    }

}
