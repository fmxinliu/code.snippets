using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.ExceptionServices;
using System.Security;

namespace ExceptionTest {
    class CSECatch {
        public static void Test() {
            TestCatchAccessViolation11();
            TestCatchAccessViolation12();
            TestCatchAccessViolation21();
        }

        /// <summary>
        /// (方法一)捕获CSE: 添加特性HandleProcessCorruptedStateExceptions【.NET4.0之后】
        /// </summary>
        [SecurityCritical]
        [HandleProcessCorruptedStateExceptions]
        static void TestCatchAccessViolation11() {
            try {
                DoSomeAccessViolation1();
            }
            catch (AccessViolationException e) {
                Console.WriteLine("方法一：捕获到CSE异常: " + e.Message);
            }
        }

        /// <summary>
        /// (方法二)捕获CSE: 配置并使能legacyCorruptedStateExceptionsPolicy【.NET4.0之后】
        /// </summary>
        static void TestCatchAccessViolation12() {
            try {
                DoSomeAccessViolation1();
            }
            catch (AccessViolationException e) {
                Console.WriteLine("方法二：捕获到CSE异常: " + e.Message);
            }
        }

        /// <summary>
        /// 捕获用户代码抛出的(不被认为是CSE的)异常
        /// </summary>
        static void TestCatchAccessViolation21() {
            try {
                DoSomeAccessViolation2();
            }
            catch (AccessViolationException e) {
                Console.WriteLine("捕获到异常: " + e.Message);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// 系统: 抛出AccessViolationException，会被当做“损坏状态异常(CSE)”
        /// </summary>
        static void DoSomeAccessViolation1() {
            var ptr = new IntPtr(42);
            Marshal.StructureToPtr(42, ptr, true);
        }

        /// <summary>
        /// 用户代码: 抛出AccessViolationException，不会被当做CSE
        /// </summary>
        static void DoSomeAccessViolation2() {
            throw new AccessViolationException("用户代码抛出的AccessViolationException，不被认为是CSE");
        }
    }
}
