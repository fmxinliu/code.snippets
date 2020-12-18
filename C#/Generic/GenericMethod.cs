using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericTest {
    class GenericMethod {
        public static void Test() {
            Console.WriteLine("---");
            GenericCompare.ComparingAGenericTypeVariableWithNull<Int32>(123);
            GenericCompare.ComparingAGenericTypeVariableWithNull<String>(null);
            GenericCompare.ComparingAGenericTypeVariableWithNull<String>("hello");

            Console.WriteLine("---");
            GenericCompare.ComparingTwoGenericTypeVariables("123", "123");
            GenericCompare.ComparingTwoGenericTypeVariables("123", new String(new Char[] { '1', '2', '3' })); // 引用不相等

            Console.WriteLine("---");
            GenericInvoke.Display("Jeff"); // 优先选择类型明确的方法
            GenericInvoke.Display(123456); // 推断泛型参数类型
            GenericInvoke.Display<String>("hello world"); // 明确指定调用泛型方法
        }
    }

    sealed class GenericCompare {
        /// <summary>
        /// 泛型类型变量与 null 比较
        /// </summary>
        public static void ComparingAGenericTypeVariableWithNull<T>(T obj) {
            if (obj == null) { // 对于值类型，永远不满足
                Console.WriteLine("obj is null");
            }
            else {
                Console.WriteLine(obj.ToString() + " is not null");
            }
        }

        /// <summary>
        /// 比较两个泛型类型变量
        /// </summary>
        public static void ComparingTwoGenericTypeVariables<T>(T o1, T o2) where T : class {
            if (o1 == o2) { // “引用类型”可以直接比较
                Console.WriteLine("{0} = {1}", o1, o2);
            }
            else {
                Console.WriteLine("{0} != {1}", o1, o2);
            }
        }

        //public static void ComparingTwoGenericTypeVariables2<T>(T o1, T o2) where T : struct {
        //    if (o1 == o2) { // “非基元”“值类型”必须重载==操作符
        //        Console.WriteLine("{0} = {1}", o1, o2);
        //    }
        //    else {
        //        Console.WriteLine("{0} != {1}", o1, o2);
        //    }
        //}
    }

    sealed class GenericInvoke {
        public static void Display(String s) {
            Console.WriteLine(s);
        }

        public static void Display<T>(T t) {
            Console.Write("call Display<T>");
            Display(t.ToString());
        }
    }
}
