using IMS_UI_Assignment.Models;
using System.Collections.ObjectModel;
using Microsoft.Data.SqlClient;
using System.Windows;

namespace IMS_UI_Assignment.Controlers
{
    public class OrderControler
    {
        private readonly string _connectionString = "Data Source=LAPTOP-J2V071AF\\SQLEXPRESS;Initial Catalog=IMS_GameStore;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        public ObservableCollection<Order> GetAllOrders()
        {
            var orderslist = new ObservableCollection<Order>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                // It's safer to parse integers from the reader directly, not from strings
                string query = "SELECT orderId, orderName, productName, supplier, Quantity FROM OrderTable";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var order = new Order
                    {
                        // Assuming the columns are in this order and of the correct type
                        _orderId = int.Parse(reader.GetString(0).Trim()),
                        _orderName = reader.GetString(1).Trim(),
                        _productName = reader.GetString(2).Trim(),
                        _supplier = reader.GetString(3).Trim(),
                        Quantity = int.Parse(reader.GetString(4).Trim()),
                    };
                    orderslist.Add(order);
                }
            }
            return orderslist;
        }

        // CORRECTED: The method now accepts an Order object
        public void CreateOrder(Order order)
        {
            // Input validation happens before this method is called,
            // or you can add it here by checking the passed object.
            if (order == null || string.IsNullOrWhiteSpace(order._orderName) || string.IsNullOrWhiteSpace(order._productName) || string.IsNullOrWhiteSpace(order._supplier))
            {
                MessageBox.Show("Order details are incomplete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO OrderTable (orderId, orderName, productName, supplier, Quantity) VALUES (@OrderId, @OrderName, @ProductName, @Supplier, @Quantity)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OrderId", order._orderId);
                cmd.Parameters.AddWithValue("@OrderName", order._orderName);
                cmd.Parameters.AddWithValue("@ProductName", order._productName);
                cmd.Parameters.AddWithValue("@Supplier", order._supplier);
                cmd.Parameters.AddWithValue("@Quantity", order.Quantity);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Order created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void UpdateOrder(Order order)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE OrderTable SET orderName = @OrderName, productName = @productName, supplier = @Supplier, Quantity = @Quantity WHERE orderId = @OrderId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OrderId", order._orderId);
                cmd.Parameters.AddWithValue("@OrderName", order._orderName);
                cmd.Parameters.AddWithValue("@ProductName", order._productName);
                cmd.Parameters.AddWithValue("@Supplier", order._supplier);
                cmd.Parameters.AddWithValue("@Quantity", order.Quantity);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteOrder(int orderId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM OrderTable WHERE orderId = @OrderId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OrderId", orderId);
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Order deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}