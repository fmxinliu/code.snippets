using System;
using System.Reflection;

[assembly: AssemblyVersion("1.1.1.1")]

namespace AssemblyTest {
    public class Speaker {
        public static void Say() {
            var ver = Assembly.GetExecutingAssembly().GetName().Version;
            Console.WriteLine("我是开发者编译的原始程序，程序集版本号：" + ver);
        }
    }
}
