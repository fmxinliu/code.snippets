using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadTest {
    class TasksScheduler {
        public static void Test() {
            Scheduler();
        }

        static void Scheduler() {
            Application.Run(new MyForm());
        }

        sealed class MyForm : Form {
            /// <summary>
            /// TaskScheduler:
            ///  (1) 线程池调度器(工作线程)
            ///  (2) 同步上下文调度器(GUI)
            /// </summary>
            private TaskScheduler synchronizationContextTaskScheduler;
            public MyForm() {
                synchronizationContextTaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
                this.Text = "单击窗体，开始测试";
                this.Width = 600;
                this.Height = 300;
            }

            private CancellationTokenSource cts;
            protected override void OnMouseClick(MouseEventArgs e) {
                if (cts != null) {
                    cts.Cancel();
                    cts = null;
                }
                else {
                    this.Text = "Running";
                    cts = new CancellationTokenSource();
                    var task = new Task<Int32>(() => Sum(cts.Token, 1000), cts.Token);

                    /// 调度到GUI线程队列，更新UI
                    task.ContinueWith(t => this.Text = "Completed: Result=" + t.Result,
                        synchronizationContextTaskScheduler);
                    task.ContinueWith(t => this.Text = "Canceled",
                        synchronizationContextTaskScheduler);
                    task.ContinueWith(t => this.Text = "Fault",
                        synchronizationContextTaskScheduler);
                    task.Start();
                }
                base.OnMouseClick(e);
            }
        }

        static Int32 Sum(CancellationToken ct, Int32 x) {
            Int32 sum = 0;
            for (Int32 i = 1; i <= x; i++) {
                ct.ThrowIfCancellationRequested();
                checked { sum += i; }
                Thread.Sleep(1);
            }
            Console.WriteLine("Sum({0})={1}.", x, sum);
            return sum;
        }
    }
}
