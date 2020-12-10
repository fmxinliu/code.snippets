using System;
using System.Reflection;

[assembly: AssemblyVersion("2.2.2.2")]

namespace PublisherPolicyRedirectAssemblyTest {
    public static class PublisherPolicyAssembly {
        public static string Version {
            get { return Assembly.GetExecutingAssembly().GetName().ToString(); }
        }
    }
}
