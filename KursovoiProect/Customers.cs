using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace KursovoiProect
{
    public partial class Customers : Form
    {
        private DataTable sourceTable;

        public Customers()
        {
            InitializeComponent();
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            using (MySqlConnection connection = new MySqlConnection(Connection.myConnection))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(
                    @"SELECT CustomerID, FirstName, LastName, Email, Phone, Address 
                      FROM Customers",
                    connection);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                sourceTable = new DataTable();
                adapter.Fill(sourceTable);

                // Привязываем данные к DataGridView
                dataGridView1.DataSource = sourceTable;

                // Переименовываем заголовки колонок для удобства
                dataGridView1.Columns["CustomerID"].HeaderText = "ID Клиента";
                dataGridView1.Columns["FirstName"].HeaderText = "Имя";
                dataGridView1.Columns["LastName"].HeaderText = "Фамилия";
                dataGridView1.Columns["Email"].HeaderText = "Электронная почта";
                dataGridView1.Columns["Phone"].HeaderText = "Телефон";
                dataGridView1.Columns["Address"].HeaderText = "Адрес";
            }
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            Admin form = new Admin();
            form.Show();
            this.Hide();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(Connection.myConnection))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(
                    @"INSERT INTO Customers (FirstName, LastName, Email, Phone, Address) 
                      VALUES (@FirstName, @LastName, @Email, @Phone, @Address)",
                    connection);

                command.Parameters.AddWithValue("@FirstName", textBoxFirstName.Text);
                command.Parameters.AddWithValue("@LastName", textBoxLastName.Text);
                command.Parameters.AddWithValue("@Email", textBoxEmail.Text);
                command.Parameters.AddWithValue("@Phone", textBoxPhone.Text);
                command.Parameters.AddWithValue("@Address", textBoxAddress.Text);

                command.ExecuteNonQuery();
            }

            // Обновляем DataGridView после добавления
            LoadCustomers();
        }

       
        private void Customers_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Seller Seller = new Seller(Name);
            this.Visible = false;
            Seller.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                int customerId = Convert.ToInt32(selectedRow.Cells["CustomerID"].Value);

                using (MySqlConnection connection = new MySqlConnection(Connection.myConnection))
                {
                    try
                    {
                        connection.Open();
                        MySqlCommand command = new MySqlCommand(
                            @"DELETE FROM Customers WHERE CustomerID = @CustomerID",
                            connection);

                        command.Parameters.AddWithValue("@CustomerID", customerId);
                        command.ExecuteNonQuery();

                        MessageBox.Show("Клиент успешно удален!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении клиента: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // Обновляем DataGridView после удаления
                LoadCustomers();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите клиента для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
