using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppDomainTest {
    /// <summary>
    /// 该类的实例可跨AppDomain的边界“按值封送”
    /// </summary>
    [Serializable]
    class MarshalByValueType {
        private DateTime dt = DateTime.Now; // DateTime 可以序列化
        public MarshalByValueType() {
            Console.WriteLine("{0} .ctor running in {1}. Created at {2}",
                this.GetType().ToString(),
                AppDomain.CurrentDomain.FriendlyName,
                this.ToString());
        }

        public override String ToString() {
            return this.dt.ToString();
        }
    }
}
