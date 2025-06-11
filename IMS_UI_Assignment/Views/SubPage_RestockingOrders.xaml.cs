using IMS_UI_Assignment.Controlers;
using IMS_UI_Assignment.Models;
using IMS_UI_Assignment.Views.CRUDWindows;
using System.Windows;
using System.Windows.Controls;

namespace IMS_UI_Assignment.Views
{
    public partial class SubPage_RestockingOrders : Page
    {
        private OrderControler orderControler;
        private Order selectedOrder;

        public SubPage_RestockingOrders()
        {
            InitializeComponent();
            orderControler = new OrderControler();
            LoadOrders();
        }

        private void LoadOrders()
        {
            var orderList = orderControler.GetAllOrders();
            OrderDataGrid.ItemsSource = orderList;
        }

        private void addOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            // No need for GlobalVariables here. The context is clear.
            OrderCreateWin orderCreateWin = new OrderCreateWin();

            // Show the window and wait for it to close.
            // If it closed with DialogResult = true, it means the user clicked "Save".
            if (orderCreateWin.ShowDialog() == true)
            {
                // Get the new order object from the window's public property
                Order newOrder = orderCreateWin.Order;

                // Tell the controller to save it
                orderControler.CreateOrder(newOrder);

                // Refresh the grid to show the new data
                LoadOrders();
            }
        }

        private void OrderDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedOrder = OrderDataGrid.SelectedItem as Order;
        }

        private void editOrderBtn_Click(object sender, RoutedEventArgs e)
        {
           
            if (selectedOrder == null)
            {
                MessageBox.Show("Please select an order to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Pass a copy of the selected order to avoid modifying the original if the user cancels
            var orderToEdit = new Order
            {
                _orderId = selectedOrder._orderId,
                _orderName = selectedOrder._orderName,
                _productName = selectedOrder._productName,
                _supplier = selectedOrder._supplier,
                Quantity = selectedOrder.Quantity
            };

            var editWindow = new OrderCreateWin(orderToEdit);
            editWindow.OrderIdInput.InputTextBox.IsReadOnly = true;
            if (editWindow.ShowDialog() == true)
            {
                // The 'editWindow.Order' property now holds the updated values
                orderControler.UpdateOrder(editWindow.Order);
                LoadOrders();
            }
        }

        private void deleteOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedOrder == null)
            {
                MessageBox.Show("Please select an order to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            // Confirm deletion
            var result = MessageBox.Show($"Are you sure you want to delete the order '{selectedOrder._orderName}'?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                orderControler.DeleteOrder(selectedOrder._orderId);
                LoadOrders();
            }
        }
    }
}