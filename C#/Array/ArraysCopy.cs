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
            ArrayCopyNonOverlapTest();
            ArrayCopyOverlapTest();
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

        /// <summary>
        /// 数组无重叠拷贝（引用类型的数组，执浅拷贝）
        /// </summary>
        private static void ArrayCopyNonOverlapTest() {
            User[] users = {
                new User { Name="1", Age=1 },
                new User { Name="2", Age=2 },
                new User { Name="3", Age=3 },
            };

            User[] users1 = new User[users.Length];
            Array.Copy(users, users1, users.Length);
            users1[0].Age = 111;
            Console.WriteLine("Array.Copy执行{0}拷贝", users[0].Age == users1[0].Age ? "浅" : "深");

            User[] users2 = new User[users.Length];
            Array.ConstrainedCopy(users, 0, users2, 0, users.Length);
            users2[1].Age = 222;
            Console.WriteLine("Array.ConstrainedCopy执行{0}拷贝", users[1].Age == users2[1].Age ? "浅" : "深");

            try {
                // 按字节拷贝，必须是连续内存，不适用于引用类型
                User[] users3 = new User[users.Length];
                Buffer.BlockCopy(users, 0, users3, 0, users.Length);
                users3[1].Age = 111;
                Console.WriteLine("Buffer.BlockCopy执行{0}拷贝", users[1].Age == users3[1].Age ? "浅" : "深");
            }
            catch (System.Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 数组有重叠拷贝（引用类型的数组，执浅拷贝）
        /// </summary>
        private static void ArrayCopyOverlapTest() {
            User[] users = {
                new User { Name="1", Age=1 },
                new User { Name="2", Age=2 },
                new User { Name="3", Age=3 },
                new User { Name="4", Age=4 },
                new User { Name="5", Age=5 },
            };

            User[] users1 = new User[users.Length];

            // src: 1 2 3 4 5
            // dst: x 1 2 3 4
            users.CopyTo(users1, 0);
            Array.Copy(users1, 0, users1, 1, users1.Length - 1);
            PrintArray(users1);

            users.CopyTo(users1, 0);
            Array.ConstrainedCopy(users1, 0, users1, 1, users1.Length - 1);
            PrintArray(users1);

            // src:   2 3 4 5
            // dst: 2 3 4 5 x
            users.CopyTo(users1, 0);
            Array.Copy(users1, 1, users1, 0, users1.Length - 1);
            PrintArray(users1);

            users.CopyTo(users1, 0);
            Array.ConstrainedCopy(users1, 1, users1, 0, users1.Length - 1);
            PrintArray(users1);
        }

        private static void PrintArray<T>(T[] array) {
            foreach (T elem in array) {
                Console.Write(elem + ",");
            }
            Console.WriteLine();
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

        private class User {
            public String Name { get; set; }
            public Int32 Age { get; set; }

            public override string ToString() {
                return this.Name;
            }
        }
    }
}
