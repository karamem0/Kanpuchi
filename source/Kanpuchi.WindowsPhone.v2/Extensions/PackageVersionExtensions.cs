using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace Kanpuchi.Extensions {

    /// <summary>
    /// <see cref="Windows.ApplicationModel.PackageVersion"/> クラスの拡張メソッドを定義します。
    /// </summary>
    public static class PackageVersionExtensions {

        /// <summary>
        /// <see cref="Windows.ApplicationModel.PackageVersion"/> の文字列形式を返します。
        /// </summary>
        /// <param name="target">対象の <see cref="Windows.ApplicationModel.PackageVersion"/>。</param>
        /// <returns>文字列形式を示す <see cref="System.String"/>。</returns>
        public static string ToFormattedString(this PackageVersion target) {
            return string.Format("{0}.{1}.{2}.{3}", target.Major, target.Minor, target.Build, target.Revision);
        }

    }

}
