using System;
using System.Reflection;

namespace Host.DllLoader {
    sealed class DynamicLoader {
        private AppDomain appDomain;
        private RemoteLoader remoteLoader;

        public DynamicLoader(String assemblyFile, String typeName) {
            this.appDomain = AppDomain.CreateDomain("DynamicLoaderDomain", null, null);
            String name = Assembly.GetExecutingAssembly().GetName().FullName;
            this.remoteLoader = (RemoteLoader)this.appDomain.CreateInstanceAndUnwrap(
                name,
                typeof(RemoteLoader).FullName,
                false,
                BindingFlags.Public | BindingFlags.Instance,
                null,
                new object[] { assemblyFile, typeName },
                null,
                null);
        }

        ~DynamicLoader() {
            this.Unload();
        }

        public void Unload() {
            if (this.appDomain != null) {
                this.remoteLoader = null;
                AppDomain.Unload(this.appDomain);
                this.appDomain = null;
            }
        }

        public void RunLib() {
            this.remoteLoader.RunLib();
        }

        public String GetVersion() {
            return this.remoteLoader.GetVersion();
        }

        public Int32 AddInt32(Int32 a, Int32 b) {
            return this.remoteLoader.AddInt32(a, b);
        }
    }
}
