using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Karamem0.Kanpuchi.Interactivity {

    /// <summary>
    /// <see cref="System.Boolean"/> を <see cref="Windows.UI.Xaml.Visibility"/> に変換するための機能を提供します。
    /// </summary>
    public class BooleanToVisibilityConveter : IValueConverter {

        /// <summary>
        /// <see cref="System.Boolean"/> を <see cref="Windows.UI.Xaml.Visibility"/> に変換します。
        /// </summary>
        /// <param name="value">変換元の値を示す <see cref="System.Object"/>。</param>
        /// <param name="targetType">変換先の型を示す <see cref="System.Type"/>。</param>
        /// <param name="parameter">パラメーターを示す <see cref="System.Object"/>。</param>
        /// <param name="culture">言語名を示す <see cref="System.String"/>。</param>
        /// <returns>変換された <see cref="System.Object"/>。</returns>
        public object Convert(object value, Type targetType, object parameter, string language) {
            if (value is bool) {
                if ((bool)value == true) {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
            return DependencyProperty.UnsetValue;
        }

        /// <summary>
        /// <see cref="Windows.UI.Xaml.Visibility"/> を <see cref="System.Boolean"/> に変換します。
        /// </summary>
        /// <param name="value">変換元の値を示す <see cref="System.Object"/>。</param>
        /// <param name="targetType">変換先の型を示す <see cref="System.Type"/>。</param>
        /// <param name="parameter">パラメーターを示す <see cref="System.Object"/>。</param>
        /// <param name="culture">言語名を示す <see cref="System.String"/>。</param>
        /// <returns>変換された <see cref="System.Object"/>。</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            if (value is Visibility) {
                if ((Visibility)value == Visibility.Visible) {
                    return true;
                }
                return false;
            }
            return DependencyProperty.UnsetValue;
        }

    }

}
