using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegateTest {
    class DelegateBasicUsages {
        public static void Test() {
            DelegateInvoker.StaticDelegateDemo();
            DelegateInvoker.InstanceDelegateDemo();
            DelegateInvoker.ChainDelegateDemo1(new DelegateListener());
            DelegateInvoker.ChainDelegateDemo2(new DelegateListener());
        }

        #region 委托基本使用演示

        // 声明一个委托类型，它的实例引用一个方法，
        // 该方法获取一个 Int32 参数，返回 void
        delegate void Feedback(Int32 value);

        class DelegateInvoker {
            public static void StaticDelegateDemo() {
                Console.WriteLine("----- Static Delegete Demo -----");
                Counter(1, 3, null);
                Counter(1, 3, new Feedback(DelegateListener.FeedbackToConsole));
                Counter(1, 1, DelegateListener.GetFeedbackToMsgBoxDelegate()); // 演示调用私有方法
                Console.WriteLine();
            }

            public static void InstanceDelegateDemo() {
                Console.WriteLine("----- Instance Delegete Demo -----");
                DelegateListener listener = new DelegateListener();
                Counter(1, 3, new Feedback(listener.FeedbackToFile));
                Console.WriteLine();
            }

            public static void ChainDelegateDemo1(DelegateListener listener) {
                Console.WriteLine("----- Chain Delegete Demo 1 -----");
                Feedback fb1 = new Feedback(DelegateListener.FeedbackToConsole);
                Feedback fb2 = new Feedback(DelegateListener.FeedbackToMsgBox);
                Feedback fb3 = new Feedback(listener.FeedbackToFile);

                Feedback fbChain = null;
                fbChain = (Feedback)Delegate.Combine(fbChain, fb1);
                fbChain = (Feedback)Delegate.Combine(fbChain, fb2);
                fbChain = (Feedback)Delegate.Combine(fbChain, fb3);
                Counter(1, 1, fbChain);

                fbChain = (Feedback)Delegate.Remove(fbChain, new Feedback(DelegateListener.FeedbackToMsgBox));
                Counter(1, 1, fbChain);
            }

            /// <summary>
            /// 编译器语法糖(重载+=、-=，语法简化)，IL与ChainDelegateDemo1相同
            /// </summary>
            public static void ChainDelegateDemo2(DelegateListener listener) {
                Console.WriteLine("----- Chain Delegete Demo 2 -----");
                Feedback fb1 = new Feedback(DelegateListener.FeedbackToConsole);
                Feedback fb2 = new Feedback(DelegateListener.FeedbackToMsgBox);
                Feedback fb3 = new Feedback(listener.FeedbackToFile);

                Feedback fbChain = null;
                fbChain += fb1;
                fbChain += fb2;
                fbChain += fb3;
                Counter(1, 1, fbChain);

                fbChain -= new Feedback(DelegateListener.FeedbackToMsgBox);
                Counter(1, 1, fbChain);
            }

            private static void Counter(Int32 from, Int32 to, Feedback fb) {
                for (Int32 val = from; val <= to; ++val) {
                    if (fb != null) {
                        fb(val);
                    }
                }
            }
        }

        class DelegateListener {
            /// <summary>
            /// 仅用来演示: 委托可以调用注册类的私有方法(实际使用时，注册类完全不用关心委托)
            /// </summary>
            public static Feedback GetFeedbackToMsgBoxDelegate() {
                return new Feedback(FeedbackToMsgBoxPrivate);
            }

            private static void FeedbackToMsgBoxPrivate(Int32 value) {
                DelegateListener.FeedbackToMsgBox(value);
            }

            public static void FeedbackToConsole(Int32 value) {
                Console.WriteLine("Item=" + value.ToString());
            }

            public static void FeedbackToMsgBox(Int32 value) {
                System.Windows.Forms.MessageBox.Show("Item=" + value.ToString());
            }

            public void FeedbackToFile(Int32 value) {
                using (var sw = new System.IO.StreamWriter("Status.txt", true)) {
                    sw.WriteLine("Item=" + value.ToString());
                }
            }
        }

        #endregion
    }
}
