using IMS_UI_Assignment.Models;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Windows;

namespace IMS_UI_Assignment.Controllers
{
    public class ProductController
    {
        private readonly string _connectionString = "Data Source=LAPTOP-J2V071AF\\SQLEXPRESS;Initial Catalog=IMS_GameStore;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        public ObservableCollection<Product> GetAllProducts()
        {
            var productslist = new ObservableCollection<Product>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM ProductsTable";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

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
                    productslist.Add(product);
                }
            }
            return productslist;
        }

        public void CreateProduct(Product product)
        {
            if (product == null || string.IsNullOrWhiteSpace(product.productName) || string.IsNullOrWhiteSpace(product.productCategory))
            {
                MessageBox.Show("Product details are incomplete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO ProductsTable ([Product Id], [Product Name], Category, [Unit Price], Quantity) VALUES (@productId, @productName, @category, @price, @Quantity)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@productId", product.productId);
                cmd.Parameters.AddWithValue("@productName", product.productName);
                cmd.Parameters.AddWithValue("@category", product.productCategory);
                cmd.Parameters.AddWithValue("@price", product.productPrice);
                cmd.Parameters.AddWithValue("@Quantity", product.quantity);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Product created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void UpdateProduct(Product product)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE ProductsTable SET [Product Name] = @productName, Category = @category, [Unit Price] = @price, Quantity = @Quantity  WHERE [Product Id] = @productId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@productId", product.productId);
                cmd.Parameters.AddWithValue("@productName", product.productName);
                cmd.Parameters.AddWithValue("@category", product.productCategory);
                cmd.Parameters.AddWithValue("@price", product.productPrice);
                cmd.Parameters.AddWithValue("@Quantity", product.quantity);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteProduct(int productId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM ProductsTable WHERE [Product Id] = @productId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@productId", productId);
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("product deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
