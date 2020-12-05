using System;
using System.Reflection;

[assembly: AssemblyVersion("1.1.1.1")]

namespace FindAssemblyTest {
    public static class MyDLL {
        public static void Version() {
            var ver = Assembly.GetExecutingAssembly().GetName().Version;
            Console.WriteLine("==程序集版本号：" + ver);
        }
    }
}
