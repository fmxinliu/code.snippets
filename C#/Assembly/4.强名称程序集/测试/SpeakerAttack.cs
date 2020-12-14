using System.IO;

namespace DLLInject {
    public class SpeakerAttack {
        // 可以用UltraEdit打开程序集，检索字符串地址。修改其他地址，可能造成DLL损坏无法加载。
        private static readonly int attackAddress = 2158; // 攻击地址
        public static void Main() {
            using (Stream steam = File.Open("AssemblyLib.dll", FileMode.Open)) {
                byte[] buffer = new byte[steam.Length];
                steam.Read(buffer, 0, buffer.Length);
                buffer[attackAddress] = (byte)(~buffer[attackAddress]); // 篡改元素
                steam.Seek(0, SeekOrigin.Begin);
                steam.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
