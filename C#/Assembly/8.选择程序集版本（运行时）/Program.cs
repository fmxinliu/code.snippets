using System;

namespace AssemblyTest {
    public class Program {
        public static void Main() {
            Console.WriteLine(RedirectAssemblyTest.AssemblyDLL.Version);
            Console.ReadKey();
        }
    }
}
