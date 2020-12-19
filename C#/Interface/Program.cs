using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceTest {
    class Program {
        static void Main(string[] args) {
            InterfaceImpl.Test();
            InterfaceEIMI.Test();
            InterfaceEIMI2.Test();
            InterfaceConstraint.Test();
            Console.ReadKey();
        }
    }
}
