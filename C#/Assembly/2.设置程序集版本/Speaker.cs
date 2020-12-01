using System;
using System.Reflection;

[assembly: AssemblyVersion("1.1.1.1")]

namespace AssemblyTest {
    public class Speaker {
        public static void Say() {
            Console.WriteLine("speaker 程序集版本：" + Assembly.GetExecutingAssembly().GetName().Version);
        }
    }
}
