using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.Design.WebControls;
using System.Windows.Forms;
using FluentTransitions;
using OpenCvSharp;
using OpenCvSharp.Extensions;


namespace Try
{ 
    public partial class ClientPanel : Form
    {
        Database db = new Database();
        private Form activeForm = null;
        private int lh, rh;
        public ClientPanel()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            UserRank();
            lh = panel2.Left;
            rh = panel1.Left;
        }

        private void ShowFormInPanel(Form childForm)
        {
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panel4.Controls.Clear(); 
            panel4.Controls.Add(childForm);
            panel4.SendToBack();
            childForm.Show();
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
            activeForm = new Customize();
            ShowFormInPanel(activeForm);

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

        private void outlinedButton3_Click(object sender, EventArgs e)
        {
            ShowFormInPanel(new Start());

            Transition
               .With(panel1, nameof(Panel.Left), -panel2.Width)
               .EaseInEaseOut(TimeSpan.FromMilliseconds(500));
            Transition
                .With(panel2, nameof(Panel.Left), this.ClientSize.Width)
                .EaseInEaseOut(TimeSpan.FromMilliseconds(500));
        }
       
        private void outlinedButton6_Click(object sender, EventArgs e)
        {
            if (activeForm != null && !activeForm.IsDisposed)
            {
                activeForm.Close();
                activeForm.Dispose();
                activeForm = null;

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
