using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventTest {
    class MailManager {
        private event EventHandler<MailEventArgs> MailEvent;

        // 预防：同一事件，无意中被注册多次，从而触发多次响应
        public void Register(EventHandler<MailEventArgs> handler) {
            this.UnRegister(handler);  // 先解挂
            this.MailEvent += handler; // 再挂载
        }

        public void UnRegister(EventHandler<MailEventArgs> handler) {
            this.MailEvent -= handler;
        }

        protected virtual void OnMailChanged(MailEventArgs e) {
            EventHandler<MailEventArgs> temp = this.MailEvent;
            if (temp != null) {
                temp(this, e);
            }
        }

        private void Raise(String from, String to, String info) {
            if (this.MailEvent != null) {
                this.OnMailChanged(new MailEventArgs(from, to, info));
            }
        }

        public void Simulate() {
            this.Raise("user@qq.com", "www.baidu.com", "访问百度");
        }
    }
}
