using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Kanpuchi.Interactivity {

    /// <summary>
    /// null または空の文字列を <see cref="Windows.UI.Xaml.Visibility"/> に変換するための機能を提供します。
    /// </summary>
    public class StringToVisibilityConveter : IValueConverter {

        /// <summary>
        /// null または空の文字列のときに <see cref="Windows.UI.Xaml.Visibility.Collapsed"/> を返します。それ以外の場合は
        /// <see cref="Windows.UI.Xaml.Visibility.Visible"/> を返します。
        /// </summary>
        /// <param name="value">変換元の値を示す <see cref="System.Object"/>。</param>
        /// <param name="targetType">変換先の型を示す <see cref="System.Type"/>。</param>
        /// <param name="parameter">パラメーターを示す <see cref="System.Object"/>。</param>
        /// <param name="culture">言語名を示す <see cref="System.String"/>。</param>
        /// <returns>変換された <see cref="System.Object"/>。</returns>
        public object Convert(object value, Type targetType, object parameter, string language) {
            if (string.IsNullOrEmpty(value as string) == true) {
                return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        /// <summary>
        /// このメソッドは実装されていないため例外を返します。
        /// </summary>
        /// <param name="value">変換元の値を示す <see cref="System.Object"/>。</param>
        /// <param name="targetType">変換先の型を示す <see cref="System.Type"/>。</param>
        /// <param name="parameter">パラメーターを示す <see cref="System.Object"/>。</param>
        /// <param name="culture">言語名を示す <see cref="System.String"/>。</param>
        /// <returns>変換された <see cref="System.Object"/>。</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }

    }

}
