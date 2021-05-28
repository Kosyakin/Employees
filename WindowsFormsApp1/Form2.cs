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
    public partial class Form2 : Form
    {
        string choose_dpt;
        public Form2()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string Connect = "Database = first_db; Data Source =localhost; User Id =root; Password=root;";
            try
            {
                MySqlConnection connection = new MySqlConnection(Connect);
                connection.Open();
                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                byte[] img = ms.ToArray();
                MySqlCommand cmd;
                string query = "INSERT INTO employee_with_photo(emp_id, emp_name, dpt_id, photo) VALUES(?emp_id, ?emp_name, ?dpt_id, ?photo)";
                cmd = new MySqlCommand(query, connection);
                cmd.Parameters.Add("?emp_id", MySqlDbType.String);
                cmd.Parameters.Add("?emp_name", MySqlDbType.String);
                cmd.Parameters.Add("?dpt_id", MySqlDbType.String);
                cmd.Parameters.Add("?photo", MySqlDbType.LongBlob);

                cmd.Parameters["?emp_id"].Value = emp_id.Text;
                cmd.Parameters["?emp_name"].Value = emp_name.Text;
                cmd.Parameters["?dpt_id"].Value = dpt_id.Text;
                cmd.Parameters["?photo"].Value = img;

                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Data Inserted");
                }
                connection.Close();
                string path = @"C:\images";
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                    MessageBox.Show(@"C:\images");
                }
                pictureBox1.Image.Save(@"C:\images" + emp_name.Text + ".jpg");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        /* try
         {
             MySqlConnection connection = new MySqlConnection(Connect);
             connection.Open();
             // преобразуем изображение из pictureBox
             MemoryStream ms = new MemoryStream();
             pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
             byte[] img = ms.ToArray();
             MySqlCommand cmd;

             // будем использовать подготовленный запрос, обеспечивающий инф.безопасность
             // ВНИМАНИЕ! Если с не работает со знаком ? (?emp_id и т.д.), попробовать с @(@emp_id)

             string query = "INSERT INTO employee_with_photo(`emp_id`, `emp_name`, `dpt_id`,photo) VALUES(?emp_id, ? emp_name, ? dpt_id, ? photo)";
             cmd = new MySqlCommand(query, connection);

             cmd.Parameters.Add("?emp_id", MySqlDbType.String);
             cmd.Parameters.Add("?emp_name", MySqlDbType.String);
             cmd.Parameters.Add("?dpt_id", MySqlDbType.String);
             cmd.Parameters.Add("?photo", MySqlDbType.LongBlob);

             cmd.Parameters["?emp_id"].Value = emp_id.Text;
             cmd.Parameters["?emp_name"].Value = emp_name.Text;
             cmd.Parameters["?dpt_id"].Value = dpt_id.Text;
             cmd.Parameters["?photo"].Value = img;

             if (cmd.ExecuteNonQuery() == 1)
             {
                 MessageBox.Show("Data Inserted");
             }

             connection.Close();


             string path = @"C:\Users\vova\source\repos\WindowsFormsApp1\фотосотрудников";

             DirectoryInfo dirInfo = new DirectoryInfo(path);
             if (!dirInfo.Exists)
             {
                 dirInfo.Create();
                 MessageBox.Show(@"C:\Users\vova\source\repos\WindowsFormsApp1\фотосотрудников");
             }
             pictureBox1.Image.Save(@"C:\Users\vova\source\repos\WindowsFormsApp1\фотосотрудников" + emp_name.Text + ".jpg");

         }
         catch (Exception ex)
         {
             //throw;
             MessageBox.Show(ex.Message);

         }
     }*/



        /*  string query = "INSERT INTO employee (`Employee_No`, `Employee_Name`, `Deportament_No`) VALUES ('" +emp_id.Text + "','" + emp_name.Text + "','" + choose_dpt + "');";
        MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                command.ExecuteReader();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }*/

        private void Form2_Load(object sender, EventArgs e)
        {
            string Connect = "server=localhost;user=root;database=first_db;password=root;Character Set=utf8;";
            try
            {
            
                MySqlConnection connection = new MySqlConnection(Connect);
                connection.Open();
                string query = "select * from depo";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                List<string> Department = new List<string>();

                while (reader.Read())
                {
                    Department.Add(reader[0].ToString());
                }
                dpt_id.DataSource = Department;
                dpt_id.SelectedIndex = 0;

                connection.Close();
                //2
                pictureBox1.Image = Image.FromFile(@"C:\Users\vova\source\repos\WindowsFormsApp1\картинки\incognito.jpg");
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openPicture = new OpenFileDialog();
            openPicture.Filter = "JPG|*.jpg;*.jpeg|PNG|*.png";
            if (openPicture.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openPicture.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void Dpt_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            choose_dpt = dpt_id.SelectedItem.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
