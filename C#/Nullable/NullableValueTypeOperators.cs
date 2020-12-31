using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullableTest {
    class NullableValueTypeOperators {
        public static void Test() {
            Common(); // #基本操作符
            Equals(); // #相等性操作符
            Compare(); // #比较操作符
            BooleanLogical(); // #逻辑操作符作用于Boolean类型（类似于SQL的三值逻辑）!!!
            OperatorsOverride(); // #操作符重载（自动调用）
        }

        private static void Common() {
            Int32? a = 5;
            Int32? b = null;

            // 一元操作符
            a++;
            b = -b; // null

            // 二元操作符
            a = a + 3;
            b = b * 3; // null
            a = a + b; // null。两个操作数，有一个是 null，结果为 null
        }

        private static void Equals() {
            Int32? a = 0;
            Int32? b = null;

            // 相等性操作符
            if (a != null) { // 等价于 a.HasValue
                Console.WriteLine("a({0}) != null", a);
            }
            else {
                Console.WriteLine("a == null");
            }

            if (b == null) { // 等价于 !b.HasValue
                Console.WriteLine("b == null", b);
            }
            else {
                Console.WriteLine("b({0}) != null", b);
            }
        }

        /// <summary>
        /// 非空值类型的比较操作符，不止 3 种情况，还包含 a != b
        /// </summary>
        private static void Compare() {
            Int32? a = 0;
            Int32? b = null;

            // 比较操作符
            if (a > b) {
                Console.WriteLine("a({0}) > b({1})", a, b.GetValueOrDefault());
            }
            else if (a < b) {
                Console.WriteLine("a({0}) < b({1})", a.GetValueOrDefault(), b);
            }
            else if (a == b) {
                if (a == null) {
                    Console.WriteLine("a(null) == b(null)");
                }
                else {
                    Console.WriteLine("a({0}) == b({1})", a, b);
                }
            }
            else if (a != b) {
                Console.WriteLine("a({0}) != b({1})",
                    (a != null) ? a.ToString() : "null",
                    (b != null) ? b.ToString() : "null");
            }
            else {
                System.Diagnostics.Debug.Assert(false, "出现非预期的比较结果");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// 注意: & 和 | 作用于 Boolean 类型，一个为 null，另一个不为 null 的情况
        /// </summary>
        private static void BooleanLogical() {
            Logical(true, true);
            Logical(true, false);
            Logical(false, true);
            Logical(false, false);

            // 2 个都为 null，结果为 null
            Logical(null, null);

            // 其中 1 个为 null !!!!!!!!!
            Logical(true, null);
            Logical(false, null);
            Logical(null, true);
            Logical(null, false);
        }

        private static void Logical(Boolean? a, Boolean? b) {
            Boolean? c = a & b;
            Boolean? d = a | b;
            Console.WriteLine("a({0}) & b({1}) == {2}",
                (a != null) ? a.ToString() : "null",
                (b != null) ? b.ToString() : "null",
                (c != null) ? c.ToString() : "null");
            Console.WriteLine("a({0}) | b({1}) == {2}",
                (a != null) ? a.ToString() : "null",
                (b != null) ? b.ToString() : "null",
                (d != null) ? d.ToString() : "null");
            Console.WriteLine();
        }

        private static void OperatorsOverride() {
            Point? p1 = new Point(1, 1);
            Point? p2 = new Point(2, 2);
            Console.WriteLine("Are {0} equal {1} ? {2}", p1.ToString(), p2.ToString(), (p1 == p2).ToString());
            Console.WriteLine("Are {0} NOT equal {1} ? {2}", p1.ToString(), p2.ToString(), (p1 != p2).ToString());
        }

        struct Point {
            private Int32 x, y;
            public Point(Int32 x, Int32 y) {
                this.x = x;
                this.y = y;
            }

            public static Boolean operator ==(Point p1, Point p2) {
                return (p1.x == p2.x) && (p1.y == p2.y);
            }

            public static Boolean operator !=(Point p1, Point p2) {
                return !(p1 == p2);
            }

            public override Boolean Equals(Object obj) {
                if (obj == null) return false;
                if (this.GetType() != obj.GetType()) return false;
                return this == (Point)obj;
            }

            public override Int32 GetHashCode() {
                return x + y;
            }

            public override String ToString() {
                return String.Format("({0},{1})", x, y);
            }
        }
    }
}
