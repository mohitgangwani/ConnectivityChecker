using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Drawing.Text;
using System.Threading;


namespace ConnectivityChecker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool IsConnectivity = false;

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            bool except = true;
            if (backgroundWorker1.CancellationPending)
            {
                e.Cancel = true;
            }
            while (true)
            {
                Thread.Sleep(100);
                try
                {
                    //timer1.Enabled = true;
                    Ping myPing = new Ping();
                    String host = "google.com";
                    byte[] buffer = new byte[32];
                    int timeout = 1000;
                    PingOptions pingOptions = new PingOptions();
                    PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                    IsConnectivity = (reply.Status == IPStatus.Success);

                }
                catch (Exception)
                {
                    except = false;
                }
                if (IsConnectivity == true)
                {
                    label4.Invoke((MethodInvoker)(() => label4.Text = "Connection Restored at: " + DateTime.Now.ToString("hh:mm:ss tt")));
                    MessageBox.Show("Connection restored");
                    break;
                }
            }

        }


        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // label1.Text = hours + ":" + minutes + ":" + seconds.ToString();
            // label1.Text = hours + ":" + minutes + ":" + seconds.ToString();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                label2.Text = "Cancelled";
            }
            else if (e.Error != null)
            {
                label2.Text = e.Error.Message;
            }
            else
            {
                //label1.Text = e.Result.ToString();
                // label1.Text = hours + ":" + minutes + ":" + seconds.ToString();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label3.Text = "Started at: " + DateTime.Now.ToString("hh:mm:ss tt");
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
            //if(IsConnectivity == true)
            // {
            //      label4.Text = DateTime.Now.ToString();
            // }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }

        }
    }
}
    