using IMS_UI_Assignment.Models;
using System.Windows;
using System.Windows.Controls;

namespace IMS_UI_Assignment.Views
{
    public partial class SubPage_Products : Page
    {

        public SubPage_Products()
        {
            InitializeComponent();
        }

        private void addProductBtn_ButtonClicked(object sender, EventArgs e)
        {
        }

        private void editProductBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteProductBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (string.Equals(GlobalVariables.UserRole?.Trim(), "Cashier", StringComparison.OrdinalIgnoreCase))
            {
                ProductActionStack.Visibility = Visibility.Collapsed;
            }
        }
    }
}
