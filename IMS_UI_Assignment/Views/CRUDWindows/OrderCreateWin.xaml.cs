using IMS_UI_Assignment.Controllers;
using IMS_UI_Assignment.Models;
using System.Windows;

namespace IMS_UI_Assignment.Views.CRUDWindows
{
    public partial class OrderCreateWin : Window
    {
        public Order Order { get; private set; }

        public OrderCreateWin()
        {
            InitializeComponent();
            Order = new Order();
            // Assuming OrderId is auto-generated or needs to be entered
        }

        public OrderCreateWin(Order existingOrder)
        {
            InitializeComponent();
            Order = existingOrder; // Use the direct reference for updates

            // Set existing values in TextBoxes
            OrderIdInput.InputTextBox.Text = Order._orderId.ToString();
            OrderNameInput.InputTextBox.Text = Order._orderName;
            productNameInput.InputTextBox.Text = Order._productName;
            supplierInput.InputTextBox.Text = Order._supplier;
            productQuantityInput.InputTextBox.Text = Order.Quantity.ToString();

            // Make OrderId read-only when updating
            OrderIdInput.InputTextBox.IsReadOnly = true;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // --- Unified Logic for both Creating and Updating ---

            // Basic validation within the window
            if (string.IsNullOrWhiteSpace(OrderNameInput.InputTextBox.Text) ||
                string.IsNullOrWhiteSpace(productNameInput.InputTextBox.Text) ||
                string.IsNullOrWhiteSpace(supplierInput.InputTextBox.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // Stop the save process
            }

            // Use TryParse for safety to avoid crashing if user types text in a number field
            if (!int.TryParse(OrderIdInput.InputTextBox.Text.Trim(), out int orderId) ||
                !int.TryParse(productQuantityInput.InputTextBox.Text.Trim(), out int quantity))
            {
                MessageBox.Show("Order ID and Quantity must be valid numbers.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Populate the public 'Order' property with the data from the text boxes
            Order._orderId = orderId;
            Order._orderName = OrderNameInput.InputTextBox.Text.Trim();
            Order._productName = productNameInput.InputTextBox.Text.Trim();
            Order._supplier = supplierInput.InputTextBox.Text.Trim();
            Order.Quantity = quantity;

            // Signal to the calling window that the user clicked "Save" and data is ready
            DialogResult = true;
            Close();
        }

        // You should have a Discard/Cancel button that simply closes the window
        private void Discard_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}