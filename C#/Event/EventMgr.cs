using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventTest {
    [Serializable]
    public class CustomEventArgsBase : EventArgs {
        //public string Msg { get; set; } // 序列化时不要这样定义
        private string msg = string.Empty;
        public string Msg {
            get { return this.msg; }
            set { this.msg = value; }
        }
    }

    public class CustomEventArgs1 : CustomEventArgsBase { }
    public class CustomEventArgs2 : CustomEventArgsBase { }

    public class EventMgr {
        private readonly EventSet eventSet = new EventSet();

        public EventMgr() { }

        public virtual void Raise(EventKey key, EventArgs e) {
            this.eventSet.Raise(key, this, e);
        }

        public void Simulate() {
            this.Raise(key1, new CustomEventArgs1 { Msg = "触发事件1" });
            this.Raise(key2, new CustomEventArgs2 { Msg = "触发事件2" });
        }

        #region 事件1
        private readonly EventKey key1 = new EventKey();

        public event EventHandler<CustomEventArgs1> CustomEvent1 {
            add { this.eventSet.Add(key1, value); }
            remove { this.eventSet.Remove(key1, value); }
        }
        #endregion

        #region 事件2
        private readonly EventKey key2 = new EventKey();

        public event EventHandler<CustomEventArgs2> CustomEvent2 {
            add { this.eventSet.Add(key2, value); }
            remove { this.eventSet.Remove(key2, value); }
        }
        #endregion
    }
}
