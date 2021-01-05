using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GCTest {
    class GCTimerObjInAdvance {
        public static void Test() {
            TestStart("TEST1: 按任意键开始测试");
            TEST1();
            TestEnd("TEST1: 测试结束");

            TestStart("TEST2: 按任意键开始测试");
            TestEnd("TEST2: 按任意键结束");
            TEST2();
            TestEnd("TEST2: 测试结束");

            TestStart("TEST3: 按任意键开始测试");
            TestEnd("TEST3: 按任意键结束");
            TEST3();
            TestEnd("TEST3: 测试结束");

            TestStart("TEST4: 按任意键开始测试");
            TestEnd("TEST4: 按任意键结束");
            TEST4();
            TestEnd("TEST4: 测试结束");
        }

        /// <summary>
        /// case1: 方法退出后，引用t退栈，定时器对象“不可达”。只要GC触发了，都会销毁定时器
        /// </summary>
        private static void TEST1() {
            Timer t = new Timer(TimerCallback, null, 0, 1000);
        }

        /// <summary>
        /// case2: 方法未退出时，Debug下正常执行，Release下只执行一次，定时器对象被GC“提前回收”
        /// </summary>
        private static void TEST2() {
            Timer t = new Timer(TimerCallback, null, 0, 1000);
            Console.ReadKey(); // Release会优化代码，发现其他地方未使用t，t直接退栈，定时器对象就“不可达”了
        }

        /// <summary>
        /// case3: 同2
        /// </summary>
        private static void TEST3() {
            Timer t = new Timer(TimerCallback, null, 0, 1000);
            Console.ReadKey();
            t = null; // Release会优化代码，忽略这条语句，生成的IL同TEST2
        }

        /// <summary>
        /// case4: Release也能正常运行，不会被“提前回收”
        /// </summary>
        private static void TEST4() {
            Timer t = new Timer(TimerCallback, null, 0, 1000);
            Console.ReadKey();
            t.Dispose(); // Release不会优化这条语句
        }

        private static void TimerCallback(Object o) {
            Console.WriteLine("In TimerCallback: " + DateTime.Now.ToString());
            // 处于演示，强制执行一次垃圾回收
            GC.Collect();
        }

        private static void TestStart(String s) {
            Console.WriteLine(s);
            Console.ReadKey();
        }

        private static void TestEnd(String s) {
            Console.WriteLine(s);
        }
    }
}
