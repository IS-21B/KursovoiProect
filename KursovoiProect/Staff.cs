using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace KursovoiProect
{
    public partial class Staff : Form
    {
        private DataTable sourceTable;

        public Staff()
        {
            InitializeComponent();
            LoadProducts();
            LoadRoles(); // Загрузка ролей в ComboBox
        }

        private void LoadProducts()
        {
            using (MySqlConnection connection = new MySqlConnection(Connection.myConnection))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(
                    @"SELECT e.Name, e.login, r.NameRoll
                      FROM employees e
                      INNER JOIN roll r ON e.RollID = r.RollID",
                    connection);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                sourceTable = new DataTable();
                adapter.Fill(sourceTable);

                // Привязываем данные к DataGridView
                dataGridView1.DataSource = sourceTable;

                dataGridView1.Columns["Name"].HeaderText = "Имя";
                dataGridView1.Columns["login"].HeaderText = "Логин";
                dataGridView1.Columns["NameRoll"].HeaderText = "Роль"; // Убедитесь, что здесь используется RollName
            }
        }

        private void LoadRoles()
        {
            using (MySqlConnection connection = new MySqlConnection(Connection.myConnection))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT RollID, NameRoll FROM roll", connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    comboBoxRoles.Items.Add(new { Text = reader["NameRoll"].ToString(), Value = reader["RollID"] });
                }

                comboBoxRoles.DisplayMember = "Text";
                comboBoxRoles.ValueMember = "Value";
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); // Преобразуем каждый байт в шестнадцатеричное представление
                }
                return builder.ToString();
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(Connection.myConnection))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(
                    @"INSERT INTO employees (Name, login, password, RollID) VALUES (@Name, @login, @password, @RollID)",
                    connection);

                command.Parameters.AddWithValue("@Name", textBoxName.Text);
                command.Parameters.AddWithValue("@login", textBoxLogin.Text);
                command.Parameters.AddWithValue("@password", HashPassword(textBoxPassword.Text)); // Хэшируем пароль перед записью
                command.Parameters.AddWithValue("@RollID", (comboBoxRoles.SelectedItem as dynamic).Value);

                command.ExecuteNonQuery();
            }

            // Обновляем DataGridView после добавления
            LoadProducts();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Admin form = new Admin();
            form.Show();
            this.Hide();
        }

        private void Staff_Load(object sender, EventArgs e)
        {

        }
    }
}
