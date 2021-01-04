using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExceptionTest {
    class Program {
        static void Main(string[] args) {
            ExceptionSearchOrder.Test();
            ExceptionStartPoint.Test();
            CustomException.Test();
            CER.Test();
            Console.ReadKey();
        }
    }
}
