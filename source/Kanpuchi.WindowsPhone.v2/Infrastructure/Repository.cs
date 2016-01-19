using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.Infrastructure {

    /// <summary>
    /// リポジトリの基本機能を提供します。
    /// </summary>
    /// <typeparam name="T">データ モデルの型。</typeparam>
    public abstract class Repository<T> where T : Model {

        /// <summary>
        /// <see cref="T:Kanpuchi.Infrastructures.Repository`1"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        protected Repository() { }

    }

}
