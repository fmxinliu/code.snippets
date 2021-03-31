using System;

namespace UtilsTool {
    class Program {
        static void Main(string[] args) {
            bool x = WinPowerManagement.ForbidWindowsAutoSleep();
            bool y = WinPowerManagement.RestoreWindowsAutoSleep();
            //AutoStartWhenStartup.Enable();
            //AutoStartWhenStartup.Disable();
            //bool success1 = AutoStartWhenStartup.EnableWithAdmin(AutoStartWhenStartup.CurrentProcess, true);
            //bool success2 = AutoStartWhenStartup.EnableWithAdmin(AutoStartWhenStartup.CurrentProcess, false);
            Console.ReadKey();
        }
    }
}

