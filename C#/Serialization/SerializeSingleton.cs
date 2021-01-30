using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace SerializationTest {
    class SerializeSingleton {
        public static void Test() {
            const String path = "singleton.txt";
            if (!File.Exists(path)) {
                // 创建数组，其中多个元素引用一个Singleton对象
                Singleton[] a1 = { Singleton.GetSingleton(), Singleton.GetSingleton() };
                Console.WriteLine("Do both elements refer to the same object? " + (a1[0] == a1[1])); // True

                // 先序列化再反序列化
                //Singleton[] a2 = a1.SerializeToMemory().Deserialize<Singleton[]>(origin: SeekOrigin.Begin, dispose: true);
                Stream stream = a1.SerializeToMemory();
                Singleton[] a2 = stream.Deserialize<Singleton[]>(origin: SeekOrigin.Begin);
                stream.SaveToFile(path);

                // 证明反序列化以后，仍是单例（同一个AppDomain中）
                a2[0].Name = "已反序列化";
                Console.WriteLine("Do both elements refer to the same object? " + (a2[0] == a2[1])); // True
                Console.WriteLine("Do both elements refer to the same object? " + (a1[1] == a2[1])); // True
                Console.WriteLine();
            }
            else {
                Singleton[] a2 = null;
                using (FileStream stream = new FileStream(path, FileMode.Open)) {
                    a2 = stream.Deserialize<Singleton[]>();
                    Console.WriteLine("Do both elements refer to the same object? " + (a2[0] == a2[1])); // True
                }
            }
        }

        [Serializable]
        class Singleton : ISerializable {
            private static readonly Singleton theOneObject = new Singleton();
            private String name = "单例";
            private Singleton() { }
            public static Singleton GetSingleton() { return theOneObject; }
            public String Name {
                get { return name; }
                set { name = value; }
            }

            // #序列化时，会调用该方法
            [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
            void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) {
                info.SetType(typeof(SingletonSerializationHelper)); // 序列化为另一个(包装)类型
                // 不要添加其他值
            }

            // 注意: 特殊构造器不是必要的，因为它永远不会被调用
            private Singleton(SerializationInfo info, StreamingContext context) {
                throw new NotImplementedException();
            }

            [Serializable]
            class SingletonSerializationHelper : IObjectReference {
                // #这个方法在对象（它没有字段）反序列化之后调用
                public object GetRealObject(StreamingContext context) {
                    return Singleton.GetSingleton(); // 反序列化为另一个(真正)类型
                }
            }
        }
    }
}
