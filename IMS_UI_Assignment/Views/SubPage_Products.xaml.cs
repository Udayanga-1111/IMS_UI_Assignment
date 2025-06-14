using IMS_UI_Assignment.Controllers;
using IMS_UI_Assignment.Models;
using IMS_UI_Assignment.Views.CRUDWindows;
using System.Windows;
using System.Windows.Controls;

namespace IMS_UI_Assignment.Views
{
    public partial class SubPage_Products : Page
    {
        private ProductController productController;
        private Product selectedProduct;

        public SubPage_Products()
        {
            InitializeComponent();
            productController = new ProductController();
            LoadProducts();
        }

        private void LoadProducts()
        {
            var productList = productController.GetAllProducts();
            ProductDataGrid.ItemsSource = productList;
        }

        private void addProductBtn_ButtonClicked(object sender, EventArgs e)
        {
            ProductAddWin productAddWin = new ProductAddWin();

            if (productAddWin.ShowDialog() == true)
            {
                Product newProduct = productAddWin.Product;

                productController.CreateProduct(newProduct);
                LoadProducts();
            }
        }

        private void editProductBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedProduct == null)
            {
                MessageBox.Show("Please select a product to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Pass a copy of the selected order to avoid modifying the original if the user cancels
            var productToEdit = new Product
            {
                productId = selectedProduct.productId,
                productName = selectedProduct.productName,
                productCategory = selectedProduct.productCategory,
                productPrice = selectedProduct.productPrice,
                quantity = selectedProduct.quantity
            };

            var editWindow = new ProductAddWin(productToEdit);
            editWindow.productIdInput.InputTextBox.IsReadOnly = true;
            if (editWindow.ShowDialog() == true)
            {
                productController.UpdateProduct(editWindow.Product);
                LoadProducts();
            }
        }

        private void deleteProductBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedProduct == null)
            {
                MessageBox.Show("Please select a product to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            // Confirm deletion
            var result = MessageBox.Show($"Are you sure you want to delete the product '{selectedProduct.productName}'?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                productController.DeleteProduct(selectedProduct.productId);
                LoadProducts();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (string.Equals(GlobalVariables.UserRole?.Trim(), "Cashier", StringComparison.OrdinalIgnoreCase))
            {
                ProductActionStack.Visibility = Visibility.Collapsed;
            }
        }

        private void ProductDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedProduct = ProductDataGrid.SelectedItem as Product;
        }


    }
}
