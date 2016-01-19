using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.Extensions {

    /// <summary>
    /// <see cref="System.String"/> クラスの拡張メソッドを定義します。
    /// </summary>
    public static class StringExtensions {

        /// <summary>
        /// 指定された文字列が null または空の文字列かどうかを判断します。
        /// </summary>
        /// <param name="target">対象の <see cref="System.String"/>。</param>
        /// <returns>文字列が null または空の文字列の場合は true。それ以外の場合は false。</returns>
        public static bool IsNullOrEmpty(this string target) {
            return string.IsNullOrEmpty(target);
        }

        /// <summary>
        /// 指定された文字列が null、空の文字列または空白文字だけの文字列かどうかを判断します。
        /// </summary>
        /// <param name="target">対象の <see cref="System.String"/>。</param>
        /// <returns>文字列が null、空の文字列または空白文字だけの文字列の場合は true。それ以外の場合は false。</returns>
        public static bool IsNullOrWhiteSpase(this string target) {
            return string.IsNullOrWhiteSpace(target);
        }

    }

}
