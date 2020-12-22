using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace StringTest {
    class StringEncoding {
        private static readonly String orgString = "你好";

        public static void Test() {
            // 对字符串进行UTF-16编码
            Byte[] encoderArray = EncodingString(orgString);

            // 模拟：字节流以数据块形式传输，先接收到 n1 个字符，再接收到 n2 个字符
            //Byte[] bytes1 = { encoderArray[0], encoderArray[1], encoderArray[2] };
            //Byte[] bytes2 = { encoderArray[3] };
            Byte[] bytes1, bytes2;
            SplitArray(encoderArray, out bytes1, out bytes2);

            // 使用无状态解码
            DecodingString1(bytes1, bytes2);

            // 使用有状态解码
            DecodingString2(bytes1, bytes2);
        }

        private static Byte[] EncodingString(String s) {
            byte[] encoderArray = Encoding.Unicode.GetBytes(s);
            String s1 = Encoding.Unicode.GetString(encoderArray);
            Debug.Assert(s.Equals(s1), "无状态编码解码失败!");
            Console.WriteLine("\n编码字符串为：{0}", s);
            return encoderArray;
        }

        private static void SplitArray(Byte[] encoderArray, out Byte[] bytes1, out Byte[] bytes2) {
            Int32 mid = encoderArray.Length / 2;
            Int32 mod = encoderArray.Length % 2;
            Int32 len1 = Math.Max(0, mid + 1);
            Int32 len2 = Math.Max(0, mid - 1 + mod);

            bytes1 = new Byte[len1];
            bytes2 = new Byte[len2];
            Array.Copy(encoderArray, 0, bytes1, 0, len1);
            Array.Copy(encoderArray, len1, bytes2, 0, len2);
        }

        /// <summary>
        /// 无状态解码
        /// </summary>
        private static void DecodingString1(Byte[] bytes1, Byte[] bytes2) {
            String s21 = Encoding.Unicode.GetString(bytes1);
            String s22 = Encoding.Unicode.GetString(bytes2);
            Console.WriteLine("无状态解码结果：{0}{1}", s21, s22);
        }

        /// <summary>
        /// 有状态解码
        /// </summary>
        private static void DecodingString2(Byte[] bytes1, Byte[] bytes2) {
            Int32 maxLen =
                Encoding.Unicode.GetMaxCharCount(bytes1.Length) +
                Encoding.Unicode.GetMaxCharCount(bytes2.Length);

            Char[] chars = new Char[maxLen];
            Decoder decoder = Encoding.Unicode.GetDecoder();
            Int32 count1 = decoder.GetChars(bytes1, 0, bytes1.Length, chars, 0, false);
            Int32 count2 = decoder.GetChars(bytes2, 0, bytes2.Length, chars, count1, false);

            Int32 count = count1 + count2;
            String s3 = new String(chars, 0, count);
            Console.WriteLine("有状态解码结果：{0}", s3);
        }
    }
}
