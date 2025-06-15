using IMS_UI_Assignment.Models;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;

namespace IMS_UI_Assignment.Controllers
{
    public class DashboardController
    {
        private readonly string _connectionString = "Data Source=LAPTOP-J2V071AF\\SQLEXPRESS;Initial Catalog=IMS_GameStore;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        public ObservableCollection<Product> GetAllLowStocks()
        {
            var productslist = new ObservableCollection<Product>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT [Product Id], [Product Name], Quantity FROM ProductsTable WHERE Quantity < 10";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var product = new Product
                    {
                        productId = reader.GetInt32(0),
                        productName = reader.GetString(1).Trim(),
                        quantity = reader.GetInt32(2)
                    };
                    productslist.Add(product);
                }
            }
            return productslist;
        }
    }
}
