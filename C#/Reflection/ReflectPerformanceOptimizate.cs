using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ReflectionTest {
    class ReflectPerformanceOptimizate {
        public static void Test() {
            Type t = typeof(MyType);
            Object obj = Activator.CreateInstance(t);

            // 反射调用方法:
            // 1.缺失编译时的类型安全检查
            // 2.调用时性能低下
            MethodInfo mi = t.GetMethod("Calc");
            Object v = mi.Invoke(obj, new Object[] { 1, 2 });

            // 让类型继承(编译时类型已知的)基类
            MyTypeBase objBase = (MyTypeBase)obj;
            Int32 v2 = objBase.Calc(1, 2);

            // 让类型实现(编译时类型已知的)接口
            ICalc objInterface = (ICalc)obj;
            Int32 v3 = objInterface.Calc(1, 2);
        }

        #region 测试
        class MyType : MyTypeBase {
            public override Int32 Calc(Int32 x, Int32 y) { return x + y; }
        }

        abstract class MyTypeBase : ICalc {
            public virtual Int32 Calc(Int32 x, Int32 y) { return -1; }
        }

        interface ICalc {
            Int32 Calc(Int32 x, Int32 y);
        }
        #endregion
    }
}
