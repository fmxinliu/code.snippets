using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericTest {
    class GenericConstraint {
        public static void Test() {
            GenericConstraint obj = new GenericConstraint();
            GenericClass1<GenericConstraint> d = new GenericClass1<GenericConstraint>();
            Console.WriteLine("泛型参数约束测试==");
            Console.WriteLine(d.CompareTo(obj));
            d.SetValue(obj);
            Console.WriteLine(d.CompareTo(obj));
        }
    }

    /// <summary>
    /// 无约束，适用于任何类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    sealed class GenericClass1<T> {
        private T t;
        public GenericClass1() {
            t = default(T); // 为类型指定默认值
        }

        public void SetValue(T t1) {
            t = t1;
        }

        public bool CompareTo(T obj) {
            T t2 = obj;
            String s = t2.ToString();
            Boolean b = t2.Equals(t);
            return b;
        }
    }

    #region 类型约束
    #region 主要约束
    sealed class GenericClass2<T> where T : class {
        public void Init(T t) {
            t = null; // 类型参数限制为“引用”类型，可指定null
        }
    }

    sealed class GenericClass3<T> where T : struct {
        public T Factory() {
            return new T(); // 类型参数限制为“值”类型，隐含public无参构造器
        }
    }
    #endregion

    #region 构造器约束
    sealed class GenericClass4<T> where T : new() { // 类型必须定义无参构造器
        public T Factory() {
            return new T();
        }
    }
    #endregion

    #region 次要约束
    sealed class GenericClass5 {
        public static List<TBase> ConvertIList<T, TBase>(IList<T> list) where T : TBase {
            List<TBase> baseList = new List<TBase>(list.Count);
            for (Int32 index = 0; index < list.Count; index++) {
                baseList.Add(list[index]);
            }
            return baseList;
        }
    }
    #endregion
    #endregion
}
