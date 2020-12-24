using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EnumTest {
    class ExtensionMethods {
        public static void Test() {
            Console.WriteLine("\n==使用扩展方法，为枚举类型添加方法==\n");
            FileAttributes fa = FileAttributes.System;
            fa = fa.Set(FileAttributes.ReadOnly);
            fa = fa.Clear(FileAttributes.System);
            fa.ForEach(f => Console.WriteLine(f));
        }
    }

    /// <summary>
    /// 使用扩展方法，为枚举类型添加方法
    /// </summary>
    internal static class FileAttributesExtensionMethods {
        public static Boolean IsSet(this FileAttributes flags, FileAttributes flagToTest) {
            if (flagToTest == 0) {
                throw new ArgumentOutOfRangeException("flagToTest", "Value must not be 0");
            }

            return (flags & flagToTest) == flagToTest;
        }

        public static Boolean IsClear(this FileAttributes flags, FileAttributes flagToTest) {
            return !IsSet(flags, flagToTest);
        }

        public static Boolean AnyFlagsSet(this FileAttributes flags, FileAttributes testFlags) {
            return (flags & testFlags) != 0;
        }

        public static FileAttributes Set(this FileAttributes flags, FileAttributes setFlags) {
            return flags | setFlags;
        }

        public static FileAttributes Clear(this FileAttributes flags, FileAttributes clearFlags) {
            return flags & ~clearFlags;
        }

        public static void ForEach(this FileAttributes flags, Action<FileAttributes> processFlags) {
            if (processFlags == null) {
                throw new ArgumentNullException("processFlags");
            }

            // FileAttributes 基础类型为 Int32，因此只需验证 32 位。
            //Type t = Enum.GetUnderlyingType(typeof(FileAttributes));
            for (UInt32 bit = 1; bit != 0; bit <<= 1) {
                UInt32 temp = ((UInt32)flags) & bit;
                if (temp != 0) {
                    processFlags((FileAttributes)temp);
                }
            }
        }
    }
}
