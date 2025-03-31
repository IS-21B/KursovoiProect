using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace KursovoiProect
{
    public partial class Products : Form
    {
        private DataTable sourceTable;
        private DataTable categoryTable;
        private DataTable supplierTable;

        public Products()
        {
            InitializeComponent();
            LoadCategories(); // Загрузка категорий
            LoadProducts(); // Затем загружаем продукты
            LoadSuppliers();
            dataGridView1.DataError += DataGridView1_DataError;
            textBoxSearch.TextChanged += TextBoxSearch_TextChanged;

            // Инициализация ComboBox для сортировки
            InitializeSortComboBox();
        }

        private void InitializeSortComboBox()
        {
            comboBoxSortByPrice.Items.Add("Не сортировать");
            comboBoxSortByPrice.Items.Add("По возрастанию");
            comboBoxSortByPrice.Items.Add("По убыванию");
            comboBoxSortByPrice.SelectedIndex = 0; // Установить по умолчанию на "Не сортировать"
            comboBoxSortByPrice.SelectedIndexChanged += ComboBoxSortByPrice_SelectedIndexChanged;
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

                // Добавляем элемент "Все категории"
                DataRow allCategoriesRow = categoryTable.NewRow();
                allCategoriesRow["CategoryID"] = DBNull.Value; // Или 0, если у вас есть категория с ID 0
                allCategoriesRow["CategoryName"] = "Все категории";
                categoryTable.Rows.InsertAt(allCategoriesRow, 0); // Добавляем в начало таблицы

                // Заполнение ComboBox категориями
                comboBoxCategories.DataSource = categoryTable;
                comboBoxCategories.DisplayMember = "CategoryName";
                comboBoxCategories.ValueMember = "CategoryID";
                comboBoxCategories.SelectedIndexChanged += ComboBoxCategories_SelectedIndexChanged; // Добавляем обработчик
            }
        }

        private void ComboBoxCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCategoryId = comboBoxCategories.SelectedValue?.ToString();
            LoadProducts(textBoxSearch.Text.Trim(), comboBoxSortByPrice.SelectedItem.ToString(), selectedCategoryId);
        }

        private void ComboBoxSortByPrice_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProducts(textBoxSearch.Text.Trim(), comboBoxSortByPrice.SelectedItem.ToString(), comboBoxCategories.SelectedValue?.ToString());
        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show($"Ошибка при отображении данных: {e.Exception.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            e.ThrowException = false;
        }

        private void LoadProducts(string filter = "", string sortOption = "Не сортировать", string categoryId = null)
        {
            using (MySqlConnection connection = new MySqlConnection(Connection.myConnection))
            {
                connection.Open();
                string query = @"SELECT p.ProductID, p.ProductName, p.Description, p.Price, p.StockQuantity, 
                         c.CategoryName, s.SuppliersName, Photo, SKU
                         FROM products p
                         JOIN categories c ON p.CategoryID = c.CategoryID
                         JOIN suppliers s ON p.SuppliersID = s.SuppliersID";

                bool hasFilter = false;

                if (!string.IsNullOrEmpty(filter))
                {
                    query += " WHERE p.ProductName LIKE @filter";
                    hasFilter = true;
                }

                if (!string.IsNullOrEmpty(categoryId))
                {
                    query += hasFilter ? " AND" : " WHERE";
                    query += " p.CategoryID = @categoryId";
                }

                // Добавляем пробел перед ORDER BY
                if (sortOption == "По возрастанию")
                {
                    query += " ORDER BY p.Price ASC"; // Добавлен пробел перед ORDER BY
                }
                else if (sortOption == "По убыванию")
                {
                    query += " ORDER BY p.Price DESC"; // Добавлен пробел перед ORDER BY
                }

                MySqlCommand command = new MySqlCommand(query, connection);
                if (!string.IsNullOrEmpty(filter))
                {
                    command.Parameters.AddWithValue("@filter", "%" + filter + "%");
                }
                if (!string.IsNullOrEmpty(categoryId))
                {
                    command.Parameters.AddWithValue("@categoryId", categoryId);
                }

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                sourceTable = new DataTable();
                adapter.Fill(sourceTable);

                // Проверка на наличие изображений
                foreach (DataRow row in sourceTable.Rows)
                {
                    if (row["Photo"] == DBNull.Value || !IsValidImage(row["Photo"]))
                    {
                        row["Photo"] = GetDefaultImage();
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
                    MessageBox.Show("Нет доступных продуктов для отображения.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            // Загрузите изображение по умолчанию, если это необходимо
            return null; // Замените на фактическое изображение по умолчанию
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
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            this.Visible = false;
            product.ShowDialog();
            this.Close();
        }

        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string selectedCategoryId = comboBoxCategories.SelectedValue?.ToString();
            LoadProducts(textBoxSearch.Text.Trim(), comboBoxSortByPrice.SelectedItem.ToString(), selectedCategoryId);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            this.Visible = false;
            admin.ShowDialog();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Обработка события клика по метке, если необходимо
        }
    }
}