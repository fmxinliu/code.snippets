using System;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace UtilsTool {
    public static class IOHelper {
        #region 以Everyone完全控制方式操作文件
        public static bool CreateFile(string path) {
            try {
                SecurityIdentifier sid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                FileSystemAccessRule newRule = new FileSystemAccessRule(sid, FileSystemRights.FullControl, AccessControlType.Allow);
                FileSecurity fss = null;

                if (File.Exists(path)) {
                    fss = File.GetAccessControl(path);
                    fss.AddAccessRule(newRule);
                    File.SetAccessControl(path, fss);
                }
                else {
                    fss = new FileSecurity();
                    fss.AddAccessRule(newRule);
                    using (File.Create(path, 1024, FileOptions.None, fss)) { }
                }

                return true;
            }
            catch (IOException e) {
                Trace.Fail(e.ToString());
            }
            catch (UnauthorizedAccessException e) {
                Trace.Fail(e.ToString());
            }
            catch (System.Runtime.InteropServices.SEHException e) {
                Trace.Fail(e.ToString());
            }

            return false;
        }

        public static bool DeleteFile(string path) {
            try {
                SecurityIdentifier sid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                FileSystemAccessRule newRule = new FileSystemAccessRule(sid, FileSystemRights.FullControl, AccessControlType.Allow);
                FileSecurity fss = null;

                if (File.Exists(path)) {
                    fss = File.GetAccessControl(path);
                    fss.AddAccessRule(newRule);
                    File.SetAccessControl(path, fss);
                    File.Delete(path);
                }

                return true;
            }
            catch (IOException e) {
                Trace.Fail(e.ToString());
            }
            catch (UnauthorizedAccessException e) {
                Trace.Fail(e.ToString());
            }
            catch (System.Runtime.InteropServices.SEHException e) {
                Trace.Fail(e.ToString());
            }

            return false;
        }

        public static bool SetFileFullControl(string path) {
            try {
                SecurityIdentifier sid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                FileSystemAccessRule newRule = new FileSystemAccessRule(sid, FileSystemRights.FullControl, AccessControlType.Allow);
                FileSecurity fss = null;

                if (File.Exists(path)) {
                    fss = File.GetAccessControl(path);
                    fss.AddAccessRule(newRule);
                    File.SetAccessControl(path, fss);
                    return true;
                }
                else {
                    Trace.Fail(string.Format("文件不存在：" + path));
                }
            }
            catch (IOException e) {
                Trace.Fail(e.ToString());
            }
            catch (UnauthorizedAccessException e) {
                Trace.Fail(e.ToString());
            }
            catch (System.Runtime.InteropServices.SEHException e) {
                Trace.Fail(e.ToString());
            }

            return false;
        }
        #endregion

        #region 以Everyone完全控制方式操作目录
        public static bool CreateDirecory(string path) {
            try {
                DirectoryInfo dir = new DirectoryInfo(path);
                SecurityIdentifier sid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                InheritanceFlags inherits = InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit;
                FileSystemAccessRule newRule = new FileSystemAccessRule(sid, FileSystemRights.FullControl, inherits, PropagationFlags.None, AccessControlType.Allow);

                if (dir.Exists) {
                    DirectorySecurity ds = dir.GetAccessControl();
                    ds.AddAccessRule(newRule);
                    dir.SetAccessControl(ds);
                }
                else {
                    DirectorySecurity ds = new DirectorySecurity();
                    ds.AddAccessRule(newRule);
                    dir.Create(ds);
                }

                return true;
            }
            catch (IOException e) {
                Trace.Fail(e.ToString());
            }
            catch (UnauthorizedAccessException e) {
                Trace.Fail(e.ToString());
            }
            catch (System.Runtime.InteropServices.SEHException e) {
                Trace.Fail(e.ToString());
            }

            return false;
        }

        public static bool DeleteDirecory(string path, bool recursive = false) {
            try {
                DirectoryInfo dir = new DirectoryInfo(path);
                SecurityIdentifier sid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                InheritanceFlags inherits = InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit;
                FileSystemAccessRule newRule = new FileSystemAccessRule(sid, FileSystemRights.FullControl, inherits, PropagationFlags.None, AccessControlType.Allow);

                if (dir.Exists) {
                    DirectorySecurity ds = dir.GetAccessControl();
                    ds.AddAccessRule(newRule);
                    dir.SetAccessControl(ds);
                    dir.Delete(recursive);
                }

                return true;
            }
            catch (IOException e) {
                Trace.Fail(e.ToString());
            }
            catch (UnauthorizedAccessException e) {
                Trace.Fail(e.ToString());
            }
            catch (System.Runtime.InteropServices.SEHException e) {
                Trace.Fail(e.ToString());
            }

            return false;
        }

        public static bool SetDirectoryFullControl(string path) {
            try {
                DirectoryInfo dir = new DirectoryInfo(path);
                SecurityIdentifier sid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                InheritanceFlags inherits = InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit;
                FileSystemAccessRule newRule = new FileSystemAccessRule(sid, FileSystemRights.FullControl, inherits, PropagationFlags.None, AccessControlType.Allow);

                if (dir.Exists) {
                    DirectorySecurity ds = dir.GetAccessControl();
                    ds.AddAccessRule(newRule);
                    dir.SetAccessControl(ds);
                    return true;
                }
                else {
                    Trace.Fail(string.Format("目录不存在：" + path));
                }
            }
            catch (IOException e) {
                Trace.Fail(e.ToString());
            }
            catch (UnauthorizedAccessException e) {
                Trace.Fail(e.ToString());
            }
            catch (System.Runtime.InteropServices.SEHException e) {
                Trace.Fail(e.ToString());
            }

            return false;
        }
        #endregion

        #region 检查Everyone完全控制权限
        /// <summary>
        /// 检查给定的文件或目录，是否包含Everyone的完全控制权限
        /// </summary>
        /// <param name="fss">文件系统权限</param>
        /// <returns>包含返回true，否则返回false</returns>
        public static bool IsHaveEveryoneFullControlRule(string path) {
            if (File.Exists(path)) {
                return IsHaveEveryoneFullControlRule(File.GetAccessControl(path));
            }
            DirectoryInfo dir = new DirectoryInfo(path);
            if (dir.Exists) {
                return IsHaveEveryoneFullControlRule(dir.GetAccessControl());
            }
            return false;
        }

        private static bool IsHaveEveryoneFullControlRule(FileSystemSecurity fss) {
            SecurityIdentifier everyoneSid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            AuthorizationRuleCollection cl = fss.GetAccessRules(true, true, everyoneSid.GetType());
            foreach (AuthorizationRule item in cl) {
                FileSystemAccessRule filers = item as FileSystemAccessRule;
                if (null != filers && filers.IdentityReference == everyoneSid && filers.FileSystemRights == FileSystemRights.FullControl) {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
