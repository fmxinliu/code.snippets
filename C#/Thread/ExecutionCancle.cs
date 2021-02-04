using System;
using System.Threading;

namespace ThreadTest {
    class ExecutionCancle {
        public static void Test() {
            TestCancleOperation();
            TestCancleCallback();
            TestCancleCallbackThrowException();
            TestLinkedCancle();
        }

        /// <summary>
        /// 取消操作的基本使用（CancellationTokenSource执行一次取消，下次使用必须重新创建）
        /// </summary>
        static void TestCancleOperation() {
            // 取消操作
            var cts1 = new CancellationTokenSource();
            Console.WriteLine("\n==取消操作");
            ThreadPool.QueueUserWorkItem(o => Sum(cts1.Token, 5000));
            Thread.Sleep(1000);
            cts1.Cancel();
            Console.ReadKey();


            // 不允许取消操作
            var cts2 = new CancellationTokenSource();
            Console.WriteLine("\n==不允许取消操作");
            ThreadPool.QueueUserWorkItem(o => Sum(CancellationToken.None, 5000));
            Thread.Sleep(1000);
            cts2.Cancel();
            Console.ReadKey();
        }

        /// <summary>
        /// 注册取消操作引发的回调函数
        /// </summary>
        static void TestCancleCallback() {
            var cts = new CancellationTokenSource();
            var ctr1 = cts.Token.Register(() => Console.WriteLine("回调1"));
            var ctr2 = cts.Token.Register(() => Console.WriteLine("回调2")); // 反向顺序调用
            cts.Cancel();
        }

        /// <summary>
        /// 回调函数抛出异常
        /// </summary>
        static void TestCancleCallbackThrowException() {
            var cts = new CancellationTokenSource();
            cts.Token.Register(() => Console.WriteLine("测试: 回调函数抛出异常"));
            cts.Token.Register(() => {
                String s = null;
                s.IndexOf("");
            });
            cts.Token.Register(() => {
                int i = 0;
                int j = 100 / i;
            });

            /// 如果调试模式会中断，请在运行模式下测试
            try {
                //cts.Cancel(true); // 回调函数链执行抛出第一个异常，就中断
                cts.Cancel(false); // 回调函数链执行完毕，才抛出抛出异常链
            }
            catch (Exception e) {
                if (e is DivideByZeroException) {
                    Console.WriteLine("Catch:DivideByZeroException");
                }
                else if (e is NullReferenceException) {
                    Console.WriteLine("Catch:NullReferenceException");
                }
                else if (e is AggregateException) {
                    Console.WriteLine("Catch:AggregateException");
                    foreach (var ex in ((AggregateException)e).InnerExceptions) {
                        Console.WriteLine(" " + ex.GetType());
                    }
                }
                else {
                    throw;
                }
            }
        }

        /// <summary>
        /// 取消组合体：其中任何一个体取消，则组合体就取消
        /// </summary>
        static void TestLinkedCancle() {
            var cts1 = new CancellationTokenSource();
            var cts2 = new CancellationTokenSource();
            var linkedcts = CancellationTokenSource.CreateLinkedTokenSource(cts1.Token, cts2.Token);

            // cts2 取消，linkedcts 自动取消
            cts2.Cancel();
            Console.WriteLine("cts1:{0}, cts2:{1}, linkedcts:{2}",
                cts1.IsCancellationRequested, cts2.IsCancellationRequested, linkedcts.IsCancellationRequested);
        }

        static Int32 Sum(CancellationToken ct, Int32 x) {
            Console.WriteLine("Sum(CancellationToken ct, Int32 x) Running");
            Int32 sum = 0;
            for (Int32 i = 1; i <= x; i++) {
                if (ct.IsCancellationRequested) {
                    Console.WriteLine("Sum(CancellationToken ct, Int32 x) break");
                    break;
                }

                checked { sum += i; }
                Thread.Sleep(1);
            }

            Console.WriteLine("Sum(CancellationToken ct, Int32 x) end. Sum=" + sum);
            return sum;
        }
    }
}
