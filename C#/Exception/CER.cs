using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace ExceptionTest {
    class CER {
        public static void Test() {
            Test1();
            try {
                Test2();
            }
            catch (OutOfMemoryException e) {
                Console.WriteLine("CER异常捕获: " + e.StackTrace);
            }
        }

        /// <summary>
        /// 先执行#1，再执行#2。当执行#2时抛出了异常，此时流程执行一半，状态可能被损坏!!!
        /// </summary>
        private static void Test1() {
            try {
                try {
                    Console.WriteLine("Start > "); // #1
                }
                finally {
                    StackOverflow(); // #2 执行到这里，发生异常
                }
            }
            catch (OutOfMemoryException e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        /// <summary>
        /// 指定CER后，执行#1之前会对#2做JIT编译，发现#2会抛出异常，便不会开始执行流程(try块)，避免了损坏状态!!!
        /// </summary>
        private static void Test2() {
            try {
                RuntimeHelpers.PrepareConstrainedRegions();
                try {
                    Console.WriteLine("Start > "); // #1
                }
                finally {
                    StackOverflowWithContract(); // #2 执行到这里，发生异常
                }
            }
            catch (OutOfMemoryException e) {
                Console.WriteLine(e.StackTrace); // 注意: 这里无法捕捉到异常!!!
            }
        }

        #region CER 测试
        unsafe struct Big {
            public fixed Byte Bytes[Int32.MaxValue]; // #会抛出内存不足异常
        }

        unsafe static void StackOverflow() {
            Big big;
            big.Bytes[Int32.MaxValue - 1] = 1;
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        unsafe static void StackOverflowWithContract() {
            Big big;
            big.Bytes[Int32.MaxValue - 1] = 1;
        }

        #endregion
    }
}
