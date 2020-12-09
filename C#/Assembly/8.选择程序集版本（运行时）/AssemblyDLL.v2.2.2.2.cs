using System;
using System.Reflection;

[assembly: AssemblyVersion("2.2.2.2")]

namespace RedirectAssemblyTest {
    public static class AssemblyDLL {
        public static string Version {
            get { return Assembly.GetExecutingAssembly().GetName().ToString(); }
        }
    }
}
