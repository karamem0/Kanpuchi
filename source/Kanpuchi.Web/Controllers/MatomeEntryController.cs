using Karamem0.Kanpuchi.Models;
using Karamem0.Kanpuchi.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Karamem0.Kanpuchi.Controllers {

    /// <summary>
    /// まとめ記事を管理する API コントローラーを表します。
    /// </summary>
    public class MatomeEntryController : ApiController {

        /// <summary>
        /// データベース コンテキストを表します。
        /// </summary>
        private DefaultConnectionContext dbContext;

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Controllers.MatomeEntryController"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MatomeEntryController() {
            this.dbContext = new DefaultConnectionContext();
        }

        /// <summary>
        /// 範囲および件数を指定してまとめ記事のコレクションを返します。
        /// </summary>
        /// <param name="minId">最小値を示す <see cref="System.Guid"/>。</param>
        /// <param name="maxId">最大値を示す <see cref="System.Guid"/>。</param>
        /// <param name="itemCount">取得する件数を示す <see cref="System.Int32"/>。</param>
        /// <param name="siteId">取得するサイトを示す <see cref="System.Int32"/> の配列。</param>
        /// <returns>検索結果を示す <see cref="System.Collections.Generic.IEnumerable{T}"/>。</returns>
        public IEnumerable<MatomeEntryViewModel> GetMatomeEntry(Guid? minId = null, Guid? maxId = null, int itemCount = 20, [FromUri]int[] siteId = null) {
            var minDate = (DateTime?)null;
            var minEntry = this.dbContext.MatomeEntries.Find(minId);
            if (minEntry != null) {
                minDate = minEntry.CreatedAt;
            }
            var maxDate = (DateTime?)null;
            var maxEntry = this.dbContext.MatomeEntries.Find(maxId);
            if (maxEntry != null) {
                maxDate = maxEntry.CreatedAt;
            }
            if (siteId.Length == 0) {
                siteId = this.dbContext.MatomeSites.Select(x => x.SiteId).ToArray();
            }
            return this.dbContext.MatomeEntries
                .Include(x => x.Site)
                .OrderByDescending(x => x.CreatedAt)
                .Where(x => minDate == null || x.CreatedAt.CompareTo(minDate.Value) > 0)
                .Where(x => maxDate == null || x.CreatedAt.CompareTo(maxDate.Value) < 0)
                .Where(x => siteId.Contains(x.SiteId) == true)
                .Take(itemCount)
                .Select(x => new MatomeEntryViewModel() {
                    EntryId = x.EntryId,
                    SiteId = x.Site.SiteId,
                    SiteName = x.Site.SiteName,
                    Title = x.Title,
                    Url = x.Url,
                    ThumbnailUrl = x.ThumbnailUrl,
                    CreatedAt = x.CreatedAt,
                })
                .ToArray();
        }

        /// <summary>
        /// 指定した記事 ID のまとめ記事を返します。
        /// </summary>
        /// <param name="entryId">記事 ID を示す <see cref="System.Guid"/>。</param>
        /// <returns>
        /// まとめ記事を示す <see cref="Karamem0.Kanpuchi.ViewModels.MatomeEntryViewModel"/>。
        /// </returns>
        public MatomeEntryViewModel GetMatomeEntry(Guid entryId) {
            var model = this.dbContext.MatomeEntries.Find(entryId);
            if (model == null) {
                throw new HttpResponseException(this.Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return new MatomeEntryViewModel() {
                EntryId = model.EntryId,
                SiteId = model.Site.SiteId,
                SiteName = model.Site.SiteName,
                Title = model.Title,
                Url = model.Url,
                CreatedAt = model.CreatedAt,
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
