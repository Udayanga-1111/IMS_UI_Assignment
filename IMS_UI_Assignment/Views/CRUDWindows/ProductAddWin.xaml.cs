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
using IMS_UI_Assignment.Models;

namespace IMS_UI_Assignment.Views.CRUDWindows
{
    /// <summary>
    /// Interaction logic for ProductAddWin.xaml
    /// </summary>
    public partial class ProductAddWin : Window
    {
        public Product Product { get; private set; }
        public ProductAddWin()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Product = new Product();
        }
        public ProductAddWin(Product existingProduct)
        {
            InitializeComponent();
            Product = existingProduct;

            productIdInput.InputTextBox.Text = Product.productId.ToString();
            productNameInput.InputTextBox.Text = Product.productName;
            productCategoryInput.InputTextBox.Text = Product.productCategory;
            productPriceInput.InputTextBox.Text = Product.productPrice.ToString();
            productQuantityInput.InputTextBox.Text = Product.quantity.ToString();

            productIdInput.InputTextBox.IsReadOnly = true;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(productNameInput.InputTextBox.Text) ||
                string.IsNullOrWhiteSpace(productCategoryInput.InputTextBox.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // Stop the save process
            }


            if (!int.TryParse(productIdInput.InputTextBox.Text.Trim(), out int productId) ||
                !int.TryParse(productQuantityInput.InputTextBox.Text.Trim(), out int quantity) ||
                !decimal.TryParse(productPriceInput.InputTextBox.Text.Trim(), out decimal price))
            {
                MessageBox.Show("Product ID, Price and Quantity must be valid numbers.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Product.productId = productId;
            Product.productName = productNameInput.InputTextBox.Text.Trim();
            Product.productCategory = productCategoryInput.InputTextBox.Text.Trim();
            Product.productPrice = price;
            Product.quantity = quantity;

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
