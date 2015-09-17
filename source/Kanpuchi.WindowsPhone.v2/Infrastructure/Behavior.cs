using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Kanpuchi.Infrastructure {

    /// <summary>
    /// ビヘイビアーの基本機能を提供します。
    /// </summary>
    public abstract class Behavior<T> : DependencyObject, IBehavior where T : DependencyObject {

        /// <summary>
        /// ビヘイビアーがアタッチされるオブジェクトを取得します。
        /// </summary>
        DependencyObject IBehavior.AssociatedObject {
            get { return this.AssociatedObject; }
        }

        /// <summary>
        /// ビヘイビアーがアタッチされるオブジェクトを取得します。
        /// </summary>
        public T AssociatedObject { get; private set; }

        /// <summary>
        /// <see cref="Kanpuchi.Infrastructure.Behavior"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        protected Behavior() { }

        /// <summary>
        /// 現在のオブジェクトを指定したオブジェクトにアタッチします。
        /// </summary>
        /// <param name="associatedObject">アタッチする <see cref="Windows.UI.Xaml.DependencyObject"/>。</param>
        public void Attach(DependencyObject associatedObject) {
            this.AssociatedObject = associatedObject as T;
            this.OnAttached();
        }

        /// <summary>
        /// 現在のオブジェクトを関連オブジェクトからデタッチします。
        /// </summary>
        public void Detach() {
            this.OnDetached();
        }

        /// <summary>
        /// ビヘイビアーがアタッチされると呼び出されます。
        /// </summary>
        protected virtual void OnAttached() { }

        /// <summary>
        /// ビヘイビアーがデタッチされると呼び出されます。
        /// </summary>
        protected virtual void OnDetached() { }

    }

}
