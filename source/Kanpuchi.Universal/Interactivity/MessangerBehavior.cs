using Karamem0.Kanpuchi.Infrastructure;
using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;

namespace Karamem0.Kanpuchi.Interactivity {

    /// <summary>
    /// メッセージをトリガーするビヘイビアーを表します。
    /// </summary>
    [ContentProperty(Name = "Actions")]
    public sealed class MessangerBehavior : Behavior<DependencyObject> {

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.MessangerBehavior.Actions"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty ActionsProperty =
            DependencyProperty.Register(
                "Actions",
                typeof(ActionCollection),
                typeof(MessangerBehavior),
                new PropertyMetadata(new ActionCollection()));

        /// <summary>
        /// アクションのコレクションを取得または設定します。
        /// </summary>
        public ActionCollection Actions {
            get { return (ActionCollection)this.GetValue(ActionsProperty); }
            set { this.SetValue(ActionsProperty, value); }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.MessangerBehavior.Messanger"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty MessangerProperty =
            DependencyProperty.Register(
                "Messanger",
                typeof(Messanger),
                typeof(MessangerBehavior),
                new PropertyMetadata(Messanger.Current));

        /// <summary>
        /// メッセンジャーを取得または設定します。
        /// </summary>
        public Messanger Messanger {
            get { return (Messanger)this.GetValue(MessangerProperty); }
            set { this.SetValue(MessangerProperty, value); }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.MessangerBehavior.Key"/>
        /// 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty KeyProperty =
            DependencyProperty.Register(
                "Key",
                typeof(string),
                typeof(MessangerBehavior),
                new PropertyMetadata(null));

        /// <summary>
        /// キー文字列を取得または設定します。
        /// </summary>
        public string Key {
            get { return (string)this.GetValue(KeyProperty); }
            set { this.SetValue(KeyProperty, value); }
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Interactivity.MessangerBehavior"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MessangerBehavior() { }

        /// <summary>
        /// ビヘイビアーがアタッチされると呼び出されます。
        /// </summary>
        protected override void OnAttached() {
            this.Messanger.AfterSend += this.OnMessangerAfterSend;
        }

        /// <summary>
        /// ビヘイビアーがデタッチされると呼び出されます。
        /// </summary>
        protected override void OnDetaching() {
            this.Messanger.AfterSend -= this.OnMessangerAfterSend;
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.Messanger.AfterSend"/> イベントで追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="Karamem0.Kanpuchi.Infrastructure.MessageEventArgs"/>。
        /// </param>
        private void OnMessangerAfterSend(object sender, MessageEventArgs e) {
            if (e.Key == this.Key) {
                foreach (var action in this.Actions.OfType<IAction>()) {
                    action.Execute(this.AssociatedObject, e.Content);
                }
            }
        }

    }

}
