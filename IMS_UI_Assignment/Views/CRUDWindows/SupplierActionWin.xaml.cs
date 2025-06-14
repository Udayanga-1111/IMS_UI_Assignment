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
using System.Windows.Shapes;

namespace IMS_UI_Assignment.Views.CRUDWindows
{
    /// <summary>
    /// Interaction logic for SupplierActionWin.xaml
    /// </summary>
    public partial class SupplierActionWin : Window
    {
        public Supplier? Supplier { get; private set; }
        public SupplierActionWin()
        {
            InitializeComponent();
            Supplier = new Supplier();
        }
        public SupplierActionWin(Supplier existingSupplier)
        {
            InitializeComponent();
            Supplier = existingSupplier;
            supplierIdInput.InputTextBox.Text = Supplier.Id.ToString();
            firstNameInput.InputTextBox.Text = Supplier.FName;
            lastNameInput.InputTextBox.Text = Supplier.LName;
            supplierCenterInput.InputTextBox.Text = Supplier.SupplierCenter;
            productsInput.InputTextBox.Text = Supplier.Products;

            supplierIdInput.InputTextBox.IsReadOnly = true;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(firstNameInput.InputTextBox.Text) ||
                string.IsNullOrWhiteSpace(lastNameInput.InputTextBox.Text) ||
                string.IsNullOrWhiteSpace(supplierCenterInput.InputTextBox.Text) ||
                string.IsNullOrWhiteSpace(productsInput.InputTextBox.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(supplierIdInput.InputTextBox.Text.Trim(), out int supplierid))
            {

                MessageBox.Show("Supplier ID must be valid numbers.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Supplier.Id = supplierid;
            Supplier.FName = firstNameInput.InputTextBox.Text.Trim();
            Supplier.LName = lastNameInput.InputTextBox.Text.Trim();
            Supplier.SupplierCenter = supplierCenterInput.InputTextBox.Text.Trim();
            Supplier.Products = productsInput.InputTextBox.Text.Trim();

            DialogResult = true;
            Close();
        }

        private void Discard_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
