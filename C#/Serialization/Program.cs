using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializationTest {
    class Program {
        static void Main(string[] args) {
            QuickStart.Test();
            ObjDeepClone.Test();
            SerializeSingleton.Test();
            ControlledByAttribute.Test();
            ControlledByISerializable.Test();
            SurrogateSelectors.Test();
            SerializationBinders.Test();
            Console.ReadKey();
        }
    }
}
