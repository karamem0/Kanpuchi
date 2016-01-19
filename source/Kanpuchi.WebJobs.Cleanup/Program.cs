using Karamem0.Kanpuchi.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi {

    /// <summary>
    /// アプリケーションのエントリ ポイントを定義します。
    /// </summary>
    public static class Program {

        /// <summary>
        /// プログラムを開始します。
        /// </summary>
        /// <param name="args">プログラムの引数を示す <see cref="System.String"/> 配列。</param>
        private static void Main(string[] args) {
            Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
            try {
                using (var tweetService = new TweetService())
                using (var matomeService = new MatomeService()) {
                    tweetService.RemoveTwitterStatus();
                    matomeService.RemoveMatomeEntry();
                }
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }

    }

}
