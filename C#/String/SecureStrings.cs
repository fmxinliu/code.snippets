using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Runtime.InteropServices;

namespace StringTest {
    class SecureStrings {
        public static void Test() {
            TestSecureString();
        }

        /// <summary>
        /// 安全字符串
        /// </summary>
        private static void TestSecureString() {
            using (SecureString ss = new SecureString()) {
                Console.WriteLine("构建安全密码:ILj");
                ss.AppendChar('I');
                ss.AppendChar('L');
                ss.AppendChar('j');

                Console.Write("获取安全密码：");
                DisplaySecureString(ss);

                // 销毁SecureString，清空进程空间中构建的密码
                //ss.Dispose();
            }
        }

        /// <summary>
        /// 获取明文密码
        /// </summary>
        private unsafe static void DisplaySecureString(SecureString ss) {
            Char* c = null;
            try {
                // 密码解密到非托管内存
                c = (Char*)Marshal.SecureStringToCoTaskMemUnicode(ss);

                // 已解密，使用时间应尽可能的短，防止被窃取
                for (Int32 i = 0; c[i] != '\0'; ++i) {
                    Console.Write(c[i]);
                }
            }
            finally {
                if (c != null) {
                    // 清除非托管内存中的明文密码
                    Marshal.ZeroFreeCoTaskMemUnicode((IntPtr)c);
                }
            }

            // SecureString未重写ToString()
            // String以明文的形式存储于托管堆上，
            // 即使被回收，堆空间没有被重用，也有密码残留，容易被窃取
            String s = ss.ToString();
        }
    }
}
