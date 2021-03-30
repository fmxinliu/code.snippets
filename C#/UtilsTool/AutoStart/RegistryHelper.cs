using System;
using Microsoft.Win32;

namespace UtilsTool {
    public class RegistryHelper {
        public static object ReadValue(RegistryKey rootName, string keyName, string name) {
            using (RegistryKey regkey = rootName.OpenSubKey(keyName)) {
                if (regkey != null) {
                    return regkey.GetValue(name);
                }
            }
            return null;
        }

        public static void DeleteValue(RegistryKey rootName, string keyName, string name) {
            using (RegistryKey regkey = rootName.OpenSubKey(keyName, true)) {
                if (regkey != null) {
                    regkey.DeleteValue(name, false); // 删除注册表项，必须以写方式打开
                }
            }
        }

        public static void WriteValue(RegistryKey rootName, string keyName, string name, object value) {
            RegistryKey regkey = null;
            try {
                regkey = rootName.OpenSubKey(keyName, true);
                if (regkey == null) {
                    regkey = Registry.LocalMachine.CreateSubKey(keyName);
                }
                regkey.SetValue(name, value);
            }
            finally {
                if (regkey != null) {
                    regkey.Close();
                }
            }
        }

        public static void WriteValueWhenUnmatch(RegistryKey rootName, string keyName, string name, object value, bool ignoreCase) {
            RegistryKey regkey = null;
            try {
                regkey = rootName.OpenSubKey(keyName, true);
                if (regkey == null) {
                    regkey = Registry.LocalMachine.CreateSubKey(keyName);
                }
                object value1 = regkey.GetValue(name);
                if (value1 == null || string.Compare(value1.ToString(), value.ToString(), ignoreCase) != 0) {
                    regkey.SetValue(name, value);
                }
            }
            finally {
                if (regkey != null) {
                    regkey.Close();
                }
            }
        }
    }
}
