/**********************************************************************
 * 文 件 名: DllProxy.cs   
 *
 * 功能描述: 动态加载非托管 DLL
 * ********************************************************************/
namespace DllOperator {
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class DllProxy {
        /// <summary>
        /// DLL模块句柄
        /// </summary>
        private IntPtr hModule = IntPtr.Zero;

        /// <summary>
        /// DLL路径
        /// </summary>
        private String lpLibFileName = String.Empty;

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
        /// 装载 DLL
        /// </summary>
        public void LoadDll(String lpLibFileName) {
            if (IntPtr.Zero != this.hModule) {
                this.UnloadDll();
            }
            // 添加环境变量
            AddDirToEnvironmentVariable(lpLibFileName);
            this.hModule = LoadLibrary(lpLibFileName);
            if (IntPtr.Zero == this.hModule) {
                throw (new Exception(lpLibFileName + " Import Fail"));
            }

            this.lpLibFileName = Path.GetFullPath(lpLibFileName);
        }

        /// <summary>
        /// 装载 DLL
        /// </summary>
        public void LoadDllEx(String lpLibFileName, Flags flag = Flags.LOAD_WITH_ALTERED_SEARCH_PATH) {
            if (IntPtr.Zero != this.hModule) {
                this.UnloadDll();
            }
            this.hModule = LoadLibraryEx(lpLibFileName, IntPtr.Zero, (Int32)flag);
            if (IntPtr.Zero == this.hModule) {
                throw (new Exception(lpLibFileName + " Import Fail"));
            }

            this.lpLibFileName = Path.GetFullPath(lpLibFileName);
        }

        /// <summary>
        /// 卸载 DLL
        /// </summary>
        public void UnloadDll() {
            FreeLibrary(this.hModule);
            this.hModule = IntPtr.Zero;
            RemoveDirToEnvironmentVariable(this.lpLibFileName);
        }

        /// <summary>
        /// 将非托管函数指针转换为委托
        /// </summary>
        /// <param name="apiName">函数名</param>
        /// <param name="t">委托类型</param>
        /// <returns>委托</returns>
        public Delegate GetFuncAddress(String apiName, Type t) {
            if (IntPtr.Zero == this.hModule) {
                throw (new Exception("Can't find Module, please import *.dll first"));
            }

            IntPtr api = GetProcAddress(this.hModule, apiName);
            if (IntPtr.Zero == api) {
                throw (new Exception("Can't find " + apiName + "() Entry Address"));
            }

            return Marshal.GetDelegateForFunctionPointer(api, t);
        }

        /// <summary>
        /// 将 Dll 所在目录加入环境变量
        /// </summary>
        public void AddDirToEnvironmentVariable(String lpLibFileName) {
            var directorypath = Path.GetDirectoryName(lpLibFileName);
            var absolutepath = Path.GetFullPath(directorypath);
            if (absolutepath.EndsWith(@"\")) {
                absolutepath = absolutepath.Substring(0, absolutepath.Length - 1);
            }

            var pathstr = Environment.GetEnvironmentVariable("Path");
            var pathArray = pathstr.Split(';');

            var findpath1 = absolutepath;
            var findpath2 = absolutepath + @"\";
            if ((Array.IndexOf(pathArray, findpath1) == -1) &&
                (Array.IndexOf(pathArray, findpath2) == -1)) {
                Environment.SetEnvironmentVariable("Path", pathstr + ";" + absolutepath);
            }
        }

        /// <summary>
        /// 从环境变量中移除 Dll 所在目录
        /// </summary>
        public void RemoveDirToEnvironmentVariable(String lpLibFileName) {
            var directorypath = Path.GetDirectoryName(lpLibFileName);
            var absolutepath = Path.GetFullPath(directorypath);
            if (absolutepath.EndsWith(@"\")) {
                absolutepath = absolutepath.Substring(0, absolutepath.Length - 1);
            }

            var pathstr = Environment.GetEnvironmentVariable("Path");
            var pathList = pathstr.Split(';').ToList();

            int removeNum = 0;
            var findpath1 = absolutepath;
            var findpath2 = absolutepath + @"\";
            removeNum += pathList.RemoveAll(s => s.Equals(findpath1));
            removeNum += pathList.RemoveAll(s => s.Equals(findpath2));

            if (removeNum != 0) {
                var pathStr2 = String.Join(";", pathList);
                Environment.SetEnvironmentVariable("Path", pathStr2);
            }
        }
    }
}
