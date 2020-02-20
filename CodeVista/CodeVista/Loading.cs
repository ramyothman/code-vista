using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CodeVista
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }
        public delegate void SetTextDelegate(string text);
        public delegate void LoadProgressDelegate(int max);
        public delegate void IncrementProgressDelegate();
        public void SetText(string text)
        {
            if (label1.InvokeRequired)
            {
                SetTextDelegate del = new SetTextDelegate(SetText);
                label1.Invoke(del, text);
            }
            else
            {
                label1.Text = text;
            }
        }

        public void LoadProgress(int max)
        {
            if (progressBarControl.InvokeRequired)
            {
                LoadProgressDelegate del = new LoadProgressDelegate(LoadProgress);
                progressBarControl.Invoke(del, max);
            }
            else
            {
                progressBarControl.Value = 0;
                progressBarControl.Maximum = max;
            }
        }
        public void IncrementProgress()
        {
            if (progressBarControl.InvokeRequired)
            {
                IncrementProgressDelegate del = new IncrementProgressDelegate(IncrementProgress);
                progressBarControl.Invoke(del);
            }
            else
                progressBarControl.Value += 1;
        }


    }
}
