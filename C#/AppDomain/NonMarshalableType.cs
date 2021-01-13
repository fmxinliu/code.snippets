using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppDomainTest {
    /// <summary>
    /// 该类的实例不能跨AppDomain边界进行封送
    /// </summary>
    class NonMarshalableType {
        public NonMarshalableType() {
            Console.WriteLine("{0} .ctor running in {1}",
                this.GetType().ToString(), AppDomain.CurrentDomain.FriendlyName);
        }
    }
}
