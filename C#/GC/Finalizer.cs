using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCTest {
    /// <summary>
    /// 定义终结器，一般是为了释放托管资源
    /// </summary>
    class Finalizer {
        public static void Test() {
            TestFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        /// <summary>
        /// 终结器按“继承链”调用: 3 > 2 > 1
        /// </summary>
        static void TestFinalizers() {
            Third t = new Third();
        }

        class First {
            ~First() {
                Console.WriteLine("First's finalizer is called."); // #1
            }
        }

        class Second : First {
            ~Second() {
                Console.WriteLine("Second's finalizer is called."); // #2
            }
        }

        class Third : Second {
            ~Third() {
                Console.WriteLine("Third's finalizer is called."); // #3
            }
        }
    }
}
