using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ReflectionTest {
    class TestLibLoader {
        public static void Run() {
            Test1();
            CallingLibMethod();
            //MyLib.Information.Tell(); // #1 MyLib.dll在Run运行前加载

            Test2();
            CallingLibMethod();
            //MyLib.Information.Tell(); // #1 MyLib.dll在Run运行前加载
        }

        static void Test1() {
            try {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "MyLib.dll");
                LibLoader.RigsterAssemblyResolve(AppDomain.CurrentDomain);
            }
            catch (UnauthorizedAccessException) { }
            catch (IOException) { }
        }

        static void Test2() {
            try {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "MyLib.dll");
                LibLoader.ExtractDllFiles(AppDomain.CurrentDomain, "MyLib.dll");
            }
            catch (UnauthorizedAccessException) { }
            catch (IOException) { }
        }

        static void CallingLibMethod() {
            MyLib.Information.Tell(); // #2 MyLib.dll在这里才加载
        }


    }
}
