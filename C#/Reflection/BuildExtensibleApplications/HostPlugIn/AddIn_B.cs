using System;
using HostSDK;

namespace HostPlugIn {
    public class AddIn_B : IAddIn {
        public String DoSomething(Int32 x) {
            return "AddIn_B: " + (x * 2).ToString();
        }
    }
}
