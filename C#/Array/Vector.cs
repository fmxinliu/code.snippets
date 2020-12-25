using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections;

namespace ArrayTest {
    /// <summary>
    /// 一维数组，又叫“向量”
    /// </summary>
    class Vector {
        public static void Test() {
            CreateArrayUseInitializer();
        }

        /// <summary>
        /// 使用“数组初始化器”创建并初始化数组
        /// </summary>
        private static void CreateArrayUseInitializer() {
            String[] sArray11 = new String[] { "你好", "Hello" };
            String[] sArray12 = new[] { "你好", "Hello" };

            // 隐式类型的局部变量
            var sArray21 = new String[] { "你好", "Hello" };
            var sArray22 = new[] { "你好", "Hello" };

            String[] sArray31 = { "你好", "Hello" };
            //var sArray32 = { "你好", "Hello" }; // Error

            Debug.Assert(ArrayEquals(sArray11, sArray12), "数组不相等");
            Debug.Assert(ArrayEquals(sArray11, sArray21), "数组不相等");
            Debug.Assert(ArrayEquals(sArray21, sArray22), "数组不相等");
            Debug.Assert(ArrayEquals(sArray11, sArray31), "数组不相等");
            //Debug.Assert(ArrayEquals(sArray11, new Int32[] { 1, 2 }), "数组不相等");
            if (!ArrayEquals(sArray11, new Int32[] { 1, 2 })) {
                Console.WriteLine("数组不相等");
            }
        }

        #region 判断数组相等
        /// <summary>
        /// 比较数组元素（类型相同）
        /// </summary>
        private static Boolean ArrayEquals<T>(T[] array1, T[] array2) {
            if (array1.GetType() != array2.GetType()) {
                return false;
            }
            if (array1.Length != array2.Length) {
                return false;
            }
            for (Int32 i = 0; i < array1.Length; ++i) {
                if (!array1[i].Equals(array2[i])) {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 比较数组元素（类型可以不相同）
        /// </summary>
        private static Boolean ArrayEquals(Array array1, Array array2) {
            if (array1.GetType() != array2.GetType()) {
                return false;
            }
            if (array1.Length != array2.Length) {
                return false;
            }
            for (Int32 i = 0; i < array1.Length; ++i) {
                if (!array1.GetValue(i).Equals(array2.GetValue(i))) {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
