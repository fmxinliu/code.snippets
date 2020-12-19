using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceTest.InterfaceEIMIInner;

namespace InterfaceTest {
    class InterfaceEIMI {
        public static void Test() {
            IDisposable disposable = new Eimi();
            disposable.Dispose();

            Eimi2 obj2 = new Eimi2();
            obj2.Dispose();
            ((IDisposable)obj2).Dispose();

            Eimi3 obj3 = new Eimi3();
            IForm form = obj3;
            IDialog dialog = obj3;
            Console.WriteLine(obj3.Name());
            Console.WriteLine(form.Name());
            Console.WriteLine(dialog.Name());

            // 容易让人困惑
            Int32 x = 5; // Int32实现了IConvertible接口
            //Single s1 = x.ToSingle(null); // 却不能直接调用“接口实现”
            Single s2 = ((IConvertible)x).ToSingle(null); // 必须通过“接口对象”来调用（转换发生“装箱”）
        }
    }

    namespace InterfaceEIMIInner {
        /// <summary>
        /// 显示接口方法实现
        /// </summary>
        internal class Eimi : IDisposable {
            void IDisposable.Dispose() {
                Console.WriteLine("显示接口方法实现EIMI");
            }
        }

        internal class Eimi2 : IDisposable {
            /// <summary>
            /// 通过类型实例对象调用
            /// </summary>
            public void Dispose() {
                Console.WriteLine("public void Dispose()");
            }

            /// <summary>
            /// 通过接口实例对象调用
            /// </summary>
            void IDisposable.Dispose() {
                Console.WriteLine("void IDisposable.Dispose()");
            }
        }

        #region EIMI实现签名相同的接口
        public interface IForm { String Name(); }
        public interface IDialog { String Name(); }

        internal class Eimi3 : IForm, IDialog {
            String IForm.Name() {
                return "IForm Interface Impl";
            }
            String IDialog.Name() {
                return "IDialog Interface Impl";
            }
            /// <summary>
            /// 不是必须，可以不定义
            /// </summary>
            public String Name() {
                return "public Method(not must-be)";
            }
        }
        #endregion
    }
}
