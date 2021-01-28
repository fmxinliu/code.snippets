using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializationTest {
    class ObjDeepClone {
        public static void Test() {
            HashCode hash = new HashCode(100);
            Money m1 = new Money();
            m1.Hash = hash;
            Money m2 = m1.DeepClone();
            m1.Hash.Code = 200;
            Boolean b = m1.Hash.Code == m2.Hash.Code;

            // 可序列化的四种类型
            var b1 = hash.IsSerializable(); // 标记[Serializable]的类
            var b2 = new Numbers().IsSerializable(); // 标记[Serializable]的结构
            var b3 = CopyType.ShallowCopy.IsSerializable();  // 枚举(不必标记[Serializable])
            var b4 = new Action(() => { }).IsSerializable(); // 委托(不必标记[Serializable])
        }

        [Serializable]
        class Money {
            private HashCode hash;
            public HashCode Hash { 
                get { return hash; }
                set { hash= value; }
            }
        }

        [Serializable]
        class HashCode {
            private Int32 code;
            public HashCode(Int32 code) {
                this.code = code;
            }
            public Int32 Code {
                get { return code; }
                set { code = value; }
            }
        }

        [Serializable]
        struct Numbers { }

        enum CopyType { ShallowCopy, DeepCopy }
    }
}
