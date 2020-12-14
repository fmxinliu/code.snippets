using System;
using System.Reflection;

[assembly: AssemblyVersion("2.2.2.2")]

namespace AssemblyLib {
    public class Speaker {
        public static void Say() {
            var ver = Assembly.GetExecutingAssembly().GetName().Version;
            Console.WriteLine("我是被黑客劫持侵入的程序，程序集版本号：" + ver);
        }
    }
}
