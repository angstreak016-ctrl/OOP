using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormAnimationDotNet;
using FluentTransitions;

namespace Try
{
    public partial class Tries : Form
    {
        public Tries()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void Try_Load(object sender, EventArgs e)
        {
            panel1.Left = this.ClientSize.Width;

            Transition.With(panel1, nameof(Panel.Left), 100)
                      .EaseInEaseOut(TimeSpan.FromMilliseconds(600));
        }
        private void button1_Click(object sender, EventArgs e)
        {
           panel1.Top = this.ClientSize.Height;

            Transition.With(panel1, nameof(Panel.Top), 100)
                      .EaseInEaseOut(TimeSpan.FromMilliseconds(600));
        }

        
    }
}
