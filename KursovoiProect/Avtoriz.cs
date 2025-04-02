using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace KursovoiProect
{
    public partial class Avtoriz : Form
    {
        private DateTime lastFailedAttempt;

        public Avtoriz()
        {
            InitializeComponent();
            this.Width = 325;
        }

        string conn = Connection.myConnection;

        private void button1_Click(object sender, EventArgs e)
        {
            // Проверка времени блокировки после неудачной попытки
            if (DateTime.Now < lastFailedAttempt.AddSeconds(10))
            {
                MessageBox.Show("Блокировка на 10 секунд. Попробуйте еще раз позже.", "Доступ запрещен", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string UserLogin = textBox1.Text;
            string UserPass = textBox2.Text;

            // Проверка введенных данных "admin"
            if (UserLogin == "admin1" && UserPass == "admin1")
            {
                Vostanovlenie adminForm = new Vostanovlenie();
                adminForm.Show();
                this.Hide();
                return;
            }

            if (!string.IsNullOrEmpty(UserLogin) && !string.IsNullOrEmpty(UserPass))
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
                                    string userName = reader["Name"].ToString();
                                    string role = reader["RollID"].ToString();

                                    if (role == "1")
                                    {
                                        Admin adminForm = new Admin(userName);
                                        adminForm.Show();
                                    }
                                    else if (role == "2")
                                    {
                                        Seller sellerForm = new Seller(userName);
                                        sellerForm.Show();
                                    }
                                    this.Hide(); 
                                }
                                else
                                {
                                    HandleInvalidLogin(); 
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
                MessageBox.Show("Введите логин и пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void HandleInvalidLogin()
        {
            lastFailedAttempt = DateTime.Now; 
            MessageBox.Show("Введен неверный логин или пароль", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Settings settingsForm = new Settings();
            this.Visible = false;
            settingsForm.ShowDialog();
            this.Close();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            Settings settingsForm = new Settings();
            this.Visible = false;
            settingsForm.ShowDialog();
            this.Close();
        }
    }
}