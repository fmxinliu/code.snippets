using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExceptionTest {
    class ExceptionSearchOrder {
        public static void Test() {
            Search1();
            Search2();
            Search3();
        }

        /// <summary>
        /// catch块重新抛出异常，finally块的语句仍会执行
        /// </summary>
        private static void Search1() {
            try {
                try {
                    CrashFunc();
                }
                catch {
                    Console.WriteLine("catch块: 重新抛出异常(第1层)");
                    throw;
                }
                finally {
                    Console.WriteLine("finally块(第1层)"); // 仍会执行
                }
            }
            catch { }
            Console.WriteLine();
        }

        /// <summary>
        /// finally块中抛出异常
        /// </summary>
        private static void Search2() {
            try {
                try {
                }
                finally {
                    Console.WriteLine("finally块: 抛出异常(第1层)");
                    CrashFunc();
                    Console.WriteLine("finally块: 异常点之后语句(第1层)"); // 不会执行
                }

                Console.WriteLine("try块: finall块之后的语句(第2层)"); // 不会执行
            }
            catch {
                Console.WriteLine("catch块: 捕捉finally块抛出的异常(第2层)");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// 回溯调用栈:
        /// 1.第1层抛出异常，没有查找到捕捉类型与抛出的异常的类型匹配的catch块。转去第2层插找
        /// 2.第2层也没有查找到。转去第3层插找
        /// 3.第3层找到了。转去执行内层所有的finally块中的语句
        /// 4.执行第1层的finally块
        /// 5.执行第2层的finally块
        /// 6.执行第3层的catch块
        /// 7.执行第3层的finally块（当第3层catch块没有抛出异常时）
        /// 8.执行第3层finally块执行的语句（当第3层finally块没有抛出异常时）
        /// </summary>
        private static void Search3() {
            try {
                try {
                    try {
                        Console.WriteLine("try块: 抛出异常(第1层)");
                        CrashFunc();
                    }
                    finally {
                        Console.WriteLine("finally块(第1层)");
                    }

                    Console.WriteLine("try块: finall块之后的语句(第2层)"); // 不会执行
                }
                finally {
                    Console.WriteLine("finally块(第2层)");
                }

                Console.WriteLine("try块: finall块之后的语句(第3层)"); // 不会执行
            }
            catch {
                Console.WriteLine("catch块(第3层)");
            }
            finally {
                Console.WriteLine("finally块(第3层)");
            }
            Console.WriteLine();
        }

        private static void CrashFunc() {
            int i = 0;
            int j = 5 / i;
        }
    }
}
