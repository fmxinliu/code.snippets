using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace UtilsTool {
    public static class UtilTools {
        private static readonly object obj = new object();
        private static Dictionary<string, Mutex> mutexs = new Dictionary<string, Mutex>();

        #region 单例
        // 获得互斥体
        public static bool IsSingleton(string identifying) {
            bool isSingleton = false;
            lock (obj) {
                Mutex mutex = new Mutex(false, identifying, out isSingleton);
                if (isSingleton) {
                    mutexs.Add(identifying, mutex);
                }
            }
            return isSingleton;
        }

        // 释放互斥体
        public static void ReleaseSingleton(string identifying) {
            lock (obj) {
                if (mutexs.ContainsKey(identifying)) {
                    mutexs[identifying].Close();
                    mutexs.Remove(identifying);
                }
            }
        }
        #endregion

        #region MiniDumper
        // 初始化min dumper
        public static void InitMiniDumper(string path="", string suffix="", int maxNum = 3) {
            MiniDumper.MaxDumpNum = maxNum >= 0 ? maxNum : 3;
            MiniDumper.ProcessSuffix = string.IsNullOrWhiteSpace(suffix) ? ExecutableName : suffix;
            MiniDumper.DumperDir = string.IsNullOrWhiteSpace(path) ? StartupPath + @"dumps\" : path;
            //string fname = @"log\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".dmp";
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
            AppDomain.CurrentDomain.UnhandledException += OnCurrentDomainUnhandledException;
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler((obj, e) => MiniDumper.TryDump(fname));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1404:CallGetLastErrorImmediatelyAfterPInvoke", Justification = "此处获取Error code，在非托管调用抛出异常情况时，可能对调试问题有用。")]
        private static void OnCurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e) {
            int ecode = System.Runtime.InteropServices.Marshal.GetExceptionCode();
            int lerror = System.Runtime.InteropServices.Marshal.GetLastWin32Error();

            // 记异常日志
            Console.WriteLine(string.Format("UnhandledException expCode is {0}, last error is {1}.", ecode, lerror));
            Console.WriteLine(e.ExceptionObject);
            MiniDumper.RecordDump();
            TryExportRuntimeEnvironment(MiniDumper.DumperDir);
        }
        #endregion

        #region 启动进程
        private static void StartProcess(string fileName, string args, bool admin, bool subProcess) {
            Process p = new Process();
            p.StartInfo.FileName = fileName;
            p.StartInfo.Arguments = args;
            if (admin) {
                p.StartInfo.Verb = "runas"; // 管理员权限运行
            }
            if (subProcess) {
                p.StartInfo.UseShellExecute = false; // 父进程启动子进程，设置false
            }
            p.Start();
        }
        public static void StartProcess(string fileName, string args = "", bool admin = false) {
            StartProcess(fileName, args, admin, false);
        }
        public static void StartSubProcess(string fileName, string args = "", bool admin = false) {
            StartProcess(fileName, args, admin, true);
        }
        public static void StartProcess(bool host) {
            StartProcess(ExecutablePath, host.ToString());
        }
        public static void StartSubProcess(string args) {
            StartSubProcess(ExecutablePath, args);
        }
        #endregion

        #region 路径
        /// <summary>
        /// EXE全路径
        /// </summary>
        public static string ExecutablePath {
            get {
                return Process.GetCurrentProcess().MainModule.FileName;
            }
        }

        /// <summary>
        /// ExE全名称
        /// </summary>
        public static string ExecutableFullName {
            get {
                return Process.GetCurrentProcess().MainModule.ModuleName;
            }
        }

        /// <summary>
        /// ExE名称
        /// </summary>
        public static string ExecutableName {
            get {
                return System.IO.Path.GetFileNameWithoutExtension(ExecutableFullName);
            }
        }

        /// <summary>
        /// EXE所在目录
        /// </summary>
        public static string StartupPath {
            get {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        /// <summary>
        /// 当前工作目录
        /// </summary>
        public static string CurrentDirectory {
            get {
                return Environment.CurrentDirectory + @"\";
            }
        }
        #endregion

        #region 线程
        /// <summary>
        /// 获取当前托管线程的唯一标识符
        /// </summary>
        public static int ManagedThreadId {
            get {
                return System.Threading.Thread.CurrentThread.ManagedThreadId;
            }
        }

        /// <summary>
        /// 获取当前线程的名称
        /// </summary>
        public static string ThreadName {
            get {
                return System.Threading.Thread.CurrentThread.Name;
            }
        }
        #endregion

        #region .NET运行时环境
        /// <summary>
        /// 导出.NET运行时环境
        /// </summary>
        public static bool TryExportRuntimeEnvironment(string savePath) {
            string runtimeDir = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory();
            string sosFileName = "sos.dll";
            string mscordacwksFileName = "mscordacwks.dll";
            string sosPath = string.Empty;
            string mscordacwksPath = string.Empty;
            string sosDstPath = string.Empty;
            string mscordacwksDstPath = string.Empty;

            sosPath = Path.Combine(runtimeDir, sosFileName);
            mscordacwksPath = Path.Combine(runtimeDir, mscordacwksFileName);
            sosDstPath = Path.Combine(savePath, sosFileName);
            mscordacwksDstPath = Path.Combine(savePath, mscordacwksFileName);

            try {
                File.Copy(sosPath, sosDstPath, true);
                File.Copy(mscordacwksPath, mscordacwksDstPath, true);
                return true;
            }
            catch (IOException e) {
                Trace.Fail(e.ToString());
            }
            catch (UnauthorizedAccessException e) {
                Trace.Fail(e.ToString());
            }
            catch (Exception e) {
                Trace.Fail(e.ToString());
            }
            return false;
        }
        #endregion

        #region 服务
        /// <summary>
        /// 获取windows服务安装路径
        /// </summary>
        public static string GetServicePath(string serviceName) {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(
                @"SYSTEM\CurrentControlSet\services\" + serviceName)) {
                if (key != null) {
                    object objPath = key.GetValue("ImagePath");
                    return objPath.ToString();
                }
            }
            return null;
        }
        #endregion
    }
}
