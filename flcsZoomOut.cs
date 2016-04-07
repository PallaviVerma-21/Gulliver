using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace GulliverII
{
    public partial class flcsZoomOut : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public string text;

        public flcsZoomOut(string text, string title)
        {
            InitializeComponent();
            this.text = text.Replace("\n", Environment.NewLine);
            kryptonHeaderGroup7.ValuesPrimary.Heading = title;
            txtText.Text = text;
        }

        private void flcsZoomOut_FormClosing(object sender, FormClosingEventArgs e)
        {
            text = txtText.Text.Trim();
        }
    }
}