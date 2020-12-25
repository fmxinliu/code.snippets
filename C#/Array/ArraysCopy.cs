using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ArrayTest {
    class ArraysCopy {
        public static void Test() {
            ArrayAsFuncParameter();
            ArrayAsFuncReturnValue();
        }

        /// <summary>
        /// 数组是引用类型，作为函数参数传递时，传递的是数组引用
        /// </summary>
        private static void ArrayAsFuncParameter() {
            Int32[] array11 = { 1, 2, 3, 4, 5 };
            Int32[] array12 = ModifyArrayElemZero1(array11);
            Console.WriteLine("数组按{0}传递", array11[0] == array12[0] ? "引用" : "值");

            Int32[] array21 = { 1, 2, 3, 4, 5 };
            Int32[] array22 = ModifyArrayElemZero2(array21);
            Console.WriteLine("Array.Copy拷贝数组{0}", array21[0] == array22[0] ? "引用" : "元素");

            Int32[] array31 = { 1, 2, 3, 4, 5 };
            Int32[] array32 = ModifyArrayElemZero3(array31);
            Console.WriteLine("CopyTo拷贝数组{0}", array31[0] == array32[0] ? "引用" : "元素");

            Int32[] array41 = { 1, 2, 3, 4, 5 };
            Int32[] array42 = ModifyArrayElemZero4(array41);
            Console.WriteLine("CopyTo拷贝数组{0}", array41[0] == array42[0] ? "引用" : "元素");
        }

        private static void ArrayAsFuncReturnValue() {
            ClassMates classmates = new ClassMates();

            // 1: 直接返回引用
            String[] s11 = classmates.GetNames1();
            s11[0] = "111";
            String[] s12 = classmates.GetNames1(); // 相当于：外部直接访问了对象私有成员
            Console.WriteLine("直接返回引用。{0}", s11[0].Equals(s12[0]) ? "不安全" : "安全");

            // 2: 深拷贝一份，返回其引用
            String[] s21 = classmates.GetNames2();
            s21[0] = "222";
            String[] s22 = classmates.GetNames2();
            Console.WriteLine("深拷贝一份，返回其引用。{0}", s21[0].Equals(s22[0]) ? "不安全" : "安全");
        }

        private static T[] ModifyArrayElemZero1<T>(T[] array) {
            if (array.Length > 0) {
                array[0] = default(T);
            }
            return array;
        }

        private static T[] ModifyArrayElemZero2<T>(T[] array) {
            T[] array2 = new T[array.Length];
            Array.Copy(array, array2, array.Length);
            if (array.Length > 0) {
                array[0] = default(T);
            }
            return array2;
        }

        private static T[] ModifyArrayElemZero3<T>(T[] array) {
            T[] array2 = new T[array.Length];
            array.CopyTo(array, 0);
            if (array.Length > 0) {
                array[0] = default(T);
            }
            return array2;
        }

        private static T[] ModifyArrayElemZero4<T>(T[] array) {
            T[] array2 = array; // #array、array2指向同一块堆内存
            Debug.Assert(Object.ReferenceEquals(array, array2));
            array.CopyTo(array2, 0);
            if (array.Length > 0) {
                array[0] = default(T);
            }
            return array2;
        }

        private class ClassMates {
            private String[] names = new[] {
                "1", "2", "3", "4", "5", "6"
            };

            /// <summary>
            /// 不安全，外部可以直接修改私有成员
            /// </summary>
            public String[] GetNames1() {
                return this.names;
            }

            /// <summary>
            /// 安全
            /// </summary>
            public String[] GetNames2() {
                String[] a = new String[names.Length];
                Array.Copy(names, a, names.Length);
                return a;
            }
        }
    }
}
