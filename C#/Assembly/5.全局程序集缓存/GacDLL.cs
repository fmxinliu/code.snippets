using System;
using System.Reflection;

[assembly: AssemblyVersion("1.1.1.1")]

namespace AssemblyTest {
    public class GacDLL {
        public static void Version() {
            var ver = Assembly.GetExecutingAssembly().GetName().Version;
            Console.WriteLine("GAC安装的程序集版本号：" + ver);
        }
    }
}
