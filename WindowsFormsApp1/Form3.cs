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
    public partial class Form3 : Form
    {

        public Form3()
        {
            InitializeComponent();
        }

        private Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            ms.Position = 0;
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            pictureBox1.Image = Image.FromFile(@"C: \Users\vova\source\repos\WindowsFormsApp1\картинки\incognito.jpg");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.MaximumSize = new Size(550, 420);
            this.MinimumSize = new Size(550, 420);
            string Connect = "Database = first_db; Data Source =localhost; User Id =root; Password=root;";
            try
            {
                MySqlConnection connection = new MySqlConnection(Connect);
                connection.Open();
                string s = "SELECT * FROM employee_with_photo ORDER BY id DESC LIMIT 1;";
                MySqlCommand mySql = new MySqlCommand(s, connection);
                mySql.ExecuteNonQuery();
                MySqlDataReader dr = mySql.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    label1.Text = dr["emp_id"].ToString();
                    label2.Text = dr["emp_name"].ToString();
                    label3.Text = dr["dpt_id"].ToString();
                    pictureBox1.Image = byteArrayToImage(dr["photo"] as byte[]);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
