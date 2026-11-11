using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Try
{
    public partial class Customize : Form
    {
        private int textBoxCounter = 0;
        public Customize()
        {
            InitializeComponent();
        }
        private void Customize_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.ControlAdded += flowLayoutPanel1_ControlAdded;
            textBox1.Text = UserSession.Username;
            LoadRankFilteredWorkouts(comboBox1);
        }
        private void ClearComboBox(ComboBox cb)
        {
            cb.SelectedIndex = -1;
            cb.Text = string.Empty;
        }
        private void flowLayoutPanel1_ControlAdded(object sender, ControlEventArgs e)
        {
            if (e.Control is ComboBox cb)
            {
                LoadRankFilteredWorkouts(cb);
            }
        }
        private void LoadRankFilteredWorkouts(ComboBox target)
        {

            string currentUsername = UserSession.Username;

            if (string.IsNullOrEmpty(currentUsername))
            {
                MessageBox.Show("No active user session found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Database db = new Database();
            db.openConnection();

            if (db.con != null && db.con.State == System.Data.ConnectionState.Open)
            {
                try
                {

                    int userRank = 0;
                    string rankQuery = "SELECT userrank FROM Users WHERE username = @Username";

                    using (SqlCommand rankCmd = new SqlCommand(rankQuery, db.con))
                    {
                        rankCmd.Parameters.AddWithValue("@Username", currentUsername);
                        object result = rankCmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            userRank = Convert.ToInt32(result);
                        }
                    }

                    int maxPoints = 5;

                    if (userRank >= 200 && userRank < 400)
                    {
                        maxPoints = 12;
                    }
                    else if (userRank >= 400)
                    {
                        maxPoints = 20;
                    }


                    string workoutQuery = "SELECT name FROM Workouts WHERE points <= @MaxPoints";

                    using (SqlCommand workoutCmd = new SqlCommand(workoutQuery, db.con))
                    {
                            workoutCmd.Parameters.AddWithValue("@MaxPoints", maxPoints);

                            SqlDataAdapter da = new SqlDataAdapter(workoutCmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            target.DataSource = dt;
                            target.DisplayMember = "name";
                            target.ValueMember = "name";
                            target.SelectedIndex = -1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading workouts: " + ex.Message, 
                       "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    db.con.Close();
                }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null) return;

            string selectedBodyPart = comboBox1.SelectedItem.ToString();
            string loggedInUsername = textBox1.Text.Trim();

            Database db = new Database();
            db.openConnection();

            if (db.con != null && db.con.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    int userRank = 0;
                    string rankQuery = "SELECT UserRank FROM Users WHERE Username = @Username";

                    using (SqlCommand rankCmd = new SqlCommand(rankQuery, db.con))
                    {
                        rankCmd.Parameters.AddWithValue("@Username", loggedInUsername);
                        object rankResult = rankCmd.ExecuteScalar();

                        if (rankResult != null)
                        {
                            userRank = Convert.ToInt32(rankResult);
                        }
                    }
                    int maxAllowedPoints = 5;

                    if (userRank >= 300 && userRank <= 400)
                    {
                        maxAllowedPoints = 12;
                    }
                    else if (userRank > 400)
                    {
                        maxAllowedPoints = 20;
                    }
                    string workoutQuery = "SELECT name FROM Workouts WHERE bodypart = @Bodypart AND points <= @Points";

                    using (SqlCommand workoutCmd = new SqlCommand(workoutQuery, db.con))
                    {
                        workoutCmd.Parameters.AddWithValue("@Bodypart", selectedBodyPart);
                        workoutCmd.Parameters.AddWithValue("@Points", maxAllowedPoints);

                            SqlDataAdapter da = new SqlDataAdapter(workoutCmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            comboBox1.DataSource = dt;
                            comboBox1.DisplayMember = "name";
                            comboBox1.ValueMember = "name";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading workouts: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    db.con.Close();
                }
            }
        }

        private void outlinedButton1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void outlinedButton2_Click(object sender, EventArgs e)
        {
            textBoxCounter--;
        }

        private void outlinedButton3_Click(object sender, EventArgs e)
        {
            ComboBox newcBox = new ComboBox();

            textBoxCounter++;
            newcBox.Name = "combo" + textBoxCounter;
            newcBox.Text = " " + textBoxCounter;
            newcBox.Font = new Font("Humato Heavy", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

            newcBox.Size = new Size(214, 21);

            flowLayoutPanel1.Controls.Add(newcBox);
        }

        private void outlinedButton4_Click(object sender, EventArgs e)
        {
            foreach (Control c in flowLayoutPanel1.Controls)
            {
                if (c is ComboBox cb)
                {
                    ClearComboBox(cb);
                }
            }
        }

        private void outlinedButton5_Click(object sender, EventArgs e)
        {
            for (int i = flowLayoutPanel1.Controls.Count - 1; i >= 0; i--)
            {
                if (flowLayoutPanel1.Controls[i] is ComboBox cb)
                {
                    flowLayoutPanel1.Controls.Remove(cb);
                    cb.Dispose();
                    textBoxCounter--;
                    break; // stop after removing one
                }
            }
        }

        private void outlinedButton6_Click(object sender, EventArgs e)
        {
            foreach (Control c in flowLayoutPanel1.Controls)
            {
                if (c is ComboBox cb)
                {
                    ClearComboBox(cb);
                }
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
