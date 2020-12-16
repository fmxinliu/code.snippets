using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace EventTest {
    /// <summary>
    /// 使用对象的哈希码标识事件
    /// </summary>
    public sealed class EventKey { }

    /// <summary>
    /// 管理多个事件
    /// </summary>
    public sealed class EventSet {
        // 映射：事件 -> 回调函数
        private readonly Dictionary<EventKey, Delegate> events = new Dictionary<EventKey, Delegate>();

        public void Add(EventKey key, Delegate handler) {
            Monitor.Enter(events);
            Delegate d;
            events.TryGetValue(key, out d);
            events[key] = Delegate.Combine(d, handler);
            Monitor.Exit(events);
        }

        public void Remove(EventKey key, Delegate handler) {
            Monitor.Enter(events);
            Delegate d;
            if (events.TryGetValue(key, out d)) {
                d = Delegate.Remove(d, handler);
                if (d != null) {
                    events[key] = d;
                }
                else {
                    events.Remove(key);
                }
            }
            Monitor.Exit(events);
        }

        public void Raise(EventKey key, Object sender, EventArgs e) {
            Delegate d;
            Monitor.Enter(events);
            events.TryGetValue(key, out d);
            Monitor.Exit(events);

            // 动态调用
            if (d != null) {
                d.DynamicInvoke(new Object[] { sender, e });
            }
        }
    }
}
