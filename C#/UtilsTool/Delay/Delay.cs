using System;
using System.Threading;

namespace UtilsTool {
    /// <summary>
    /// 延时类
    /// </summary>
    public static class Delay {
        public static void Start(int millisecondsTimeout) {
            Int64 stop = Environment.TickCount + millisecondsTimeout;
            while (Environment.TickCount < stop) {
                Thread.Sleep(1);
            }
        }

        // interrupt() - 查询外部是否置位终止标志，提前结束延时
        public static void Start(int millisecondsTimeout, Func<bool> interrupt) {
            Int64 stop = Environment.TickCount + millisecondsTimeout;
            while (Environment.TickCount < stop && !interrupt()) {
                Thread.Sleep(1);
            }
        }

        public static void Start(int millisecondsTimeout, InterruptState state) {
            Int64 stop = Environment.TickCount + millisecondsTimeout;
            while (Environment.TickCount < stop && !state.IsSet()) {
                Thread.Sleep(1);
            }
        }

        public static void DelayTest(InterruptState state) {
            ThreadPool.QueueUserWorkItem(o => {
                Console.WriteLine("开始100000ms延时");
                Delay.Start(100000, state);
                Console.WriteLine("100000ms已终止");
            });

            ThreadPool.QueueUserWorkItem(o => {
                Console.WriteLine("延时3s");
                Delay.Start(3000);
                Console.WriteLine("终止100000ms延时");
                state.Set();
            });
        }
    }
}
