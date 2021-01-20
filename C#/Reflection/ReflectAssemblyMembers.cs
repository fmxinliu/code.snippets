using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ReflectionTest {
    class ReflectAssemblyMembers {
        public static void Test() {
            //Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Assembly a = typeof(Object).Assembly;
            //a = Assembly.GetEntryAssembly();
            ListoutAssemblyMember(new Assembly[] { a });
        }

        static void ListoutAssemblyMember(Assembly[] assemblies) {
            foreach (Assembly a in assemblies) {
                Show(0, "Assembly: {0}", a);

                foreach (Type t in a.GetExportedTypes()) {
                    Show(1, "Type: {0}", t);

                    foreach (MemberInfo mi in t.GetDefaultMembers()) {
                        String typeName = String.Empty;
                        if (mi is Type) typeName = "(Nested) Type";
                        if (mi is FieldInfo) typeName = "FieldInfo";
                        if (mi is MethodInfo) typeName = "MethodInfo";
                        if (mi is ConstructorInfo) typeName = "ConstructorInfo";
                        if (mi is PropertyInfo) typeName = "PropertyInfo";
                        if (mi is EventInfo) typeName = "EventInfo";
                        Show(2, "{0}: {1}", typeName, mi);
                    }
                }
            }
        }

        static void Show(Int32 indent, String format, params Object[] args) {
            Console.WriteLine(new String(' ', 3 * indent) + format, args);
        }
    }
}
