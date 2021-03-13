using System;
using System.Text;

namespace UtilsTool {
    public class StringUtils {
        #region 字符串解析为数字
        public static short ParseShort(string s, short defaultValue = 0) {
            short v;
            if (!short.TryParse(s, out v)) {
                v = defaultValue;
            }
            return v;
        }

        public static ushort ParseUShort(string s, ushort defaultValue = 0) {
            ushort v;
            if (!ushort.TryParse(s, out v)) {
                v = defaultValue;
            }
            return v;
        }

        public static int ParseInt(string s, int defaultValue = 0) {
            int v;
            if (!int.TryParse(s, out v)) {
                v = defaultValue;
            }
            return v;
        }

        public static uint ParseUInt(string s, uint defaultValue = 0) {
            uint v;
            if (!uint.TryParse(s, out v)) {
                v = defaultValue;
            }
            return v;
        }

        public static long ParseLong(string s, long defaultValue = 0) {
            long v;
            if (!long.TryParse(s, out v)) {
                v = defaultValue;
            }
            return v;
        }

        public static ulong ParseULong(string s, ulong defaultValue = 0) {
            ulong v;
            if (!ulong.TryParse(s, out v)) {
                v = defaultValue;
            }
            return v;
        }

        /// <summary>
        /// 一维基本数组转Json
        /// </summary>
        public static string ToJson<T>(T[] arr) {
            return string.Format("[{0}]", string.Join(",", arr));
        }

        /// <summary>
        /// 解析一维基本数组
        /// </summary>
        public static T[] ParseArray<T>(string json, Func<string, T, T> f) {
            string s = json.Trim();
            if (s.Length > 2) {
                s = s.Remove(s.Length - 1, 1).Remove(0, 1);
            }
            var chs = s.Split(',');
            var arr = new T[chs.Length];
            for (int i = 0; i < chs.Length; ++i) {
                arr[i] = f(chs[i], default(T));
            }
            return arr;
        }
        #endregion

        #region 字符串转换
        /// <summary>
        /// 字符串转十六进制字符串
        /// </summary>
        /// <param name="inputString">原始字符串</param>
        /// <param name="delimiter">字符间分割符</param>
        /// <param name="hexWidth">十六进制字符占位宽，不足时，填充前导0</param>
        /// <param name="showPrefix">是否显示十六进制前缀</param>
        /// <returns>转换后的字符串</returns>
        public static string ToHexString(string inputString, string delimiter = ", ", int hexWidth = 2, bool showPrefix = false) {
            StringBuilder sb = new StringBuilder();
            foreach (char letter in inputString) {
                int value = Convert.ToInt32(letter);
                string fmt = (showPrefix ? "0x{0}" : "{0}") + delimiter;
                if (hexWidth <= 0) {
                    sb.AppendFormat(fmt, Convert.ToString(value, 16));
                }
                else {
                    sb.AppendFormat(fmt, value.ToString("X" + hexWidth));
                    //sb.AppendFormat(fmt, value.ToString("X").PadLeft(fillZeroNum, '0'));
                }
            }
            string hexString = sb.ToString(0, (sb.Length > delimiter.Length) ? sb.Length - delimiter.Length : 0);
            return hexString;
        }
        #endregion
    }
}
