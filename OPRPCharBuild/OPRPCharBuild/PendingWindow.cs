using System;
using System.Timers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OPRPCharBuild
{
    public partial class PendingWindow : Form {
        private const int MAX_DOTS = 4;
        private const string DOT_SEPARATE = ".     ";
        private static int dots = 0;
        private static System.Timers.Timer mainTimer;
        private bool timerLoop = true;

        public PendingWindow() {
            InitializeComponent();
            label_Dots.Text = "";
        }

        // This should be the Thread
        public void start_Timer(object sender, DoWorkEventArgs e) {
            this.TopMost = true;
            this.Show();
            mainTimer = new System.Timers.Timer();
            mainTimer.Interval = 1000;
            mainTimer.Elapsed += onTimedEvent;
            mainTimer.AutoReset = true;
            mainTimer.Enabled = true;
            while (timerLoop) { } // Infinite loop until timerLoop is false
        }

        public void finished_Timer(object sender, RunWorkerCompletedEventArgs e) {
            mainTimer.Enabled = false;
            this.Close();
        }

        // This should stop the Timer
        public void StopTimer() {
            timerLoop = false;
        }

        // Adds on the dots
        private void onTimedEvent(Object source, ElapsedEventArgs e) {
            if (dots == MAX_DOTS) {
                label_Dots.Text = "";
                dots = 0;
            }
            label_Dots.Text += DOT_SEPARATE;
            dots += 1;
            label_Dots.Text.TrimEnd(' ');
        }
    }
}