using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArrayTest {
    class ArraysAccess {
        public static void Test() {
            const Int32 elementCount = 3;

            // 二维数组
            Int32[,] a2Dim = new Int32[elementCount, elementCount] {
                { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 }
            };

            // 二维交错数组（向量构成的向量）
            Int32[][] aJagged = new Int32[elementCount][];
            for (Int32 x = 0; x < elementCount; ++x) {
                aJagged[x] = new Int32[elementCount] {
                    x * elementCount + 1, x * elementCount + 2, x * elementCount + 3 };
            }

            // 1: 用普通的安全技术访问数组中的所有元素
            Safe2DimArrayAccess(a2Dim);

            // 2: 用交错数组技术访问数组中的所有元素
            SafeJaggedArrayAccess(aJagged);

            // 3: 用不安全技术访问数组中的所有元素
            Unsafe2DimArrayAccess(a2Dim);
        }

        private static void Safe2DimArrayAccess<T>(T[,] a) {
            Console.WriteLine("1: 用普通的安全技术访问数组中的所有元素");
            for (Int32 x = 0; x < a.GetLength(0); ++x) {
                for (Int32 y = 0; y < a.GetLength(1); ++y) {
                    Console.Write(a[x, y].ToString() + ",");
                }
                Console.WriteLine();
            }
        }

        private static void SafeJaggedArrayAccess<T>(T[][] a) {
            Console.WriteLine("2: 用交错数组技术访问数组中的所有元素");
            for (Int32 x = 0; x < a.Length; ++x) {
                for (Int32 y = 0; y < a[x].Length; ++y) {
                    Console.Write(a[x][y].ToString() + ",");
                }
                Console.WriteLine();
            }
        }

        private static unsafe void Unsafe2DimArrayAccess(Int32[,] a) {
            Console.WriteLine("3: 用不安全技术访问数组中的所有元素");
            fixed (Int32* pi = a) {
                for (Int32 x = 0; x < a.Length; ++x) {
                    Console.Write(pi[x].ToString() + ",");
                    if ((x + 1) % a.GetLength(0) == 0) {
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
