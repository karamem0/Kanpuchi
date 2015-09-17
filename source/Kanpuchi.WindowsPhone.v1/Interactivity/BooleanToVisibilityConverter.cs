using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Kanpuchi.Interactivity {

    /// <summary>
    /// <see cref="System.Boolean"/> と <see cref="System.Windows.Visibility"/>
    /// の値を変換するための機能を提供します。
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter {

        /// <summary>
        /// <see cref="System.Boolean"/> を <see cref="System.Windows.Visibility"/> に変換します。
        /// </summary>
        /// <param name="value">変換元の値を示す <see cref="System.Object"/>。</param>
        /// <param name="targetType">変換先の型を示す <see cref="System.Type"/>。</param>
        /// <param name="parameter">パラメーターを示す <see cref="System.Object"/>。</param>
        /// <param name="culture">カルチャを示す <see cref="System.Globalization.CultureInfo"/>。</param>
        /// <returns>変換された <see cref="System.Object"/>。</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is bool) {
                return (bool)value == true
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
            return value;
        }

        /// <summary>
        /// <see cref="System.Windows.Visibility"/> を <see cref="System.Boolean"/> に変換します。
        /// </summary>
        /// <param name="value">変換元の値を示す <see cref="System.Object"/>。</param>
        /// <param name="targetType">変換先の型を示す <see cref="System.Type"/>。</param>
        /// <param name="parameter">パラメーターを示す <see cref="System.Object"/>。</param>
        /// <param name="culture">カルチャを示す <see cref="System.Globalization.CultureInfo"/>。</param>
        /// <returns>変換された <see cref="System.Object"/>。</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is Visibility) {
                return (Visibility)value == Visibility.Visible;
            }
            return value;
        }

    }

}
