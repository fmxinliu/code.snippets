using System;

namespace UtilsTool {
    class Program {
        static void Main(string[] args) {
            bool x = WinPowerManagement.ForbidWindowsAutoSleep();
            bool y = WinPowerManagement.RestoreWindowsAutoSleep();
            //WinShortcut.SetAutoStartWhenStartup(WinShortcut.AppStartupPath, false);
            Console.ReadKey();
        }
    }
}

