using System;
using System.Reflection;

[assembly: AssemblyVersion("1.1.1.1")]

namespace SearchOrderAssemblyTest {
    public static class AssemblyDLL {
        public static string Name {
            get { return "PrivatePath DLL"; }
        }
    }
}
