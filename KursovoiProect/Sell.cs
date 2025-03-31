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
    public partial class Sell : Form
    {
        private DataTable sourceTable;
        public Sell()
        {
            InitializeComponent();
            InitializeTargetDataGridViews(); // Инициализация столбцов
            InitializeTargetDataGridView();
            LoadData();
        }
        private void InitializeTargetDataGridView()
        {
            // Очистка существующих столбцов, если необходимо
            dataGridViewTarget.Columns.Clear();
            dataGridViewTarget.Columns.Add("ProductName", "Название");
            dataGridViewTarget.Columns.Add("Quantity", "Количество");
            dataGridViewTarget.Columns.Add("Price", "Цена");
            dataGridViewTarget.Columns.Add("totalPrice", "Сумма");

        }
        private void InitializeTargetDataGridViews()
        {
            // Очистка существующих столбцов, если необходимо
            dataGridViewTarget.Columns.Clear();

            // Добавление столбцов в dataGridViewTarget
            dataGridViewTarget.Columns.Add("ProductID", "Product ID");
            dataGridViewTarget.Columns.Add("ProductName", "Product Name");
            dataGridViewTarget.Columns.Add("Description", "Description");
            dataGridViewTarget.Columns.Add("Price", "Price");
            dataGridViewTarget.Columns.Add("StockQuantity", "Stock Quantity");
            dataGridViewTarget.Columns.Add("Quantity", "Quantity"); // Количество для корзины
        }

        private void LoadData()
        {
            sourceTable = new DataTable();
            using (MySqlConnection connection = new MySqlConnection(Connection.myConnection))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT ProductID, ProductName, Description, Price, StockQuantity FROM products";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    adapter.Fill(sourceTable);
                    dataGridViewSource.DataSource = sourceTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataRow selectedRow = sourceTable.Rows[e.RowIndex];
                DataRow newRow = ((DataTable)dataGridViewTarget.DataSource)?.NewRow();
                if (newRow != null)
                {
                    newRow["ProductID"] = selectedRow["ProductID"];
                    newRow["ProductName"] = selectedRow["ProductName"];
                    newRow["Description"] = selectedRow["Description"];
                    newRow["Price"] = selectedRow["Price"];
                    newRow["StockQuantity"] = selectedRow["StockQuantity"];
                    ((DataTable)dataGridViewTarget.DataSource).Rows.Add(newRow);
                }
            }
        }
        private void dataGridViewSource_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataRow selectedRow = sourceTable.Rows[e.RowIndex];
                DataRow newRow = ((DataTable)dataGridViewTarget.DataSource)?.NewRow();
                if (newRow != null)
                {
                    newRow["ProductID"] = selectedRow["ProductID"];
                    newRow["ProductName"] = selectedRow["ProductName"];
                    newRow["Description"] = selectedRow["Description"];
                    newRow["Price"] = selectedRow["Price"];
                    newRow["StockQuantity"] = selectedRow["StockQuantity"];
                    newRow["Quantity"] = 1; // Устанавливаем начальное количество
                    ((DataTable)dataGridViewTarget.DataSource).Rows.Add(newRow);
                }

            }
        }
        private void UpdateTotalSum()
        {
            decimal totalSum = 0;

            foreach (DataGridViewRow row in dataGridViewTarget.Rows)
            {
                if (row.Cells["totalPrice"].Value != null)
                {
                    totalSum += Convert.ToDecimal(row.Cells["totalPrice"].Value);
                }
            }

            labelTotalSum.Text = $"Общая сумма: {totalSum:C}"; // Форматирование как денежная сумма
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridViewSource.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewSource.SelectedRows[0];
                string productName = selectedRow.Cells["ProductName"].Value?.ToString();
                int quantityToAdd = (int)numericUpDown1.Value;
                decimal price = Convert.ToDecimal(selectedRow.Cells["Price"].Value);
                int stockQuantity = Convert.ToInt32(selectedRow.Cells["StockQuantity"].Value);

                // Проверка, достаточно ли товара на складе
                if (quantityToAdd > stockQuantity)
                {
                    MessageBox.Show("Недостаточно товара на складе.");
                    return;
                }

                // Уменьшаем количество на складе
                selectedRow.Cells["StockQuantity"].Value = stockQuantity - quantityToAdd;

                // Проверка, существует ли товар в корзине
                bool productExists = false;
                foreach (DataGridViewRow row in dataGridViewTarget.Rows)
                {
                    // Проверяем, существует ли значение в ячейке
                    if (row.Cells["ProductName"].Value != null && row.Cells["ProductName"].Value.ToString() == productName)
                    {
                        // Обновляем количество и общую сумму
                        int existingQuantity = Convert.ToInt32(row.Cells["Quantity"].Value ?? 0);
                        row.Cells["Quantity"].Value = existingQuantity + quantityToAdd;

                        // Обновляем общую цену
                        decimal totalPrice = price * (existingQuantity + quantityToAdd);
                        row.Cells["TotalPrice"].Value = totalPrice;

                        productExists = true;
                        break;
                    }
                }

                // Если товар не найден в корзине, добавляем новую строку
                if (!productExists)
                {
                    decimal totalPrice = price * quantityToAdd;
                    dataGridViewTarget.Rows.Add(productName, quantityToAdd, price, totalPrice);
                }

                // Обновляем общую сумму
                UpdateTotalSum();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите продукт для добавления в корзину.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Seller Seller = new Seller(Name);
            this.Visible = false;
            Seller.ShowDialog();
            this.Close();
        }
    }
}
