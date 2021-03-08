using System;

namespace PathTest {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("===================");
            Console.WriteLine("=== 相对路径测试 ==");
            Console.WriteLine("===================");
            TestRelativePath1();
            TestRelativePath2();
            TestRelativePath3();
            TestRelativePath4();
            TestRelativePath5();
            TestRelativePath6();

            Console.WriteLine("===================");
            Console.WriteLine("=== 绝对路径测试 ==");
            Console.WriteLine("===================");
            TestAbsolutePath1();
            TestAbsolutePath2();
            TestAbsolutePath3();
            TestAbsolutePath4();
            TestAbsolutePath5();
            TestAbsolutePath6();

            Console.WriteLine("===================");
            Console.WriteLine("= 标准化绝对路径 ==");
            Console.WriteLine("===================");
            TestAbsolutePath111();
            Console.ReadKey();
        }

        // 测试1: 路径是否以\结尾
        static void TestRelativePath1() {
            string p1;
            string p2;

            p1 = @"C:\A\B\C\D\E\F\G\H\";
            p2 = @"C:\A\B\M\N\";
            TestRelativePath(p1, p2);

            p1 = @"C:\A\B\C\D\E\F\G\H";
            p2 = @"C:\A\B\M\N\";
            TestRelativePath(p1, p2);

            p1 = @"C:\A\B\C\D\E\F\G\H\";
            p2 = @"C:\A\B\M\N";
            TestRelativePath(p1, p2);

            p1 = @"C:\A\B\C\D\E\F\G\H";
            p2 = @"C:\A\B\M\N";
            TestRelativePath(p1, p2);
        }

        // 测试2: 路径中混合获取分隔符
        static void TestRelativePath2() {
            string p1;
            string p2;

            p1 = @"C:\A\B\C\D\E\F\G/H\";
            p2 = @"C:\A\B\M\N\";
            TestRelativePath(p1, p2);

            p1 = @"C:/A\B\C\D\E\F\G\H";
            p2 = @"C:\A\B\M\N\";
            TestRelativePath(p1, p2);
        }

        // 测试3: 路径中包含不正确的分隔符
        static void TestRelativePath3() {
            string p1;
            string p2;

            try {
                p1 = @"C:\A\B\C\D\E\F\G\H\\";
                p2 = @"C:\A\B\M\N\";
                TestRelativePath(p1, p2);
            }
            catch (ArgumentException e) {
                Console.WriteLine(e.Message);
            }

            try {
                p1 = @"C:\A\B\C\D\E\F\G\H";
                p2 = @"C://A\B\M\N\";
                TestRelativePath(p1, p2);
            }
            catch (ArgumentException e) {
                Console.WriteLine(e.Message);
            }
        }

        // 测试4: 路径中包含中文
        static void TestRelativePath4() {
            string p1;
            string p2;

            p1 = @"C:\我的文档\B\C\D\E\F\G\H\";
            p2 = @"C:\我的文档\B\M\N\";
            TestRelativePath(p1, p2);

            p1 = @"C:\A\B\C\D\E\F\我的文档\H\";
            p2 = @"C:\A\B\M\N\";
            TestRelativePath(p1, p2);

            p1 = @"C:\A\B\C\D\E\F\G\H\";
            p2 = @"C:\A\B\我的文档\N\";
            TestRelativePath(p1, p2);
        }

        // 测试5: 路径中包含文件名
        static void TestRelativePath5() {
            string p1;
            string p2;

            p1 = @"C:\A\B\C\D\E\F\G\H\1.txt";
            p2 = @"C:\A\B\M\N\";
            Console.WriteLine(p1.GetRelativePath(p2, true, false));
            Console.WriteLine(p2.GetRelativePath(p1, false, true));

            p1 = @"C:\A\B\C\D\E\F\G\H\";
            p2 = @"C:\A\B\M\N\2.txt";
            Console.WriteLine(p1.GetRelativePath(p2, false, true));
            Console.WriteLine(p2.GetRelativePath(p1, true, false));

            p1 = @"C:\A\B\C\D\E\F\G\H\1.txt";
            p2 = @"C:\A\B\M\N\2.txt";
            Console.WriteLine(p1.GetRelativePath(p2, true, true));
            Console.WriteLine(p2.GetRelativePath(p1, true, true));
        }

        // 测试6: 参数传入相对路径
        static void TestRelativePath6() {
            String p1;
            String p2;
            p1 = @"C:\A\B\C\D\E\F\G\H\";
            p2 = @"..\..\..\X\Y\Z";
            try {
                Console.WriteLine(p2.GetRelativePath(p1));
            }
            catch (ArgumentException e) {
                Console.WriteLine(e.Message);
            }
        }

        static void TestRelativePath(String p1, String p2) {
            Console.WriteLine(p1.GetRelativePath(p2));
            Console.WriteLine(p2.GetRelativePath(p1));
        }


        // 测试1: 路径是否以\结尾
        static void TestAbsolutePath1() {
            string p1;
            string p2;

            p1 = @"C:\A\B\C\D\E\F\G\H\";
            p2 = @"..\..\..\X\Y\Z\";
            TestAbsolutePath(p1, p2);

            p1 = @"C:\A\B\C\D\E\F\G\H";
            p2 = @"..\..\..\X\Y\Z\";
            TestAbsolutePath(p1, p2);

            p1 = @"C:\A\B\C\D\E\F\G\H\";
            p2 = @"..\..\..\X\Y\Z";
            TestAbsolutePath(p1, p2);

            p1 = @"C:\A\B\C\D\E\F\G\H";
            p2 = @"..\..\..\X\Y\Z";
            TestAbsolutePath(p1, p2);
        }

        // 测试2: 路径中混合获取分隔符
        static void TestAbsolutePath2() {
            string p1;
            string p2;

            p1 = @"C:\A\B\C\D\E\F\G/H\";
            p2 = @"..\..\..\X\Y\Z";
            TestAbsolutePath(p1, p2);

            p1 = @"C:/A\B\C\D\E\F\G\H";
            p2 = @"../..\..\X\Y\Z";
            TestAbsolutePath(p1, p2);
        }

        // 测试3: 路径中包含不正确的分隔符
        static void TestAbsolutePath3() {
            string p1;
            string p2;

            try {
                p1 = @"C:\A\B\C\D\E\F\G\H\\";
                p2 = @"..\..\..\X\Y\Z";
                TestAbsolutePath(p1, p2);
            }
            catch (ArgumentException e) {
                Console.WriteLine(e.Message);
            }

            try {
                p1 = @"C:\A\B\C\D\E\F\G\H";
                p2 = @"..//..\..\X\Y\Z";
                TestAbsolutePath(p1, p2);
            }
            catch (ArgumentException e) {
                Console.WriteLine(e.Message);
            }
        }

        // 测试4: 路径中包含中文
        static void TestAbsolutePath4() {
            string p1;
            string p2;

            p1 = @"C:\我的文档\B\C\D\E\F\G\H\";
            p2 = @"..\..\..\X\Y\Z";
            TestAbsolutePath(p1, p2);

            p1 = @"C:\A\B\C\D\E\F\我的文档\H\";
            p2 = @"..\..\..\X\Y\Z";
            TestAbsolutePath(p1, p2);

            p1 = @"C:\A\B\C\D\E\F\G\H\";
            p2 = @"..\..\..\X\我的文档\Z";
            TestAbsolutePath(p1, p2);
        }

        // 测试5: 路径中包含文件名
        static void TestAbsolutePath5() {
            string p1;
            string p2;

            p1 = @"C:\A\B\C\D\E\F\G\H\1.txt";
            p2 = @"..\..\..\X\Y\Z";
            Console.WriteLine(p1.GetAbsolutePath(p2, true, false));

            p1 = @"C:\A\B\C\D\E\F\G\H\";
            p2 = @"..\..\..\X\Y\Z\2.txt";
            Console.WriteLine(p1.GetAbsolutePath(p2, false, true));

            p1 = @"C:\A\B\C\D\E\F\G\H\1.txt";
            p2 = @"..\..\..\X\Y\Z\2.txt";
            Console.WriteLine(p1.GetAbsolutePath(p2, true, true));
        }

        // 测试6: 参数传反
        static void TestAbsolutePath6() {
            String p1;
            String p2;
            p1 = @"C:\A\B\C\D\E\F\G\H\";
            p2 = @"..\..\..\X\Y\Z";
            try {
                Console.WriteLine(p2.GetAbsolutePath(p1));
            }
            catch (ArgumentException e) {
                Console.WriteLine(e.Message);
            }
        }

        static void TestAbsolutePath(String p1, String p2) {
            Console.WriteLine(p1.GetAbsolutePath(p2));
            //Console.WriteLine(p2.GetAbsolutePath(p1));
        }

        static void TestAbsolutePath111() {
            String p1;

            p1 = @"C:\A\B\C\D\E\F\G\H\..\..";
            TestAbsolutePathStd(p1);

            p1 = @"C:\A\B\C\D\E\F\G\H\..\..\";
            TestAbsolutePathStd(p1);

            p1 = @"C:\A\B\..\MyFile.txt";
            TestAbsolutePathStd(p1);
        }

        static void TestAbsolutePathStd(String path) {
            Console.WriteLine("原始：{0}， 标准化：{1}", path, path.GetAbsolutePath());
        }
    }
}
