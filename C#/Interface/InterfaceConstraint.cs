using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceTest {
    class InterfaceConstraint {
        public static void Test() {
            Int32 x = 5;
            Guid g = new Guid();
            M(x); // OK，Int32同时实现了IComparable和IConvertible
            //M(g); // Error, Guid 只实现了IComparable，没有实现IConvertible
        }

        // 必须实现所有约束
        internal static void M<T>(T t) where T : IComparable, IConvertible {
        }
    }
}
