using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppDomainTest {
    class Program {
        static void Main(string[] args) {
            MarshalBetweenAppDomains.Test();
            MarshalByRefType.TestMBROFieldsAccessPerf();
            Console.ReadKey();
        }
    }
}
