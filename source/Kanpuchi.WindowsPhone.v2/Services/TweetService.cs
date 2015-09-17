using Kanpuchi.Extensions;
using Kanpuchi.Infrastructure;
using Kanpuchi.Models;
using Kanpuchi.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kanpuchi.Services {

    /// <summary>
    /// ツイートを管理するためのサービスを表します。
    /// </summary>
    public class TweetService : Service {

        /// <summary>
        /// ツイートのコレクションを取得します。
        /// </summary>
        public ObservableCollection<Tweet> Items { get; private set; }

        /// <summary>
        /// <see cref="Kanpuchi.Services.TweetService"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TweetService() {
            this.Items = new ObservableCollection<Tweet>();
        }

        /// <summary>
        /// 最新のツイートを読み込みます。
        /// </summary>
        /// <returns>非同期操作を示す <see cref="System.Threading.Tasks.Task"/>。</returns>
        public async void LoadLatestAsync() {
            var context = SynchronizationContext.Current;
            try {
                context.Post(() => this.RaiseAsyncStarted());
                var deviceRepository = new DeviceRepository();
                var device = deviceRepository.Load();
                if (device == null) {
                    device = await deviceRepository.PostAsync(deviceRepository.Create());
                    if (device != null) {
                        deviceRepository.Save(device);
                    }
                }
                var tweetRepository = new TweetRepository();
                var tweets = await tweetRepository.GetAsync();
                if (tweets != null) {
                    this.Items.InsertRangeIf(0, tweets.OrderByDescending(x => x.StatusId), x => x.StatusId);
                    this.RaiseAsyncCompleted();
                }
            } catch (Exception ex) {
                context.Post(() => this.RaiseAsyncCompleted(ex));
            }
        }

        /// <summary>
        /// 現在読み込まれているツイートより前のツイートを読み込みます。
        /// </summary>
        /// <returns>非同期操作を示す <see cref="System.Threading.Tasks.Task"/>。</returns>
        public async void LoadPreviousAsync() {
            var context = SynchronizationContext.Current;
            try {
                context.Post(() => this.RaiseAsyncStarted());
                var deviceRepository = new DeviceRepository();
                var device = deviceRepository.Load();
                if (device == null) {
                    device = await deviceRepository.PostAsync(deviceRepository.Create());
                    if (device != null) {
                        deviceRepository.Save(device);
                    }
                }
                var maxTweet = this.Items.LastOrDefault();
                var maxId = (maxTweet == null) ? null : maxTweet.StatusId;
                var tweetRepository = new TweetRepository();
                var tweets = await tweetRepository.GetAsync(maxId: maxId);
                if (tweets != null) {
                    this.Items.AddRangeIf(tweets.OrderByDescending(x => x.StatusId), x => x.StatusId);
                    this.RaiseAsyncCompleted();
                }
            } catch (Exception ex) {
                context.Post(() => this.RaiseAsyncCompleted(ex));
            }
        }

    }

}
