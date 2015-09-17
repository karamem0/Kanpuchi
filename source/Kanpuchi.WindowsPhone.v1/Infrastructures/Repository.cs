using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Kanpuchi.Infrastructures {

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
