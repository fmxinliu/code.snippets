using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest {
    class Tasks {
        public static void Test() {
            TestBasicUsages();
            TestTaskChain();
            //TestTaskThrowException();
            TestMonitorTaskRunning();
        }

        /// <summary>
        /// 任务可以返回结果（调用点能知道任务是否结束），而线程池不能
        /// </summary>
        static void TestBasicUsages() {
            var task = new Task<Int32>(n => Sum((Int32)n), 100);
            task.Start();
            Console.WriteLine("sum={0}\n", task.Result); // task.Result 会调用 task.Wait()等待任务执行结束
        }

        /// <summary>
        /// 任务会“吞噬”异常，调用 Wait() 会抛出 AggregateException
        /// </summary>
        static void TestThrowException() {
            var task = new Task<Int32>(n => Sum((Int32)n), 1000000000);
            task.Start();
            try {
                task.Wait();
                Console.WriteLine("sum=" + task.Result);
            }
            catch (AggregateException e) {
                e.Handle(ex => ex is OverflowException);
                Console.WriteLine("Completed={0},Faulted={1},Exception={2}\n",
                    task.IsCompleted, task.IsFaulted, task.Exception.InnerException.Message);
            }
        }

        /// <summary>
        /// 延续任务
        /// </summary>
        static void TestTaskChain() {
            var task = new Task<Int32>(n => Sum((Int32)n), 10000);
            var lasttask =
            task.ContinueWith(t => Console.WriteLine("计算结束"))
                .ContinueWith(t => Console.WriteLine("计算结果Sum={0}", task.Result));
            task.Start();
            lasttask.Wait(); // 等待最后一个任务执行结束
            Console.WriteLine("->全部任务执行结束\n");

            var tasks = new Task[3];
            tasks[0] = new Task<Int32>(n => Sum((Int32)n), 10000);
            tasks[1] = tasks[0].ContinueWith(t => Console.WriteLine("计算结束"));
            tasks[2] = tasks[1].ContinueWith(t => Console.WriteLine("计算结果Sum={0}", ((Task<Int32>)tasks[0]).Result));
            tasks[0].Start();
            Task.WaitAll(tasks); // 等待所有任务执行结束
            Console.WriteLine("->全部任务执行结束\n");
        }

        /// <summary>
        /// 利用延续任务监视任务执行情况
        /// </summary>
        static void TestMonitorTaskRunning() {
            var cts = new CancellationTokenSource();
            var task = new Task<Int32>(() => Sum(cts.Token, 10000), cts.Token);
            task.ContinueWith(t => Console.WriteLine("任务执行成功.Sum={0}\n", t.Result), TaskContinuationOptions.OnlyOnRanToCompletion);
            task.ContinueWith(t => Console.WriteLine("任务抛出异常\n"), TaskContinuationOptions.OnlyOnFaulted);
            task.ContinueWith(t => Console.WriteLine("任务被取消\n"), TaskContinuationOptions.OnlyOnCanceled);
            task.Start();
            Thread.Yield(); // 让出CPU，让其他线程有机会执行
            //Thread.Sleep(0);
            cts.Cancel();
        }

        static Int32 Sum(Int32 x, Boolean showInfo = false) {
            Int32 sum = 0;
            for (Int32 i = 1; i <= x; i++) {
                checked { sum += i; }
            }
            if (showInfo) {
                Console.WriteLine("Sum({0})={1}.", x, sum);
            }
            return sum;
        }

        static Int32 Sum(CancellationToken ct, Int32 x) {
            Int32 sum = 0;
            for (Int32 i = 1; i <= x; i++) {
                // 非Task(如ThreadPool)取消操作: 触发取消，跳出
                //if (ct.IsCancellationRequested) {
                //    break;
                //}
                // Task取消操作: 触发取消，抛出异常。用于区分Task是正常完成还是被取消
                ct.ThrowIfCancellationRequested();
                checked { sum += i; }
                Thread.Sleep(1);
            }
            return sum;
        }
    }
}
