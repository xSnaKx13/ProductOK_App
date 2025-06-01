using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductOK_App
{
    public class ProductRepository
    {
        private readonly ProductDbConnection _dbConnection;
        public ProductRepository()
        {
            _dbConnection = new ProductDbConnection();
        }
        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT Id, Category, ProductName, Price FROM Products";

                using(var command = new SqlCommand(query, connection)) 
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            Id = (int)reader["Id"],
                            Category = reader["Category"].ToString(),
                            ProductName = reader["ProductName"].ToString(),
                            Price = (decimal)reader["Price"]
                        });
                    }
                }
                connection.Close();
            }
            return products;
        }
    }
}
