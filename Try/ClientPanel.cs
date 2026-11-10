using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.Design.WebControls;
using System.Windows.Forms;
using FluentTransitions;

namespace Try
{ 
    public partial class ClientPanel : Form
    {
        Database db = new Database();
        public ClientPanel()
        {
            InitializeComponent();
        }
        private int lh, rh;
        private void Form1_Load(object sender, EventArgs e)
        {
            UserRank();
            lh = panel2.Left;
            rh = panel1.Left;
        }
        Customize f = new Customize();
        private void ShowFormInPanel()
        {
            f.Show();
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            panel4.Controls.Add(f);
        }

        private void UserRank()
        {
            Database db = new Database();
            string query = "SELECT UserRank FROM Users WHERE Username = @Username";

            using (SqlConnection connection = new SqlConnection(db.constring))
            {
                using (SqlCommand command = new SqlCommand(query, connection)) 
                {
                    command.Parameters.AddWithValue("@Username", UserSession.Username);
                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                           gradientLabel1.Text = result.ToString();
                        }
                        else
                        {
                           gradientLabel1.Text = "No rank found";
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void speechBubbleControl1_Load(object sender, EventArgs e)
        {

        }

        private void outlinedButton5_Click_1(object sender, EventArgs e)
        {
            ShowFormInPanel();

            Transition
                 .With(panel1, nameof(Panel.Left), -panel2.Width)
                 .EaseInEaseOut(TimeSpan.FromMilliseconds(500));
            Transition
                .With(panel2, nameof(Panel.Left), this.ClientSize.Width)
                .EaseInEaseOut(TimeSpan.FromMilliseconds(500));
        }
        private void closeDoors()
        {
            Transition
                .With(panel2, nameof(Panel.Left), lh)
                .With(panel1, nameof(Panel.Left), rh)
                .EaseInEaseOut(TimeSpan.FromMilliseconds(500));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            UserSession.Username = textBox1.Text;
        }

        private void outlinedButton6_Click(object sender, EventArgs e)
        {
            if (f != null && !f.IsDisposed)
            {
                f.Hide();
                f = null;

                closeDoors();
            }
            else
            {
               var back = new MainMenu();
               back.Show();
               this.Hide();
            }
        }
    }
}
