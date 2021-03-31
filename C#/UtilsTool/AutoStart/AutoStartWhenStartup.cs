using System;

namespace UtilsTool {
    public static class AutoStartWhenStartup {
        public static readonly string CurrentProcess = WinShortcut.CurrentProcessPath;

        public static void Enable() {
            Enable(WinShortcut.CurrentProcessPath, true);
        }

        public static void Disable() {
            Enable(WinShortcut.CurrentProcessPath, false);
        }

        // 方法1: 在 C:\Users\XXX\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup 创建快捷方式
        public static void Enable(string exePath, bool enable) {
            WinShortcut.SetAutoStartWhenStartup(exePath, enable);
        }

        // 方法2: 在 HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Run 写入注册表字符串值，需管理员权限
        public static bool EnableWithAdmin(string exePath, bool enable) {
            var root = Microsoft.Win32.Registry.LocalMachine;
            var KeyName = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            var itemName = System.IO.Path.GetFileNameWithoutExtension(exePath);
            try {
                if (enable) {
                    RegistryHelper.WriteValueWhenUnmatch(root, KeyName, itemName, exePath, true);
                } else {
                    RegistryHelper.DeleteValue(root, KeyName, itemName);
                }
                return true;
            }
            catch (Exception ex) {
                System.Diagnostics.Debugger.Log(0, "自启动设置失败", ex.ToString());
            }
            return false;
        }
    }
}
