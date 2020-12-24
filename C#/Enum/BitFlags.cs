using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace EnumTest {
    /// <summary>
    /// 1.用“枚举”标识“位域”。
    /// 2.“枚举”可组合使用，打印时会尝试分解为位域的组合。
    /// 3.“位域”解析时，会尝试组合为多个枚举的组合。
    /// </summary>
    class BitFlags {
        public static void Test() {
            Test1();
            Test2();
            Test3();
            Test4();
            Test5();
        }

        private static void Test1() {
            String file = Assembly.GetEntryAssembly().Location;
            FileAttributes attributes = File.GetAttributes(file);
            Console.WriteLine("Is {0} hidden? {1}",file, (attributes & FileAttributes.Hidden) != 0); // #装箱

            File.SetAttributes(file, FileAttributes.ReadOnly | FileAttributes.Hidden);
            attributes = File.GetAttributes(file);
            Console.WriteLine("Is {0} hidden? {1}", file, (attributes & FileAttributes.Hidden) != 0); // #装箱

            File.SetAttributes(file, FileAttributes.Normal);
        }

        /// <summary>
        /// 使用FlagsAttribute特性，正确打印多个位标记的组合
        /// </summary>
        private static void Test2() {
            Actions actions = Actions.Read | Actions.Query;
            Console.WriteLine(actions.ToString()); // Read, Query

            // 完全不能分解为位域组合
            actions = (Actions)100;
            Console.WriteLine(actions.ToString()); // 100

            // 不能完全分解为位域组合
            actions = (Actions)101;
            Console.WriteLine(actions.ToString()); // 101

            actions = (Actions)(0);
            Console.WriteLine(actions.ToString()); // None

            actions = (Actions)(-1);
            Console.WriteLine(actions.ToString()); // -1

            actions = (Actions)(0x001F);
            Console.WriteLine(actions.ToString()); // All
        }

        /// <summary>
        /// 不使用FlagsAttribute特性，正确打印多个位标记的组合
        /// </summary>
        private static void Test3() {
            ActionsNoFlags actions = ActionsNoFlags.Read | ActionsNoFlags.Query;
            Console.WriteLine(actions.ToString()); // 9
            Console.WriteLine(actions.ToString("F")); // Read, Query
        }

        /// <summary>
        /// 多个位标记的组合，解析为枚举符号
        /// </summary>
        private static void Test4() {
            Actions actions = (Actions)Enum.Parse(typeof(Actions), "query", true);
            Console.WriteLine(actions.ToString()); // Query

            if (Enum.TryParse("Query,Read", false, out actions)) {
                Console.WriteLine(actions.ToString()); // Read, Query
            }

            actions = (Actions)Enum.Parse(typeof(Actions), "28");
            Console.WriteLine(actions.ToString()); // Delete, Query, Sync
        }

        /// <summary>
        /// 不要对位标志枚举类型使用IsDefined方法（精确匹配）
        /// </summary>
        private static void Test5() {
            Actions actions = Actions.Read | Actions.Query;
            Boolean b1 = Enum.IsDefined(typeof(Actions), actions.ToString());
        }
    }

    [Flags]
    internal enum Actions {
        None = 0,
        Read = 0x0001,
        Write = 0x0002,
        ReadWrite = Read | Write,
        Delete = 0x0004,
        Query = 0x0008,
        Sync = 0x0010,
        All = ReadWrite | Delete | Query | Sync, // 0x001F
    }

    internal enum ActionsNoFlags {
        None = 0,
        Read = 0x0001,
        Write = 0x0002,
        ReadWrite = Read | Write,
        Delete = 0x0004,
        Query = 0x0008,
        Sync = 0x0010,
        All = ReadWrite | Delete | Query | Sync, // 0x001F
    }
}
