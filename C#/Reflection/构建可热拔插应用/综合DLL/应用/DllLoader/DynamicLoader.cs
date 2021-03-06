using System;
using System.Reflection;
using RemoteLoader1 = Host.DllLoader_1.RemoteLoader;
using RemoteLoader2 = Host.DllLoader_2.RemoteLoader;
using RemoteLoader3 = Host.DllLoader_3.RemoteLoader;

namespace Host.DllLoader {
    sealed class DynamicLoader {
        private AppDomain appDomain;
        private RemoteLoader1 remoteLoader1;
        private RemoteLoader2 remoteLoader2;
        private RemoteLoader3 remoteLoader3;

        /// <summary>
        /// 插件加载器
        /// </summary>
        /// <param name="assemblyFile">托管插件path</param>
        /// <param name="className">托管插件类className</param>
        /// <param name="lpLibFileName">非托管插件path</param>
        public DynamicLoader(String assemblyFile, String className, String lpLibFileName) {
            this.appDomain = AppDomain.CreateDomain("DynamicLoaderDomain", null, null);
            String assemblyName = Assembly.GetExecutingAssembly().GetName().FullName;
            this.remoteLoader1 = Load<RemoteLoader1>(assemblyFile, className, assemblyName);
            this.remoteLoader2 = Load<RemoteLoader2>(assemblyFile, className, assemblyName);
            this.remoteLoader3 = Load<RemoteLoader3>(lpLibFileName, className, assemblyName);
        }

        ~DynamicLoader() {
            this.Unload();
        }

        #region 初始化插件域
        public void Unload() {
            if (this.appDomain != null) {
                this.remoteLoader1.Unload();
                this.remoteLoader2.Unload();
                this.remoteLoader3.Unload();
                AppDomain.Unload(this.appDomain);
                this.appDomain = null;
            }
        }

        private T Load<T>(String assemblyFile, String typeName, String assemblyName) where T : class {
            return (T)this.appDomain.CreateInstanceAndUnwrap(
                assemblyName,
                typeof(T).FullName,
                false,
                BindingFlags.Public | BindingFlags.Instance,
                null,
                new object[] { assemblyFile, typeName },
                null,
                null);
        }
        #endregion

        #region 托管DLL导出函数
        public void RunLib() {
            this.remoteLoader1.RunLib();
        }

        public String GetVersion() {
            return this.remoteLoader2.GetVersion();
        }
        #endregion

        #region 非托管DLL导出函数
        public Int32 AddInt32(Int32 a, Int32 b) {
            return this.remoteLoader3.AddInt32(a, b);
        }
        #endregion
    }
}
