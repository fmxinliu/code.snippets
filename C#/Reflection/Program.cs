using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReflectionTest {
    class Program {
        static void Main(string[] args) {
            TestLibLoader.Run();
            LoadAssembly.Test();
            LoadAssemblyReflectType.Test();
            ReflectAssemblyMembers.Test();
            ReflectMembersThenInvoke.Test();
            CreateInstance.Test();
            Console.ReadKey();
        }
    }
}
