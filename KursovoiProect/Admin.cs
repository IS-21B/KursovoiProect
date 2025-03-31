using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;


namespace KursovoiProect
{
    public partial class Admin : Form
    {
        private new string Name;
        public Admin(string Name)
        {
            InitializeComponent();
            
        }

        public Admin()
        {
            InitializeComponent();
            this.Name = Name;
        }
        private void Admin_load(object sender, EventArgs e)
        {
            label1.Text = $"Добро пожаловать, {Name}!";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Avtoriz Avtoriz = new Avtoriz();
            this.Visible = false;
            Avtoriz.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Products Products = new Products();
            this.Visible = false;
            Products.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Staff Staff = new Staff();
            this.Visible = false;
            Staff.ShowDialog();
            this.Close();
        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }
    }
}
