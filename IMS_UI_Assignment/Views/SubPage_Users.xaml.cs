using System.Windows;
using System.Windows.Controls;

namespace IMS_UI_Assignment.Views
{
    /// <summary>
    /// Interaction logic for SubPage_Users.xaml
    /// </summary>
    public partial class SubPage_Users : Page
    {
        public SubPage_Users()
        {
            InitializeComponent();
        }

        private void addUserBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void editUserBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteUserBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            editUserBtn.IsEnabled = UsersDataGrid.SelectedItem != null;
        }
    }
}
