using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullableTest {
    class NullableValueType {
        public static void Test() {
            BasicUsage();
            TypeCasting();
            GetNullableType();
        }

        private static void GetNullableType() {
            Int32? a = 0;
            Console.WriteLine("Int32?.GetType():" + a.GetType());
            Console.WriteLine("typeof(Int32?): " + typeof(Int32?));
            Console.WriteLine();
        }

        private static void BasicUsage() {
            Nullable<Int32> x1 = 5;
            Nullable<Int32> y1 = null;
            Console.WriteLine("x1: {0}", NullableValueTypePrivate.GetNullableString(x1));
            Console.WriteLine("y1: {0}", NullableValueTypePrivate.GetNullableString(y1));

            // Int32? 等价于 Nullable<Int32>
            Int32? x2 = 5;
            Int32? y2 = null;
            Console.WriteLine("x2: {0}", NullableValueTypePrivate.GetNullableString(x2));
            Console.WriteLine("y2: {0}", NullableValueTypePrivate.GetNullableString(y2));
            Console.WriteLine();
        }

        private static void TypeCasting() {
            Int32? a = 5;
            Int32? b = null;

            Int32 c = (Int32)a;
            Int32 c2 = NullableValueTypePrivate.ConvertTo(a);
            //Int32 d = (Int32)b; // Error，无法转换
            Int32 d2 = NullableValueTypePrivate.ConvertTo(b);

            Double? e = 5;
            Double? f = a;
            Double? g = b;
        }

        static class NullableValueTypePrivate {
            public static T? ConvertTo<T>(T t) where T : struct {
                return t;
            }

            public static T ConvertTo<T>(T? t) where T : struct {
                return (t == null) ? // 等价于 !t.HasValue
                    default(T) :
                    (T)t; // 等价于 t.Value
            }

            public static String GetNullableString<T>(Nullable<T> v) where T : struct {
                return (v.HasValue) ?
                    String.Format("HasValue={0}, Value={1}", v.HasValue.ToString(), v.Value.ToString()) :
                    String.Format("HasValue={0}, Value={1}", v.HasValue.ToString(), v.GetValueOrDefault().ToString());
            }
        }
    }
}
