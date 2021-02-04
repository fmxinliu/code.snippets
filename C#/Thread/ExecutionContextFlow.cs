using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace ThreadTest {
    class ExecutionContextFlow {
        public static void Test() {
            CallContext.LogicalSetData("Name", "调用线程的执行上下文");
            ThreadPool.QueueUserWorkItem(o => Console.WriteLine("1:" + CallContext.LogicalGetData("Name")));

            // 禁止上下文流动，可优化性能
            ExecutionContext.SuppressFlow();
            ThreadPool.QueueUserWorkItem(o => Console.WriteLine("2:" + CallContext.LogicalGetData("Name"))); // null

            // 恢复上下文流动
            ExecutionContext.RestoreFlow();
            ThreadPool.QueueUserWorkItem(o => Console.WriteLine("3:" + CallContext.LogicalGetData("Name")));

            // 清空
            Thread.Sleep(500);
            var beforeFree = CallContext.LogicalGetData("Name");
            CallContext.FreeNamedDataSlot("Name"); // 清空
            var afterFree = CallContext.LogicalGetData("Name");
            Console.WriteLine("beforeFree={0}, afterFree={1}", beforeFree, afterFree);
        }
    }
}
