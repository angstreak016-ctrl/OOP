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
using System.Data.SqlClient;

namespace Try
{

    public partial class SignInForm : Form
    {
        Database db = new Database();

        public SignInForm()
        {
            InitializeComponent();

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            FormBorderStyle = FormBorderStyle.None;
        }
       

        private void SignInForm_Load(object sender, EventArgs e)
        {
            PlayStartupSound();
        }
        private void PlayStartupSound()
        {
           
            string soundPath = @"C:\Users\Ard\source\repos\Try\Try\Resources\videoplayback (1).wav";

            if (File.Exists(soundPath))
            {
                try
                {
                    using (SoundPlayer player = new SoundPlayer(soundPath))
                    {
                        // player.Play();     
                        player.Play();   
                    }
                }
                catch
                {
                  
                }
            }
        }
      
        private void outlinedButton1_Click(object sender, EventArgs e)
        {
            Visible = false;
            Close();
        }

        private void outlinedButton2_Click(object sender, EventArgs e)
        {
            Database db = new Database();
            db.openConnection();



            if (db.con != null && db.con.State == System.Data.ConnectionState.Open)
            {
                string query = "SELECT Role FROM Users WHERE Username = @Username AND Password = @Password";

                try
                {
                    using (SqlCommand cmd = new SqlCommand(query, db.con))
                    {
                        cmd.Parameters.AddWithValue("@Username", txtname.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", txtpass.Text.Trim());

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            string role = result.ToString();

                            if (role == "Admin")
                            {
                                MessageBox.Show("Login Successful: Welcome Admin!", "Admin Portal", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                UserSession.Username = txtname.Text.Trim();

                                Form1 adminForm = new Form1();
                                adminForm.Show();

                                List<Form> formsToClose = new List<Form>();
                                foreach (Form frm in Application.OpenForms)
                                {
                                    if (frm != adminForm)
                                    {
                                        formsToClose.Add(frm);
                                    }
                                }

                                foreach (Form frm in formsToClose)
                                {
                                    frm.Hide(); 
                                }
                            }
                            else if (role == "Client")
                            {
                                MessageBox.Show("Login Successful: Welcome Client!", "Client Portal", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // TODO: Open your Client Dashboard Form here
                                // ClientForm clientForm = new ClientForm();
                                // clientForm.Show();
                                // this.Hide();
                            }
                            else
                            {
                                MessageBox.Show($"Unrecognized user role: {role}", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            // If result is null, either the username or password was incorrect
                            MessageBox.Show("Invalid Username or Password.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Query Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // 6. Always close the connection when finished
                    db.con.Close();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
