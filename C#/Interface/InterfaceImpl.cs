using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceTest.InterfaceImplInner;

namespace InterfaceTest {
    class InterfaceImpl {
        public static void Test() {
            Base b = new Derived();
            b.Dispose(); // 是否发生多态，取决于是否重写了基类的接口实现
            ((IDisposable)b).Dispose();
        }
    }

    namespace InterfaceImplInner {
        internal class Base : IDisposable {
#if OVERRIDE
            public virtual void Dispose() { // 实现接口时，可以指定virtual，然后被派生类重写
#else
            public void Dispose() {
#endif
                Console.WriteLine("Base's Dispose");
            }
        }

        internal class Derived : Base, IDisposable {
#if OVERRIDE
            public override void Dispose() {
#else
            public new void Dispose() {
#endif
                Console.WriteLine("Derived's Dispose");
            }
        }
    }
}
