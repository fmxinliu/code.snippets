using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace SerializationTest {
    class ControlledByISerializable {
        public static void Test() {
            TestBaseWithISerializablImpl();
            TestBaseWithoutISerializablImpl();
        }

        public static void TestBaseWithISerializablImpl() {
            var obj = new Adder(100, 200);
            var stream = obj.SerializeToMemory();
            stream.SaveToFile("adder.txt");

            obj = null;
            stream.Position = 0;
            obj = stream.Deserialize<Adder>();
            stream.Dispose();
        }

        public static void TestBaseWithoutISerializablImpl() {
            var obj = new Operands(77);
            var stream = obj.SerializeToMemory();
            stream.SaveToFile("operand.txt");

            obj = null;
            stream.Position = 0;
            obj = stream.Deserialize<Operands>();
            stream.Dispose();
        }

        #region 基类实现ISerializable接口

        [Serializable]
        private class Adder : Operator, ISerializable, IDeserializationCallback {
            //[NonSerialized] // 不生效，被忽略!!!
            private Int32 x, y, sum;

            public Adder(Int32 x, Int32 y) : base("加法运算") {
                this.x = x;
                this.y = y;
                this.sum = x + y;
            }

            public override string ToString() {
                return String.Format("{0}+{1}={2}", x, y, sum);
            }

            [OnDeserialized]
            private void OnDeserialized(StreamingContext context) {
                Console.WriteLine("反序列化完成");
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

            // 用于反序列化
            protected Adder(SerializationInfo info, StreamingContext context) : base(info, context) {
                x = info.GetInt32("X");
                y = info.GetInt32("Y");
                sum = x + y;
            }

            // 用于序列化
            public override void GetObjectData(SerializationInfo info, StreamingContext context) {
                info.AddValue("X", x);
                info.AddValue("Y", y);
                base.GetObjectData(info, context); // #必须调用基类的GetObjectData，否则基类定义的实例字段无法序列化
            }

            void IDeserializationCallback.OnDeserialization(object sender) {
                Console.WriteLine(base.ToString() + ":" + this);
            }
        }

        [Serializable]
        private class Operator : ISerializable {
            private const String args = "Name"; // 用于（反）序列化
            private readonly String name;
            public Operator(String name) {
                this.name = name;
            }

            public override string ToString() {
                return name;
            }

            // 用于反序列化
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
            protected Operator(SerializationInfo info, StreamingContext context) {
                name = (String)info.GetValue(args, typeof(String)); // #从SerializationInfo获取字段值，初始化成员
            }

            // 用于序列化
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
            public virtual void GetObjectData(SerializationInfo info, StreamingContext context) {
                info.AddValue(args, name); // #将要序列化的字段加入SerializationInfo
            }
        }

        #endregion

        #region 基类未实现ISerializable接口

        [Serializable]
        private class Operands : Identifier, ISerializable {
            private Int32 value;
            public Operands(Int32 value) : base(value) { this.value = value; }
            public override string ToString() { return value.ToString(); }

            [OnDeserialized]
            private void OnDeserialized(StreamingContext context) {
                Console.WriteLine("Operands: {0}, identifier:{1}", this, base.ToString());
            }

            // 用于反序列化
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
            protected Operands(SerializationInfo info, StreamingContext context) : base(0) {
                // 1.#反序列化未实现ISerializable接口的基类Identifier
                Type basetype = this.GetType().BaseType;
                MemberInfo[] members = FormatterServices.GetSerializableMembers(basetype, context);
                foreach (var mi in members) {
                    FieldInfo fi = mi as FieldInfo;
                    if (fi != null) {
                        fi.SetValue(this, info.GetValue(basetype.FullName + "+" + fi.Name, fi.FieldType));
                    }
                }

                // 2.#反序列化本类
                value = info.GetInt32("value");
            }

            // 用于序列化
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
            public virtual void GetObjectData(SerializationInfo info, StreamingContext context) {
                // 1.#序列化本类
                info.AddValue("value", value);

                // 2.#序列化未实现ISerializable接口的基类Identifier
                Type basetype = this.GetType().BaseType;
                MemberInfo[] members = FormatterServices.GetSerializableMembers(basetype, context);
                foreach (var mi in members) {
                    FieldInfo fi = mi as FieldInfo;
                    if (fi != null) {
                        info.AddValue(basetype.FullName + "+" + fi.Name, fi.GetValue(this));
                    }
                }
            }
        }

        [Serializable]
        private class Identifier {
            private readonly Int32 hashcode;
            public Identifier(Int32 hashcode) { this.hashcode = hashcode; }
            public override string ToString() { return hashcode.ToString(); }
        }

        #endregion
    }
}
