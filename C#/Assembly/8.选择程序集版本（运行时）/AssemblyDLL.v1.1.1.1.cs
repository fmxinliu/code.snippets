using System;
using System.Reflection;

[assembly: AssemblyVersion("1.1.1.1")]

namespace RedirectAssemblyTest {
    public static class AssemblyDLL {
        public static string Version {
            get { return Assembly.GetExecutingAssembly().GetName().ToString(); }
        }
    }
}
