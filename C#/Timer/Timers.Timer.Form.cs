using System;
using System.Windows.Forms;

namespace TimerTest {
    public partial class TimersTimerForm : Form {
        private TextBox tbxTime1;
        private TextBox tbxTime2;
        private TextBox tbxTime3;
        private System.Timers.Timer timer1;
        private System.Timers.Timer timer2;

        public TimersTimerForm() {
            InitializeComponent();
            this.InitTimer();
        }

        private void TimersTimerForm_FormClosing(object sender, FormClosingEventArgs e) {
            timer1.Dispose();
            timer2.Dispose();
        }

        private void InitTimer() {
            timer1 = new System.Timers.Timer();
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed_1);
            timer1.Interval = 1000;
            timer1.Start();

            timer2 = new System.Timers.Timer();
            timer2.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed_2);
            timer2.SynchronizingObject = this; /// 与窗体关联
            timer2.Interval = 1000;
            timer2.Start();
        }

        private void Timer_Elapsed_1(object sender, System.Timers.ElapsedEventArgs e) {
            /// 可能抛出 ObjectDisposedException，退出窗体前，需关闭定时器
            if (this.InvokeRequired && !this.Disposing && !this.IsDisposed) {
                this.Invoke(new Action<object, System.Timers.ElapsedEventArgs>(Timer_Elapsed_1), sender, e);
            }
            else {
                if (!this.Disposing && !this.IsDisposed) {
                    this.tbxTime1.Text = DateTime.Now.ToString();
                }
            }
        }

        private void Timer_Elapsed_2(object sender, System.Timers.ElapsedEventArgs e) {
            this.tbxTime2.Text = DateTime.Now.ToString();
        }

        private void InitializeComponent() {
            this.tbxTime1 = new System.Windows.Forms.TextBox();
            this.tbxTime2 = new System.Windows.Forms.TextBox();
            this.tbxTime3 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbxTime1
            // 
            this.tbxTime1.Location = new System.Drawing.Point(84, 39);
            this.tbxTime1.Name = "tbxTime1";
            this.tbxTime1.Size = new System.Drawing.Size(166, 25);
            this.tbxTime1.TabIndex = 1;
            // 
            // tbxTime2
            // 
            this.tbxTime2.Location = new System.Drawing.Point(84, 89);
            this.tbxTime2.Name = "tbxTime2";
            this.tbxTime2.Size = new System.Drawing.Size(166, 25);
            this.tbxTime2.TabIndex = 2;
            // 
            // tbxTime3
            // 
            this.tbxTime3.Location = new System.Drawing.Point(84, 139);
            this.tbxTime3.Name = "tbxTime3";
            this.tbxTime3.Size = new System.Drawing.Size(166, 25);
            this.tbxTime3.TabIndex = 3;
            // 
            // TimersTimerForm
            // 
            this.ClientSize = new System.Drawing.Size(319, 200);
            this.Controls.Add(this.tbxTime3);
            this.Controls.Add(this.tbxTime2);
            this.Controls.Add(this.tbxTime1);
            this.Name = "TimersTimerForm";
            this.Text = "计时器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TimersTimerForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
