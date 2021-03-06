using System;
using System.Reflection;

namespace Host.DllLoader {
    sealed class RemoteLoader : MarshalByRefObject {
        private SDK.IPlugIn obj;

        public RemoteLoader(String assemblyFile, String typeName) {
            var assembly = Assembly.LoadFrom(assemblyFile);
            var type = assembly.GetType(typeName);
            obj = (SDK.IPlugIn)Activator.CreateInstance(type);
        }

        public void RunLib() {
            obj.RunLib();
        }

        public String GetVersion() {
            return obj.GetVersion();
        }

        public Int32 AddInt32(Int32 a, Int32 b) {
            return obj.AddInt32(a, b);
        }
    }
}
