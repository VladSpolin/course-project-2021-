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
using KP.Server;

namespace KP
{
    public partial class MainForm : Form, KP.Server.IServerCallback
    {
        ServerClient serverClient;
        
        int id;
        SqlConnection sqlConnection;
        public int IdClient;

        public MainForm()
        {
            InitializeComponent();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Clients"].ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand($"SELECT FIO FROM ClientTable WHERE ID='{IdClient}'", sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
               label1.Text = Convert.ToString(reader["FIO"]);
            }
            reader.Close();
            serverClient = new ServerClient(new System.ServiceModel.InstanceContext(this));
            id = serverClient.Connect(label1.Text);
            
        }
     

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public void MsgCallBack(string msg)
        {
            listBox1.Items.Add(msg);
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            serverClient.SendMsg(textBox3.Text, id);

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        public void InfoCallBack(string msg)
        {
            //listBox2.Items.Add(msg);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string info="\n";
            double sum = 0;
            if(textBox2.Text!="")
            {
                sum += Convert.ToDouble(textBox2.Text);
                info = $"{comboBox1.Text}({tabPage1.Text}) - {textBox2.Text}BYN\n";
            }
            if (textBox4.Text != "")
            {
                sum += Convert.ToDouble(textBox4.Text);
                info += $"{comboBox2.Text}({tabPage2.Text}) - {textBox4.Text}BYN\n";
            }
            if (textBox6.Text != "")
            {
                sum += Convert.ToDouble(textBox6.Text);
                info += $"{comboBox3.Text}({tabPage3.Text}) - {textBox6.Text}BYN\n";
            }
            if (textBox8.Text != "")
            {
                sum += Convert.ToDouble(textBox8.Text);
                info += $"{comboBox4.Text}({tabPage4.Text}) - {textBox8.Text}BYN\n";
            }
            info += $"Итоговая сумма платежа - {sum}BYN\n";


            serverClient.SendInfo(info, id);
        }
    }
}
