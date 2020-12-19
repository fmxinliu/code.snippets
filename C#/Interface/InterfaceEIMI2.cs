using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceTest.InterfaceEIMI2Inner;

namespace InterfaceTest {
    class InterfaceEIMI2 {
        public static void Test() {
            Object o = new Object();
            SomeValueType v1 = new SomeValueType();
            Int32 n1 = v1.CompareTo(v1); // 装箱
            //Int32 n2 = v1.CompareTo(o); // 运行时抛出异常

            SomeValueTypeOptimize v2 = new SomeValueTypeOptimize();
            Int32 n3 = v2.CompareTo(v2); // 不会装箱
            //Int32 n4 = v2.CompareTo(o); // 编译时类型检查报错!!!
            //Int32 n5 = ((IComparable)v2).CompareTo(o); // 装箱 + 类型不安全
        }
    }

    namespace InterfaceEIMI2Inner {
        internal struct SomeValueType : IComparable {
            private Int32 x;
            public Int32 CompareTo(object obj) { // 值类型，装箱
                return (x - ((SomeValueType)obj).x); // 类型不安全，运行时无法转换，CLR会抛出异常
            }
        }

        internal struct SomeValueTypeOptimize : IComparable {
            private Int32 x;
            public Int32 CompareTo(SomeValueTypeOptimize obj) { // 值类型，不会装箱
                return (x - obj.x); // 类型安全，编译时会检查
            }

            Int32 IComparable.CompareTo(object obj) { // 值类型，装箱
                return this.CompareTo((SomeValueTypeOptimize)obj); // 类型不安全
            }
        }
    }
}
