using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ReflectionTest {
    class LoadAssemblyReflectType {
        public static void Test() {
            ListoutTypeInheritanceHierarchy(typeof(Exception));
        }

        private static void ListoutTypeInheritanceHierarchy(Type type) {
            // 显式加载想要反射的程序集
            LoadAssemblies();

            // 对所有类型进行筛选和排序
            var allTypes =
                // 遍历当前AppDomain加载的所有程序集
                (from a in AppDomain.CurrentDomain.GetAssemblies()
                 // 查找公共类型
                 from t in a.GetExportedTypes()
                 // 找到从type继承的类
                 where type.IsAssignableFrom(t)
                 orderby t.Name
                 select t).ToArray();

            // 生成并显式继承层次结构
            Console.WriteLine(
                WalkInheritanceHierarchy(new StringBuilder(), 0, type, allTypes));
        }

        private static StringBuilder WalkInheritanceHierarchy(
            StringBuilder sb, Int32 indent, Type baseType, IEnumerable<Type> allTypes) {
            String spaces = new String(' ', indent * 3);
            sb.AppendLine(spaces + baseType.FullName);
            foreach (var t in allTypes) {
                if (t.BaseType != baseType) continue;
                WalkInheritanceHierarchy(sb, indent + 1, t, allTypes);
            }
            return sb;
        }

        private static void LoadAssemblies() {
            String[] assemblies = {
                "System,                        PublicKeyToken={0}",
                "System.Core,                   PublicKeyToken={0}",
                "System.Data,                   PublicKeyToken={0}",
                "System.Design,                 PublicKeyToken={1}",
                "System.DirectoryServices,      PublicKeyToken={1}",
                "System.Drawing,                PublicKeyToken={1}",
                "System.Drawing.Design,         PublicKeyToken={1}",
                "System.Management,             PublicKeyToken={1}",
                "System.Messaging,              PublicKeyToken={1}",
                "System.Runtime.Remoting,       PublicKeyToken={0}",
                "System.Security,               PublicKeyToken={1}",
                "System.ServiceProcess,         PublicKeyToken={1}",
                "System.Web,                    PublicKeyToken={1}",
                "System.Web.RegularExpressions, PublicKeyToken={1}",
                "System.Web.Services,           PublicKeyToken={1}",
                "System.Xml,                    PublicKeyToken={0}",
            };

            String EcmaPublicKeyToken = "b77a5c561934e089";
            String MSPublicKeyToken = "b03f5f7f11d50a3a";

            // 获取包含System.Object的程序集的版本号
            // 假定其他所有程序集都是相同的版本号
            Version version = typeof(System.Object).Assembly.GetName().Version;

            // 显式加载想要反射的程序集
            foreach (String a in assemblies) {
                String assemblyIdentity =
                    String.Format(a, EcmaPublicKeyToken, MSPublicKeyToken) +
                    ", Culture=neutral, Version=" + version;
                Assembly.Load(assemblyIdentity);
            }
        }
    }
}
