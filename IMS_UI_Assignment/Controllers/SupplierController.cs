using IMS_UI_Assignment.Models;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Windows;

namespace IMS_UI_Assignment.Controllers
{
    public class SupplierController
    {
        private readonly string _connectionString = "Data Source=LAPTOP-J2V071AF\\SQLEXPRESS;Initial Catalog=IMS_GameStore;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        public ObservableCollection<Supplier> GetAllSuppliers()
        {
            var supplierList = new ObservableCollection<Supplier>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT [Supplier Id], [First Name], [Last Name], [Supplier Center], Products FROM SupplierTable";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
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
                    supplierList.Add(supplier);
                }
            }
            return supplierList;
        }

        public void CreateSupplier(Supplier supplier)
        {
            if (supplier == null || string.IsNullOrWhiteSpace(supplier.FName) || string.IsNullOrWhiteSpace(supplier.LName) || string.IsNullOrWhiteSpace(supplier.SupplierCenter) || string.IsNullOrWhiteSpace(supplier.Products))
            {
                MessageBox.Show("Supplier details are incomplete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO SupplierTable ([Supplier Id], [First Name], [Last Name], [Supplier Center], Products) VALUES (@Id, @FirstName, @LastName, @SupplierCenter, @Products)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", supplier.Id);
                cmd.Parameters.AddWithValue("@FirstName", supplier.FName);
                cmd.Parameters.AddWithValue("@LastName", supplier.LName);
                cmd.Parameters.AddWithValue("@SupplierCenter", supplier.SupplierCenter);
                cmd.Parameters.AddWithValue("@Products", supplier.Products);
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Supplier Added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void UpdateSupplier(Supplier supplier)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            string query = "UPDATE SupplierTable SET [First Name] = @FName, [Last Name] = @LName, [Supplier Center]= @SupplierCenter, Products = @Products WHERE [Supplier Id] = @Id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", supplier.Id);
            cmd.Parameters.AddWithValue("@FName", supplier.FName);
            cmd.Parameters.AddWithValue("@LName", supplier.LName);
            cmd.Parameters.AddWithValue("@UserName", supplier.SupplierCenter);
            cmd.Parameters.AddWithValue("@Password", supplier.Products);
            cmd.ExecuteNonQuery();
        }


        public void DeleteUser(int supplierId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM SupplierTable WHERE [Supplier Id] = @supplierId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@supplierId", supplierId);
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Supplier deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
