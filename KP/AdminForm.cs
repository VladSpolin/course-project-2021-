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
    public partial class AdminForm : Form, KP.Server.IServerCallback
    {
        ServerClient serverClient;
        SqlConnection sqlConnection;
        public int IdClient;
        int id;
        public AdminForm()
        {
            InitializeComponent();
            label5.Text = "";
        }

        private void AdminForm_Load(object sender, EventArgs e)
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
            id=serverClient.Connect(label1.Text);
        }
        public void MsgCallBack(string msg)
        {
            listBox1.Items.Add(msg);
        }
        public void InfoCallBack(string msg)
        {
            label5.Text+=msg;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serverClient.SendMsg(textBox1.Text, id);
            textBox1.Text = "";
        }
    }
}
