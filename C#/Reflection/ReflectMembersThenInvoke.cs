using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.CSharp.RuntimeBinder;

namespace ReflectionTest {
    class ReflectMembersThenInvoke {
        public static void Test() {
            Type t = typeof(SomeType);
            BindToMemberThenInvokeTheMember(t);
            Console.WriteLine();

            BindToMemberCreateDelegateToMemberThenInvokeTheMember(t);
            Console.WriteLine();

            UseDynamicToBindAndInvokeTheMember(t);
            Console.WriteLine();
        }

        static void BindToMemberThenInvokeTheMember(Type t) {
            Console.WriteLine("BindToMemberThenInvokeTheMember");

            // 构造类型
            ConstructorInfo ctor =
                t.GetConstructor(new Type[] { typeof(System.Int32).MakeByRefType() });
            Object[] x = new Object[] { 12 };
            Console.WriteLine("before constructor: x={0}", x[0]);
            Object obj = ctor.Invoke(x);
            Console.WriteLine("after constructor: x={0}", x[0]);

            // 反射字段(私有)
            FieldInfo fi =
                t.GetField("m_someField", BindingFlags.NonPublic | BindingFlags.Instance);
            fi.SetValue(obj, 100);
            Console.WriteLine("m_someField={0}", fi.GetValue(obj));

            // 反射方法
            MethodInfo mi = t.GetMethod("ToString");
            Console.WriteLine("ToString: {0}", mi.Invoke(obj, null));

            // 反射属性
            PropertyInfo pi = t.GetProperty("SomeProperty");
            try {
                Int32 value = -999;
                var b = t.GetProperty("IgnoreArgumentOutOfRangeExceptionMDAWhenDebug");
                b.SetValue(obj, true, null);
                pi.SetValue(obj, value, null);
                if (System.Diagnostics.Debugger.IsAttached && Convert.ToInt32(pi.GetValue(obj, null)) != value) {
                    Console.WriteLine("catch set property");
                }
            }
            catch (TargetInvocationException e) {
                if (e.InnerException.GetType() != typeof(ArgumentOutOfRangeException)) throw;
                Console.WriteLine("catch set property");
            }
            pi.SetValue(obj, 1, null);
            Console.WriteLine("SomeProperty: {0}", pi.GetValue(obj, null));

            // 反射事件
            EventInfo ei = t.GetEvent("SomeEvent");
            Delegate handler = new EventHandler(Callback);
            ei.AddEventHandler(obj, handler);
            ei.RemoveEventHandler(obj, handler);
        }

        static void BindToMemberCreateDelegateToMemberThenInvokeTheMember(Type t) {
            Console.WriteLine("BindToMemberCreateDelegateToMemberThenInvokeTheMember");

            // 构造类型
            Object[] x = new Object[] { 12 };
            Console.WriteLine("before constructor: x={0}", x[0]);
            Object obj = Activator.CreateInstance(t, x);
            Console.WriteLine("after constructor: x={0}", x[0]);

            // 无法创建构造器、字段的委托

            // 反射方法
            MethodInfo mi = t.GetMethod("ToString");
            var methodInvoker = mi.CreateDelegate<Func<String>>(obj);
            Console.WriteLine("ToString: {0}", methodInvoker());

            // 反射属性
            PropertyInfo pi = t.GetProperty("SomeProperty");
            var setPropertyInvoker = pi.GetSetMethod().CreateDelegate<Action<Int32>>(obj);
            var getPropertyInvoker = pi.GetGetMethod().CreateDelegate<Func<Int32>>(obj);
            try {
                setPropertyInvoker(-999);
            }
            catch (ArgumentOutOfRangeException) {
                Console.WriteLine("catch set property");
            }
            setPropertyInvoker(2);
            Console.WriteLine("SomeProperty: {0}", getPropertyInvoker());

            // 反射事件
            EventInfo ei = t.GetEvent("SomeEvent");
            EventHandler handler = new EventHandler(Callback);
            var addEventInvoker = ei.GetAddMethod().CreateDelegate<Action<EventHandler>>(obj);
            var removeEventInvoker = ei.GetRemoveMethod().CreateDelegate<Action<EventHandler>>(obj);
            addEventInvoker(handler);
            removeEventInvoker(handler);
        }

        static void UseDynamicToBindAndInvokeTheMember(Type t) {
            Console.WriteLine("UseDynamicToBindAndInvokeTheMember");

            // 构造类型
            Object[] x = new Object[] { 12 };
            Console.WriteLine("before constructor: x={0}", x[0]);
            dynamic obj = Activator.CreateInstance(t, x);
            Console.WriteLine("after constructor: x={0}", x[0]);

            // 访问字段
            try {
                obj.m_someField = 12;
                Int32 v = obj.m_someField;
                Console.Write("m_someField: " + v);
            }
            catch (RuntimeBinderException e) {
                /// 访问非 public 字段，引发异常
                Console.Write("m_someField: {0}{1}", e.Message, Environment.NewLine);
            }

            // 访问方法
            var toString = (String)obj.ToString();
            Console.WriteLine("ToString: {0}", toString);

            // 访问属性
            try {
                obj.SomeProperty = -999;
            }
            catch (ArgumentOutOfRangeException) {
                Console.WriteLine("catch set property");
            }
            obj.SomeProperty = 2;
            Console.WriteLine("SomeProperty: {0}", obj.SomeProperty);

            // 访问事件
            EventHandler handler = new EventHandler(Callback);
            obj.SomeEvent += handler;
            obj.SomeEvent -= handler;
        }

        static void Callback(object sender, EventArgs e) {}
    }

    /// <summary>
    /// 反射演示类型
    /// </summary>
    internal sealed class SomeType {
        private Int32 m_someField;
        public SomeType(ref Int32 value) { value *= 2; }
        public override String ToString() { return this.m_someField.ToString(); }
        public Int32 SomeProperty {
            get { return this.m_someField; }
            set {
                if (value < 0) {
                    // 反射触发时，
                    // 如果附加了调试器，可能需关闭 MDA (ArgumentOutOfRangeException)，否则调试器会中断
                    // 方法: 调试 > 异常，找到 ArgumentOutOfRangeException，取消勾选“用户未经处理的”
                    if (System.Diagnostics.Debugger.IsAttached &&
                        this.IgnoreArgumentOutOfRangeExceptionMDAWhenDebug) {
                        return;
                    }
                    else {
                        throw new ArgumentOutOfRangeException("value");
                    }
                }
                this.m_someField = value;
            }
        }
        public event EventHandler SomeEvent;

        // 指示附加了调试器后，是否忽略 MDA (ArgumentOutOfRangeException)
        public Boolean IgnoreArgumentOutOfRangeExceptionMDAWhenDebug { get; set; }
    }

    /// <summary>
    /// 扩展方法
    /// </summary>
    internal static class ReflectDelegate {
        public static TDelegate CreateDelegate<TDelegate>(this MethodInfo mi, Object target = null) {
            return (TDelegate)(Object)Delegate.CreateDelegate(typeof(TDelegate), target, mi);
        }
    }
}
