using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegateTest {
    /// <summary>
    /// 委托多播存在缺陷：
    /// 1.某个委托抛出异常(或执行占用太长时间)，会导致委托链中断(或整体延时)
    /// 2.如果委托有返回值，只能返回最后一个委托回调执行的返回值
    /// </summary>
    class DelegateMulticastQuestions {
        public static void Test() {
            DelegateChainOperate.Question1();
            DelegateChainOperate.Question2();
        }

        class DelegateChainOperate {
            delegate String Feedback();

            public static void Question1() {
                Feedback fbChain = null;
                fbChain += Feedback1;
                fbChain += () => {
                    throw new Exception("委托 #2抛出异常");
                };
                fbChain += Feedback3;

                try {
                    fbChain();
                }
                catch (Exception ex){
                    Console.WriteLine(ex.Message + "，多播中断");
                }
            }

            public static void Question2() {
                Feedback fbChain = null;
                fbChain += Feedback1;
                fbChain += Feedback2;
                fbChain += Feedback3;
                Console.WriteLine("委托多播执行结果：" + fbChain());
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
