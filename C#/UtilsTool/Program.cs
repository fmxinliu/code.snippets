using System;

namespace UtilsTool {
    class Program {
        static void Main(string[] args) {
            bool x = WinPowerManagement.ForbidWindowsAutoSleep();
            bool y = WinPowerManagement.RestoreWindowsAutoSleep();
            Console.ReadKey();
        }
    }
}

