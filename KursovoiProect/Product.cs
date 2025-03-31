using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace KursovoiProect
{
    public partial class Product : Form
    {
        private DataTable sourceTable;
        private DataTable categoryTable;
        private DataTable supplierTable;
        private string selectedPhotoPath;
        private int selectedProductID; // Переменная для хранения ID выбранного продукта

        public Product()
        {
            InitializeComponent();
            LoadProducts();
            LoadCategories();
            LoadSuppliers();

            // Подписываемся на событие DataError
            dataGridView1.DataError += DataGridView1_DataError;

            // Подписываемся на событие двойного щелчка
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show($"Ошибка при отображении данных: {e.Exception.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            e.ThrowException = false; // Предотвращаем выброс исключения
        }

        private void LoadProducts()
        {
            using (MySqlConnection connection = new MySqlConnection(Connection.myConnection))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(
                    @"SELECT p.ProductID, p.ProductName, p.Description, p.Price, p.StockQuantity, 
              c.CategoryID, c.CategoryName, s.SuppliersID, s.SuppliersName, Photo, SKU
              FROM products p
              JOIN categories c ON p.CategoryID = c.CategoryID
              JOIN suppliers s ON p.SuppliersID = s.SuppliersID",
                    connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                sourceTable = new DataTable();
                adapter.Fill(sourceTable);

                // Заменяем некорректные данные в столбце Photo
                foreach (DataRow row in sourceTable.Rows)
                {
                    if (row["Photo"] == DBNull.Value || !IsValidImage(row["Photo"]))
                    {
                        row["Photo"] = GetDefaultImage(); // Получите изображение по умолчанию
                    }
                }

                dataGridView1.DataSource = sourceTable;

                if (sourceTable.Rows.Count > 0)
                {
                    dataGridView1.Columns["CategoryName"].HeaderText = "Категория";
                    dataGridView1.Columns["SuppliersName"].HeaderText = "Поставщик";
                }
                else
                {
                    MessageBox.Show("Нет доступных продуктов для отображения.");
                }
            }
        }

        private bool IsValidImage(object imageData)
        {
            try
            {
                using (var ms = new MemoryStream((byte[])imageData))
                {
                    Image.FromStream(ms);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private byte[] GetDefaultImage()
        {
            // Возвращает изображение по умолчанию (например, пустое изображение)
            return null; // Или верните массив байтов изображения по умолчанию
        }

        private void LoadCategories()
        {
            using (MySqlConnection connection = new MySqlConnection(Connection.myConnection))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT CategoryID, CategoryName FROM categories", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                categoryTable = new DataTable();
                adapter.Fill(categoryTable);
                comboBoxCategory.DataSource = categoryTable;
                comboBoxCategory.DisplayMember = "CategoryName";
                comboBoxCategory.ValueMember = "CategoryID";
            }
        }

        private void LoadSuppliers()
        {
            using (MySqlConnection connection = new MySqlConnection(Connection.myConnection))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT SuppliersID, SuppliersName, Email, Phon FROM suppliers", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                supplierTable = new DataTable();
                adapter.Fill(supplierTable);

                comboBoxSupplier.DataSource = supplierTable;
                comboBoxSupplier.DisplayMember = "SuppliersName"; // Поле для отображения
                comboBoxSupplier.ValueMember = "SuppliersID"; // Поле для значения
            }
        }
        


        private void ClearInputFields()
        {
            textBoxProductName.Clear();
            textBoxDescription.Clear();
            textBoxPrice.Clear();
            textBoxStockQuantity.Clear();
            comboBoxCategory.SelectedIndex = -1; // Сбрасываем выбор категории
            comboBoxSupplier.SelectedIndex = -1; // Сбрасываем выбор поставщика
            textBoxSKU.Clear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Проверяем, что кликнули по ячейке, а не по заголовку
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                // Заполняем текстовые поля данными из выбранной строки
                textBoxProductName.Text = row.Cells["ProductName"].Value.ToString();
                textBoxDescription.Text = row.Cells["Description"].Value.ToString();
                textBoxPrice.Text = row.Cells["Price"].Value.ToString();
                textBoxStockQuantity.Text = row.Cells["StockQuantity"].Value.ToString();
                textBoxSKU.Text = row.Cells["StockQuantity"].Value.ToString();
                comboBoxCategory.SelectedValue = row.Cells["CategoryID"].Value; // Устанавливаем выбранную категорию
                comboBoxSupplier.SelectedValue = row.Cells["SupplierID"].Value; // Устанавливаем выбранного поставщика
            }
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Проверяем, что кликнули по ячейке, а не по заголовку
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Заполняем текстовые поля данными из выбранной строки
                textBoxProductName.Text = row.Cells["ProductName"].Value.ToString();
                textBoxDescription.Text = row.Cells["Description"].Value.ToString();
                textBoxPrice.Text = row.Cells["Price"].Value.ToString();
                textBoxStockQuantity.Text = row.Cells["StockQuantity"].Value.ToString();
                textBoxSKU.Text = row.Cells["SKU"].Value.ToString(); // Исправлено на SKU
                comboBoxCategory.SelectedValue = row.Cells["CategoryID"].Value; // Устанавливаем выбранную категорию
                comboBoxSupplier.SelectedValue = row.Cells["SuppliersID"].Value; // Устанавливаем выбранного поставщика

                // Сохраняем ID продукта для редактирования
                selectedProductID = (int)row.Cells["ProductID"].Value;
            }
        }

        private void buttonSelectPhoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Выберите фотографию продукта";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedPhotoPath = openFileDialog.FileName; // Сохраняем путь к выбранной фотографии
                    MessageBox.Show("Фотография выбрана: " + selectedPhotoPath);
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string productName = textBoxProductName.Text;
            string description = textBoxDescription.Text;
            decimal price;
            int stockQuantity;

            // Проверка на выбор категории и поставщика
            if (comboBoxCategory.SelectedValue == null || comboBoxSupplier.SelectedValue == null)
            {
                MessageBox.Show("Пожалуйста, выберите категорию и поставщика.");
                return;
            }

            int categoryId = (int)comboBoxCategory.SelectedValue;
            int suppliersId = (int)comboBoxSupplier.SelectedValue;
            string SKU = textBoxSKU.Text;

            if (decimal.TryParse(textBoxPrice.Text, out price) && int.TryParse(textBoxStockQuantity.Text, out stockQuantity))
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(Connection.myConnection))
                    {
                        connection.Open();
                        MySqlCommand command = new MySqlCommand(
                            "INSERT INTO products (ProductName, Description, Price, StockQuantity, CategoryID, SuppliersID, Photo, SKU) " +
                            "VALUES (@ProductName, @Description, @Price, @StockQuantity, @CategoryID, @SuppliersID, @Photo, @SKU) " +
                            "ON DUPLICATE KEY UPDATE ProductName = @ProductName, Description = @Description, Price = @Price, StockQuantity = @StockQuantity, CategoryID = @CategoryID, SuppliersID = @SuppliersID, Photo = @Photo",
                            connection);

                        command.Parameters.AddWithValue("@ProductName", productName);
                        command.Parameters.AddWithValue("@Description", description);
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@StockQuantity", stockQuantity);
                        command.Parameters.AddWithValue("@CategoryID", categoryId);
                        command.Parameters.AddWithValue("@SuppliersID", suppliersId);
                        command.Parameters.AddWithValue("@SKU", SKU);

                        // Добавьте обработку изображения, если необходимо
                        byte[] imageBytes = null;
                        if (!string.IsNullOrEmpty(selectedPhotoPath))
                        {
                            imageBytes = File.ReadAllBytes(selectedPhotoPath);
                        }
                        command.Parameters.AddWithValue("@Photo", imageBytes);

                        command.ExecuteNonQuery();
                    }

                    LoadProducts(); // Обновляем список продуктов
                    ClearInputFields(); // Очищаем текстовые поля
                    MessageBox.Show("Продукт успешно добавлен или обновлен!");
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Ошибка при добавлении продукта: {ex.Message}\nКод ошибки: {ex.ErrorCode}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Общая ошибка: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректные данные для цены и количества.");
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int productId = (int)dataGridView1.SelectedRows[0].Cells["ProductID"].Value;

                string productName = textBoxProductName.Text;
                string description = textBoxDescription.Text;
                decimal price;
                int stockQuantity;

                // Проверка на корректность введенных данных
                if (string.IsNullOrWhiteSpace(productName) ||
                    string.IsNullOrWhiteSpace(description) ||
                    !decimal.TryParse(textBoxPrice.Text, out price) ||
                    !int.TryParse(textBoxStockQuantity.Text, out stockQuantity))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля корректно.");
                    return;
                }

                try
                {
                    byte[] imageBytes = null;

                    if (!string.IsNullOrEmpty(selectedPhotoPath))
                    {
                        imageBytes = File.ReadAllBytes(selectedPhotoPath);
                    }

                    using (MySqlConnection connection = new MySqlConnection(Connection.myConnection))
                    {
                        connection.Open();
                        MySqlCommand command = new MySqlCommand(
                            "UPDATE products SET ProductName = @ProductName, Description = @Description, Price = @Price, StockQuantity = @StockQuantity, CategoryID = @CategoryID, SuppliersID = @SuppliersID, Photo = @Photo, SKU = @SKU WHERE ProductID = @ProductID",
                            connection);
                        command.Parameters.AddWithValue("@ProductName", productName);
                        command.Parameters.AddWithValue("@Description", description);
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@StockQuantity", stockQuantity);
                        command.Parameters.AddWithValue("@CategoryID", comboBoxCategory.SelectedValue);
                        command.Parameters.AddWithValue("@SuppliersID", comboBoxSupplier.SelectedValue);
                        command.Parameters.AddWithValue("@SKU", textBoxSKU.Text);
                        command.Parameters.AddWithValue("@ProductID", productId);
                        command.Parameters.AddWithValue("@Photo", imageBytes);

                        command.ExecuteNonQuery();
                    }

                    LoadProducts(); // Обновляем список продуктов
                    ClearInputFields(); // Очищаем текстовые поля
                    selectedPhotoPath = null; // Сбрасываем путь к выбранной фотографии
                    MessageBox.Show("Продукт успешно обновлён!");
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Ошибка при обновлении продукта: {ex.Message}\nКод ошибки: {ex.ErrorCode}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Общая ошибка: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите продукт для обновления.");
            }
        }

        

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearInputFields(); // Очищаем текстовые поля
            selectedPhotoPath = null; // Сбрасываем путь к выбранной фотографии
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            this.Visible = false;
            admin.ShowDialog();
            this.Close();
        }

        private void buttonDelete_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int productId = (int)dataGridView1.SelectedRows[0].Cells["ProductID"].Value;

                try
                {
                    using (MySqlConnection connection = new MySqlConnection(Connection.myConnection))
                    {
                        connection.Open();
                        MySqlCommand command = new MySqlCommand("DELETE FROM products WHERE ProductID = @ProductID", connection);
                        command.Parameters.AddWithValue("@ProductID", productId);
                        command.ExecuteNonQuery();
                    }

                    LoadProducts(); // Обновляем список продуктов
                    ClearInputFields(); // Очищаем текстовые поля
                    MessageBox.Show("Продукт успешно удалён!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении продукта: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите продукт для удаления.");
            }
        }
    }
}
