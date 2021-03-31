using System;

namespace UtilsTool {
    /// <summary>
    /// 延时中断标志
    /// </summary>
    public class InterruptState {
        private Boolean stopFlag = false;
        private readonly Object obj = new Object();

        public void Reset() {
            lock (obj) {
                this.stopFlag = false;
            }
        }

        public void Set() {
            lock (obj) {
                this.stopFlag = true;
            }
        }

        public Boolean IsSet() {
            lock (obj) {
                return this.stopFlag;
            }
        }
    }
}
