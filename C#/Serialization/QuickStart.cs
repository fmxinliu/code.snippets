using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SerializationTest {
    class QuickStart {
        public static void Test() {
            // 要序列化的对象图
            var objectGraph = new List<String> { "A", "B", "C", "D" };

            // 序列化
            TestXmlSerializeToFile(objectGraph, "objectGraph.xml");
            TestBinarySerializeToFile(objectGraph, "objectGraph.txt");

            // 反序列化（可从不同进程、主机进行）
            var objectGraph1 = TestXmlDeserializeFromFile<List<String>>("objectGraph.xml");
            var objectGraph2 = TestBinaryDeserializeFromFile<List<String>>("objectGraph.txt");
        }

        static T TestXmlDeserializeFromFile<T>(String file) {
            using (Stream stream = new FileStream(file, FileMode.Open)) {
                return stream.Deserialize<T>(FormatterType.Xml);
            }
        }

        static T TestBinaryDeserializeFromFile<T>(String file) {
            using (Stream stream = new FileStream(file, FileMode.Open)) {
                return stream.Deserialize<T>();
            }
        }

        static void TestXmlSerializeToFile(List<String> objectGraph, String file) {
            // 序列化对象图到流中
            using (Stream stream = objectGraph.SerializeToMemory(FormatterType.Xml)) {
                /// 重置
                stream.Position = 0;
                objectGraph = null;

                // 反序列化对象
                objectGraph = stream.Deserialize<List<String>>(FormatterType.Xml);
                foreach (var s in objectGraph) Console.Write(s + " ");
                Console.WriteLine();

                // 持久化到文件
                stream.SaveToFile(file);
            }
        }

        static void TestBinarySerializeToFile(List<String> objectGraph, String file) {
            // 序列化对象图到流中
            using (Stream stream = objectGraph.SerializeToMemory()) {
                /// 重置
                stream.Position = 0;
                objectGraph = null;

                // 反序列化对象
                objectGraph = stream.Deserialize<List<String>>();
                foreach (var s in objectGraph) Console.Write(s + " ");
                Console.WriteLine();

                // 持久化到文件
                stream.SaveToFile(file);
            }
        }
    }
}
