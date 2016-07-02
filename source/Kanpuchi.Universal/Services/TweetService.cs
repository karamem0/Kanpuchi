using AutoMapper;
using Karamem0.Kanpuchi.Infrastructure;
using Karamem0.Kanpuchi.Models;
using Karamem0.Kanpuchi.Repositories;
using Karamem0.Kanpuchi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.Services {

    /// <summary>
    /// ツイートを管理するためのサービスを表します。
    /// </summary>
    public class TweetService : Service {

        /// <summary>
        /// オブジェクトが解放されたかどうかを示す値を表します。
        /// </summary>
        private bool disposed;

        /// <summary>
        /// ビュー モデルを表します。
        /// </summary>
        private HomeViewModel viewModel;

        /// <summary>
        /// デバイスを格納するリポジトリを表します。
        /// </summary>
        private DeviceRepository deviceRepository;

        /// <summary>
        /// ツイートのコレクションを格納するリポジトリを表します。
        /// </summary>
        private TweetRepository tweetRepository;

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Services.TweetService"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        private TweetService() {
            this.deviceRepository = new DeviceRepository();
            this.tweetRepository = new TweetRepository();
        }

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Services.TweetService"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="viewModel"><see cref="Karamem0.Kanpuchi.ViewModels.HomeViewModel"/>。</param>
        public TweetService(HomeViewModel viewModel)
            : this() {
            this.viewModel = viewModel;
        }

        /// <summary>
        /// 最新のツイートを読み込みます。
        /// </summary>
        public async void LoadLatestAsync() {
            if (this.disposed == true) {
                throw new ObjectDisposedException(nameof(TweetService));
            }
            try {
                this.RaiseAsyncStarted();
                var device = await this.deviceRepository.RegisterAsync();
                var tweets = await this.tweetRepository.SearchAsync(
                    deviceId: device.DeviceId.ToString(),
                    deviceKey: device.DeviceKey);
                if (tweets != null) {
                    var config = new MapperConfiguration(x => x.CreateMap<Tweet, TweetViewModel>());
                    var mapper = config.CreateMapper();
                    foreach (var tweet in tweets.OrderBy(x => x.CreatedAt)) {
                        if (this.viewModel.Tweets.Any(x => x.StatusId == tweet.StatusId) != true) {
                            this.viewModel.Tweets.Insert(0, mapper.Map<TweetViewModel>(tweet));
                        }
                    }
                }
                this.RaiseAsyncCompleted();
            } catch (Exception ex) {
                this.RaiseAsyncCompleted(ex);
            }
        }

        /// <summary>
        /// 現在読み込まれているツイートより前のツイートを読み込みます。
        /// </summary>
        public async void LoadPreviousAsync() {
            if (this.disposed == true) {
                throw new ObjectDisposedException(nameof(TweetService));
            }
            try {
                this.RaiseAsyncStarted();
                var device = await this.deviceRepository.RegisterAsync();
                var maxTweet = this.viewModel.Tweets.LastOrDefault();
                var maxId = (maxTweet == null) ? null : maxTweet.StatusId;
                var tweets = await this.tweetRepository.SearchAsync(
                    deviceId: device.DeviceId.ToString(),
                    deviceKey: device.DeviceKey,
                    maxId: maxId);
                if (tweets != null) {
                    var config = new MapperConfiguration(x => x.CreateMap<Tweet, TweetViewModel>());
                    var mapper = config.CreateMapper();
                    foreach (var tweet in tweets.OrderByDescending(x => x.CreatedAt)) {
                        if (this.viewModel.Tweets.Any(x => x.StatusId == tweet.StatusId) != true) {
                            this.viewModel.Tweets.Add(mapper.Map<TweetViewModel>(tweet));
                        }
                    }
                }
                this.RaiseAsyncCompleted();
            } catch (Exception ex) {
                this.RaiseAsyncCompleted(ex);
            }
        }

        /// <summary>
        /// 現在のインスタンスで使用されているリソースを解放します。
        /// </summary>
        /// <param name="disposing">
        /// アンマネージ リソースとマネージ リソースの両方を解放する場合は true。アンマネージ
        /// リソースのみ解放する場合は false。
        /// </param>
        protected override void Dispose(bool disposing) {
            if (this.disposed != true) {
                if (disposing == true) {
                    this.viewModel = null;
                }
            }
            this.disposed = true;
        }

    }

}
