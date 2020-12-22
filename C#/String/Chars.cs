using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace StringTest {
    class Chars {
        public static void Test() {
            // 1.System.Char表示范围：U+0000 ~ U+FFFF

            // 2.转义字符
            Char omegaSymbol = '\u03A9'; // Ω
            Char copyrightSymbol = '\u00A9'; // ©
            Console.WriteLine(omegaSymbol);
            Console.WriteLine(copyrightSymbol);

            // 4位表示，可省略前导0
            Char omegaSymbol2 = '\x3A9'; // Ω
            Char copyrightSymbol2 = '\xA9'; // ©
            Console.WriteLine(omegaSymbol2);
            Console.WriteLine(copyrightSymbol2);

            // 3.语言文化
            Char c1 = 'i';
            //Char c2 = Char.ToUpper(c1); // 当前线程语言环境
            Char c2 = Char.ToUpper(c1, CultureInfo.CurrentCulture);
            Char c3 = Char.ToUpper(c1, new CultureInfo("tr-TR")); // 土耳其语言环境
            Char c4 = Char.ToUpper(c1, CultureInfo.InvariantCulture); // 忽略语言文化
            Char c5 = Char.ToUpperInvariant(c1); // 忽略语言文化

            // 4.字符扩展：一个字符代表了2个实际的字符
            //Char c6 = 'ß'; // 德语 ß -> ss

            // 5.字符代理(Surrogate)：使用2个字符来表示一个实际的字符
            // High Surrogate（高代理项，16位）
            // Low  Surrogate（低代理项，16位）
        }
    }
}
