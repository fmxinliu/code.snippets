using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;

namespace StringTest {
    class Strings {
        public static void Test() {
            // 1.System.String不可变的(immutable)
            String s = "I,love You";
            Console.WriteLine(s.Replace(',', ' ').ToUpper());
            Console.WriteLine(s);

            TestLiteralConcat1();
            TestLiteralConcat2();

            TestCultureInfo1();
            TestCultureInfo2();
        }

        /// <summary>
        /// 拼接“字面量字符串”，编译时直接嵌入到元数据中
        /// </summary>
        private static void TestLiteralConcat1() {
            String s = "Hello" + " " + "World"; // Hello World直接嵌入到元数据
            Console.WriteLine(s);
        }

        /// <summary>
        /// 拼接字符串中“含非字面量”字符串，运行时才会连接成字符串
        /// </summary>
        private static void TestLiteralConcat2() {
            String s = "Hello" + Environment.NewLine + "World"; // 运行时才会连接
            Console.WriteLine(s);
        }

        /// <summary>
        /// 拼接字符串中含“值类型”，值类型会装箱
        /// </summary>
        private static void TestValueBoxWhenConcat1() {
            String s = "Hello" + 123 + "World"; // 123会装箱
            Console.WriteLine(s);
        }

        /// <summary>
        /// 拼接字符串中含“值类型”，显示调用ToString()，不会装箱
        /// </summary>
        private static void TestValueBoxWhenConcat2() {
            String s = "Hello" + 123.ToString() + "World"; // 不会装箱
            Console.WriteLine(s);
        }

        /// <summary>
        /// 3 个实现 IFormatProvider 接口的类
        /// </summary>
        private static void TestCultureInfo1() {
            CultureInfo ci;
            ci = new CultureInfo("zh-CN"); // 中国
            ci = CultureInfo.CurrentCulture; // 当前线程
            ci = Thread.CurrentThread.CurrentCulture; // 当前线程

            NumberFormatInfo nfi;
            nfi = NumberFormatInfo.CurrentInfo;
            nfi = ci.GetFormat(typeof(NumberFormatInfo)) as NumberFormatInfo;

            DateTimeFormatInfo dtfi;
            dtfi = DateTimeFormatInfo.CurrentInfo;
            dtfi = ci.GetFormat(typeof(DateTimeFormatInfo)) as DateTimeFormatInfo;
        }

        /// <summary>
        /// 注意受语言文化影响的方法
        /// </summary>
        private static void TestCultureInfo2() {
            String s = "i love you";
            String s1 = s.ToUpperInvariant();
            //String s1 = s.ToUpper(CultureInfo.CurrentCulture);
            String s2 = s.ToUpper(new CultureInfo("tr-TR"));
            Console.WriteLine(s2);

            Int32 i1 = String.Compare(s, s2, StringComparison.CurrentCulture); // 用户显示使用
            Int32 i2 = String.Compare(s, s2, StringComparison.InvariantCulture); // 不推荐
            Int32 i3 = String.Compare(s, s2, StringComparison.Ordinal); // 最快（程序中使用）

            Int32 i4 = String.Compare(s, s2, StringComparison.CurrentCultureIgnoreCase);
            Int32 i5 = String.Compare(s, s2, StringComparison.InvariantCultureIgnoreCase);
            Int32 i6 = String.Compare(s, s2, StringComparison.OrdinalIgnoreCase);
        }
    }
}
