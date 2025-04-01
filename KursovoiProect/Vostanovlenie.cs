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
            using (MySqlConnection connection = new MySqlConnection(Connection.myConnection))
            {
                try
                {
                    connection.Open();

                    // SQL команды для восстановления структуры базы данных
                    string[] commands = new string[]
                    {
                "DROP TABLE IF EXISTS `categories`;",
                "CREATE TABLE `categories` ( `CategoryID` int NOT NULL AUTO_INCREMENT, `CategoryName` varchar(100) NOT NULL, `Description` text, PRIMARY KEY (`CategoryID`)) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;",
                "DROP TABLE IF EXISTS `customers`;",
                "CREATE TABLE `customers` ( `CustomerID` int NOT NULL AUTO_INCREMENT, `FirstName` varchar(100) NOT NULL, `LastName` varchar(100) NOT NULL, `Email` varchar(100) NOT NULL, `Phone` varchar(20) DEFAULT NULL, `Address` varchar(255) DEFAULT NULL, PRIMARY KEY (`CustomerID`), UNIQUE KEY `Email` (`Email`)) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;",
                "DROP TABLE IF EXISTS `employees`;",
                "CREATE TABLE `employees` ( `EmployeeID` int NOT NULL AUTO_INCREMENT, `Name` varchar(255) NOT NULL, `login` varchar(100) NOT NULL, `password` varchar(100) NOT NULL, `RollID` int NOT NULL, PRIMARY KEY (`EmployeeID`), KEY `1_idx` (`RollID`)) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;",
                "DROP TABLE IF EXISTS `roll`;",
                "CREATE TABLE `roll` ( `RollID` int NOT NULL AUTO_INCREMENT, `NameRoll` varchar(100) NOT NULL, PRIMARY KEY (`RollID`)) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;",
                "DROP TABLE IF EXISTS `suppliers`;",
                "CREATE TABLE `suppliers` ( `SuppliersID` int NOT NULL AUTO_INCREMENT, `SuppliersName` varchar(100) NOT NULL, `Email` varchar(255) DEFAULT NULL, `Phon` varchar(45) DEFAULT NULL, PRIMARY KEY (`SuppliersID`)) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;",
                "DROP TABLE IF EXISTS `products`;",
                "CREATE TABLE `products` ( `ProductID` int NOT NULL AUTO_INCREMENT, `ProductName` varchar(100) NOT NULL, `Description` text, `Price` decimal(10,2) NOT NULL, `StockQuantity` int NOT NULL, `CategoryID` int DEFAULT NULL, `SuppliersID` int DEFAULT NULL, `Photo` longblob, `SKU` varchar(50) DEFAULT NULL, PRIMARY KEY (`ProductID`), UNIQUE KEY `SKU` (`SKU`)) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;",
                "DROP TABLE IF EXISTS `orders`;",
                "CREATE TABLE `orders` ( `OrderID` int NOT NULL AUTO_INCREMENT, `CustomerID` int NOT NULL, `EmployeesID` int NOT NULL, `OrderDate` datetime DEFAULT CURRENT_TIMESTAMP, `TotalAmount` decimal(10,2) DEFAULT NULL, `Status` varchar(50) DEFAULT NULL, PRIMARY KEY (`OrderID`), KEY `CustomerID` (`CustomerID`), KEY `orders_ibfk_2_idx` (`EmployeesID`)) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;",
                "DROP TABLE IF EXISTS `orderitems`;",
                "CREATE TABLE `orderitems` ( `OrderItemID` int NOT NULL AUTO_INCREMENT, `OrderID` int DEFAULT NULL, `ProductID` int DEFAULT NULL, `Quantity` int NOT NULL, `Price` decimal(10,2) NOT NULL, PRIMARY KEY (`OrderItemID`), KEY `OrderID` (`OrderID`), KEY `ProductID` (`ProductID`)) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;"
                    };

                    // Выполнение каждой команды
                    foreach (string command in commands)
                    {
                        using (MySqlCommand cmd = new MySqlCommand(command, connection))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Структура базы данных успешно восстановлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void button3_Click(object sender, EventArgs e)
        {
            Avtoriz Avtoriz = new Avtoriz();
            this.Visible = false;
            Avtoriz.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    try
                    {
                        using (MySqlConnection connection = new MySqlConnection(Connection.myConnection))
                        {
                            connection.Open();
                            string[] csvLines = System.IO.File.ReadAllLines(filePath);

                            // Получаем заголовки из первой строки
                            string[] headers = csvLines[0].Split(',');

                            // Начинаем с 1 строки, чтобы пропустить заголовки
                            for (int i = 1; i < csvLines.Length; i++)
                            {
                                string line = csvLines[i];
                                string[] columns = line.Split(',');

                                // Создание списка параметров
                                var parameters = new List<string>();
                                var commandParams = new List<MySqlParameter>();

                                // Динамически создаем параметры и значения
                                for (int j = 0; j < headers.Length; j++)
                                {
                                    string paramName = $"@param{j}"; // можно использовать более осмысленные имена
                                    parameters.Add(paramName);
                                    commandParams.Add(new MySqlParameter(paramName, columns[j]));
                                }

                                // Создаем запрос
                                string query = $"INSERT INTO your_table_name ({string.Join(", ", headers)}) VALUES ({string.Join(", ", parameters)})";

                                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                                {
                                    cmd.Parameters.AddRange(commandParams.ToArray());
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        MessageBox.Show("Данные успешно импортированы!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Ошибка импорта данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Общая ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
