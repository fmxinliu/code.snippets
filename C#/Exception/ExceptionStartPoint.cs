using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExceptionTest {
    class ExceptionStartPoint {
        public static void Test() {
            TryCatchException1();
            TryCatchException2();
            TryCatchException3();
            TryCatchException4();
        }

        /// <summary>
        /// catch 块中， throw xxx 向上层抛出异常，windows会重置栈的起点!!!
        /// </summary>
        private static void TryCatchException1() {
            try {
                try {
                    int i = 0;
                    int j = 10 / i; // #1 异常点
                }
                catch (DivideByZeroException e) {
                    Console.WriteLine("catch块(第1层)： 捕获到DivideByZeroException，throw e");
                    Console.WriteLine(e.StackTrace); // #2 捕捉到异常，异常点位于 #1
                    throw e; // #3
                }
            }
            catch (DivideByZeroException e) {
                Console.WriteLine("catch块(第2层)： 捕获到DivideByZeroException");
                Console.WriteLine(e.StackTrace); // 捕捉到异常，异常点位于 #3
            }
            Console.WriteLine();
        }

        /// <summary>
        /// catch 块中， throw 重新抛出异常对象，windows会重置栈的起点!!!
        /// </summary>
        private static void TryCatchException2() {
            try {
                try {
                    int i = 0;
                    int j = 10 / i; // #1
                }
                catch (DivideByZeroException e) {
                    Console.WriteLine("catch块(第1层)： 捕获到DivideByZeroException，throw");
                    Console.WriteLine(e.StackTrace); // #2 捕捉到异常，异常点位于 #1
                    throw; // #3
                }
            }
            catch (DivideByZeroException e) {
                Console.WriteLine("catch块(第2层)： 捕获到DivideByZeroException");
                Console.WriteLine(e.StackTrace); // 捕捉到异常，异常点位于 #3
            }
            Console.WriteLine();
        }

        /// <summary>
        /// 第1层try块抛出异常后，在finally块检测到并执行一些操作，但不抛出也不重新抛出异常，让第2层捕捉，这样就不会重置栈的起点!!!
        /// </summary>
        private static void TryCatchException3() {
            Boolean trySuccess = false;
            try {
                try {
                    int i = 0;
                    int j = 10 / i; // #1
                    trySuccess = true;
                }
                finally {
                    if (!trySuccess) {
                        Console.WriteLine("finally块(第1层)： 捕获到DivideByZeroException");
                    }
                }
            }
            catch (DivideByZeroException e) {
                Console.WriteLine("catch块(第2层)： 捕获到DivideByZeroException");
                Console.WriteLine(e.StackTrace); // 捕捉到异常，异常点位于 #1
            }
            Console.WriteLine();
        }

        /// <summary>
        /// catch 块中，包装抛出新异常。第2层捕获到异常后，可以通过内层异常来获取原始的异常点
        /// </summary>
        private static void TryCatchException4() {
            try {
                try {
                    int i = 0;
                    int j = 10 / i; // #1 异常点
                }
                catch (DivideByZeroException e) {
                    Console.WriteLine("catch块(第1层)： 捕获到DivideByZeroException，throw e");
                    Console.WriteLine(e.StackTrace); // #2 捕捉到异常，异常点位于 #1
                    throw new Exception("抛出新异常", e); // #3 包装抛出新的异常
                }
            }
            catch (Exception e) {
                Console.WriteLine("catch块(第2层)： 捕获到Exception");
                Console.WriteLine(e.StackTrace); // 捕捉到异常，异常点位于 #3
                if (e.InnerException != null) {
                    Console.WriteLine(e.InnerException.StackTrace); // 内部异常点位于 #1
                }
            }
            Console.WriteLine();
        }
    }
}
