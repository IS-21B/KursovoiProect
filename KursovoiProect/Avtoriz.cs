using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace KursovoiProect
{
    public partial class Avtoriz : Form
    {
        private string captchaText;
        private DateTime lastFailedAttempt;
        private bool captchaShown;

        public Avtoriz()
        {
            InitializeComponent();
            this.Width = 325;
            pictureBoxCaptcha.Visible = false;
            textBoxCaptcha.Visible = false;
            GenerateCaptcha(); 
            captchaShown = false; 
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
            string UserCaptcha = textBoxCaptcha.Text;

            if (textBoxCaptcha.Visible && !string.IsNullOrEmpty(UserCaptcha) && UserCaptcha != captchaText)
            {
                lastFailedAttempt = DateTime.Now;
                MessageBox.Show("Неверный код CAPTCHA", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                GenerateCaptcha(); 
                return;
            }
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
            if (!captchaShown) 
            {
                this.Width += 300; 
                captchaShown = true; 
            }

            MessageBox.Show("Введен неверный логин или пароль", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ShowCaptcha();
            textBoxCaptcha.Clear();
        }
        private void ShowCaptcha()
        {
            
            pictureBoxCaptcha.Visible = true;
            textBoxCaptcha.Visible = true;
            GenerateCaptcha();
        }
        private void GenerateCaptcha()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();

            captchaText = new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());
            Bitmap bitmap = new Bitmap(200, 80);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                using (Font font = new Font("Arial", 40, FontStyle.Bold))
                {
                    g.DrawString(captchaText, font, Brushes.Black, new PointF(20, 20));
                }
                for (int i = 0; i < 10; i++)
                {
                    g.DrawLine(new Pen(Color.LightGray, 2),
                               random.Next(0, bitmap.Width),
                               random.Next(0, bitmap.Height),
                               random.Next(0, bitmap.Width),
                               random.Next(0, bitmap.Height));
                }
            }
            pictureBoxCaptcha.Image = bitmap; 
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

        private void rez_Click(object sender, EventArgs e)
        {
            GenerateCaptcha();
        }
    }
}