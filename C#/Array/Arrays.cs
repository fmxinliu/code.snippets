using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArrayTest {
    class Arrays {
        public static void Test() {
            CreateArrayInstance();
        }

        /// <summary>
        /// 创建数组的多种方法
        /// </summary>
        private static void CreateArrayInstance() {
            Array a; // 数组基类
            Int32 lowerBound; // 起始下标

            // 创建一维的0基数组，不包含任何元素
            a = new String[0];
            Console.WriteLine(a.GetType()); // System.String[]
            lowerBound = a.GetLowerBound(0); // 0

            // 创建一维的0基数组，不包含任何元素
            a = Array.CreateInstance(typeof(String), new Int32[] { 0 }, new Int32[] { 0 });
            Console.WriteLine(a.GetType()); // System.String[]
            lowerBound = a.GetLowerBound(0); // 0

            // 创建一维的1基数组，不包含任何元素
            a = Array.CreateInstance(typeof(String), new Int32[] { 0 }, new Int32[] { 1 });
            Console.WriteLine(a.GetType()); // System.String[*]
            lowerBound = a.GetLowerBound(0); // 1

            Console.WriteLine();

            // 创建二维的0基数组，不包含任何元素
            a = new String[0, 0];
            Console.WriteLine(a.GetType()); // System.String[,]
            lowerBound = a.GetLowerBound(0); // 0

            // 创建二维的0基数组，不包含任何元素
            a = Array.CreateInstance(typeof(String), new Int32[] { 0, 0 }, new Int32[] { 0, 0 });
            Console.WriteLine(a.GetType()); // System.String[,]
            lowerBound = a.GetLowerBound(0); // 0

            // 创建二维的1基数组，不包含任何元素
            a = Array.CreateInstance(typeof(String), new Int32[] { 0, 0 }, new Int32[] { 1, 1 });
            Console.WriteLine(a.GetType()); // System.String[,]
            lowerBound = a.GetLowerBound(1); // 1
        }
    }
}
