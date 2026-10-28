using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        private void button1_Click(object sender, EventArgs e)
        {
           panel1.Left = this.ClientSize.Width;

            // 2. Animate it sliding into view on the left
            Transition.With(panel1, nameof(Panel.Left), 100)
                      .EaseInEaseOut(TimeSpan.FromMilliseconds(600));
        }

        private void Try_Load(object sender, EventArgs e)
        {
            panel1.Left = this.ClientSize.Width;

            // Animate it sliding into view
            Transition.With(panel1, nameof(Panel.Left), 100)
                      .EaseInEaseOut(TimeSpan.FromMilliseconds(600));
        }
    }
}
