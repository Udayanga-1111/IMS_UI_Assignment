using IMS_UI_Assignment.Models;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace IMS_UI_Assignment.Views
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            myImage.Source = new BitmapImage(new Uri("C:\\Users\\dilsh\\source\\repos\\Udayanga-1111\\IMS_UI_Assignment\\IMS_UI_Assignment\\Images\\download (13).png", UriKind.RelativeOrAbsolute));
            Main.Navigate(new SubPage_dashboard());
        }


        // Logout Function
        private void Logout_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to continue?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if (System.Windows.Application.Current.MainWindow is MainWindow mainWin) // Corrected namespace
                {
                    mainWin.MainWinFrame.Navigate(new LoginPage());
                }
                else
                {
                    MessageBox.Show("Navigation service is not available!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        // Method to navigate Dashboard
        private void dashboardBtn_Click(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new SubPage_dashboard());
        }

        // Method to navigate Users Page
        private void usersBtn_Click(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new SubPage_Users());
        }

        // Method to navigate Products Page
        private void productsBtn_Click(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new SubPage_Products());
        }

        // Method to navigate Suppliers Page
        private void suppliersBtn_Click(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new SubPage_Suppliers());
        }

        // Method to navigate Reports Page
        private void reportsBtn_Click(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new SubPage_Reports());
        }

        // Method to navigate Orders page
        private void OrdersBtn_Click(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new SubPage_RestockingOrders());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (string.Equals(GlobalVariables.UserRole?.Trim(), "Manager", StringComparison.OrdinalIgnoreCase))
            {
                usersBtn.Visibility = Visibility.Collapsed;
            }
            else if(string.Equals(GlobalVariables.UserRole?.Trim(), "Cashier", StringComparison.OrdinalIgnoreCase))
            {
                usersBtn.Visibility = Visibility.Collapsed;
                reportsBtn.Visibility = Visibility.Collapsed;

            }
        }
    }
}
