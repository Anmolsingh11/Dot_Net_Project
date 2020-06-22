using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Insert insert = new Insert();
            insert.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Search search = new Search();
            search.Show();
            this.Hide();
        }
    }
}
