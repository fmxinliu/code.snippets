using System;
using System.IO;
using Host.DllLoader;
using Host.PlugIn;

namespace Host {
    class Program {
        private DynamicLoader loader;
        private PlugInConfig plugInManaged;
        private PlugInConfig plugInUnmanaged;

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
            String basepath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);

            // #托管DLL路径
            plugInManaged = new PlugInConfig();
            plugInManaged.AssemblyFile = basepath
#if DEBUG
                + @"\plugins\pluginManaged.dll";
#else
                + @"\plugins\pluginManaged.dll";
#endif

            plugInManaged.TypeName = "PlugIn.MyPlugIn";

            // #非托管DLL路径
            plugInUnmanaged = new PlugInConfig();
            plugInUnmanaged.AssemblyFile = basepath
#if DEBUG
                + @"\plugins\pluginUnmanaged.dll";
#else
                + @"\plugins\pluginUnmanaged.dll";
#endif
        }

        bool CheckPlugInPath() {
            return this.CheckPlugInPath(plugInManaged) && CheckPlugInPath(plugInUnmanaged);
        }

        bool CheckPlugInPath(PlugInConfig plugin) {
            if (!File.Exists(plugin.AssemblyFile)) {
                Console.WriteLine("请编译插件工程：" + plugin.AssemblyName);
                return false;
            }
            return true;
        }

        void Invoke() {
            loader = new DynamicLoader(
                plugInManaged.AssemblyFile, plugInManaged.TypeName,
                plugInUnmanaged.AssemblyFile);

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
            TestDllUnload(plugInManaged);
            TestDllUnload(plugInUnmanaged);
        }

        void TestDllUnload(PlugInConfig plugin) {
            String assemblyFile = plugin.AssemblyFile;
            String assemblyBakFile = plugin.AssemblyFile + ".bak";
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
