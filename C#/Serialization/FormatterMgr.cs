using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace SerializationTest {
    public static partial class FormatterMgr {
        public static Stream SerializeToMemory<T>(this T objectGraph, FormatterType formatterType = FormatterType.Binary) {
            if (formatterType == FormatterType.Xml) {
                return XmlFormatters.SerializeToMemory(objectGraph);
            }
            return BinaryFormatters.SerializeToMemory(objectGraph);
        }

        public static T Deserialize<T>(this Stream stream, FormatterType formatterType = FormatterType.Binary) {
            if (formatterType == FormatterType.Xml) {
                return (T)XmlFormatters.Deserialize(stream, typeof(T));
            }
            return (T)BinaryFormatters.Deserialize(stream);
        }

        public static void SaveToFile(this Stream stream, String file, SeekOrigin origin = SeekOrigin.Begin) {
            using (FileStream fs = new FileStream(file, FileMode.Create)) {
                Byte[] buffer = new Byte[stream.Length];
                stream.Seek(0, origin); // 指定写入位置，等价于设置stream.Position
                stream.Read(buffer, 0, buffer.Length);
                fs.Write(buffer, 0, buffer.Length);
            }
        }

        public static T DeepClone<T>(this T original) {
            return (T)BinaryFormatters.DeepClone(original);
        }

        public static Boolean IsSerializable<T>(this T obj) {
            // [Serializable]标记的类、结构；枚举、委托(不必标记[Serializable], b1返回true)
            bool b1 = Attribute.IsDefined(typeof(T), typeof(SerializableAttribute));
            bool b2 = typeof(T).IsSerializable;
            if (b1 != b2) {
                System.Diagnostics.Debugger.Break();
            }
            return b2;
        }
    }

    /// <summary>
    /// 序列化器类型
    /// </summary>
    public enum FormatterType { Binary, Xml }

    public static partial class FormatterMgr {
        private static class BinaryFormatters {
            public static Stream SerializeToMemory(Object objectGraph) {
                // 构造流来容纳序列化的对象
                MemoryStream stream = new MemoryStream();

                // 构造序列化格式化器来执行所有真正的工作
                BinaryFormatter formatter = new BinaryFormatter();

                // 告诉格式化器将对象序列化到流中
                formatter.Serialize(stream, objectGraph);

                // 将序列化好的对象流返回给调用者
                return stream;
            }

            public static Object Deserialize(Stream stream) {
                // 构造序列化格式化器来执行所有真正的工作
                BinaryFormatter formatter = new BinaryFormatter();

                // 告诉格式化器从流中反序列化对象
                return formatter.Deserialize(stream);
            }

            public static Object DeepClone(Object original) {
                // 构造临时内存流
                using (MemoryStream stream = new MemoryStream()) {
                    // 构造序列化格式化器来执行所有真正的工作
                    BinaryFormatter formatter = new BinaryFormatter();

                    // 指定流上下文
                    formatter.Context = new StreamingContext(StreamingContextStates.Clone);

                    // 将对象图序列化到内存流中
                    formatter.Serialize(stream, original);

                    // 反序列化前，定位到内存流的其实位置
                    stream.Position = 0;

                    // 将对象图反序列化成一组新对象
                    // 像调用者返回对象图（深拷贝）的根
                    return formatter.Deserialize(stream);
                }
            }
        }

        private static class XmlFormatters {
            public static Stream SerializeToMemory(Object objectGraph) {
                // 构造流来容纳序列化的对象
                MemoryStream stream = new MemoryStream();

                // 构造序列化格式化器来执行所有真正的工作
                XmlSerializer formatter = new XmlSerializer(objectGraph.GetType());

                // 告诉格式化器将对象序列化到流中
                formatter.Serialize(stream, objectGraph);

                // 将序列化好的对象流返回给调用者
                return stream;
            }

            public static Object Deserialize(Stream stream, Type type) {
                // 构造序列化格式化器来执行所有真正的工作
                XmlSerializer formatter = new XmlSerializer(type);

                // 告诉格式化器从流中反序列化对象
                return formatter.Deserialize(stream);
            }
        }
    }
}
