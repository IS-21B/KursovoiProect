using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KursovoiProect
{
    public partial class Seller : Form
    {
        private new string Name; // Переменная для хранения имени пользователя

        public Seller(string Name)
        {
            InitializeComponent();
            this.Name = Name; // Сохранение имени пользователя
        }

        private void Seller_Load(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            Sell sell = new Sell();
            this.Visible = false;
            sell.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Customers customers = new Customers();
            this.Visible = false;
            customers.ShowDialog();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
