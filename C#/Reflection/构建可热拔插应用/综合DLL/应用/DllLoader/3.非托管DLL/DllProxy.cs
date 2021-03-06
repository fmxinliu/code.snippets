/**********************************************************************
 * 文 件 名: DllProxy.cs   
 *
 * 功能描述: 动态加载非托管 DLL
 * ********************************************************************/
namespace DllOperator {
    using System;
    using System.Runtime.InteropServices;

    public class DllProxy {
        /// <summary>  
        /// Loadlibrary 返回的函数库模块的句柄
        /// </summary>  
        private IntPtr hModule = IntPtr.Zero;

        [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern IntPtr LoadLibrary(String lpLibFileName);

        [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern IntPtr LoadLibraryEx(String lpLibFileName, IntPtr hFile, Int32 dwFlags);

        [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, String lpProcName);

        [DllImport("kernel32", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern bool FreeLibrary(IntPtr hModule);

        public enum Flags {
            DONT_RESOLVE_DLL_REFERENCES = 0x00000001,
            LOAD_LIBRARY_AS_DATAFILE = 0x00000002,
            LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008,
            LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,
            LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,
            LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,
            LOAD_LIBRARY_REQUIRE_SIGNED_TARGET = 0x00000080
        }

        ~DllProxy() {
            UnloadDll();
        }

        /// <summary>  
        /// 装载 Dll
        /// </summary>  
        public void LoadDll(String lpLibFileName) {
            if (IntPtr.Zero != this.hModule) {
                this.UnloadDll();
            }
            this.hModule = LoadLibrary(lpLibFileName);
            if (IntPtr.Zero == this.hModule) {
                throw (new Exception(lpLibFileName + " Import Fail"));
            }
        }

        /// <summary>  
        /// 装载 Dll
        /// </summary>
        public void LoadDllEx(String lpLibFileName, Flags flag = Flags.LOAD_WITH_ALTERED_SEARCH_PATH) {
            if (IntPtr.Zero != this.hModule) {
                this.UnloadDll();
            }
            this.hModule = LoadLibraryEx(lpLibFileName, IntPtr.Zero, (Int32)flag);
            if (IntPtr.Zero == this.hModule) {
                throw (new Exception(lpLibFileName + " Import Fail"));
            }
        }

        /// <summary>  
        /// 卸载 Dll  
        /// </summary>  
        public void UnloadDll() {
            FreeLibrary(this.hModule);
            this.hModule = IntPtr.Zero;
        }

        public Delegate Invoke(String apiName, Type t) {
            if (IntPtr.Zero == this.hModule) {
                throw (new Exception("Can't find Module, please import *.dll first"));
            }

            IntPtr api = GetProcAddress(this.hModule, apiName);
            if (IntPtr.Zero == api) {
                throw (new Exception("Can't find " + apiName + "() Entry Address"));
            }

            return Marshal.GetDelegateForFunctionPointer(api, t);
        }
    }
}
