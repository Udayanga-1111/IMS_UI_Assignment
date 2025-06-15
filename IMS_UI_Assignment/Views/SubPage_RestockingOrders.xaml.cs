using IMS_UI_Assignment.Controllers;
using IMS_UI_Assignment.Models;
using IMS_UI_Assignment.Views.CRUDWindows;
using System.Windows;
using System.Windows.Controls;

namespace IMS_UI_Assignment.Views
{
    public partial class SubPage_RestockingOrders : Page
    {
        private OrderController orderController;
        private Order selectedOrder;

        public SubPage_RestockingOrders()
        {
            InitializeComponent();
            orderController = new OrderController();
            LoadOrders();
        }

        private void LoadOrders()
        {
            var orderList = orderController.GetAllOrders();
            OrderDataGrid.ItemsSource = orderList;
        }

        private void addOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            OrderCreateWin orderCreateWin = new OrderCreateWin();

            if (orderCreateWin.ShowDialog() == true)
            {
                Order newOrder = orderCreateWin.Order;

                orderController.CreateOrder(newOrder);

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
                orderController.UpdateOrder(editWindow.Order);
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
                orderController.DeleteOrder(selectedOrder._orderId);
                LoadOrders();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (string.Equals(GlobalVariables.UserRole?.Trim(), "Cashier", StringComparison.OrdinalIgnoreCase))
            {
                OrderActionStack.Visibility = Visibility.Collapsed;
            }
        }
    }
}