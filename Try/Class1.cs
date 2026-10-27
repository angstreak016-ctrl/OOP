using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace Try
{
    class Database
    {

        string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ard\source\repos\Try\Try\GymDB.mdf;Integrated Security = True";

        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;

        void openConnection()
        {
            con = new SqlConnection(constring);
            try
            {
                con.Open();
            }
            catch
            {
                MessageBox.Show("Connection Error!");
            }
        }

        public void CUD(string query)
        {
            openConnection();

            cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
        }

        // db.create("Profile", "first_name, middle_name, lastname", [txtFirstName, txtMiddleName, txtLastName]);
        public void create(string table, string columns, TextBox[] textbox)
        {
            openConnection();

            string values = "";
            foreach (TextBox txt in textbox)
            {
                if (values == "")
                {
                    values = "'" + txt.Text + "'";
                }
                else
                {
                    values = values + ",'" + txt.Text + "'";
                }
            }

            string query = "INSERT INTO " + table + " (" + columns + ") VALUES (" + values + ")";


            cmd = new SqlCommand(query, con);
            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Successfully saved!");
            }
            else
            {
                MessageBox.Show("Failed!");
            }

        }

        // db.update("Profile", {"first_name", "middle_name", "last_name"}, {txtFirstName, txtMiddleName, txtLastName}, textBox1.text)
        public void update(string table, string[] columns, TextBox[] txt, TextBox id)
        {
            openConnection();

            string fields = "";
            int index = 0;
            foreach (string col in columns)
            {
                if (fields == "")
                {
                    fields = col + " = '" + txt[index].Text + "'";
                }
                else
                {
                    fields = fields + ", " + col + " = '" + txt[index].Text + "'";
                }
                index++;
            }

            string query = "UPDATE " + table + " SET " + fields + " WHERE Id = " + id.Text;

            //MessageBox.Show(query);

            cmd = new SqlCommand(query, con);
            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Successfully updated!");
            }
            else
            {
                MessageBox.Show("Failed!");
            }
        }

        public void delete(string table, TextBox txtId)
        {

            openConnection();

            string query = "DELETE FROM Profile WHERE Id = @Id";

            cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Id", txtId.Text);

            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Successfully deleted!");
            }
            else
            {
                MessageBox.Show("Failed!");
            }
        }

        public DataTable select(string query)
        {
            openConnection();

            da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

    }
}
