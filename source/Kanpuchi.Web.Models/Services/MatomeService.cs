using Kanpuchi.Extensions;
using Kanpuchi.Models;
using Microsoft.Azure;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Kanpuchi.Services {

    /// <summary>
    /// まとめデータを操作するオブジェクトを表します。
    /// </summary>
    public class MatomeService : IDisposable {

        private static readonly int ThumbnailMinLength = 240;

        private static readonly int ThumbnailWidth = 96;

        private static readonly int ThumbnailHeight = 96;

        /// <summary>
        /// データベース コンテキストを表します。
        /// </summary>
        private DefaultConnection dbContext;

        /// <summary>
        /// <see cref="Kanpuchi.Services.MatomeService"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MatomeService() {
            this.dbContext = new DefaultConnection();
            this.dbContext.Database.Log = str => Debug.WriteLine(str);
        }

        /// <summary>
        /// フィードを読み込んでまとめ記事を追加します。
        /// </summary>
        public void AddMatomeEntry() {
            this.dbContext.MatomeEntries.AddOrUpdate(
                matomeEntry => matomeEntry.Url,
                this.dbContext.MatomeSites
                    .ToList()
                    .SelectMany(matomeSite => {
                        var element = XElement.Load(matomeSite.FeedUrl);
                        if (matomeSite.FeedFormat == "Atom0.3") {
                            var ns = XNamespace.Get("http://purl.org/atom/ns#");
                            return element.Elements(ns + "entry")
                                .Select(x => new MatomeEntry() {
                                    EntryId = Guid.NewGuid(),
                                    SiteId = matomeSite.SiteId,
                                    Title = (string)x.Element(ns + "title"),
                                    Url = (string)x.Element(ns + "link").Attribute("href"),
                                    Content = (string)x.Element(ns + "content"),
                                    CreatedAt = (DateTime)x.Element(ns + "issued"),
                                    UpdatedAt = DateTime.UtcNow,
                                });
                        }
                        return Enumerable.Empty<MatomeEntry>();
                    })
                    .ToArray()
            );
            this.dbContext.SaveChanges();
        }

        /// <summary>
        /// 古くなったまとめ記事を削除します。
        /// </summary>
        public void RemoveMatomeEntry() {
            var storeCount = default(int);
            var storeCountString = ConfigurationManager.AppSettings["MatomeEntryStoreCount"];
            if (int.TryParse(storeCountString, out storeCount) == true) {
                this.dbContext.Database.ExecuteSqlCommand(
                    " DELETE T FROM (" +
                    " SELECT * FROM [dbo].[MatomeEntry]" +
                    " ORDER BY [CreatedAt] DESC" +
                    " OFFSET @offset ROWS" +
                    " ) AS T",
                    new SqlParameter("@offset", storeCount)
                );
            }
            this.dbContext.SaveChanges();
        }

        /// <summary>
        /// サムネイル画像を更新します。
        /// </summary>
        public void UpdateMatomeEntryThumbnail() {
            var connectionString = ConfigurationManager.ConnectionStrings["StorageConnection"].ConnectionString;
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference("thumbnails");
            this.dbContext.MatomeEntries
                .Where(matomeEntry => matomeEntry.ThumbnailUpdated != true)
                .ToList()
                .ForEach(matomeEntry => {
                    var pattern = "<img.+?src=\\\\?\"((?:http|https)://.+?\\.(?:gif|jpg|jpeg|png))\\\\?\"";
                    var matches = Regex.Matches(matomeEntry.Content, pattern, RegexOptions.IgnoreCase);
                    for (int index = 0; index < matches.Count; index++) {
                        if (matomeEntry.ThumbnailUrl != null) {
                            break;
                        }
                        var match = matches[index];
                        var sourceUrl = match.Groups[1].Value;
                        using (var httpClient = new HttpClient())
                        using (var sourceStream = httpClient.GetStreamAsync(sourceUrl).Wait<Stream>()) {
                            if (sourceStream != null) {
                                using (var sourceBitmap = new Bitmap(sourceStream)) {
                                    var sourceWidth = sourceBitmap.Width;
                                    var sourceHeight = sourceBitmap.Height;
                                    if (sourceWidth >= ThumbnailMinLength && sourceHeight >= ThumbnailMinLength) {
                                        var sourceLength = Math.Min(sourceHeight, sourceWidth);
                                        var sourceX = (int)((sourceWidth - sourceLength) / 2);
                                        var sourceY = (int)((sourceHeight - sourceLength) / 2);
                                        using (var destBitmap = new Bitmap(ThumbnailWidth, ThumbnailHeight))
                                        using (var destGrapics = Graphics.FromImage(destBitmap))
                                        using (var destStream = new MemoryStream()) {
                                            destGrapics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                            destGrapics.DrawImage(
                                                sourceBitmap,
                                                new Rectangle(0, 0, destBitmap.Width, destBitmap.Height),
                                                new Rectangle(sourceX, sourceY, sourceLength, sourceLength),
                                                GraphicsUnit.Pixel);
                                            destBitmap.Save(destStream, ImageFormat.Png);
                                            destStream.Position = 0;
                                            var blockBlob = blobContainer.GetBlockBlobReference(matomeEntry.EntryId.ToString() + ".png");
                                            blockBlob.Properties.ContentType = "image/png";
                                            blockBlob.UploadFromStream(destStream);
                                            matomeEntry.ThumbnailUrl = blockBlob.Uri.ToString();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    matomeEntry.ThumbnailUpdated = true;
                    matomeEntry.UpdatedAt = DateTime.UtcNow;
                    this.dbContext.SaveChanges();
                });
        }

        /// <summary>
        /// 現在のオブジェクトで使用されているリソースを解放します。
        /// </summary>
        public void Dispose() {
            if (this.dbContext != null) {
                this.dbContext.Dispose();
            }
        }

    }

}
