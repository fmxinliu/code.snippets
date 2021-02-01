using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializationTest {
    class SerializationBinders {
        public static void Test() {
            Console.WriteLine();
            using (var stream = new MemoryStream()) {
                // 1.构造格式化器
                IFormatter formatter = new BinaryFormatter();

                // 2.构造反序列化类型查找器
                formatter.Binder = new Ver1ToVer2SerializationBinder();

                // 构造 Ver1 类型对象，并序列化
                formatter.Serialize(stream, new Ver1 { Name = "版本1" });

                // 反序列化
                stream.Position = 0;
                Object obj = formatter.Deserialize(stream);

                // 证明: Ver1 反序列化成 Ver2
                Console.WriteLine(obj);
            }
        }

        #region 1.类型的老版本v1
        [Serializable]
        class Ver1 {
            private String name;
            public String Name {
                get { return name; }
                set { name = value; }
            }
        }
        #endregion

        #region 2.类型的新版本v2
        [Serializable]
        class Ver2 {
            private String name;
            public String Name {
                get { return name; }
                set { name = value; }
            }

            private String newName;
            public String NewName {
                get { return newName; }
                set { newName = value; }
            }

            [OnDeserialized]
            private void OnDeserialized(StreamingContext context) {
                if (newName == null) {
                    newName = "版本2";
                }
            }

            public override String ToString() {
                return String.Format("name: {0} -> {1}", name, newName);
            }
        }
        #endregion

        #region 3.使用v1版本序列化的实例，强制反序列为新版本v2
        class Ver1ToVer2SerializationBinder : SerializationBinder {
            #region 控制: 序列化为不同类型
            /// <summary>
            /// 控制: 序列化为不同类型
            /// </summary>
            /// <param name="serializedType">预期序列化的类型1</param>
            /// <param name="assemblyName">类型2所在的程序集</param>
            /// <param name="typeName">实际序列化的类型2</param>
            public override void BindToName(Type serializedType, out String assemblyName, out String typeName) {
                // 不变换类型
                assemblyName = null;
                typeName = null;

                //assemblyName = Assembly.GetExecutingAssembly().GetName().ToString();
                //typeName = typeof(Ver2).ToString();
            }
            #endregion

            #region 控制: 反序列化为不同类型
            /// <summary>
            /// 控制: 反序列化为不同类型
            /// </summary>
            /// <param name="assemblyName">程序集名</param>
            /// <param name="typeName">预期反序列化的类型1</param>
            /// <returns>实际反序列化的类型2</returns>
            public override Type BindToType(String assemblyName, String typeName) {
                // 将任何 Ver1 对象从版本 1.0.0.0 反序列化成一个 Ver2 对象

                // 计算定义 Ver1 类型的程序集名称
                AssemblyName assemVer1 = Assembly.GetExecutingAssembly().GetName();
                assemVer1.Version = new Version(1, 0, 0, 0);

                // 如果从 v1.0.0.0 反序列化 Ver1 对象，就把它转变成一个 Ver2 对象
                if (assemblyName == assemVer1.FullName && typeName == typeof(Ver1).FullName) {
                    return typeof(Ver2);
                }

                // 否则，就只返回请求的同一个类型
                return Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));
            }
            #endregion
        }
        #endregion
    }
}
