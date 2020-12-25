using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArrayTest {
    class Program {
        static void Main(string[] args) {
            Vector.Test();
            Arrays.Test();
            ArraysCopy.Test();
            ArraysAccess.Test();
            ArraysOnStack.Test();
            DynamicArrays.Test();
            Console.ReadKey();
        }
    }
}
