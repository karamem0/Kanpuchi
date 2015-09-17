using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kanpuchi {
    
    /// <summary>
    /// <see cref="Kanpuchi.Application"/> クラスを取得します。
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
