using System;

namespace UtilsTool {
    class Program {
        static void Main(string[] args) {
            Delay.DelayTest(new InterruptState());
            Console.ReadKey();
        }
    }
}

