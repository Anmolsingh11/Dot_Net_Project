using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Student
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Log"].ConnectionString);
            string pass = textBox2.Text;
            string userName = textBox1.Text;
            DataSet ds = new DataSet();
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Log where Username='"+pass+"'and Pass='"+userName+"'",conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            conn.Close();
            if(ds.Tables[0].Rows.Count >0)
            {
                this.Hide();
                Insert ins = new Insert();
                ins.Show();
            }
        }
    }
}
