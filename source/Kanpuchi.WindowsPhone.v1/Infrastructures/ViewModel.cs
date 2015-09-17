using Microsoft.Phone.Tasks;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kanpuchi.Infrastructures {

    /// <summary>
    /// ビュー モデルの基本機能を提供します。
    /// </summary>
    public abstract class ViewModel : NotificationObject {

        /// <summary>
        /// ユーザーに情報を表示するリクエストを取得します。
        /// </summary>
        public InteractionRequest<Notification> NotificationRequest { get; private set; }

        /// <summary>
        /// ユーザーに確認を要求するリクエストを取得します。
        /// </summary>
        public InteractionRequest<Confirmation> ConfirmationRequest { get; private set; }

        /// <summary>
        /// <see cref="Kanpuchi.Infrastructures.ViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        protected ViewModel() {
            this.NotificationRequest = new InteractionRequest<Notification>();
            this.ConfirmationRequest = new InteractionRequest<Confirmation>();
        }

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
