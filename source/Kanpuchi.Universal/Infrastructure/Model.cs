using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Kanpuchi.Infrastructure {

    /// <summary>
    /// データ モデルの基本機能を提供します。
    /// </summary>
    [DataContract()]
    public abstract class Model {

        /// <summary>
        /// <see cref="Karamem0.Kanpuchi.Infrastructure.Model"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        protected Model() { }

        /// <summary>
        /// 現在のインスタンスの文字列表現を返します。
        /// </summary>
        /// <returns>現在のインスタンスの文字列表現を示す <see cref="System.String"/>。</returns>
        public override string ToString() {
            using (var stream = new MemoryStream()) {
                var serializer = new JsonSerializer();
                using (var writer = new JsonTextWriter(new StreamWriter(stream))) {
                    serializer.Serialize(writer, this);
                }
                var buffer = stream.ToArray();
                return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            }

        }

    }

}
