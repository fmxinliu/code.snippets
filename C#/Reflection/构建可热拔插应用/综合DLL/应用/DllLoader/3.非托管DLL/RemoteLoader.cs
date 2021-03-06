using System;
using DllOperator;
using Host.DllLoader;

namespace Host.DllLoader_3 {
    /// <summary>
    /// 加载并导出托管 DLL 中的函数
    /// </summary>
    sealed class RemoteLoader : MarshalByRefObject, IDllLoader {
        private DllProxy proxy = new DllProxy();

        public RemoteLoader(String lpLibFileName, String typeName = null) {
            Load(lpLibFileName, typeName);
        }

        #region DLL初始化
        public Object Load(String assemblyFile, String typeName) {
            proxy.LoadDll(assemblyFile);
            return null;
        }

        public void Unload() {
            proxy.UnloadDll();
        }
        #endregion

        #region 函数导出
        public Int32 AddInt32(Int32 a, Int32 b) {
            var funcAdd = (Add)proxy.Invoke("Add", typeof(Add));
            return funcAdd(a, b); // #必须是__stdcall方式导出的非托管DLL
        }
        #endregion
    }
}
