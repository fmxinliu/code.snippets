using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SerializationTest {
    class ControlledByAttribute {
        public static void Test() {
            var obj = new Circle(100);
            var stream = obj.SerializeToMemory();
            stream.SaveToFile("rules.txt");

            obj = null;
            stream.Position = 0;
            obj = stream.Deserialize<Circle>();
            stream.Dispose();
            Console.WriteLine(obj);
        }

        [Serializable]
        private class Circle {
            private static readonly Double PI = Math.PI; // #静态字段不会被序列化
            private Int32 radius;

            [NonSerialized]
            private Double area; // #非必需序列化字段

            [OptionalField]
            private Double precision = 0.1; // #版本升级，新添加字段

            /// <summary>
            /// 序列化的类，不要定义自动实现属性
            /// </summary>
            public String Unit { get; set; } // #序列化的是编译器实现的匿名字段，反序列化时可能会报错

            static Circle() {
                Console.WriteLine("Circle .cctor called."); // #反序列化，不会调用静态构造器
            }

            public Circle(Int32 radius) {
                this.radius = radius;
                this.area = PI * radius * radius + precision;
                this.Unit = "m * m";
                Console.WriteLine("Circle .ctor called."); // #反序列化，不会调用实例构造器
            }

            public override string ToString() {
                return String.Format("radius={0}, area={1}", radius, area);
            }

            [OnDeserialized]
            private void OnDeserialized(StreamingContext context) {
                Console.WriteLine("反序列化完成");
                this.area = Math.PI * radius * radius;
            }

            [OnDeserializing]
            private void OnDeserializing(StreamingContext context) {
                Console.WriteLine("开始反序列化");
            }

            [OnSerializing]
            private void OnSerializing(StreamingContext context) {
                Console.WriteLine("开始序列化");
            }

            [OnSerialized]
            private void OnSerialized(StreamingContext context) {
                Console.WriteLine("序列化完成");
            }
        }
    }
}
