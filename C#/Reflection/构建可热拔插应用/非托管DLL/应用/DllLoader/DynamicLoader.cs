using System;
using System.Reflection;

namespace Host.DllLoader {
    sealed class DynamicLoader {
        private AppDomain appDomain;
        private RemoteLoader remoteLoader;

        public DynamicLoader(String assemblyFile) {
            this.appDomain = AppDomain.CreateDomain("ApplicationLoaderDomain", null, null);
            String name = Assembly.GetExecutingAssembly().GetName().FullName;
            this.remoteLoader = (RemoteLoader)this.appDomain.CreateInstanceAndUnwrap(
                name,
                typeof(RemoteLoader).FullName,
                false,
                BindingFlags.Public | BindingFlags.Instance,
                null,
                new object[] { assemblyFile },
                null,
                null);
        }

        ~DynamicLoader() {
            this.Unload();
        }

        public void Unload() {
            if (this.appDomain != null) {
                remoteLoader.Unload(); // #必须卸载DLL
                AppDomain.Unload(this.appDomain);
                this.appDomain = null;
            }
        }

        public Int32 Add(Int32 a, Int32 b) {
            return this.remoteLoader.AddInt32(a, b);
        }
    }
}
