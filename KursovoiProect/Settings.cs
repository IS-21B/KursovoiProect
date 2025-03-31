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
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Settings_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default.host;
            textBox2.Text = Properties.Settings.Default.database;
            textBox3.Text = Properties.Settings.Default.uid;
            textBox4.Text = Properties.Settings.Default.pwd;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string host = textBox1.Text;
            Properties.Settings.Default.host = host;

            string database = textBox2.Text;
            Properties.Settings.Default.database = database;

            string uid = textBox3.Text;
            Properties.Settings.Default.uid = uid;

            string pwd = textBox4.Text;
            Properties.Settings.Default.pwd = pwd;

            Properties.Settings.Default.Save();

            Connection.host = host;
            Connection.database = database;
            Connection.uid = uid;
            Connection.pwd = pwd;

            Connection.myConnection = $@"host={Connection.host};uid={Connection.uid};pwd={Connection.pwd};database={Connection.database}";
            MessageBox.Show("Соеденение установлено", "Настройка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Avtoriz avtoriz = new Avtoriz();
            this.Visible = false;
            avtoriz.ShowDialog();
            this.Close();
        }
    }
}
