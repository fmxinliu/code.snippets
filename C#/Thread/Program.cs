using System;
using System.Diagnostics;

namespace ThreadTest {
    class Program {
        static void Main(string[] args) {
            ExecutionContextFlow.Test();
            Tasks.Test();
            if (!Debugger.IsAttached) {
                ExecutionCancle.Test();
                AggregateExceptionAnalysis.Test();
                SubTasks.Test();
                TasksFactory.Test();
                TasksScheduler.Test();
            }
            Console.ReadKey();
        }
    }
}
