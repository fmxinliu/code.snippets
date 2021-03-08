using System;
using System.Text;
using System.IO;

namespace PathTest {
    public static class PathExtensionMethods {
        #region 获取两个绝对路径之间的相对路径
        /// <summary>
        /// 获取两个绝对路径之间的相对路径。如果包含“文件路径”，请调用：
        /// static String GetRelativePath(this String fromAbsolutepath, String toAbsolutepath,
        ///     Boolean frompathIsFilePath, Boolean topathIsFilePath, Char pathSeparator = '\\')
        /// </summary>
        /// <param name="fromAbsolutepath">绝对路径p1</param>
        /// <param name="toAbsolutepath">绝对路径p2</param>
        /// <param name="pathSeparator">路径分隔符</param>
        /// <returns>p2相对于p1的相对路径</returns>
        public static String GetRelativePath(this String fromAbsolutepath, String toAbsolutepath, Char pathSeparator = '\\') {
            return GetRelativePath(fromAbsolutepath, toAbsolutepath, false, false, pathSeparator);
        }

        /// <summary>
        /// 获取两个绝对路径之间的相对路径。如果包含“文件路径”，请使用该方法。
        /// </summary>
        /// <param name="fromAbsolutepath">绝对路径p1</param>
        /// <param name="toAbsolutepath">绝对路径p2</param>
        /// <param name="frompathIsFilePath">p1是否为文件路径</param>
        /// <param name="topathIsFilePath">p2是否为文件路径</param>
        /// <param name="pathSeparator">路径分隔符</param>
        /// <returns>p2相对于p1的相对路径</returns>
        public static String GetRelativePath(this String fromAbsolutepath, String toAbsolutepath,
            Boolean frompathIsFilePath, Boolean topathIsFilePath, Char pathSeparator = '\\') {
            // 1.检查入参
            String path1 = CheckPath(fromAbsolutepath, "fromAbsolutepath", frompathIsFilePath);
            String path2 = CheckPath(toAbsolutepath, "toAbsolutepath", topathIsFilePath);
            // 2.转换
            try
            {
                Uri u1 = new Uri(path1, UriKind.Absolute);
                Uri u2 = new Uri(path2, UriKind.Absolute);
                Uri u3 = u1.MakeRelativeUri(u2);  // u2相对于u1的uri
                String path = UrlDecode(u3.OriginalString); // 中文以明文显示
                return (pathSeparator != '/') ? path.Replace('/', pathSeparator) : path;
            }
            catch (UriFormatException) {
                throw new ArgumentException("路径转换失败，请检查入参。",
                    String.Format("\nfromAbsolutepath={0}, isFilePath={2}\ntoAbsolutepath={1}, isFilePath={3}",
                        fromAbsolutepath, toAbsolutepath, frompathIsFilePath, topathIsFilePath));
            }
        }
        #endregion

        #region 根据相对路径，获取其相对于基准(绝对)路径的绝对路径
        /// <summary>
        /// 根据相对路径，获取其相对于基准(绝对)路径的绝对路径。如果包含“文件路径”，请调用：
        /// static String GetAbsolutePath(this String absolutepath, String relativepath,
        ///     Boolean absolutepathIsFilePath, Boolean relativepathIsFilePath, Char pathSeparator = '\\')
        /// </summary>
        /// <param name="absolutepath">绝对路径p1</param>
        /// <param name="relativepath">相对路径p2</param>
        /// <param name="pathSeparator">路径分隔符</param>
        /// <returns>p2相对于p1的绝对路径</returns>
        public static String GetAbsolutePath(this String absolutepath, String relativepath, Char pathSeparator = '\\') {
            return GetAbsolutePath(absolutepath, relativepath, false, false, pathSeparator);
        }
        /// <summary>
        /// 根据相对路径，获取其相对于基准(绝对)路径的绝对路径。如果包含“文件路径”，请使用该方法。
        /// </summary>
        /// <param name="absolutepath">绝对路径p1</param>
        /// <param name="relativepath">相对路径p2</param>
        /// <param name="absolutepathIsFilePath">p1是否为文件路径</param>
        /// <param name="relativepathIsFilePath">p2是否为文件路径</param>
        /// <param name="pathSeparator">路径分隔符</param>
        /// <returns>p2相对于p1的绝对路径</returns>
        public static String GetAbsolutePath(this String absolutepath, String relativepath,
            Boolean absolutepathIsFilePath, Boolean relativepathIsFilePath, Char pathSeparator = '\\') {
            // 1.检查入参
            String path1 = CheckPath(absolutepath, "absolutepath", absolutepathIsFilePath);
            String path2 = CheckPath(relativepath, "relativepath", relativepathIsFilePath);

            // 2.转换
            try {
                Uri baseUri = new Uri(path1, UriKind.Absolute);
                Uri relativeUri = new Uri(path2, UriKind.Relative);
                Uri uri = new Uri(baseUri, relativeUri);
                String path = uri.LocalPath;
                return (pathSeparator != '\\') ? path.Replace('/', pathSeparator) : path;
            }
            catch (UriFormatException) {
                throw new ArgumentException("路径转换失败，请检查入参。",
                    String.Format("\nabsolutepath={0}, isFilePath={2}\nrelativepath={1}, isFilePath={3}",
                        absolutepath, relativepath, absolutepathIsFilePath, relativepathIsFilePath));
            }
        }
        #endregion

        #region 获取标准化后的绝对路径
        /// <summary>
        /// 获取标准化后的绝对路径。
        /// </summary>
        /// <param name="path">要为其获取绝对路径信息的文件或目录。</param>
        /// <returns>例如：输入“C:\A\B\..\MyFile.txt”，返回“C:\A\MyFile.txt”。</returns>
        public static String GetAbsolutePath(this String path) {
            return Path.GetFullPath(path);
        }
        #endregion

        private static String CheckPath(String path, String pathName, Boolean isFilePath) {
            // 1.检查路径分隔符
            if (path.IndexOf(@"//") != -1) {
                throw new ArgumentException(@"格式不正确，路径包含//", pathName + " = " + path);
            }
            if (path.IndexOf(@"\\") != -1) {
                throw new ArgumentException(@"格式不正确，路径包含\\", pathName + " = " + path);
            }

            // 2.检查是否为“文件路径”
            Boolean b1 = path.EndsWith(@"/");
            Boolean b2 = path.EndsWith(@"\");
            if (isFilePath && b1) {
                throw new ArgumentException(@"格式不正确，文件名以/结尾", pathName + " = " + path);
            }
            if (isFilePath && b2) {
                throw new ArgumentException(@"格式不正确，文件名以\结尾", pathName + " = " + path);
            }

            // 3.对于非“文件路径”，末尾添加分隔符
            return (isFilePath || b1 || b2) ? path : path + @"\";
        }

        #region URL中文编码与解码
        private static String UrlEncode(String url) {
            Byte[] bs = System.Text.Encoding.UTF8.GetBytes(url);
            //Byte[] bs = Encoding.GetEncoding("GB2312").GetBytes(url);
            StringBuilder sb = new StringBuilder();
            for (Int32 i = 0; i < bs.Length; i++) {
                if (bs[i] < 128)
                    sb.Append((Char)bs[i]);
                else {
                    sb.Append("%" + bs[i++].ToString("X").PadLeft(2, '0'));
                    sb.Append("%" + bs[i].ToString("X").PadLeft(2, '0'));
                }
            }
            return sb.ToString();
        }

        private static String UrlDecode(String url, String encodingName = "utf-8") {
            String[] ps = url.Split('/');
            for (Int32 k = 0; k < ps.Length; k++) {
                if (ps[k].StartsWith("%")) {
                    String[] bStrs = ps[k].Split('%');
                    Byte[] bs = new Byte[bStrs.Length - 1];
                    for (Int32 i = 1; i < bStrs.Length; i++) {
                        bs[i - 1] = Convert.ToByte(bStrs[i], 16);
                    }
                    ps[k] = Encoding.GetEncoding(encodingName).GetString(bs);
                }
            }
            return String.Join("/", ps);
        }
        #endregion
    }
}
