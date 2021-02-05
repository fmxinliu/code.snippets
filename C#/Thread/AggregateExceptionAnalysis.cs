using System;
using System.Threading;

namespace ThreadTest {
    class AggregateExceptionAnalysis {
        public static void Test() {
            Console.WriteLine();
            ThrowAggregateException();
        }

        static void ThrowAggregateException() {
            var cts = new CancellationTokenSource();
            cts.Token.Register(() => {
                throw new Exception("异常1");
            });
            cts.Token.Register(() => {
                throw new Exception("异常2");
            });

            try {
                try {
                    cts.Cancel(false);
                }
                catch (AggregateException e) {
                    var f = e.GetBaseException();
                    Console.WriteLine("内层捕获到异常:AggregateException");
                    foreach (var ex in e.InnerExceptions) {
                        Console.WriteLine(" " + ex.Message);
                    }

                    // 只处理指定的异常
                    // 未处理的异常，包装成新的AggregateException抛出
                    Console.WriteLine("内层处理异常:异常1");
                    e.Handle(ex => ex.Message == "异常1");
                }
            }
            catch (AggregateException e) {
                Console.WriteLine("外层捕获到异常：AggregateException");
                foreach (var ex in e.InnerExceptions) {
                    Console.WriteLine(" " + ex.Message);
                }
            }
        }
    }
}
