using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace AttributeTest {
    class AttributeForeach {
        public static void Test() {
            Program.AttributeForeach1();
            Program.AttributeForeach2();
        }

        [Serializable]
        [DefaultMember("Program")]
        [DebuggerDisplay("Richter", Name = "Jeff", Target = typeof(Program))]
        sealed class Program {
            public Program() { }

            [Conditional("Debug")]
            [Conditional("Release")]
            public void DoSomething() { }

            public static void AttributeForeach1() {
                Console.WriteLine("---- Attribute Foreach #1----");

                // 显示应用于这个类型的特性集
                ShowAttributes1(typeof(Program));

                // 获取与类型关联的方法集
                MethodInfo[] methods = typeof(Program).GetMethods(
                    BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
                foreach (MethodInfo method in methods) {
                    ShowAttributes1(method);
                }
            }

            public static void AttributeForeach2() {
                Console.WriteLine("---- Attribute Foreach #2----");
                // 显示应用于这个类型的特性集
                ShowAttributes2(typeof(Program));

                // 获取与类型关联的方法集
                MethodInfo[] methods = typeof(Program).GetMethods(
                    BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
                foreach (MethodInfo method in methods) {
                    ShowAttributes2(method);
                }
            }

            #region 显示成员关联的定制特性
            private static void ShowAttributes1(MemberInfo member) {
                Object[] attributes = member.GetCustomAttributes(true);

                Console.WriteLine("Attributes applied to {0}: {1}",
                    member.Name, (attributes.Length == 0 ? "None" : String.Empty));

                foreach (Attribute attribute in attributes) {
                    // 显示所应用的每个特性的类型
                    Console.WriteLine(" {0}", attribute.GetType().ToString());

                    if (attribute is DefaultMemberAttribute) {
                        Console.WriteLine(" MemberName={0}", ((DefaultMemberAttribute)attribute).MemberName);
                    }
                    else if (attribute is ConditionalAttribute) {
                        Console.WriteLine(" ConditionString={0}", ((ConditionalAttribute)attribute).ConditionString);
                    }
                    else if (attribute is CLSCompliantAttribute) {
                        Console.WriteLine(" IsCompliant={0}", ((CLSCompliantAttribute)attribute).IsCompliant);
                    }

                    DebuggerDisplayAttribute dda = attribute as DebuggerDisplayAttribute;
                    if (dda != null) {
                        Console.WriteLine(" Value={0}, Name={1}, Target={2}", dda.Value, dda.Name, dda.Target);
                    }
                }
                Console.WriteLine();
            }

            private static void ShowAttributes2(MemberInfo member) {
                IList<CustomAttributeData> attributes = CustomAttributeData.GetCustomAttributes(member);

                Console.WriteLine("Attributes applied to {0}: {1}",
                    member.Name, (attributes.Count == 0 ? "None" : String.Empty));

                foreach (CustomAttributeData attribute in attributes) {
                    // 显示所应用的每个特性的类型
                    Console.WriteLine(" {0}", attribute.Constructor.DeclaringType.ToString());
                    Console.WriteLine("   Constructor called={0}", attribute.Constructor);

                    // 定位参数
                    IList<CustomAttributeTypedArgument> posArgs = attribute.ConstructorArguments;
                    Console.WriteLine("   Positional arguments passed to constructor:" +
                        (posArgs.Count == 0 ? " None" : String.Empty));
                    foreach (CustomAttributeTypedArgument pa in posArgs) {
                        Console.WriteLine("   Type={0}, Value={1}", pa.ArgumentType, pa.Value);
                    }

                    // 命名参数
                    IList<CustomAttributeNamedArgument> namedArgs = attribute.NamedArguments;
                    Console.WriteLine("   Named arguments set after constructor:" +
                        (namedArgs.Count == 0 ? " None" : String.Empty));
                    foreach (CustomAttributeNamedArgument na in namedArgs) {
                        Console.WriteLine("   Name={0}, Type={1}, Value={2}",
                            na.MemberInfo.Name, na.TypedValue.ArgumentType, na.TypedValue.Value);
                    }
                }
                Console.WriteLine();
            }
            #endregion
        }
    }
}
