using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArrayTest {
    /// <summary>
    /// 在栈上分配数组（只支持值类型，结构类型不能包含引用）
    /// </summary>
    class ArraysOnStack {
        public static void Test() {
            StackallocDemo();
            InlineArrayDemo();
        }

        private static unsafe void StackallocDemo() {
            const Int32 width = 20;
            Char* pc = stackalloc Char[width]; // 在栈上分配数组

            String s = "姓名：Jeff";

            for (Int32 index = 0; index < width; ++index) {
                pc[width - index - 1] =
                    (index < s.Length) ? s[index] : '.';
            }

            Console.WriteLine(new String(pc, 0, width));
        }

        private static unsafe void InlineArrayDemo() {
            CharArray ca; // 在栈上分配数组
            Int32 widthInBytes = sizeof(CharArray);
            Int32 width = widthInBytes / 2; // Char占 2 字节

            String s = "姓名：Jeff";

            for (Int32 index = 0; index < width; ++index) {
                ca.Characters[width - index - 1] =
                    (index < s.Length) ? s[index] : '.';
            }

            Console.WriteLine(new String(ca.Characters, 0, width));
        }

        /// <summary>
        /// 数组内联(嵌入)到结构中
        /// </summary>
        internal unsafe struct CharArray {
            public fixed Char Characters[20];
        }
    }
}
