using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace AppDomainTest {
    /// <summary>
    ///  该类的实例可跨AppDomain的边界“按引用封送”
    /// </summary>
    class MarshalByRefType : MarshalByRefObject {
        public MarshalByRefType() {
            Console.WriteLine("{0} .ctor running in {1}",
                this.GetType().ToString(), AppDomain.CurrentDomain.FriendlyName);
        }

        // #1
        public void SomeMethod() {
            Console.WriteLine("Executing in: " + AppDomain.CurrentDomain.FriendlyName);
        }

        // #2
        public MarshalByValueType MethodWithReturn() {
            Console.WriteLine("Executing in: " + AppDomain.CurrentDomain.FriendlyName);
            MarshalByValueType t = new MarshalByValueType();
            return t;
        }

        // #3
        public NonMarshalableType MethodWithArgAndReturn(String callingDomainName) {
            // String可序列化，且密封，可跨AppDomain封送
            Console.WriteLine("Calling from '{0}' to '{1}'.",
                callingDomainName, AppDomain.CurrentDomain.FriendlyName);
            NonMarshalableType t = new NonMarshalableType();
            return t;
        }

        #region 访问MarshalByRefObject派生类的实例字段的性能测试

        public static void TestMBROFieldsAccessPerf() {
            NonMBRO nonMbro = new NonMBRO();
            MBRO mbro = new MBRO();

            Stopwatch sw = Stopwatch.StartNew();
            for (Int32 i = 0; i < 1e8; i++) nonMbro.x++;
            Console.WriteLine("NonMBRO: " + sw.Elapsed);

            // 性能低下
            sw = Stopwatch.StartNew();
            for (Int32 i = 0; i < 1e8; i++) mbro.x++;
            Console.WriteLine("MBRO: " + sw.Elapsed);

            Console.WriteLine();
        }

        private sealed class NonMBRO : Object             { public Int32 x; }
        private sealed class MBRO    : MarshalByRefObject { public Int32 x; }

        #endregion
    }
}
