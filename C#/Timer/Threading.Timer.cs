using System;
using System.Diagnostics;
using System.Threading;

namespace TimerTest {
    class ThreadingTimer {
        private static Timer timer;
        public static void Test() {
            Console.WriteLine("0: 线程池定时器重入现象复现");
            Console.WriteLine("1: 通过回调函数加锁解决重入");
            Console.WriteLine("2: 通过单次启动定时器解决重入");
            Console.WriteLine("q: 退出");
            Console.WriteLine("-----------------------------");

            while (true) {
                switch ((Char)Console.Read()) {
                    case '0':
                        StopTimer();
                        TestReentry();
                        break;
                    case '1':
                        StopTimer();
                        StartTimer_1();
                        break;
                    case '2':
                        StopTimer();
                        StartTimer_2();
                        break;
                    case 'Q':
                    case 'q':
                        StopTimer();
                        return;
                }
            }
        }

        #region 问题: 线程定时器重入
        static void TestReentry() {
            timer = new Timer(ReentryOnTimer, null, 0, 1000);
        }

        /// <summary>
        /// 发生重入:
        /// (1)回调方法阻塞，
        /// (2)回调方法执行时间 > 定时器周期
        /// </summary>
        static void ReentryOnTimer(Object state) {
            Console.WriteLine("{0,2}, {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now);
            Thread.Sleep(3000);
            Console.WriteLine("==");
        }
        #endregion

        #region (#1)解决: 线程定时器重入问题
        private static Int32 entryCount = 0;
        static void StartTimer_1() {
            Console.WriteLine("no Reentry(#1) Start:  " + DateTime.Now.ToString());
            timer = new Timer(NoReentryOnTimer_1, null, 0, 1000);
        }
        static void NoReentryOnTimer_1(Object state) {
            int count = 0;
            try {
                count = Interlocked.Add(ref entryCount, 1);
                if (count == 1) {
                    Console.WriteLine("no Reentry(#1) Running:" + DateTime.Now.ToString());
                    Thread.Sleep(3000);
                    Console.WriteLine("->");
                }
            }
            finally {
                if (count > 0) {
                    Interlocked.Decrement(ref entryCount);
                }
            }
        }
        #endregion

        #region (#2)解决: 线程定时器重入问题
        static readonly Object obj = new Object();
        static void StartTimer_2() {
            Console.WriteLine("no Reentry(#2) Start:  " + DateTime.Now.ToString());
            timer = new Timer(NoReentryOnTimer_2, null, 1000, Timeout.Infinite); // 启动一次
        }
        static void NoReentryOnTimer_2(Object state) {
            var sw = Stopwatch.StartNew();

            // 定时器实际任务
            Console.WriteLine("no Reentry(#2) Running:" + DateTime.Now.ToString());
            Thread.Sleep(3000);
            Console.WriteLine("-->");

            // 计算下次启动时间
            long dueTime = Math.Max(0, 1000 - sw.ElapsedMilliseconds);
            lock (obj) {
                if (timer != null) {
                    timer.Change(dueTime, Timeout.Infinite); // 再次启动
                }
            }
        }
        #endregion

        private static void StopTimer() {
            lock (obj) {
                if (timer != null) {
                    timer.Dispose();
                    timer = null;
                }
            }
        }
    }
}
