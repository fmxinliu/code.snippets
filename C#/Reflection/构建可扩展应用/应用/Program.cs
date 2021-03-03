using System;
using System.IO;
using System.Linq;
using System.Reflection;
using SDK;

/// <summary>
/// 依托公共类型，加载插件
/// </summary>
namespace Host {
    class Program {
        static void Main(String[] args) {
            // 插件地址
            String plugInDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
#if DEBUG
                + @"..\..\..\..\插件\bin\Debug";
#else
                + @"..\..\..\..\插件\bin\Release";
#endif
            if (!Directory.Exists(plugInDir)) {
                Console.WriteLine("请先编译插件工程：plugin");
                goto EXIT;
            }

            // 搜索插件DLL
            var plugInAssemblies = Directory.EnumerateFiles(plugInDir, "*.dll");

            // 加载插件，获取定义类型
            var plugInTypes =
                from file in plugInAssemblies
                let assembly = Assembly.LoadFrom(file)
                from type in assembly.GetExportedTypes()
                // 如果类型实现了IAddIn接口，该类型就可以由宿主使用
                where type.IsClass && typeof(IAddIn).IsAssignableFrom(type)
                select type;

            if (!(plugInAssemblies.Count() > 0 && plugInTypes.Count() > 0)) {
                Console.WriteLine("未发现可用的插件");
                goto EXIT;
            }

            // 初始化完成: 宿主已发现了所有可用的加载项
            // 下面演示宿主如何构造加载项对象并使用它们
            foreach (var type in plugInTypes) {
                IAddIn ai = (IAddIn)Activator.CreateInstance(type);
                Console.WriteLine(ai.DoSomething(5));
            }
EXIT:
            Console.ReadKey();
        }
    }
}
