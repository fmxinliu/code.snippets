using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericTest {
    class Program {
        static void Main(string[] args) {
            GenericType.Test();
            GenericNode.Test();
            GenericTypeNode.Test();
            GenericInstance.Test();
            GenericVariance.Test();
            GenericConstraint.Test();
            GenericMethod.Test();
            Console.ReadKey();
        }
    }
}
