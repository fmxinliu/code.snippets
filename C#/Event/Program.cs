using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventTest {
    class Program {
        static MailManager mailMgr1 = new MailManager();
        static MailManager mailMgr2 = new MailManager();

        static void Main(string[] args) {
            TestEventRegister();
            Console.ReadKey();

            TestUnRegister();
            Console.ReadKey();
        }

        // 测试：事件被无意中重复注册
        static void TestEventRegister() {
            Program p = new Program();

            // 被重复注册，无法拦截！！！
            mailMgr1.Register((sender, e) => {
                Console.WriteLine(e);
            });
            mailMgr1.Register((sender, e) => {
                Console.WriteLine(e);
            });

            // OK，只注册一次
            mailMgr1.Register(p.MailNotifyHandler);
            mailMgr1.Register(p.MailNotifyHandler);

            mailMgr1.Simulate();

            // 退出方法后，对象 p 不再使用，但无法被垃圾回收（事件没有解除注册!!!）
        }

        void MailNotifyHandler(object sender, MailEventArgs e) {
            Console.WriteLine("-----");
            Console.WriteLine(e);
        }

        static void TestUnRegister() {
            Program p = new Program();

            mailMgr2.Register(p.MailNotifyHandler);
            mailMgr2.Simulate();
            mailMgr2.UnRegister(p.MailNotifyHandler);

            // 匿名函数注册的，无法解除注册。只能等注册的类实例对象释放后，清理注册列表
            //mailMgr2.Register((sender, e) => {
            //    Console.WriteLine(e);
            //});

            // 退出方法后，对象 p 可以被垃圾回收（所有事件都已解除注册）
        }
    }
}
