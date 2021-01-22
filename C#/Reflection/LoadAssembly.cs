using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace ReflectionTest {
    class LoadAssembly {
        public static void Test() {
            String name = Assembly.GetEntryAssembly().FullName;
            String path = Assembly.GetEntryAssembly().Location;

            // 1.指定程序集标识字符串
            Assembly.Load(name);

            try {
                Assembly.Load(path);
            }
            catch (FileLoadException) {
                Console.WriteLine("Assembly.Load()不能通过指定路径的方式，来加载程序集");
            }

            // 2.打开path指定的文件，获取标识字符串，调用Assembly.Load()
            // 如果Assembly.Load找到程序集(可能不是path指定的程序集)，直接加载；
            // 如果Assembly.Load未找到程序集，加载path指定的程序集。
            Assembly.LoadFrom(path);

            // 3.加载path指定的程序集
            Assembly.LoadFile(path);
        }
    }
}
