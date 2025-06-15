using IMS_UI_Assignment.Models;
using Microsoft.Data.SqlClient;
using System.Windows;

namespace IMS_UI_Assignment.Services
{
    public class ReportService
    {

        private readonly string _connectionString;
        public ReportService()
        {
            _connectionString = "Data Source=LAPTOP-J2V071AF\\SQLEXPRESS;Initial Catalog=IMS_GameStore;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        }
        public List<Product> GetProductsReport()
        {
            var products = new List<Product>();
            // The 'using' statement ensures the connection is closed even if an error occurs.
            using (var connection = new SqlConnection(_connectionString))
            {
                // NOTE: Change 'ProductsTable' if your table has a different name
                string query = "SELECT * FROM ProductsTable";
                var command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var product = new Product
                            {
                                productId = reader.GetInt32(0),
                                productName = reader.GetString(1).Trim(),
                                productCategory = reader.GetString(2).Trim(),
                                productPrice = reader.GetDecimal(3),
                                quantity = reader.GetInt32(4)
                            };
                            products.Add(product);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return products;
        }

        public List<Supplier> GetSuppliersReport()
        {
            var suppliers = new List<Supplier>();
            using (var connection = new SqlConnection(_connectionString))
            {
                // NOTE: Change 'SupplierTable' if your table has a different name
                string query = "SELECT [Supplier Id], [First Name], [Last Name], [Supplier Center], Products FROM SupplierTable";
                var command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var supplier = new Supplier
                            {
                                Id = int.Parse(reader.GetString(0).Trim()),
                                FName = reader.GetString(1).Trim(),
                                LName = reader.GetString(2).Trim(),
                                SupplierCenter = reader.GetString(3).Trim(),
                                Products = reader.GetString(4).Trim()
                            };
                            suppliers.Add(supplier);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return suppliers;
        }

        public List<Product> GetLowStockReport(int lowStockThreshold = 10)
        {
            var products = new List<Product>();
            using (var connection = new SqlConnection(_connectionString))
            {
                // Use a parameterized query to prevent SQL injection
                string query = "SELECT [Product Id], [Product Name], Quantity FROM ProductsTable WHERE Quantity <= @Threshold";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Threshold", lowStockThreshold);

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var product = new Product
                            {
                                productId = reader.GetInt32(0),
                                productName = reader.GetString(1).Trim(),
                                quantity = reader.GetInt32(2)
                            };
                            products.Add(product);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return products;
        }
    }
   
 }
