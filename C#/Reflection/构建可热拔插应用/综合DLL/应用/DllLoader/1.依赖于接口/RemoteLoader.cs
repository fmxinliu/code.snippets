using System;
using System.Reflection;
using Host.DllLoader;

namespace Host.DllLoader_1 {
    /// <summary>
    /// 依赖于接口，加载并导出托管 DLL 中的函数
    /// </summary>
    sealed class RemoteLoader : MarshalByRefObject, IDllLoader {
        private SDK.IPlugIn obj;

        public RemoteLoader(String assemblyFile, String typeName) {
            obj = (SDK.IPlugIn)this.Load(assemblyFile, typeName);
        }

        #region DLL初始化
        public Object Load(String assemblyFile, String typeName) {
            var assembly = Assembly.LoadFrom(assemblyFile);
            var type = assembly.GetType(typeName);
            var obj = (SDK.IPlugIn)Activator.CreateInstance(type);
            return obj;
        }

        public void Unload() { }
        #endregion

        #region 函数导出
        public void RunLib() {
            obj.RunLib();
        }

        public String GetVersion() {
            return obj.GetVersion();
        }

        public Int32 AddInt32(Int32 a, Int32 b) {
            return obj.AddInt32(a, b);
        }
        #endregion
    }
}
