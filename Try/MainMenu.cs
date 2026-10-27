using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Try
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();

            panel1.BackColor = Color.Transparent;
        }
        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void ShowmemyForm(Form f, Panel host)
        {
            f.FormClosed += (s, e) => host.Visible = false;
            f.TopLevel = false;
            host.Visible = true;
            host.Controls.Add(f);
            f.Show();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Location = new Point(0, 0);
            ShowmemyForm(new SignInForm(), panel1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
