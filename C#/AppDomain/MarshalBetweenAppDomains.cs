using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Security.Policy;
using System.Runtime.Remoting;

namespace AppDomainTest {
    class MarshalBetweenAppDomains {
        public static void Test() {
            String exeAssembly = GetEntryAssemblyName();
            TestMarshalByRefType(exeAssembly, "AD #2");
            TestMarshalByValueType(exeAssembly, "AD #3");
            TestNonMarshalableType(exeAssembly, "AD #4");
        }

        static String GetEntryAssemblyName() {
            // 获取当前线程所在的AppDomain
            String callingDomainName = Thread.GetDomain().FriendlyName;
            Console.WriteLine("In AppDomain: " + callingDomainName);

            // 获取包含Main()的程序集
            String exeAssembly = Assembly.GetEntryAssembly().FullName;
            Console.WriteLine("Entry Assembly: " + exeAssembly);
            Console.WriteLine();
            return exeAssembly;
        }

        static AppDomain CreateAppDomain(String friendlyName) {
            /*
            // Set up the AppDomainSetup
            AppDomainSetup setup = new AppDomainSetup();
            setup.ApplicationBase = "(some directory)";
            setup.ConfigurationFile = "(some file)";
            setup.DisallowBindingRedirects = false;
            setup.DisallowCodeDownload = true;

            // Set up the Evidence
            Evidence baseEvidence = AppDomain.CurrentDomain.Evidence;
            Evidence evidence = new Evidence(baseEvidence);
            evidence.AddAssembly("(some assembly)");
            evidence.AddHost("(some host)");

            // Create the AppDomain
            AppDomain newDomain = AppDomain.CreateDomain("newDomain", evidence, setup);
*/
            // 创建新的AppDomain，继承当前AppDomain的安全策略和配置
            AppDomain ad2 = AppDomain.CreateDomain(friendlyName, null, null);
            return ad2;
        }

        static void UnloadAppDomain(AppDomain ad) {
            AppDomain.Unload(ad);
        }

        static void TestMarshalByRefType(String exeAssembly, String friendlyName) {
            Console.WriteLine("=== Demo1: 按引用封送 ===");
            AppDomain ad2 = CreateAppDomain(friendlyName);

            // 在新建AppDomain中，新建MarshalbyRefType的实例对象
            // 返回一个代理(proxy)
            MarshalByRefType mbrt = (MarshalByRefType)ad2.CreateInstanceAndUnwrap(
                exeAssembly, typeof(MarshalByRefType).FullName);

            // 判断跨AppDomain，“按引用封送”返回的是代理 or 实际对象
            Console.WriteLine("Is Proxy={0}", RemotingServices.IsTransparentProxy(mbrt));

            // 通过代理调用方法，会切换AppDomain
            mbrt.SomeMethod();

            // 卸载新建AppDomain
            UnloadAppDomain(ad2);

            // 再次通过代理调用方法
            try {
                mbrt.SomeMethod();
                Console.WriteLine("Sucessful call.");
            }
            catch (AppDomainUnloadedException) {
                Console.WriteLine("Failed call; AppDomain Unloaded.");
            }
            Console.WriteLine();
        }

        static void TestMarshalByValueType(String exeAssembly, String friendlyName) {
            Console.WriteLine("=== Demo2: 按值封送 ===");
            AppDomain ad2 = CreateAppDomain(friendlyName);

            // 在新建AppDomain中，新建MarshalbyRefType的实例对象
            // 返回一个代理(proxy)
            MarshalByRefType mbrt = (MarshalByRefType)ad2.CreateInstanceAndUnwrap(
                exeAssembly, typeof(MarshalByRefType).FullName);

            // 调用
            MarshalByValueType mbvt = mbrt.MethodWithReturn();

            // 判断跨AppDomain，“按值封送”返回的是代理 or 实际对象
            Console.WriteLine("Is Proxy={0}", RemotingServices.IsTransparentProxy(mbvt));

            // 卸载新建AppDomain
            UnloadAppDomain(ad2);

            // 再次调用方法，判断是否代理对象
            try {
                Console.WriteLine(mbvt.ToString());
                Console.WriteLine("Sucessful call.");
            }
            catch (AppDomainUnloadedException) {
                Console.WriteLine("Failed call; AppDomain Unloaded.");
            }
            Console.WriteLine();
        }

        static void TestNonMarshalableType(String exeAssembly, String friendlyName) {
            Console.WriteLine("=== Demo3: 不能封送类型 ===");
            AppDomain ad2 = CreateAppDomain(friendlyName);

            // 在新建AppDomain中，新建MarshalbyRefType的实例对象
            // 返回一个代理(proxy)
            MarshalByRefType mbrt = (MarshalByRefType)ad2.CreateInstanceAndUnwrap(
                exeAssembly, typeof(MarshalByRefType).FullName);

            try {
                // 调用
                NonMarshalableType nomt = mbrt.MethodWithArgAndReturn(Thread.GetDomain().FriendlyName);

                // 判断跨AppDomain，“按值封送”返回的是代理 or 实际对象
                Console.WriteLine("Is Proxy={0}", RemotingServices.IsTransparentProxy(nomt));

                // 卸载新建AppDomain
                UnloadAppDomain(ad2);

                // 再次调用方法，判断是否代理对象
                try {
                    Console.WriteLine(nomt.ToString());
                    Console.WriteLine("Sucessful call.");
                }
                catch (AppDomainUnloadedException) {
                    Console.WriteLine("Failed call; AppDomain Unloaded.");
                }
            }
            catch (Exception e)  {
                Console.WriteLine("不能封送类型： " + e.ToString());
            }
            Console.WriteLine();
        }
    }
}
