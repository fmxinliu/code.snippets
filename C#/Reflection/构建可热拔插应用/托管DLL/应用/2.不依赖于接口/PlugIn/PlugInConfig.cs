using System;
using System.IO;

namespace Host.PlugIn {
    sealed class PlugInConfig {
        private String assemblyFile;
        private String typeName;

        /// <summary>
        /// 插件全路径
        /// </summary>
        public String AssemblyFile {
            get { return this.assemblyFile; }
            set { this.assemblyFile = value; }
        }

        /// <summary>
        /// 插件完整类名
        /// </summary>
        public String TypeName {
            get { return this.typeName; }
            set { this.typeName = value; }
        }

        public String AssemblyDir {
            get { return Path.GetDirectoryName(this.AssemblyFile); }
        }

        public String AssemblyName {
            get { return Path.GetFileNameWithoutExtension(this.AssemblyFile); }
        }

        public String AssemblyExt {
            get { return Path.GetExtension(this.AssemblyFile); }
        }
    }
}
