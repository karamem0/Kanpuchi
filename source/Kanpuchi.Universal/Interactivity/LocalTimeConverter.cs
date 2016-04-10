using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Karamem0.Kanpuchi.Interactivity {

    /// <summary>
    /// 世界協定時刻と現地時刻の値を変換するための機能を提供します。
    /// </summary>
    public class LocalTimeConverter : IValueConverter {

        /// <summary>
        /// 世界協定時刻を現地時刻に変換します。
        /// </summary>
        /// <param name="value">変換元の値を示す <see cref="System.Object"/>。</param>
        /// <param name="targetType">変換先の型を示す <see cref="System.Type"/>。</param>
        /// <param name="parameter">パラメーターを示す <see cref="System.Object"/>。</param>
        /// <param name="culture">言語名を示す <see cref="System.String"/>。</param>
        /// <returns>変換された <see cref="System.Object"/>。</returns>
        public object Convert(object value, Type targetType, object parameter, string language) {
            if (value is DateTime) {
                return ((DateTime)value).ToLocalTime();
            }
            return value;
        }

        /// <summary>
        /// 現地時刻を世界協定時刻に変換します。
        /// </summary>
        /// <param name="value">変換元の値を示す <see cref="System.Object"/>。</param>
        /// <param name="targetType">変換先の型を示す <see cref="System.Type"/>。</param>
        /// <param name="parameter">パラメーターを示す <see cref="System.Object"/>。</param>
        /// <param name="culture">言語名を示す <see cref="System.String"/>。</param>
        /// <returns>変換された <see cref="System.Object"/>。</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            if (value is DateTime) {
                return ((DateTime)value).ToUniversalTime();
            }
            return value;
        }

    }

}
