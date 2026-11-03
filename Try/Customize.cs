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
        public Customize()
        {
            InitializeComponent();
        }
        private void Customize_Load(object sender, EventArgs e)
        {
            LoadRankFilteredWorkouts();
        }
        private void LoadRankFilteredWorkouts()
        {
            // 1. Get the logged-in username from our session helper
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
                    // 2. Fetch the user's rank from the Users table (defaults to 0 if not set)
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

                    // 3. Determine maximum workout points allowed based on their userrank
                    // Since default is 0, rank 0 will fall into the beginner bracket.
                    int maxPoints = 5; // Beginner tier (Points 1 to 5)

                    if (userRank >= 200 && userRank < 400)
                    {
                        maxPoints = 12; // Intermediate tier (Points up to 12)
                    }
                    else if (userRank >= 400)
                    {
                        maxPoints = 20; // Advanced / Expert tier (All workouts unlocked up to 20)
                    }

                    // 4. Query the Workouts table filtering by points <= maxPoints
                    string workoutQuery = "SELECT name FROM Workouts WHERE points <= @MaxPoints";

                    using (SqlCommand workoutCmd = new SqlCommand(workoutQuery, db.con))
                    {
                        workoutCmd.Parameters.AddWithValue("@MaxPoints", maxPoints);

                        SqlDataAdapter da = new SqlDataAdapter(workoutCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // 5. Bind the filtered list to your ComboBox (e.g., cbWorkouts)
                        comboBox1.DataSource = dt;
                        comboBox1.DisplayMember = "name"; // Column shown in dropdown
                        comboBox1.ValueMember = "name";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading workouts: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    db.con.Close();
                }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Make sure a body part is actually selected
            if (comboBox1.SelectedItem == null) return;

            string selectedBodyPart = comboBox1.SelectedItem.ToString();
            string loggedInUsername = textBox1.Text.Trim(); // Or from your active session variable

            Database db = new Database();
            db.openConnection();

            if (db.con != null && db.con.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    // 1. Get the user's rank from the Users table
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

                    // 2. Determine the maximum workout points allowed based on the user's rank
                    // You can adjust these threshold rules however you like!
                    int maxAllowedPoints = 5; // Default / Beginner tier (Rank 100-200)

                    if (userRank >= 300 && userRank <= 400)
                    {
                        maxAllowedPoints = 12; // Intermediate tier
                    }
                    else if (userRank > 400)
                    {
                        maxAllowedPoints = 20; // Advanced / Expert tier (All workouts unlocked)
                    }

                    // 3. Query the Workouts table filtered by Body Part AND the user's Rank/Points limit
                    string workoutQuery = "SELECT name FROM Workouts WHERE bodypart = @Bodypart AND points <= @Points";

                    using (SqlCommand workoutCmd = new SqlCommand(workoutQuery, db.con))
                    {
                        workoutCmd.Parameters.AddWithValue("@Bodypart", selectedBodyPart);
                        workoutCmd.Parameters.AddWithValue("@Points", maxAllowedPoints);

                        // Use a SqlDataAdapter and DataTable to fill the ComboBox
                        SqlDataAdapter da = new SqlDataAdapter(workoutCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Bind the filtered data to your workouts ComboBox
                        comboBox1.DataSource = dt;
                        comboBox1.DisplayMember = "name"; // The column name shown in the dropdown
                        comboBox1.ValueMember = "name";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading workouts: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    db.con.Close();
                }
            }
        }

       
    }
}
