using Kanpuchi.Models;
using Kanpuchi.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace Kanpuchi.Controllers {

    /// <summary>
    /// デバイス トークンを管理する API コントローラーを表します。
    /// </summary>
    public class DeviceController : ApiController {

        /// <summary>
        /// データベース コンテキストを表します。
        /// </summary>
        private DefaultConnection dbContext;

        /// <summary>
        /// <see cref="Kanpuchi.Controllers.DeviceController"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public DeviceController() {
            this.dbContext = new DefaultConnection();
        }

        /// <summary>
        /// デバイスを追加または更新します。
        /// </summary>
        /// <param name="viewModel">追加する <see cref="Kanpuchi.ViewModels.DeviceViewModel"/>。</param>
        /// <returns>応答メッセージを示す <see cref="System.Net.Http.HttpResponseMessage"/>。</returns>
        public HttpResponseMessage PostDevice(DeviceViewModel viewModel) {
            if (this.ModelState.IsValid == true) {
                var model = this.dbContext.Devices.Find(viewModel.DeviceId);
                if (model == null) {
                    var keyGenerator = RandomNumberGenerator.Create();
                    var keyData = new byte[32];
                    keyGenerator.GetBytes(keyData);
                    model = new Device();
                    model.DeviceId = viewModel.DeviceId.GetValueOrDefault(Guid.NewGuid());
                    model.DeviceKey = Convert.ToBase64String(keyData);
                    model.CreatedAt = DateTime.UtcNow;
                }
                model.FirmwareVersion = viewModel.FirmwareVersion;
                model.HardwareVersion = viewModel.HardwareVersion;
                model.Manufacturer = viewModel.Manufacturer;
                model.Name = viewModel.Name;
                model.AppVersion = viewModel.AppVersion;
                model.UpdatedAt = DateTime.UtcNow;
                this.dbContext.Devices.AddOrUpdate(x => x.DeviceId, model);
                this.dbContext.SaveChanges();
                viewModel.DeviceId = model.DeviceId;
                viewModel.DeviceKey = model.DeviceKey;
                return this.Request.CreateResponse(HttpStatusCode.Created, viewModel);
            } else {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, this.ModelState);
            }
        }

        /// <summary>
        /// 指定したデバイス ID のデバイスを削除します。
        /// </summary>
        /// <param name="deviceId">デバイス ID を示す <see cref="System.Guid"/>。</param>
        /// <returns>応答メッセージを示す <see cref="System.Net.Http.HttpResponseMessage"/>。</returns>
        public HttpResponseMessage DeleteDevice(Guid deviceId) {
            try {
                var model = this.dbContext.Devices.Find(deviceId);
                if (model == null) {
                    return this.Request.CreateResponse(HttpStatusCode.NotFound);
                }
                this.dbContext.Devices.Remove(model);
                this.dbContext.SaveChanges();
                return this.Request.CreateResponse(HttpStatusCode.OK, model);
            } catch (DbUpdateConcurrencyException ex) {
                return this.Request.CreateResponse(HttpStatusCode.NotFound, ex);
            }
        }

        /// <summary>
        /// 現在のオブジェクトで使用されているリソースを解放します。
        /// </summary>
        /// <param name="disposing">
        /// マネージ オブジェクトとアンマネージ オブジェクトの両方を解放する場合は
        /// true。アンマネージ オブジェクトのみ解放する場合は false。
        /// </param>
        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);
            if (disposing == true) {
                if (this.dbContext != null) {
                    this.dbContext.Dispose();
                }
            }
        }

    }

}
