using Karamem0.Kanpuchi.Models;
using Karamem0.Kanpuchi.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

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
                using (var matomeService = new MatomeService()) {
                    matomeService.AddOrUpdateMatomeEntry();
                    matomeService.UpdateMatomeEntryThumbnail();
                }
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }

    }

}
