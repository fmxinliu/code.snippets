using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegateTest {
    class DelegateMulticastAnalyse {
        public static void Test() {
            Delegate d =
            DelegateChainOperate.Combine();
            DelegateChainOperate.Remove(d);
            DelegateChainOperate.CombineChain();
        }

        /// <summary>
        /// Delegate 内部包含 3 个字段：
        /// - _target: 注册实例方法的对象，回调时，以this形式传给方法【回调函数为静态方法时，访问该字段返回null】
        /// - _methodPtr: 回调方法的地址
        /// - _invocationList: 委托列表
        /// </summary>
        class DelegateChainOperate {
            delegate void Feedback();

            /// <summary>
            /// 单个委托依次连接，形成委托链
            /// </summary>
            public static Delegate Combine() {
                Console.WriteLine("---- Combine ----");
                Feedback fb1 = new Feedback(Feedback1);
                Feedback fb2 = new Feedback(Feedback2);
                Feedback fb3 = new Feedback(Feedback3);

                Feedback fbChain = null;
                // 第一次: fbChain 和 fb1 指向同一个委托，委托列表=null
                fbChain = (Feedback)Delegate.Combine(fbChain, fb1);
                ShowInvocationNum("Delegate.Combine: ", fbChain); // #委托列表为null，只有当前委托
                fbChain();

                // 第二次: fbChain 指向一个新的委托，委托列表中包含 fb1、fb2
                fbChain = (Feedback)Delegate.Combine(fbChain, fb2);
                ShowInvocationNum("Delegate.Combine: ", fbChain);
                fbChain();

                // 第三次: fbChain 指向一个新的委托，委托列表中包含 fb1、fb2、fb3
                fbChain = (Feedback)Delegate.Combine(fbChain, fb3);
                ShowInvocationNum("Delegate.Combine: ", fbChain);
                fbChain();

                return fbChain;
            }

            public static void Remove(Delegate d) {
                Console.WriteLine("---- Remove ----");
                Feedback fbChain = d as Feedback;
                if (fbChain == null) {
                    return;
                }
                Feedback fb1 = new Feedback(Feedback1);
                Feedback fb2 = new Feedback(Feedback2);
                Feedback fb3 = new Feedback(Feedback3);

                // 初始:   fbChain 指向一个委托，委托列表中包含 fb1、fb2、fb3
                ShowInvocationNum("Delegate.Remove: ", fbChain);
                fbChain();

                // 第一次: fbChain 指向一个新的委托，委托列表中包含 fb2、fb3
                fbChain = (Feedback)Delegate.Remove(fbChain, fb1);
                ShowInvocationNum("Delegate.Remove: ", fbChain);
                fbChain();

                // 第二次: fbChain 和 fb3 指向同一个委托，委托列表=null
                fbChain = (Feedback)Delegate.Remove(fbChain, fb2);
                ShowInvocationNum("Delegate.Remove: ", fbChain); // #委托列表为null，只有当前委托
                fbChain();

                // 第三次: fbChain = null
                fbChain = (Feedback)Delegate.Remove(fbChain, fb3);
            }

            /// <summary>
            /// 委托链连接另一个委托链
            /// </summary>
            public static void CombineChain() {
                Console.WriteLine("---- Combine Chain ----");
                Feedback fb1 = new Feedback(Feedback1);
                Feedback fb2 = new Feedback(Feedback2);
                Feedback fb3 = new Feedback(Feedback3);

                Feedback fbChain1 = null;
                Feedback fbChain2 = null;

                // 第一次: fbChain1 和 fb1 指向同一个委托，委托列表=null
                fbChain1 = (Feedback)Delegate.Combine(fbChain1, fb1);
                ShowInvocationNum("fbChain1: ", fbChain1); // #委托列表为null，只有当前委托
                fbChain1();

                // 第二次: fbChain1 指向一个新的委托，委托列表中包含 fb1、fb2
                fbChain1 = (Feedback)Delegate.Combine(fbChain1, fb2);
                ShowInvocationNum("fbChain1: ", fbChain1);
                fbChain1();

                // 第三次: fbChain2 和 fb3 指向同一个委托，委托列表=null
                fbChain2 = (Feedback)Delegate.Combine(fbChain2, fb3);
                ShowInvocationNum("fbChain2: ", fbChain2);
                fbChain2();

                // 第四次: fbChain2 指向一个新的委托，委托列表中包含 fb1、fb2、fb3
                fbChain2 = (Feedback)Delegate.Combine(fbChain2, fbChain1); // #fbChain1委托列表中的每个委托，都加入到fbChain2的委托列表中
                ShowInvocationNum("fbChain2: ", fbChain2);
                fbChain2();
            }

            /// <summary>
            /// GetInvocationList() 原理:
            /// - (1) 当委托列表为 null，表明当前是单个委托，返回当前委托，长度 1
            /// - (2) 当委托列表不为 null，表明当前是多播，返回_invocationList中的所有委托
            /// </summary>
            private static void ShowInvocationNum(String msg, Delegate @delegate) {
                if (@delegate != null) {
                    Console.WriteLine(msg + "Count=" + @delegate.GetInvocationList().Length.ToString());
                }
            }

            private static void Feedback1() {
                Console.WriteLine("Feedback #1");
            }

            private static void Feedback2() {
                Console.WriteLine("Feedback #2");
            }

            private static void Feedback3() {
                Console.WriteLine("Feedback #3");
            }
        }
    }
}
