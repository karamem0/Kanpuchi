using Kanpuchi.Extensions;
using Kanpuchi.Infrastructure;
using Kanpuchi.Repositories;
using Kanpuchi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanpuchi.Services {

    /// <summary>
    /// ツイートを管理するためのサービスを表します。
    /// </summary>
    public class TweetService : Service {

        /// <summary>
        /// ビュー モデルを表します。
        /// </summary>
        private MainViewModel viewModel;

        /// <summary>
        /// デバイスを格納するリポジトリを表します。
        /// </summary>
        private DeviceRepository deviceRepository;

        /// <summary>
        /// ツイートのコレクションを格納するリポジトリを表します。
        /// </summary>
        private TweetRepository tweetRepository;

        /// <summary>
        /// <see cref="Kanpuchi.Services.TweetService"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        private TweetService() {
            this.deviceRepository = new DeviceRepository();
            this.tweetRepository = new TweetRepository();
        }

        /// <summary>
        /// <see cref="Kanpuchi.Services.TweetService"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="viewModel"><see cref="Kanpuchi.ViewModels.MainViewModel"/>。</param>
        public TweetService(MainViewModel viewModel)
            : this() {
            this.viewModel = viewModel;
        }

        /// <summary>
        /// 最新のツイートを読み込みます。
        /// </summary>
        public async void LoadLatestAsync() {
            try {
                this.RaiseAsyncStarted();
                var device = await this.deviceRepository.RegisterAsync();
                var tweets = await this.tweetRepository.SearchAsync(
                    deviceId: device.DeviceId.ToString(),
                    deviceKey: device.DeviceKey);
                if (tweets != null) {
                    this.viewModel.Tweets.InsertRangeIf(0, tweets.OrderByDescending(x => x.StatusId), x => x.StatusId);
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
                    this.viewModel.Tweets.AddRangeIf(tweets.OrderByDescending(x => x.StatusId), x => x.StatusId);
                }
                this.RaiseAsyncCompleted();
            } catch (Exception ex) {
                this.RaiseAsyncCompleted(ex);
            }
        }

    }

}
