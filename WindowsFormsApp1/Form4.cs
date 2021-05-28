using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            select();

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.MaximumSize = new Size(641, 490);
            this.MinimumSize = new Size(641, 490);
            dataGridView1.AllowUserToAddRows = false;
        }

        public void select()
        {
            string Connect = "Database = first_db; Data Source =localhost; User Id =root; Password=root;";
            MySqlConnection connection = new MySqlConnection(Connect);
            connection.Open();
            string query = "SELECT emp_id, emp_name, dpt_id FROM employee_with_photo;";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            dataGridView1.Columns.Add("emp_id", "Employee ID");
            dataGridView1.Columns["emp_id"].Width = 70;
            dataGridView1.Columns.Add("emp_name", "Employee Name");
            dataGridView1.Columns["emp_name"].Width = 150;
            dataGridView1.Columns.Add("dpt_id", "Dpt ID");
            dataGridView1.Columns["dpt_id"].Width = 70;
            try
            {
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["emp_id"].ToString(), reader["emp_name"].ToString(), reader["dpt_id"].ToString());
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                command.Connection.Close();
            }
        }

        private Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            ms.Position = 0;
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            string Connect = "Database = first_db; Data Source =localhost; User Id =root; Password=root;";
            string s = dataGridView1.CurrentCell.Value.ToString();
            try
            {
                MySqlConnection connection = new MySqlConnection(Connect);
                connection.Open();
                string query = "select * from employee_with_photo where emp_id='" + s + "';";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    pictureBox1.Image = byteArrayToImage(reader["photo"] as byte[]);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
