using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegateTest {
    class DelegateMulticastOptimize {
        public static void Test() {
            DelegateChainOperate.FixQuestion1();
            DelegateChainOperate.FixQuestion2();
        }

        class DelegateChainOperate {
            delegate String Feedback();

            public static void FixQuestion1() {
                Feedback fbChain = null;
                fbChain += Feedback1;
                fbChain += () => {
                    throw new Exception("委托 #2抛出异常");
                };
                fbChain += Feedback3;

                Delegate[] delegates = fbChain.GetInvocationList();
                /// 注意 foreach 中的 fb 类型
                foreach (Feedback fb in delegates) {
                    try {
                        fb();
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message + "，当前委托中断，继续执行下个委托");
                    }
                }
            }

            public static void FixQuestion2() {
                Feedback fbChain = null;
                fbChain += Feedback1;
                fbChain += Feedback2;
                fbChain += Feedback3;

                StringBuilder sb = new StringBuilder();
                Delegate[] delegates = fbChain.GetInvocationList();

                for (Int32 i = 0; i < delegates.Length; i++) {
                    Feedback fb = delegates[i] as Feedback;
                    if (fb != null) {
                        sb.AppendFormat("{2}{0} 执行结果为: {1}", (i + 1).ToString(), fb(), Environment.NewLine);
                    }
                }

                Console.WriteLine("委托多播执行结果：" + sb.ToString());
            }

            private static String Feedback1() {
                String s = "委托 #1";
                Console.WriteLine(s);
                return s;
            }

            private static String Feedback2() {
                String s = "委托 #2";
                Console.WriteLine(s);
                return s;
            }

            private static String Feedback3() {
                String s = "委托 #3";
                Console.WriteLine(s);
                return s;
            }
        }
    }
}
