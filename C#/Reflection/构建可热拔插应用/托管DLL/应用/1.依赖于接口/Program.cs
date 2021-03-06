using System;
using System.IO;
using Host.DllLoader;
using Host.PlugIn;

namespace Host {
    class Program {
        private DynamicLoader loader;
        private PlugInConfig plugIn;

        static void Main(string[] args) {
            Program p = new Program();
            p.InitPlugInPath();
            if (p.CheckPlugInPath()) {
                p.Invoke(); // 加载插件
                p.Unload(); // 卸载插件
            }
            Pause();
        }

        void InitPlugInPath() {
            plugIn = new PlugInConfig();
            plugIn.AssemblyFile = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)
#if DEBUG
                + @"\..\..\..\..\..\托管DLL\插件\bin\Debug\PlugIn.dll";
#else
                + @"\..\..\..\..\..\托管DLL\插件\bin\Release\PlugIn.dll";
#endif

            plugIn.TypeName = "PlugIn.MyPlugIn";
        }

        bool CheckPlugInPath() {
            if (!File.Exists(plugIn.AssemblyFile)) {
                Console.WriteLine("请先编译插件工程：" + plugIn.AssemblyName);
                return false;
            }
            return true;
        }

        void Invoke() {
            loader = new DynamicLoader(plugIn.AssemblyFile, plugIn.TypeName);

            // #1
            Console.WriteLine(loader.AddInt32(1, 2));

            // #2
            Console.WriteLine(loader.GetVersion());

            // #3
            loader.RunLib();
        }

        void Unload() {
            /// 卸载插件
            loader.Unload();
            //loader = null;
            //GC.Collect();
            //GC.WaitForFullGCComplete();
            //GC.WaitForPendingFinalizers();

            /// 测试是否卸载
            String assemblyFile = plugIn.AssemblyFile;
            String assemblyBakFile = plugIn.AssemblyFile + ".bak";
            File.Copy(assemblyFile, assemblyBakFile, true);
            File.Delete(assemblyFile);
            File.Copy(assemblyBakFile, assemblyFile);
            File.Delete(assemblyBakFile);
        }

        static void Pause() {
            Console.ReadKey();
        }
    }
}
