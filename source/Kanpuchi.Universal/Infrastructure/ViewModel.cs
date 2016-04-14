using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.Infrastructure {

    /// <summary>
    /// ビュー モデルを表します。
    /// </summary>
    public abstract class ViewModel : NotifyPropertyChanged {

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.ViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ViewModel() { }

        /// <summary>
        /// ビュー モデルがロードされると呼び出されます。
        /// </summary>
        public virtual void OnLoaded() { }

        /// <summary>
        /// ビュー モデルがアンロードされると呼び出されます。
        /// </summary>
        public virtual void OnUnloaded() { }

    }

}
