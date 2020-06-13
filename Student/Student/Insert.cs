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
using System.Data.Sql;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Student
{
    public partial class Insert : Form
    {
        public Insert()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Regex rEmail = new Regex("[a-zA-Z0-9_+&*-]+(?:\\." +
                            "[a-zA-Z0-9_+&*-]+)*@" +
                            "(?:[a-zA-Z0-9-]+\\.)+[a-z" +
                            "A-Z]{2,7}$");
            Regex rPhone = new Regex("^[0-9]");
            if (!rEmail.IsMatch(textBox3.Text))
            {
                MessageBox.Show("Please Enter a Valid Email");
                return;
            }
            if(!rPhone.IsMatch(textBox2.Text))
            {
                MessageBox.Show("Please Enter a Valid Phone Number");
                return;
            }
            
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("PLease Insert Name","Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                textBox1.Focus();
                return;
            }
            if (textBox2.Text.Length > 10)
            {
                MessageBox.Show("phone NO length must be 10", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("PLease Insert Phone Number", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("PLease Insert Email", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("PLease Insert Adrress", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("PLease Insert Course", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("Student id can not be empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SqlConnection CO = new SqlConnection(ConfigurationManager.ConnectionStrings["Log"].ConnectionString);
            CO.Open();
            SqlCommand cm = CO.CreateCommand();
            cm.CommandText = "select * from insertion where name='" + textBox1.Text + "'";
            SqlDataReader rd = cm.ExecuteReader();
            while (rd.Read())
            {
                if (rd[1].ToString() == textBox1.Text)
                {
                    MessageBox.Show("Name Already Exists!!!");
                    return;
                }
                else if (rd[2].ToString() == textBox2.Text)
                {
                    MessageBox.Show("Phone number already exists");
                    return;
                }
                else if (rd[3].ToString() == textBox3.Text)
                {
                    MessageBox.Show("Email id  already exists");
                    return;
                }
            }



            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Log"].ConnectionString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "insert into insertion (Name,Phone,Email,Address,Course,Stud_id) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','"+textBox6.Text+"');";
            int Result = cmd.ExecuteNonQuery();
            if (Result == 1)
            {
                MessageBox.Show("Resigestration Done Sucessfully");
                Clear();
                DisplayData();
            }
            else 
            {
                MessageBox.Show("Problem occured try again!!!");
            }
        }
       
        public void DisplayData()
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Log"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from insertion", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
            }
        }
        public void Clear()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
        }

        private void Insert_Load(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            textBox6.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox6.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["Log"].ConnectionString);
            Con.Open();
            SqlCommand cmd = Con.CreateCommand();
            cmd.CommandText = "update insertion set Name='"+textBox1.Text+"', Phone='"+textBox2.Text+"', Email='"+textBox3.Text+"', Address='"+textBox4.Text+"', Course='"+textBox5.Text +"'where Stud_id='"+textBox6.Text+"'";
             int Result= cmd.ExecuteNonQuery();
            if (Result == 1)
            {
                MessageBox.Show("Updated Successfully");
                DisplayData();
            }
            else 
            {
                MessageBox.Show("Somethinng Wrong occured!!!");
                DisplayData();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox6.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Log"].ConnectionString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "delete from insertion where Stud_id='"+textBox6.Text+"'";
            int result = cmd.ExecuteNonQuery();
            con.Close();
            if (result == 1)
            {
                MessageBox.Show("Deleted Successfully");
                DisplayData();
            }
            else
            {
                MessageBox.Show("Something wrong Happened");
                DisplayData();
            }
        }
    }
}
