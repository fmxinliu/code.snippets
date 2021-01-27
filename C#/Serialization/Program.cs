using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializationTest {
    class Program {
        static void Main(string[] args) {
            QuickStart.Test();
            ControlledByAttribute.Test();
            ControlledByISerializable.Test();
            Console.ReadKey();
        }
    }
}
