using IMS_UI_Assignment.Models;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Windows;

namespace IMS_UI_Assignment.Controllers
{
    public class UserController
    {
        private readonly string _connectionString = "Data Source=LAPTOP-J2V071AF\\SQLEXPRESS;Initial Catalog=IMS_GameStore;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        public ObservableCollection<UserModel> GetAllUsers()
        {
            var usersList = new ObservableCollection<UserModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Users";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var user = new UserModel
                    {
                        Id = int.Parse(reader.GetString(0).Trim()),
                        FName = reader.GetString(reader.GetOrdinal("FirstName")).Trim(),
                        LName = reader.GetString(reader.GetOrdinal("LastName")).Trim(),
                        UserName = reader.GetString(reader.GetOrdinal("Username")).Trim(),
                        Password = reader.GetString(reader.GetOrdinal("Password")).Trim(),
                        UserRole = reader.GetString(reader.GetOrdinal("UserRole")).Trim()

                    };
                    usersList.Add(user);
                }
            }
            return usersList;
        }

        public void CreateUser(UserModel user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.FName) || string.IsNullOrWhiteSpace(user.LName) || string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Password) || string.IsNullOrWhiteSpace(user.UserRole))
            {
                MessageBox.Show("Order details are incomplete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Users (Id, FirstName, LastName, UserName, Password, UserRole) VALUES (@Id, @FirstName, @LastName, @UserName, @Password, @UserRole)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", user.Id);
                cmd.Parameters.AddWithValue("@FirstName", user.FName);
                cmd.Parameters.AddWithValue("@LastName", user.LName);
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@UserRole", user.UserRole);
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Order created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void UpdateUser(UserModel user) 
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            string query = "UPDATE Users SET FirstName = @FName, LastName = @LName, Username = @UserName, Password = @Password, UserRole = @UserRole WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", user.Id);
            cmd.Parameters.AddWithValue("@FName", user.FName);
            cmd.Parameters.AddWithValue("@LName", user.LName);
            cmd.Parameters.AddWithValue("@UserName", user.UserName);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@UserRole", user.UserRole);
            cmd.ExecuteNonQuery();
        }

        public void DeleteUser(int userId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Users WHERE Id = @userId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("User deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

}
