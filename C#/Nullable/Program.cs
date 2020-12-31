using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullableTest {
    class Program {
        static void Main(string[] args) {
            NullableValueType.Test();
            NullableValueTypeOperators.Test();
            NullCoalescingOperator.Test();
            Console.ReadKey();
        }
    }
}
