using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Helpers.OpenFolderBrowserDialog(txtSource);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Helpers.OpenFolderBrowserDialog(txtDestination);
        }

        

        BackgroundWorker myBackGroundWorker;
        private void button3_Click(object sender, EventArgs e)
        {
            myBackGroundWorker = new BackgroundWorker();
            myBackGroundWorker.DoWork += (obj, ea) => Helpers.DirectoryCopy(txtSource.Text, txtDestination.Text, true);
            myBackGroundWorker.RunWorkerAsync();

            timer1.Start();
            label1.Text = "Copying in progress. Please wait";

            Helpers.SetControlEnabled(false, btnSource, btnDestination, btnCopy);

            myBackGroundWorker.RunWorkerCompleted += (obj, ea) =>
            {
                MessageBox.Show("Copy process has been completed.", "Message", MessageBoxButtons.OK);
                timer1.Stop();
                progressBar1.Value = 100;
                label1.Text = "Copying status: FINISHED.";
                Helpers.SetControlEnabled(true, btnSource, btnDestination, btnCopy);
                Helpers.ClearTextBoxContent(txtSource, txtDestination);
            };
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;

            if (progressBar1.Value < progressBar1.Maximum)
            {
                progressBar1.Value += 20;
            }
            else if (progressBar1.Value == progressBar1.Maximum)
            {
                progressBar1.Value = 0;
            }
        }
    }
}
