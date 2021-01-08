using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace GCTest {
    class FreeNativeResources {
        public static void Test() {
            Console.WriteLine();
            Console.WriteLine("----------------------------");
            Console.WriteLine("场景: 创建临时文件，中途删除");
            Console.WriteLine("----------------------------");
            Console.WriteLine();
            Test1();
            Test2();
            Test3();
            Test4();
            Test5();
        }

        static void Test1() {
            Console.WriteLine("TEST1 - 临时文件，未关闭就删除，大概率会抛出异常");
            try {
                // 创建要写入临时文件的字节
                Byte[] bytesToWrite = new Byte[] { 1, 2, 3, 4, 5 };

                // 创建临时文件
                FileStream fs = new FileStream("test1.dat", FileMode.Create);

                // 将字节写入临时文件
                fs.Write(bytesToWrite, 0, bytesToWrite.Length);

                // 删除临时文件
                File.Delete("test1.dat");
                Console.WriteLine("删除成功");
            }
            catch (IOException e) {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine();
        }

        static void Test2() {
            Console.WriteLine("TEST2 - 临时文件，未关闭就删除，release有几率能删除成功");
            try {
                // 创建要写入临时文件的字节
                Byte[] bytesToWrite = new Byte[] { 1, 2, 3, 4, 5 };

                // 创建临时文件
                FileStream fs = new FileStream("test2.dat", FileMode.Create);

                // 将字节写入临时文件
                fs.Write(bytesToWrite, 0, bytesToWrite.Length);

                /// release下会优化代码，“提前回收”fs，并关闭打开的文件(句柄)
                GC.Collect();
                GC.WaitForPendingFinalizers();

                // 删除临时文件
                File.Delete("test2.dat");
                Console.WriteLine("删除成功");
            }
            catch (IOException e) {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine();
        }

        static void Test3() {
            Console.WriteLine("TEST3 - 主动释放打开的本地资源(临时文件句柄)，然后删除文件");

            // 创建要写入临时文件的字节
            Byte[] bytesToWrite = new Byte[] { 1, 2, 3, 4, 5 };

            // 创建临时文件
            FileStream fs = new FileStream("test3.dat", FileMode.Create);

            // 将字节写入临时文件
            fs.Write(bytesToWrite, 0, bytesToWrite.Length);

            /// 主动释放本地资源
            fs.Dispose();

            // 删除临时文件
            File.Delete("test3.dat");
            Console.WriteLine("删除成功\n");
        }

        static void Test4() {
            Console.WriteLine("TEST4 - TEST3更健壮的写法(确保写文件异常后，仍能释放资源)");

            // 创建要写入临时文件的字节
            Byte[] bytesToWrite = new Byte[] { 1, 2, 3, 4, 5 };

            FileStream fs = null;

            try {
                // 创建临时文件
                fs = new FileStream("test4.dat", FileMode.Create);

                // 将字节写入临时文件
                fs.Write(bytesToWrite, 0, bytesToWrite.Length);
            }
            finally {
                if (fs != null) {
                    fs.Dispose(); /// 主动释放本地资源
                }
            }

            // 删除临时文件
            File.Delete("test4.dat");
            Console.WriteLine("删除成功\n");
        }

        static void Test5() {
            Console.WriteLine("TEST5 - TEST4简化写法(using退出后，自动调用Dispose释放资源)");

            // 创建要写入临时文件的字节
            Byte[] bytesToWrite = new Byte[] { 1, 2, 3, 4, 5 };

            using (FileStream fs = new FileStream("test5.dat", FileMode.Create)) {
                // 将字节写入临时文件
                fs.Write(bytesToWrite, 0, bytesToWrite.Length);
            }

            // 删除临时文件
            File.Delete("test5.dat");
            Console.WriteLine("删除成功\n");
        }
    }
}
