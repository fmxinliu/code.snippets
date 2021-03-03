using System;
using SDK;

namespace PlugIn {
    public class AddIn_B : IAddIn {
        public String DoSomething(Int32 x) {
            return "AddIn_B: " + (x * 2).ToString();
        }
    }
}
