using Kanpuchi.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanpuchi.Interactivity {

    /// <summary>
    /// 文字列リソースを格納します。
    /// </summary>
    public class StringResourceLocator {

        /// <summary>
        /// <see cref="Kanpuchi.Resources.StringResource"/> を取得または設定します。
        /// </summary>
        public StringResource StringResource { get; private set; }

        /// <summary>
        /// <see cref="Kanpuchi.Interactivity.StringResourceLocator"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public StringResourceLocator() {
            this.StringResource = new StringResource();
        }

    }

}
