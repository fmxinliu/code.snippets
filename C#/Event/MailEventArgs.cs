using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventTest {
    class MailEventArgs : EventArgs {
        private String from, to, info;

        public MailEventArgs(String from, String to, String info) {
            this.from = from;
            this.to = to;
            this.info = info;
        }

        public String From { get { return this.from; } }
        public String To { get { return this.to; } }
        public String Info { get { return this.info; } }

        public override String ToString() {
            return String.Format("from:{0}, to:{1}, info:{2}", from, to, info);
        }
    }
}
