using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericTest {
    class GenericVariance {
        public static void Test() {
            //          fn1       fn2
            // 逆变量：Object <- String
            // 协变量：ArgumentException -> Exception
            MyFunc<Object, ArgumentException> fn1 = MyCallBack;
            MyFunc<String, Exception> fn2 = fn1; // 可以直接转换，不需显示指定
            fn1(123);
            fn2("666");
        }

        public static ArgumentException MyCallBack(Object o) {
            Console.WriteLine(o.ToString());
            return null;
        }

        //【interface 和 delegate】
        // 不变性：参数不变
        // 逆变性：输入性参数(in)，“基类类型”可用“派生类型”替换（类型兼容）
        // 协变性：输出性参数(out)，“派生类型”可用“基类类型”替换（类型兼容）
        public delegate TResult MyFunc<in T, out TResult>(T arg);
    }
}
