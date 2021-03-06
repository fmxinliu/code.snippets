using System;
using DllOperator;

namespace Host.DllLoader {
    sealed class RemoteLoader : MarshalByRefObject {
        private DllProxy proxy = new DllProxy();

        public RemoteLoader(String lpLibFileName) {
            proxy.LoadDllEx(lpLibFileName);
        }

        public Int32 AddInt32(Int32 a, Int32 b) {
            var funcAdd = (Add)proxy.GetFuncAddress("Add", typeof(Add));
            return funcAdd(a, b); // #必须是__stdcall方式导出的非托管DLL
        }

        public void Unload() {
            proxy.UnloadDll();
        }
    }
}
