using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventTest {
    class Program {
        static void Main(string[] args) {
            TestEventRegister();

            Console.ReadKey();
        }

        // 测试：事件被无意中重复注册
        static void TestEventRegister() {
            MailManager mailMgr = new MailManager();

            // 被重复注册，无法拦截！！！
            mailMgr.Register((sender, e) => {
                Console.WriteLine(e);
            });
            mailMgr.Register((sender, e) => {
                Console.WriteLine(e);
            });

            // OK，只注册一次
            mailMgr.Register(MailNotifyHandler);
            mailMgr.Register(MailNotifyHandler);

            mailMgr.Simulate();
        }

        static void MailNotifyHandler(object sender, MailEventArgs e) {
            Console.WriteLine("-----");
            Console.WriteLine(e);
        }
    }
}
