using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericTest {
    class GenericInstance {
        public static void Test() {
            object o;
            o = CreateInstance(typeof(Dictionary<,>)); // 开放类型
            o = CreateInstance(typeof(StringKeyDictionary<>)); // 开放类型
            o = CreateInstance(typeof(StringKeyDictionary<Guid>)); // 封闭类型
            Console.WriteLine("对象类型为：" + o.GetType().ToString());
        }
        public static Object CreateInstance(Type t) {
            Object o = null;
            try {
                o = Activator.CreateInstance(t);
                Console.WriteLine("创建对象成功", t);
            }
            catch (ArgumentException e) {
                Console.WriteLine(e.Message);
            }
            return o;
        }
    }

    class StringKeyDictionary<TValue> : Dictionary<String, TValue> { }
}
