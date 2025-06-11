using IMS_UI_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IMS_UI_Assignment.Views
{
    /// <summary>
    /// Interaction logic for SubPage_Suppliers.xaml
    /// </summary>
    public partial class SubPage_Suppliers : Page
    {
        public SubPage_Suppliers()
        {
            InitializeComponent();
        }

        private void addSupplierBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void editSupplierBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteSupplierBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (string.Equals(GlobalVariables.UserRole?.Trim(), "Cashier", StringComparison.OrdinalIgnoreCase))
            {
                SuppliersActionStack.Visibility = Visibility.Collapsed;
            }
        }
    }
}
