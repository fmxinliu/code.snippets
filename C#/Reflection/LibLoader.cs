using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace ReflectionTest {
    public class LibLoader {
        /// <summary>
        /// 注册: 程序集加载失败后，从内嵌资源中查找并加载DLL
        /// </summary>
        public static void RigsterAssemblyResolve(AppDomain ad) {
            ad.AssemblyResolve -= new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            ad.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        }

        /// <summary>
        /// 导出内嵌DLL(编译时需要依赖DLL，运行时在加载DLL前，提取并生成文件)
        /// </summary>
        /// <param name="files"></param>
        public static void ExtractDllFiles(AppDomain ad, params String[] files) {
            String path = ad.BaseDirectory;
            foreach (String dllName in files) {
                String fullName = path + dllName;
                if (!File.Exists(fullName)) {
                    Byte[] assemblyData = GetAssemblyDataFromResource(dllName);
                    if (assemblyData != null) {
                        using (FileStream fs = new FileStream(fullName, FileMode.CreateNew, FileAccess.Write)) {
                            fs.Write(assemblyData, 0, assemblyData.Length);
                        }
                    }
                }
            }
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args) {
            // 获取程序集
            String dllName = new AssemblyName(args.Name).Name + ".dll";
            // 从内嵌的资源中检索程序集
            Byte[] assemblyData = GetAssemblyDataFromResource(dllName);
            // 加载程序集
            return (assemblyData != null) ? Assembly.Load(assemblyData) : null;
        }

        private static Byte[] GetAssemblyDataFromResource(String dllName) {
            // 从内嵌的资源中检索程序集
            Assembly assembly = Assembly.GetExecutingAssembly();
            String resourceName = assembly.GetManifestResourceNames().FirstOrDefault(rn => rn.EndsWith(dllName));
            if (resourceName == null) {
                return null;
            }
            // 加载程序集
            using (Stream stream = assembly.GetManifestResourceStream(resourceName)) {
                Byte[] assemblyData = new Byte[stream.Length];
                stream.Read(assemblyData, 0, assemblyData.Length);
                return assemblyData;
            }
        }
    }
}
