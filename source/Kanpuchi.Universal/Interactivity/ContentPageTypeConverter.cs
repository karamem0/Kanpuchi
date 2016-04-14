using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Karamem0.Kanpuchi.Interactivity {

    /// <summary>
    /// パラメーターで渡された名前のページを変換するための機能を提供します。
    /// </summary>
    public sealed class ContentPageTypeConverter : IValueConverter {

        /// <summary>
        /// <see cref="System.Type"/> を <see cref="System.Boolean"/> に変換します。
        /// </summary>
        /// <param name="value">変換元の値を示す <see cref="System.Object"/>。</param>
        /// <param name="targetType">変換先の型を示す <see cref="System.Type"/>。</param>
        /// <param name="parameter">パラメーターを示す <see cref="System.Object"/>。</param>
        /// <param name="culture">言語名を示す <see cref="System.String"/>。</param>
        /// <returns>変換された <see cref="System.Object"/>。</returns>
        public object Convert(object value, Type targetType, object parameter, string language) {
            var typedValue = value as Type;
            var typedParameter = parameter as string;
            if (typedValue != null) {
                if (typedValue.Name == typedParameter) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// <see cref="System.Boolean"/> を <see cref="System.Type"/> に変換します。
        /// </summary>
        /// <param name="value">変換元の値を示す <see cref="System.Object"/>。</param>
        /// <param name="targetType">変換先の型を示す <see cref="System.Type"/>。</param>
        /// <param name="parameter">パラメーターを示す <see cref="System.Object"/>。</param>
        /// <param name="culture">言語名を示す <see cref="System.String"/>。</param>
        /// <returns>変換された <see cref="System.Object"/>。</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            var typedValue = value as bool?;
            var typedParameter = parameter as string;
            if (typedValue == true) {
                return this.GetType().GetTypeInfo().Assembly.GetTypes()
                    .FirstOrDefault(x => x.Name == typedParameter);
            }
            return DependencyProperty.UnsetValue;
        }

    }

}
