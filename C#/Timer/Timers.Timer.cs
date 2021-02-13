using System;
using System.Timers;

namespace TimerTest {
    class TimersTimer {
        public static void Test() {
            Console.WriteLine("按'q'键退出，其他键继续...");
            if (Char.ToLower((Char)Console.Read()) == 'q') {
                return;
            }

            StartTimer();
            while (Char.ToLower((Char)Console.Read()) != 'q');
            StopTimer();
        }

        static Timer t;
        static readonly Object obj = new Object();
        static void StartTimer() {
            t = new Timer();
            //t.BeginInit();
            t.AutoReset = true;
            t.Interval = 1000;
            t.Elapsed += new ElapsedEventHandler(Timer_Elapsed_2);
            t.Start();
            //t.EndInit();
        }
        static void StopTimer() {
            lock (obj) {
                if (t != null) {
                    t.Dispose();
                    t = null;
                }
            }
        }

        static Int32 entryCount = 0;
        static void Timer_Elapsed(object sender, ElapsedEventArgs e) {
            int count = 0;
            try {
                count = System.Threading.Interlocked.Add(ref entryCount, 1);
                if (count != 1) {
                    Console.WriteLine("Timer_Elapsed: Reentry > " + DateTime.Now.ToString());
                    return;
                }
                Console.WriteLine("Timer_Elapsed: Running > " + DateTime.Now.ToString());
                System.Threading.Thread.Sleep(3000);
                Console.WriteLine("->");
            }
            finally {
                if (count > 0) {
                    System.Threading.Interlocked.Decrement(ref entryCount);
                }
            }
        }
        static void Timer_Elapsed_2(object sender, ElapsedEventArgs e) {
            // 关闭定时器，防止任务执行期间再次触发，导致重入
            lock (obj) {
                t.Stop();
            }

            // 实际任务
            Console.WriteLine("Timer_Elapsed: Running > " + DateTime.Now.ToString());
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("->");

            // 启动下次定时
            lock (obj) {
                t.Start();
            }
        }
	}
}
