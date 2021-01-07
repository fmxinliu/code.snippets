using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime;
using System.Runtime.CompilerServices;

namespace GCTest {
    class GCCollects {
        public static void Test() {
            GCMode();
            GCCollect();
        }

        static void GCMode() {
            // 1
            String gcMode = GCSettings.IsServerGC ? "服务器" : "工作站";
            Console.WriteLine("当前GC运行模式: " + gcMode);
            // 2
            GCLatencyMode gcLatencyMode = GCSettings.LatencyMode;
            var gcLatencyModeList = new Dictionary<GCLatencyMode, String>();
            gcLatencyModeList.Add(GCLatencyMode.Batch, "禁用并发GC");
            gcLatencyModeList.Add(GCLatencyMode.Interactive, "启用并发GC");
            gcLatencyModeList.Add(GCLatencyMode.LowLatency, "尽量避免第 2 代GC");
            Console.WriteLine("当前GC侵入模式: " + gcLatencyModeList[gcLatencyMode]);
            // 3
            // #启用低时延，更容易发生OutOfMemoryException，切换时间要尽量短
            RuntimeHelpers.PrepareConstrainedRegions();
            try {
                GCSettings.LatencyMode = GCLatencyMode.LowLatency; // 切换到指定模式
                // 这里执行用户代码
            }
            finally {
                GCSettings.LatencyMode = gcLatencyMode; // 切换回先前模式
            }
            Console.WriteLine();
        }

        private static String title = "GC基本测试";
        static void GCCollect() {
            Console.WriteLine("系统支持的GC代数: " + GC.MaxGeneration);
            Int32 before = GC.CollectionCount(0);
            Int32 currentGenbefore = GC.GetGeneration(title); // 对象当前所处的代数
            GC.Collect(1); // 强制第0、1代回收
            Int32 currentGenafter = GC.GetGeneration(title);
            Int32 after = GC.CollectionCount(0);
            GC.Collect(); // 强制完全GC
            Int32 currentGenafter2 = GC.GetGeneration(title);
            Int32 after2 = GC.CollectionCount(0);

            Console.WriteLine("===强制GC回收第0、1代 2 次===");
            Console.WriteLine("第一次回收前，第0代GC回收次数: " + currentGenbefore);
            Console.WriteLine("第一次回收后，第0代GC回收次数: " + currentGenafter);
            Console.WriteLine("第二次回收后，第0代GC回收次数: " + currentGenafter2);
            Console.WriteLine("第一次回收前，对象所处的代数: " + before);
            Console.WriteLine("第一次回收后，对象所处的代数: " + after);
            Console.WriteLine("第二次回收后，对象所处的代数: " + after2);
        }
    }
}
