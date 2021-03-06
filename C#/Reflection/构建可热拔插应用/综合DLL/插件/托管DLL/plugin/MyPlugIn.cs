using System;
using SDK;

namespace PlugIn {
    public class MyPlugIn : IPlugIn {
        public Int32 AddInt32(Int32 a, Int32 b) {
            return a + b;
        }

        public String GetVersion() {
            return "PlugIn v1.0";
        }

        public void RunLib() {
            Console.WriteLine("hello world");
        }
    }
}
