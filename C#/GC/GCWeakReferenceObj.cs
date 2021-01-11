using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.CompilerServices;

namespace GCTest {
    class GCWeakReferenceObj {
        public static void Test() {
            TestStrongReference();
            TestWeakReference();
            TestObjGCWatcher();
        }

        static void TestObjGCWatcher() {
            new Object().GCWatch("对象创建于" + DateTime.Now);
            // 主动触发一次回收
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        static void TestStrongReference() {
            MyObject obj = new MyObject("强引用");
            ObjectStrongWrapper wrapper = new ObjectStrongWrapper(obj);
            obj = null;
            GC.KeepAlive(wrapper);
            GC.Collect(); // MyObject对象仍存在强引用(wrapper对象字段)，无法被释放
            Console.WriteLine("\n测试强引用 vs 弱引用，按任意键继续...");
            Console.ReadKey();
        }

        static void TestWeakReference() {
            MyObject obj = new MyObject("弱引用");
            ObjectWeakWrapper wrapper = new ObjectWeakWrapper(obj);
            obj = null;
            GC.KeepAlive(wrapper);
            GC.Collect(); // MyObject对象仍存在弱引用(wrapper对象字段)，可以被释放
            Thread.Sleep(500);
            Console.WriteLine("测试完成，按任意键继续...");
            Console.ReadKey();
        }

        /// <summary>
        /// 对象强引用包装器
        /// </summary>
        sealed class ObjectStrongWrapper {
            private Object target;
            public ObjectStrongWrapper(Object o) {
                this.target = o;
            }
        }

        /// <summary>
        /// 对象弱引用包装器
        /// </summary>
        sealed class ObjectWeakWrapper {
            private WeakReference target;
            public ObjectWeakWrapper(Object o) {
                this.target = new WeakReference(o);
            }
        }

        /// <summary>
        /// 自定义引用对象
        /// </summary>
        sealed class MyObject {
            private readonly String m_info;
            public MyObject(String info) {
                m_info = info;
            }
            ~MyObject() {
                Console.WriteLine(m_info + ": MyObject destoryed.");
            }
        }
    }

    /// <summary>
    /// 对象GC监视器
    /// </summary>
    internal static class GCWatcher {
        /// <summary>
        /// 只要外部不存在强引用，即使弱引用仍存在，GC时对象都会被回收
        /// </summary>
        private static readonly ConditionalWeakTable<Object, NotifyWhenGCd<String>> s_cwt =
            new ConditionalWeakTable<Object, NotifyWhenGCd<String>>();

        private sealed class NotifyWhenGCd<T> {
            private readonly T value;
            internal NotifyWhenGCd(T value) {
                this.value = value;
            }
            ~NotifyWhenGCd() {
                Console.WriteLine("GC'd: {0}{1}", this.value, Environment.NewLine);
            }
        }

        public static T GCWatch<T>(this T @object, String tag) where T : class {
            s_cwt.Add(@object, new NotifyWhenGCd<String>(tag));
            return @object;
        }
    }
}
