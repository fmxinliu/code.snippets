using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

namespace SerializationTest {
    class SurrogateSelectors {
        public static void Test() {
            Console.WriteLine();
            using (var stream = new MemoryStream()) {
                // 1.构造格式化器
                IFormatter formatter = new SoapFormatter();

                // 2.构造代理选择器
                SurrogateSelector ss = new SurrogateSelector();

                // 3.告诉代理选择器: 为 DateTime 对象使用我们的代理
                ss.AddSurrogate(typeof(DateTime), formatter.Context,
                    new UniversalToLocalTimeSerializationSurrogate());
                // 注意: 可多次调用 AddSurrogate，注册多个代理

                // 4.告诉格式化器使用代理选择器
                formatter.SurrogateSelector = ss;

                // 创建一个 DateTime 来代表机器上的本地时间，并序列化它
                DateTime localTimeBeforeSerialize = DateTime.Now;
                formatter.Serialize(stream, localTimeBeforeSerialize);

                // 显示序列化后的 Universal
                stream.Position = 0;
                Console.WriteLine(new StreamReader(stream).ReadToEnd());

                // 反序列化 Universal，并且转换成本地 DateTime
                stream.Position = 0;
                DateTime localTimeAfterDeserialize = (DateTime)formatter.Deserialize(stream);

                // 证明它能正常工作
                Console.WriteLine("localTimeBeforeSerialize={0}", localTimeBeforeSerialize);
                Console.WriteLine("localTimeAfterDeserialize={0}", localTimeAfterDeserialize);
            }
        }

        /// <summary>
        /// 序列化代理项选择器，控制 [无法修改的类型] DateTime 对象的序列化和反序列化方式
        /// </summary>
        class UniversalToLocalTimeSerializationSurrogate : ISerializationSurrogate {
            public void GetObjectData(Object obj, SerializationInfo info, StreamingContext context) {
                // 将 DateTime 从本地时间转换成 UTC
                info.AddValue("DateTime", ((DateTime)obj).ToUniversalTime().ToString("u"));
            }

            public Object SetObjectData(Object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector) {
                // 将 DateTime 从 UTC 转换成本地时间
                return DateTime.Parse(info.GetString("DateTime"));
            }
        }
    }
}
