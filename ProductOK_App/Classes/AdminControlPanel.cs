using ProductOK_App;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

public class AdminControlPanel
{
    public string _login { get; private set; }
    public string _password { get; private set; }
    private int _remainingAttempts = 3;
    private readonly ProductDbConnection _dbConnection;

    public AdminControlPanel()
    {
        _login = "aamartynov21";
        _password = "QAZwsx-123";
        _dbConnection = new ProductDbConnection();
    }

    public void UserEnter(TextBox textLogin, TextBox textPassword, TabControl tabControl)
    {

        var login = textLogin.Text.Trim().ToLower();
        var password = textPassword.Text.Trim();

        if (login == _login && password == _password)
        {
            MessageBox.Show("Добро пожаловать в систему!");
            tabControl.SelectedIndex = 6;
            _remainingAttempts = 3;
        }
        else
        {
            _remainingAttempts--;

            if (_remainingAttempts <= 0)
            {
                MessageBox.Show("Учетная запись заблокирована");
                tabControl.SelectedIndex = 0;
                _remainingAttempts = 3;
            }
            else
            {
                MessageBox.Show($"Неверный логин или пароль\nОсталось {_remainingAttempts} попыток");
            }
        }
    }
    public void PrintAllProduct(DataGridView dataGridView)
    {
        DataGridViewManager _GridManager = new DataGridViewManager();
        ProductDbConnection productDbConnection = new ProductDbConnection();
        try
        {
            using (var connection = productDbConnection.GetConnection())
            {
                connection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Products", connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                dataGridView.DataSource = dataSet.Tables[0];
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
        }
    }
    public void AddProductinDB(TextBox textbox3, TextBox textbox4, TextBox textbox5)
    {
        // Указываем CultureInfo с точкой как разделителем дробной части
        if (!decimal.TryParse(textbox5.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price))
        {
            MessageBox.Show("Некорректная цена! Используйте точку или запятую как разделитель (например: 135.55 или 135,55)");
            return;
        }

        // Проверяем, что цена положительная
        if (price <= 0)
        {
            MessageBox.Show("Цена должна быть больше нуля!");
            return;
        }

        try
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();

                // Получаем максимальный ID
                int newId;
                using (var cmdGetMaxId = new SqlCommand("SELECT ISNULL(MAX(ID), 0) + 1 FROM Products", connection))
                {
                    newId = (int)cmdGetMaxId.ExecuteScalar();
                }

                // Вставляем запись с новым ID
                string query = "INSERT INTO Products (ID, Category, ProductName, Price) " +
                              "VALUES (@ID, @Category, @ProductName, @Price)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", newId);
                    command.Parameters.AddWithValue("@Category", textbox3.Text.Trim());
                    command.Parameters.AddWithValue("@ProductName", textbox4.Text.Trim());
                    command.Parameters.AddWithValue("@Price", price);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Товар успешно добавлен!");
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка: {ex.Message}");
        }
    }
    public void DeleteProductById(int productId)
    {
        try
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();

                // Запрос на удаление товара с указанным ID
                string query = "DELETE FROM Products WHERE ID = @ProductId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Товар успешно удален!");
                    }
                    else
                    {
                        MessageBox.Show("Товар с указанным ID не найден.");
                    }
                }
            }
        }
        catch (SqlException sqlEx)
        {
            MessageBox.Show($"Ошибка базы данных: {sqlEx.Message}");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Неожиданная ошибка: {ex.Message}");
        }
    }
}