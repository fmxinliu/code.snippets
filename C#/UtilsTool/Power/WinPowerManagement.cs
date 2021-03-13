using System.Runtime.InteropServices;

namespace UtilsTool {
    public static class WinPowerManagement {
        /// <summary>
        /// 进制操作系统自动进入休眠状态
        /// </summary>
        /// <returns>成功返回true，失败返回false。</returns>
        public static bool ForbidWindowsAutoSleep() {
            uint res = 0;
            res = WinPowerManagementNativeMethods.SetThreadExecutionState((uint)(
                WinPowerManagementNativeMethods.ExecutionState.SystemRequired | WinPowerManagementNativeMethods.ExecutionState.Continuous));
            return res != (uint)WinPowerManagementNativeMethods.ExecutionState.None;
        }

        /// <summary>
        /// 恢复操作系统自动休眠功能
        /// </summary>
        /// <returns>成功返回true，失败返回false。</returns>
        public static bool RestoreWindowsAutoSleep() {
            uint res = 0;
            res = WinPowerManagementNativeMethods.SetThreadExecutionState((uint)WinPowerManagementNativeMethods.ExecutionState.Continuous);
            return res != (uint)WinPowerManagementNativeMethods.ExecutionState.None;
        }

        private class WinPowerManagementNativeMethods {
            #region enum
            public enum ExecutionState : uint {
                None = 0,
                Continuous = 0x80000000,
                DisplayRequired = 0x00000002,
                SystemRequired = 0x00000001,
            }
            #endregion

            #region interop methods
            [DllImport("kernel32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, EntryPoint = "SetThreadExecutionState")]
            public static extern uint SetThreadExecutionState(uint esflags);
            #endregion
        }
    }
}
