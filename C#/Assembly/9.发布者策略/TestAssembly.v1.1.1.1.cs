using System;
using System.Reflection;

[assembly: AssemblyVersion("1.1.1.1")]

namespace PublisherPolicyRedirectAssemblyTest {
    public static class PublisherPolicyAssembly {
        public static string Version {
            get { return Assembly.GetExecutingAssembly().GetName().ToString(); }
        }
    }
}
