using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;

namespace Host {
    class Program {
        static void Main(string[] args) {
            Test();
            Pause();
        }

        static void Pause() {
            //if (Debugger.IsAttached) {
                Console.ReadKey();
            //}
        }

        static void Test() {
            // 插件地址
            String plugInDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
#if DEBUG
                + @"\..\..\..\..\..\托管DLL\插件\bin\Debug\";
#else
                + @"\..\..\..\..\..\托管DLL\插件\bin\Release\";
#endif

            String assemblyFile = plugInDir + "PlugIn.dll";

            if (!Directory.Exists(plugInDir) || !File.Exists(assemblyFile)) {
                Console.WriteLine("请先编译插件工程：plugin");
                return;
            }

            /// 1.创建独立的应用程序域，加载插件
            AppDomain appdomain = AppDomain.CreateDomain("PlugInDomain");
            String assemblyName = Assembly.GetExecutingAssembly().FullName;
            PlugInWarp warpper = (PlugInWarp)appdomain.CreateInstanceAndUnwrap(assemblyName, typeof(PlugInWarp).FullName);

            /// 2.调用插件

            // #1
            Console.WriteLine(warpper.AddInt321(1, 2, assemblyFile));
            Console.WriteLine(warpper.AddInt322(1, 2, assemblyFile));

            // #2
            Console.WriteLine(warpper.GetVersion1(assemblyFile));
            Console.WriteLine(warpper.GetVersion2(assemblyFile));

            // #3
            warpper.RunLib1(assemblyFile);
            warpper.RunLib2(assemblyFile);

            /// 3.卸载应用程序域，卸载插件
            AppDomain.Unload(appdomain);

            // 测试是否可以热拔插
            String assemblyBakFile = assemblyFile + ".bak";
            File.Copy(assemblyFile, assemblyBakFile, true);
            File.Delete(assemblyFile);
            File.Copy(assemblyBakFile, assemblyFile);
            File.Delete(assemblyBakFile);
        }

        sealed class PlugInWarp : MarshalByRefObject {
            /// <summary>
            /// 插件的类名
            /// </summary>
            private const String PlugInClassName = "PlugIn.MyPlugIn";

            #region 调用 void RunLib()
            public void RunLib1(String assemblyFile, String typeName = PlugInClassName) {
                var assembly = Assembly.LoadFrom(assemblyFile);
                var type = assembly.GetType(typeName);
                var obj = (SDK.IPlugIn)Activator.CreateInstance(type);
                obj.RunLib();
            }

            public void RunLib2(String assemblyFile, String typeName = PlugInClassName, String methodName = "RunLib") {
                var assembly = Assembly.LoadFrom(assemblyFile);
                var type = assembly.GetType(typeName);
                var obj = Activator.CreateInstance(type);
                var lambda = Expression.Lambda<Action>(
                    Expression.Call(Expression.Constant(obj), type.GetMethod(methodName)), null);
                lambda.Compile()();
            }
            #endregion

            #region 调用 String GetVersion()
            public String GetVersion1(String assemblyFile, String typeName = PlugInClassName) {
                var assembly = Assembly.LoadFrom(assemblyFile);
                var type = assembly.GetType(typeName);
                var obj = (SDK.IPlugIn)Activator.CreateInstance(type);
                return obj.GetVersion();
            }

            public String GetVersion2(String assemblyFile, String typeName = PlugInClassName, String methodName = "GetVersion") {
                var assembly = Assembly.LoadFrom(assemblyFile);
                var type = assembly.GetType(typeName);
                var obj = Activator.CreateInstance(type);
                var lambda = Expression.Lambda<Func<String>>(
                    Expression.Call(Expression.Constant(obj), type.GetMethod(methodName)), null);
                return lambda.Compile()();
            }
            #endregion

            #region 调用 Int32 AddInt32(Int32 a, Int32 b)
            public Int32 AddInt321(Int32 a, Int32 b, String assemblyFile, String typeName = PlugInClassName) {
                var assembly = Assembly.LoadFrom(assemblyFile);
                var type = assembly.GetType(typeName);
                var obj = (SDK.IPlugIn)Activator.CreateInstance(type); // 接口方式调用
                return obj.AddInt32(a, b);
            }

            public Int32 AddInt322(Int32 a, Int32 b, String assemblyFile, String typeName = PlugInClassName, String methodName = "AddInt32") {
                var assembly = Assembly.LoadFrom(assemblyFile);
                var type = assembly.GetType(typeName);
                var obj = Activator.CreateInstance(type);
                var lambda = Expression.Lambda<Func<Int32>>( // 表达式方式调用，可不依赖于接口
                    Expression.Call(
                        Expression.Constant(obj),
                        type.GetMethod(methodName),
                        Expression.Constant(a),
                        Expression.Constant(b)
                    )
                );
                return lambda.Compile()();
            }
            #endregion
        }
    }
}
