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

namespace KursovoiProect
{
    public partial class Vostanovlenie : Form
    {
        public Vostanovlenie()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (true)
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = "C:\\";
                    openFileDialog.Filter = "SQL files (*.sql)|*.sql|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName;

                        using (MySqlConnection connection = new MySqlConnection(Connection.myConnection))
                            try
                            {
                                connection.Open();

                                string script = System.IO.File.ReadAllText(filePath);

                                MySqlCommand cmd = new MySqlCommand(script, connection);
                                cmd.ExecuteNonQuery();

                                MessageBox.Show("База данных успешно восстановлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (MySqlException ex)
                            {

                                MessageBox.Show($"Ошибка восстановления базы данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {
                                connection.Close();
                            }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Avtoriz Avtoriz = new Avtoriz();
            this.Visible = false;
            Avtoriz.ShowDialog();
            this.Close();
        }
    }
}
