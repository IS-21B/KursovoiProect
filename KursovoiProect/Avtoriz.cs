using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace KursovoiProect
{
    public partial class Avtoriz : Form
    {
        public Avtoriz()
        {
            InitializeComponent();
        }

        string conn = Connection.myConnection;

        private void button1_Click(object sender, EventArgs e)
        {
            string UserLogin = textBox1.Text;
            string UserPass = textBox2.Text;

            // Проверка на введенные данные "admin"
            if (UserLogin == "admin1" && UserPass == "admin1")
            {
                // Создаем и открываем форму администратора
                Vostanovlenie adminForm = new Vostanovlenie(); // Имя можно изменить или оставить "Admin"
                adminForm.Show();
                this.Hide(); // Скрываем форму авторизации
                return; // Завершаем выполнение метода

            }

            if (UserLogin.Length != 0)
            {
                using (MySqlConnection con = new MySqlConnection(conn))
                {
                    try
                    {
                        con.Open();
                        string hashedPassword = HashPassword(UserPass);
                        using (MySqlCommand cmd = new MySqlCommand("SELECT Name, RollID FROM employees WHERE login = @login AND password = @password", con))
                        {
                            cmd.Parameters.AddWithValue("@login", UserLogin);
                            cmd.Parameters.AddWithValue("@password", hashedPassword);

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    reader.Read();
                                    string userName = reader["Name"].ToString(); // Получаем имя пользователя
                                    string role = reader["RollID"].ToString();

                                    if (role == "1")
                                    {
                                        Admin form = new Admin(userName); // Передаем имя администратора
                                        form.Show();
                                    }
                                    else if (role == "2")
                                    {
                                        Seller form = new Seller(userName); // Передаем имя продавца
                                        form.Show();
                                    }

                                    this.Hide(); // Скрываем форму авторизации
                                }
                                else
                                {
                                    MessageBox.Show("Введен неверный логин или пароль", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Введите логин", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void Avtoriz_Load(object sender, EventArgs e)
        {
            // Дополнительная инициализация, если требуется
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            Settings serverSettinng = new Settings();
            this.Visible = false;
            serverSettinng.ShowDialog();
            this.Close();
        }
    }
}