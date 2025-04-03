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

        private int currentPage = 1;
        private const int pageSize = 20;
        private int totalRecords;
        private int totalPages;

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
            InitializePagination(); // Инициализация пагинации
        }

        private void InitializeSortComboBox()
        {
            comboBoxSortByPrice.Items.Add("Не сортировать");
            comboBoxSortByPrice.Items.Add("По возрастанию");
            comboBoxSortByPrice.Items.Add("По убыванию");
            comboBoxSortByPrice.SelectedIndex = 0; // Установить по умолчанию на "Не сортировать"
            comboBoxSortByPrice.SelectedIndexChanged += ComboBoxSortByPrice_SelectedIndexChanged;
        }

        private void InitializePagination()
        {
            // Создаем кнопки для пагинации в зависимости от количества страниц
            for (int i = 1; i <= totalPages; i++)
            {
                Button buttonPage = new Button();
                buttonPage.Text = i.ToString();
                buttonPage.Tag = i; // Для обработки номера страницы
                buttonPage.Click += ButtonPage_Click;
                flowLayoutPanelPagination.Controls.Add(buttonPage); // Используйте FlowLayoutPanel для кнопок
            }
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
            LoadProducts(textBoxSearch.Text.Trim(), comboBoxSortByPrice.SelectedItem.ToString(), comboBoxCategories.SelectedValue?.ToString());
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
                string query = @"SELECT COUNT(*) FROM products p
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

                MySqlCommand countCommand = new MySqlCommand(query, connection);
                if (!string.IsNullOrEmpty(filter))
                {
                    countCommand.Parameters.AddWithValue("@filter", "%" + filter + "%");
                }
                if (!string.IsNullOrEmpty(categoryId))
                {
                    countCommand.Parameters.AddWithValue("@categoryId", categoryId);
                }

                totalRecords = Convert.ToInt32(countCommand.ExecuteScalar());
                totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

                query = @"SELECT p.ProductID, p.ProductName, p.Description, p.Price, p.StockQuantity, 
                                 c.CategoryName, s.SuppliersName, Photo, SKU
                          FROM products p
                          JOIN categories c ON p.CategoryID = c.CategoryID
                          JOIN suppliers s ON p.SuppliersID = s.SuppliersID";

                if (hasFilter || !string.IsNullOrEmpty(categoryId))
                {
                    query += hasFilter ? " AND" : " WHERE";
                    query += " p.ProductName LIKE @filter";
                    if (!string.IsNullOrEmpty(categoryId))
                    {
                        query += " AND p.CategoryID = @categoryId";
                    }
                }

                query += " LIMIT @offset, @limit";

                MySqlCommand command = new MySqlCommand(query, connection);
                if (hasFilter)
                    command.Parameters.AddWithValue("@filter", "%" + filter + "%");
                if (!string.IsNullOrEmpty(categoryId))
                    command.Parameters.AddWithValue("@categoryId", categoryId);
                command.Parameters.AddWithValue("@offset", (currentPage - 1) * pageSize);
                command.Parameters.AddWithValue("@limit", pageSize);

                sourceTable = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
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

                UpdatePagination();
            }
        }

        private void UpdatePagination()
        {
            flowLayoutPanelPagination.Controls.Clear(); // Очищаем старые кнопки

            // Создаем новые кнопки для пагинации
            for (int i = 1; i <= totalPages; i++)
            {
                Button buttonPage = new Button();
                buttonPage.Text = i.ToString();
                buttonPage.Tag = i; // Для обработки номера страницы
                buttonPage.Click += ButtonPage_Click;
                flowLayoutPanelPagination.Controls.Add(buttonPage);
                labelCurrentPage.Text = $"Страница {currentPage} из {totalPages}";
            }
        }

        private void ButtonPage_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                int page = Convert.ToInt32(button.Tag);
                if (page != currentPage)
                {
                    currentPage = page;
                    LoadProducts(textBoxSearch.Text.Trim(), comboBoxSortByPrice.SelectedItem.ToString(), comboBoxCategories.SelectedValue?.ToString());
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
            LoadProducts(textBoxSearch.Text.Trim(), comboBoxSortByPrice.SelectedItem.ToString(), comboBoxCategories.SelectedValue?.ToString());
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

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadProducts(textBoxSearch.Text.Trim(), comboBoxSortByPrice.SelectedItem.ToString(), comboBoxCategories.SelectedValue?.ToString());
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadProducts(textBoxSearch.Text.Trim(), comboBoxSortByPrice.SelectedItem.ToString(), comboBoxCategories.SelectedValue?.ToString());
            }
        }
    }
}