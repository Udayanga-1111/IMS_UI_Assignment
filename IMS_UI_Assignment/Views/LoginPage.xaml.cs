using Microsoft.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using IMS_UI_Assignment.Models;

namespace IMS_UI_Assignment.Views
{
    public partial class LoginPage : Page
    {
        private string? userRole;
        private string? F_Name;

        public LoginPage()
        {
            InitializeComponent();
            logoImg.Source = new BitmapImage(new Uri("C:\\Users\\dilsh\\source\\repos\\Udayanga-1111\\IMS_UI_Assignment\\IMS_UI_Assignment\\Images\\download (13).png", UriKind.RelativeOrAbsolute));
        }

        private void Page_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginBtn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }


        // Login Oparation : Where the User Role is retrieved from the database after successful login
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = UserInput.InputTextBox.Text.Trim();
            string password = PasswordInput.InputPasswordBox.Password.Trim();

            if (AuthenticateUser(username, password))
            {
                string connectionString = "Data Source=LAPTOP-J2V071AF\\SQLEXPRESS;Initial Catalog=IMS_GameStore;Integrated Security=True;Trust Server Certificate=True";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "SELECT UserRole, FirstName FROM Users WHERE Username = @Username AND Password = @Password";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Username", username);
                            cmd.Parameters.AddWithValue("@Password", password);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read()) // Check if a row is found
                                {
                                    userRole = reader["UserRole"].ToString(); // Get UserRole
                                    F_Name = reader["FirstName"].ToString(); // Get First Name

                                    MessageBox.Show($"Login successful! Role: {userRole}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                                    // Navigate to main page after successful login
                                    if (System.Windows.Application.Current.MainWindow is MainWindow mainWin)
                                    {
                                        MainPage mainPage = new MainPage();
                                        mainPage.welcomeMssg.Text = F_Name?.Trim(); // Set welcome message
                                        if (!string.IsNullOrEmpty(userRole))
                                        {
                                            GlobalVariables.UserRole = userRole;
                                            mainWin.MainWinFrame.Navigate(mainPage);
                                        }
                                        else
                                        {
                                            MessageBox.Show("User role is not defined!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Navigation service is not available!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Invalid username or password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

            }
            else
            {
                MessageBox.Show("Invalid username or password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // Method to authenticate user credentials against the database
        private bool AuthenticateUser(string username, string password)
        {
            string connectionString = "Data Source=LAPTOP-J2V071AF\\SQLEXPRESS;Initial Catalog=IMS_GameStore;Integrated Security=True;Trust Server Certificate=True";
            bool isValidUser = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        isValidUser = count > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return isValidUser;
        }
    }
}
