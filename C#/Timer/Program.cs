﻿using System;

namespace TimerTest {
    static class Program {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main() {
            //ThreadingTimer.Test();
            TimersTimer.Test();
        }
    }
}
