using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.Infrastructure {

    /// <summary>
    /// プロパティの変更を通知可能なオブジェクトを表します。
    /// </summary>
    public abstract class NotifyPropertyChanged : INotifyPropertyChanged {

        /// <summary>
        /// プロパティが変更されると発生します。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.NotifyPropertyChanged.PropertyChanged"/>
        /// イベントを発生させます。
        /// </summary>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="System.ComponentModel.PropertyChangedEventArgs"/>。
        /// </param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) {
            var handler = this.PropertyChanged;
            if (handler != null) {
                handler.Invoke(this, e);
            }
        }

        /// <summary>
        /// 指定した名前のプロパティが変更されたことを通知します。
        /// </summary>
        /// <param name="propertyName">プロパティの名前を示す <see cref="System.String"/>。</param>
        public void RaisePropertyChanged(string propertyName) {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 指定した式ツリーで表されるプロパティが変更されたことを通知します。
        /// </summary>
        /// <typeparam name="T">プロパティの型。</typeparam>
        /// <param name="propertyExpression">プロパティの式ツリーを示す <see cref="System.Linq.Expressions.Expression{TDelegate}"/>。</param>
        public void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression) {
            this.RaisePropertyChanged(((MemberExpression)propertyExpression.Body).Member.Name);
        }

    }

}
