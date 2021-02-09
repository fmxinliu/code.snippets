using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest {
    class TasksFactory {
        public static void Test() {
            var parent = new Task(() => {
                var cts = new CancellationTokenSource();
                var tf = new TaskFactory(
                    cts.Token,
                    TaskCreationOptions.AttachedToParent,
                    TaskContinuationOptions.ExecuteSynchronously,
                    TaskScheduler.Default);

                // 创建 3 个子任务
                var childTasks = new[] {
                    tf.StartNew(() => Sum(cts.Token, 1000), cts.Token),
                    tf.StartNew(() => Sum(cts.Token, 2000), cts.Token),
                    tf.StartNew(() => Sum(cts.Token, Int32.MaxValue), cts.Token) // OverflowException
                };

                // 任何子任务抛出异常，就取消其余子任务
                for (Int32 i = 0; i < childTasks.Length; i++) {
                    childTasks[i].ContinueWith(t => cts.Cancel(), TaskContinuationOptions.OnlyOnFaulted);
                }

                // 所有子任务完成后，从未出错/未取消的任务获取结果最大值
                // 然后将最大值传给另一个任务显示
                tf.ContinueWhenAll(
                    childTasks,
                    completedTasks => completedTasks
                        .Where(t => !t.IsFaulted && !t.IsCanceled)
                        .Max(t => t.Result),
                    CancellationToken.None /// 不允许取消操作
                ).ContinueWith(
                    t => Console.WriteLine("The maximum is: " + t.Result),
                    TaskContinuationOptions.ExecuteSynchronously);
            });

            // 打印发生的异常
            parent.ContinueWith(t => {
                var sb = new StringBuilder("Occur follows exception(s)\n");
                foreach (var e in t.Exception.Flatten().InnerExceptions) {
                    sb.AppendLine(" " + e.GetType());
                }
                Console.WriteLine(sb.ToString());
            }, TaskContinuationOptions.OnlyOnFaulted);

            // -------
            // 启动父任务
            parent.Start();
        }

        static Int32 Sum(CancellationToken ct, Int32 x) {
            Int32 sum = 0;
            for (Int32 i = 1; i <= x; i++) {
                ct.ThrowIfCancellationRequested();
                checked { sum += i; }
            }
            Console.WriteLine("Sum({0})={1}.", x, sum);
            return sum;
        }
    }
}
