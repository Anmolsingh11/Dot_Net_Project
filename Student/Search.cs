using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Student
{
    public partial class Search : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Log"].ConnectionString);
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter adpt;
        public Search()
        {
            InitializeComponent();
            displayValue();
        }
        public void displayValue()
        {
            con.Open();
            adpt = new SqlDataAdapter("select * from insertion", con);
            dt = new DataTable();
            adpt.Fill(dt);
            dataGridView1.DataSource = dt;
                con.Close();
        }

        private void Search_Load(object sender, EventArgs e)
        {
           
        }
       

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SearchData(textBox1.Text,comboBox1.Text);
        }
        public void SearchData(string search,string category)
        {
            con.Open();
            string query = "select * from insertion where "+category+" like '%" + search + "%'";
            adpt = new SqlDataAdapter(query,con);
            dt = new DataTable();
            adpt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        
    }
}
