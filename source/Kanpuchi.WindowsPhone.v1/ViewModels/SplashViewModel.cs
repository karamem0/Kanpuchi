using Kanpuchi.Infrastructures;
using Kanpuchi.Interactivity;
using Kanpuchi.Resources;
using Kanpuchi.Services;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kanpuchi.ViewModels {

    /// <summary>
    /// スプラッシュ ページのビュー モデルを表します。
    /// </summary>
    public class SplashViewModel : ViewModel {

        /// <summary>
        /// 初期化処理が正常終了したときに呼び出されます。
        /// </summary>
        public event EventHandler InitializeCompleted;

        /// <summary>
        /// <see cref="Kanpuchi.ViewModels.SplashViewModel.InitializeCompleted"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベントのデータを格納する <see cref="System.EventArgs"/>。</param>
        protected virtual void OnInitializeCompleted(EventArgs e) {
            var handler = this.InitializeCompleted;
            if (handler != null) {
                handler.Invoke(this, e);
            }
        }

        /// <summary>
        /// <see cref="Kanpuchi.ViewModels.SplashViewModel.InitializeCompleted"/> イベントを発生させます。
        /// </summary>
        public void RaiseInitializeCompleted() {
            this.OnInitializeCompleted(new EventArgs());
        }

        /// <summary>
        /// デバイスを管理するサービスを取得します。
        /// </summary>
        public DeviceService Service { get; private set; }

        /// <summary>
        /// <see cref="Kanpuchi.ViewModels.SplashViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public SplashViewModel() {
            this.Service = new DeviceService();
        }

        /// <summary>
        /// ビュー モデルがロードされると呼び出されます。
        /// </summary>
        public override void OnLoaded() {
            base.OnLoaded();
            this.Service.AsyncStarted += this.OnAsyncStarted;
            this.Service.AsyncCompleted += this.OnAsyncCompleted;
            this.Service.AsyncError += this.OnAsyncError;
            this.Service.RegisterAsync();
        }

        /// <summary>
        /// <see cref="Kanpuchi.Infrastructures.Service.AsyncStarted"/> イベントで追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="System.EventArgs"/>。</param>
        private void OnAsyncStarted(object sender, EventArgs e) {
            BusyStateManager.Current.Begin(StringResource.StartProgress);
        }

        /// <summary>
        /// <see cref="Kanpuchi.Infrastructures.Service.AsyncCompleted"/> イベントで追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">イベントのデータを格納する <see cref="System.EventArgs"/>。</param>
        private void OnAsyncCompleted(object sender, EventArgs e) {
            BusyStateManager.Current.End();
            this.RaiseInitializeCompleted();
        }

        /// <summary>
        /// <see cref="Kanpuchi.Infrastructures.Service.AsyncError"/> イベントで追加の処理を実行します。
        /// </summary>
        /// <param name="sender">イベントを発生させた <see cref="System.Object"/>。</param>
        /// <param name="e">
        /// イベントのデータを格納する <see cref="Kanpuchi.Infrastructures.AsyncErrorEventArgs"/>。
        /// </param>
        private void OnAsyncError(object sender, AsyncErrorEventArgs e) {
            BusyStateManager.Current.End();
            this.RaiseInitializeCompleted();
        }

    }

}
