using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullableTest {
    class NullCoalescingOperator {
        public static void Test() {
            NullCoalescingOperatorTest();
        }

        private static void NullCoalescingOperatorTest() {
            // 等价
            Int32? b = null;
            Int32 x1 = (b != null) ? (Int32)b : 0;
            Int32 x2 = b ?? 0;

            // 等价
            String s = null;
            String s1 = (s != null) ? s : "Untitled";
            String s2 = s ?? "Untitled";

            // 复合情况
            String s3 = s ?? s1 ?? s2;
        }
    }
}
