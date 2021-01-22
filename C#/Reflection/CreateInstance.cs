using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ReflectionTest {
    class CreateInstance {
        public static void Test() {
            Type openType = typeof(GenericType<>);
            Type closedType = openType.MakeGenericType(typeof(String));

            CreateInstanceUsingActivator(closedType);
            Console.WriteLine();

            CreateInstanceUsingConstructor(closedType);
            Console.WriteLine();

            CreateInstanceUsingActivator(typeof(Dictionary<String, Object>));
            Console.WriteLine();

            CreateInstanceUsingConstructor(typeof(Dictionary<String, Object>));
            Console.WriteLine();
        }

        static void CreateInstanceUsingActivator(Type closedType) {
            Object o = Activator.CreateInstance(closedType); // 调用无参构造器
            Console.WriteLine(o.GetType());
        }

        static void CreateInstanceUsingConstructor(Type closedType) {
            ConstructorInfo ctor = closedType.GetConstructor(Type.EmptyTypes); // 获取无参构造器
            Object o = ctor.Invoke(null); // 调用无参构造器
            Console.WriteLine(o.GetType());
        }
    }

    class GenericType<TKey> : Dictionary<TKey, Object> {
        public GenericType() {
            Console.WriteLine(".ctor called.");
        }
    }
}
