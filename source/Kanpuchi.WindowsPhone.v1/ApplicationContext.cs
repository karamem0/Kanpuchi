using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Karamem0.Kanpuchi {
    
    /// <summary>
    /// <see cref="Karamem0.Kanpuchi.Application"/> クラスを取得します。
    /// </summary>
    public static class ApplicationContext {

        /// <summary>
        /// 現在のアプリケーションのオブジェクトを取得します。
        /// </summary>
        public static Application Current {
            get { return (Application)System.Windows.Application.Current; }
        }

    }

}
