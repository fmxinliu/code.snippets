using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using IWshRuntimeLibrary; // 需引用 COM : Windows Script Host Object Model

namespace UtilsTool {
    public class WinShortcut {
        /// <summary>
        /// 桌面路径
        /// </summary>
        private static readonly string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        /// <summary>
        /// 系统开机自启动目录
        /// </summary>
        public static readonly string SystemStartupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

        /// <summary>
        /// 当前程序全路径
        /// </summary>
        public static readonly string AppStartupPath = Process.GetCurrentProcess().MainModule.FileName;

        /// <summary>
        /// 设置开机自动启动-只需要调用改方法就可以了参数里面的bool变量是控制开机启动的开关的，默认为开启自启启动
        /// </summary>
        /// <param name="enable">是否开机自启</param>
        public static void SetAutoStartWhenStartup(string exePath, bool enable, bool createDesktopShortcut = false) {
            var startupShortcuts = GetLnkFileFromFolder(SystemStartupPath, exePath);
            if (enable) {
                if (startupShortcuts.Count > 1) {
                    // 存在多个快捷方式，只保留一个
                    for (int i = 1; i < startupShortcuts.Count; i++) {
                        DeleteFile(startupShortcuts[i]);
                    }
                }
                else if (startupShortcuts.Count < 1) {
                    // 不存在快捷方式，新创建一个
                    var shortcutName = Path.GetFileNameWithoutExtension(exePath);
                    CreateShortcut(SystemStartupPath, shortcutName, exePath, shortcutName);
                }
            }
            else { // 删除所有快捷方式
                for (int i = 0; i < startupShortcuts.Count; i++) {
                    DeleteFile(startupShortcuts[i]);
                }
            }

            if (!createDesktopShortcut) {
                return;
            }

            var desktopShortcuts = GetLnkFileFromFolder(DesktopPath, exePath);
            for (int i = 1; i < desktopShortcuts.Count; i++) {
                DeleteFile(desktopShortcuts[i]);
            }

            if (desktopShortcuts.Count < 1) {
                var shortcutName = Path.GetFileNameWithoutExtension(exePath);
                CreateShortcut(DesktopPath, shortcutName, exePath, shortcutName);
            }
        }

        /// <summary>
        /// 删除指定文件
        /// </summary>
        /// <param name="path">路径</param>
        public static void DeleteFile(string path) {
            FileAttributes attr = System.IO.File.GetAttributes(path);
            if (attr == FileAttributes.Directory) {
                //Directory.Delete(path, true);
            }
            else {
                System.IO.File.Delete(path);
            }
        }

        /// <summary>
        /// 获取指定目录下，指定应用程序的所有快捷方式
        /// </summary>
        /// <param name="directory">待查找的目录</param>
        /// <param name="targetPath">待查找的应用程序路径</param>
        /// <returns>快捷方式路径集合</returns>
        public static List<string> GetLnkFileFromFolder(string directory, string targetPath) {
            var targetLnkPathList = new List<string>();
            var files = Directory.GetFiles(directory, "*.lnk");
            foreach (var file in files) {
                string tempPath = GetTargetPathFromLnkFile(file);
                if (string.Compare(tempPath, targetPath, true) == 0) {
                    targetLnkPathList.Add(file);
                }
            }
            return targetLnkPathList;
        }

        /// <summary>
        /// 获取快捷方式链接的目标文件
        /// </summary>
        /// <param name="shortcutPath">快捷方式文件的路径，如: @"d:\Test.lnk"</param>
        /// <returns>快捷方式引用的目标文件路径</returns>
        public static string GetTargetPathFromLnkFile(string shortcutPath) {
            if (System.IO.File.Exists(shortcutPath)) {
                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
                // 快捷方式文件指向的路径.Text = 当前快捷方式文件IWshShortcut类.TargetPath;
                // 快捷方式文件指向的目标目录.Text = 当前快捷方式文件IWshShortcut类.WorkingDirectory;
                return shortcut.TargetPath;
            }
            return string.Empty;
        }

        /// <summary>
        /// 向目标路径创建指定文件的快捷方式
        /// </summary>
        /// <param name="directory">目标目录</param>
        /// <param name="shortcutName">快捷方式名字</param>
        /// <param name="targetPath">文件全路径</param>
        /// <param name="description">描述</param>
        /// <param name="iconLocation">图标地址</param>
        /// <returns>成功或失败</returns>
        public static bool CreateShortcut(string directory, string shortcutName, string targetPath, string description = null, string iconLocation = null) {
            try {
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);                         // 目录不存在则创建
                string shortcutPath = Path.Combine(directory, string.Format("{0}.lnk", shortcutName));          // 生成快捷方式路径
                WshShell shell = new IWshRuntimeLibrary.WshShell();
                IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(shortcutPath);    // 创建快捷方式对象
                shortcut.TargetPath = targetPath;                                                               // 指定目标路径
                shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath);                                  // 设置起始位置
                shortcut.WindowStyle = 1;                                                                       // 设置运行方式，默认为常规窗口
                shortcut.Description = description;                                                             // 设置备注
                shortcut.IconLocation = string.IsNullOrWhiteSpace(iconLocation) ? targetPath : iconLocation;    // 设置图标路径
                shortcut.Save();                                                                                // 保存快捷方式
                return true;
            }
            catch (Exception ex) {
                System.Diagnostics.Debugger.Log(0, "快捷方式创建失败", ex.ToString());
            }
            return false;
        }
    }
}
