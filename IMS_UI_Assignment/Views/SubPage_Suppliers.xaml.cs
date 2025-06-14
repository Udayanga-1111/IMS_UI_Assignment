using IMS_UI_Assignment.Controllers;
using IMS_UI_Assignment.Models;
using IMS_UI_Assignment.Views.CRUDWindows;
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
        private SupplierController supplierController;
        private Supplier? SelectedSupplier;
        public SubPage_Suppliers()
        {
            InitializeComponent();
            supplierController = new SupplierController();
            LoadSuppliers();
        }
        public void LoadSuppliers()
        {
            var suppliers = supplierController.GetAllSuppliers();
            if (suppliers != null && suppliers.Count > 0)
            {
                SupplierDataGrid.ItemsSource = suppliers;
            }
            else
            {
                MessageBox.Show("No suppliers found.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void addSupplierBtn_Click(object sender, RoutedEventArgs e)
        {
            SupplierActionWin supplierActionWin = new SupplierActionWin();
            if (supplierActionWin.ShowDialog() == true)
            {
                Supplier? newSupplier = supplierActionWin.Supplier;
                if (newSupplier != null)
                {
                    supplierController.CreateSupplier(newSupplier);
                    LoadSuppliers();
                }
                else
                {
                    MessageBox.Show("Failed to create supplier. Supplier data is incomplete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void editSupplierBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedSupplier == null)
            {
                MessageBox.Show("Please select a supplier to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var supplierToEdit = new Supplier
            {
                Id = SelectedSupplier.Id,
                FName = SelectedSupplier.FName,
                LName = SelectedSupplier.LName,
                SupplierCenter = SelectedSupplier.SupplierCenter,
                Products = SelectedSupplier.Products
            };

            var editActionWin = new SupplierActionWin(supplierToEdit);
            editActionWin.supplierIdInput.InputTextBox.IsReadOnly = true;
            if (editActionWin.ShowDialog() == true)
            {
                if (editActionWin.Supplier != null)
                {
                    supplierController.UpdateSupplier(editActionWin.Supplier);
                    LoadSuppliers();
                }
            }
        }

        private void deleteSupplierBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedSupplier == null)
            {
                MessageBox.Show("Please select a supplier to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete Supplier {SelectedSupplier.FName} {SelectedSupplier.LName}?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                supplierController.DeleteUser(SelectedSupplier.Id);
                LoadSuppliers();
            }
        }

        private void supplierDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            SelectedSupplier = SupplierDataGrid.SelectedItem as Supplier;
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
