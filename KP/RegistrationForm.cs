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
    public partial class RegistrationForm : Form
    {
        private SqlConnection sqlConnection = null;
        

        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand($"SELECT * FROM ClientTable WHERE Login='{textBox1.Text}'",sqlConnection);
                adapter.SelectCommand = command;
                adapter.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    command = new SqlCommand($"INSERT INTO ClientTable (Login, FIO, Card, Password) VALUES ('{textBox1.Text}', N'{textBox2.Text}','{Convert.ToInt32(textBox3.Text)}', '{textBox4.Text}')", sqlConnection);
                    this.Close();
                    Form1 form1 = new Form1();
                    sqlConnection.Close();
                    form1.ShowDialog();
                }
                else { MessageBox.Show("Аккаунт с таким логином уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else { MessageBox.Show("Заполните все поля!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error); }
        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Clients"].ConnectionString);
            sqlConnection.Open();
        }
    }
}
