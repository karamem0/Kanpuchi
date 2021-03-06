﻿using Karamem0.Kanpuchi.Filters;
using Karamem0.Kanpuchi.Models;
using Karamem0.Kanpuchi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Karamem0.Kanpuchi.Controllers {

    /// <summary>
    /// まとめサイトを管理する API コントローラーを表します。
    /// </summary>
    public class MatomeSiteController : ApiController {

        /// <summary>
        /// データベース コンテキストを表します。
        /// </summary>
        private DefaultConnectionContext dbContext;

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Controllers.MatomeSiteController"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MatomeSiteController() {
            this.dbContext = new DefaultConnectionContext();
        }

        /// <summary>
        /// まとめサイトのコレクションを返します。
        /// </summary>
        /// <returns>検索結果を示す <see cref="System.Collections.Generic.IEnumerable{T}"/>。</returns>
        [BasicAuthetication()]
        public IEnumerable<MatomeSiteViewModel> GetMatomeSite() {
            return this.dbContext.MatomeSites
                .Select(x => new MatomeSiteViewModel() {
                    SiteId = x.SiteId,
                    SiteName = x.SiteName,
                    SiteUrl = x.SiteUrl,
                    FeedUrl = x.FeedUrl,
                })
                .ToArray();
        }

        /// <summary>
        /// 指定した ID のまとめサイトを返します。
        /// </summary>
        /// <param name="siteId">サイト ID を示す <see cref="System.Int32"/>。</param>
        /// <returns>
        /// まとめサイトを示す <see cref="Karamem0.Kanpuchi.ViewModels.MatomeSiteViewModel"/>。
        /// </returns>
        [BasicAuthetication()]
        public MatomeSiteViewModel GetMatomeSite(int siteId) {
            var model = this.dbContext.MatomeSites.Find(siteId);
            if (model == null) {
                throw new HttpResponseException(this.Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return new MatomeSiteViewModel() {
                SiteId = model.SiteId,
                SiteName = model.SiteName,
                SiteUrl = model.SiteUrl,
                FeedUrl = model.FeedUrl,
            };
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
