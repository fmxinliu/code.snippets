using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnumTest {
    class EnumUtils {
        public static void Test() {
            Type t0 = typeof(Color);

            // 1.枚举是值类型
            Object o = Color.Yellow; // #装箱

            // 2.枚举基础类型（容纳值的类型，默认Int32，可指定）
            // 注意与基元类型（语言支持，编译器可直接识别）的区别
            Type t1 = Enum.GetUnderlyingType(t0);
            Console.WriteLine("枚举类型：{0}", t0);
            Console.WriteLine("基础类型：{0}", t1);

            // 3.枚举定义符号、值
            String[] names = Enum.GetNames(t0);
            Color[] colors = GetEnumValues<Color>();

            //String.Format("Number of symbols defined: {0}", colors.Length); // #装箱
            //Console.WriteLine("Number of symbols defined: " + colors.Length); // #装箱
            //Console.WriteLine("Number of symbols defined: {0}", colors.Length); // #装箱
            Console.Write("\nNumber of symbols defined: {0}", colors.Length.ToString());
            Console.Write("\nValues\tSymbols\n-----\t------");
            foreach (Color c in colors) {
                Console.WriteLine("{0,5:D}\t{0:G}", c);
            }

            // 4.枚举常量值直接嵌入元数据中
            Color color = Color.Blue; // 对应的值2嵌入元数据中
            PrintEnumString(color);

            // 5.枚举赋值
            color = (Color)100; // 赋值后，值为100。但没有对应的枚举符号!!!
            Console.WriteLine("\ncolor=" + color.ToString());
            SetEnumValue(color);

            // 6.获取值对应的枚举符号
            GetEnumName<Int32>(t0, 66); // 不存在，返回null
            GetEnumName<Int32>(t0, 0);
        }

        // 找不到值对应的枚举定义，返回null
        private static String GetEnumName<T>(Type t, T o) {
            String s = Enum.GetName(t, o);
            Console.WriteLine(s);
            return s;
        }

        /// <summary>
        /// 获取枚举值数组
        /// </summary>
        private static TEnum[] GetEnumValues<TEnum>() where TEnum : struct {
            return (TEnum[])Enum.GetValues(typeof(TEnum));
        }

        /// <summary>
        /// 防御式设置枚举值
        /// </summary>
        private static void SetEnumValue(Color value) {
            Object o = value.ToString(); // 避免枚举类型装箱
            if (!Enum.IsDefined(value.GetType(), o)) {
                Console.WriteLine("ERR: {0} is not defined in Color.", o);
                //throw (new ArgumentOutOfRangeException("value", value, "无效的枚举值"));
            }
            else {
                Console.WriteLine("== set Color value success.", o);
            }
        }

        /// <summary>
        /// 打印枚举类型
        /// </summary>
        private static void PrintEnumString(Color value) {
            Console.Write("\n----print----\n");
            Console.WriteLine(value);
            Console.WriteLine(value.ToString());
            Console.WriteLine(value.ToString("G")); // 常用格式
            Console.WriteLine(value.ToString("X")); // 16进制格式（位数由基础类型决定）
            Console.WriteLine(value.ToString("x")); // 总是输出16进制大写字母
            Console.WriteLine(Enum.Format(value.GetType(), (Byte)0xff, "G")); // #装箱
        }
    }

    // 值类型
    enum Color : byte {
        Red,
        Green,
        Blue = 2,
        White,
        Yellow = 0xAA,
        None = 0xFF
    }
}
