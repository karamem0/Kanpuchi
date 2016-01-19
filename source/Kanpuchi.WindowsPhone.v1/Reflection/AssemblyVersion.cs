using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Karamem0.Kanpuchi.Reflection {

    /// <summary>
    /// アセンブリのバージョン情報を取得します。
    /// </summary>
    public class AssemblyVersion {

        /// <summary>
        /// 現在実行中のアセンブリのバージョン情報を取得します。
        /// </summary>
        public static AssemblyVersion Current {
            get { return new AssemblyVersion(Assembly.GetExecutingAssembly()); }
        }

        /// <summary>
        /// アセンブリの詳細情報を表します。
        /// </summary>
        private AssemblyName assemblyName;

        /// <summary>
        /// バージョン情報を取得します。
        /// </summary>
        public string Version {
            get {
                return this.assemblyName.Version.ToString();
            }
        }

        /// <summary>
        /// <see cref="AssemblyVersion"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="assembly">アセンブリを示す <see cref="System.Reflection.Assembly"/>。</param>
        public AssemblyVersion(Assembly assembly) {
            this.assemblyName = new AssemblyName(assembly.FullName);
        }

    }

}
