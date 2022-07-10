using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace KP
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;
        public int id;
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Blue;
            this.Cursor = Cursors.Hand;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Black;
            this.Cursor = Cursors.Arrow;
        }

        private void label3_MouseClick(object sender, MouseEventArgs e)
        {
            RegistrationForm registrationForm = new RegistrationForm();
            DialogResult result = registrationForm.ShowDialog();
            sqlConnection.Close();
            this.Hide();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand($"SELECT * FROM ClientTable WHERE Login='{textBox1.Text}'", sqlConnection);
                adapter.SelectCommand = command;
                adapter.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    SqlCommand sqlCom = new SqlCommand($"SELECT Id FROM ClientTable WHERE Login='{textBox1.Text}'", sqlConnection);
                    SqlDataReader reader1 = sqlCom.ExecuteReader();
                    while (reader1.Read())
                    {
                        id = Convert.ToInt32(reader1["Id"]);
                    }
                    reader1.Close();
                    SqlCommand sqlCommand = new SqlCommand($"SELECT Password FROM ClientTable WHERE Login='{textBox1.Text}'", sqlConnection);
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        if (textBox2.Text.ToString() == Convert.ToString(reader["Password"]))
                        {
                            if (textBox1.Text == "Admin")
                            {
                                AdminForm adminForm = new AdminForm();
                                adminForm.IdClient = id;
                                adminForm.ShowDialog();
                            }
                            else
                            {
                                MainForm mainForm = new MainForm();
                                mainForm.IdClient = id;
                                mainForm.ShowDialog();
                                
                            }
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Введен неверный пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBox2.Text = "";
                        }
                    }
                    reader.Close();
                }
                else { MessageBox.Show("Аккаунта с таким логином не существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }


            }
            else { MessageBox.Show("Введите логин!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Clients"].ConnectionString);
            sqlConnection.Open();
        }
    }
}
