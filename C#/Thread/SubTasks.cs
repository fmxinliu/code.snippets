using System;
using System.Threading.Tasks;

namespace ThreadTest {
    class SubTasks {
        public static void Test() {
            TestSubTasks();
        }

        /// <summary>
        /// 子任务
        /// </summary>
        static void TestSubTasks() {
            Console.WriteLine("----- 父/子任务协调工作 -----");
            var parent = new Task<Int32[]>(() => {
                var results = new Int32[3]; // 创建一个数组来存储结果

                // 这个任务创建并启动 3 个子任务
                new Task(() => results[0] = Sum(10000), TaskCreationOptions.AttachedToParent).Start();
                new Task(() => results[1] = Sum(20000), TaskCreationOptions.AttachedToParent).Start();
                new Task(() => results[2] = Sum(30000), TaskCreationOptions.AttachedToParent).Start();

                /// #1.注意：parent 执行到这，3 个子任务可能还没结束，因此results数组中的结果并不一定是最终结果
                Console.WriteLine("父任务:执行完毕, 但不会启动延续任务");
                return results;
            });

            /// #2.注意：cwt 等待 parent 结束才执行。而 parent 要等待 3 个子任务结束后，才结束
            // 父任务及其子任务运行完成后，用一个延续任务显示结果
            var cwt = parent.ContinueWith(
                parentTask => Array.ForEach(parentTask.Result, Console.WriteLine));

            // 启动父任务
            parent.Start();

            // 测试：父任务会等待所有子任务(以及子任务的子任务)结束
            parent.Wait();
            Console.WriteLine("父任务:结束, 全部子任务结束! 启动延续任务。");
        }

        static Int32 Sum(Int32 x) {
            Int32 sum = 0;
            for (Int32 i = 1; i <= x; i++) {
                checked { sum += i; }
            }
            Console.WriteLine("子任务:结束, Sum({0})={1}.", x, sum);
            return sum;
        }
    }
}
