using IMS_UI_Assignment.Services;
using System.Windows;
using System.Windows.Controls;

namespace IMS_UI_Assignment.Views
{
    /// <summary>
    /// Interaction logic for SubPage_Reports.xaml
    /// </summary>
    public partial class SubPage_Reports : Page
    {
        private readonly ReportService _reportService;
        public SubPage_Reports()
        {
            InitializeComponent();
            _reportService = new ReportService();
        }
        private void GenerateReportButton_Click(object sender, RoutedEventArgs e)
        {
            if (ReportSelectorComboBox.SelectedItem is not ComboBoxItem selectedItem)
            {
                MessageBox.Show("Please select a report type.", "Selection Missing", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string reportName = selectedItem.Content.ToString();
            HideAllReportGrids();

            switch (reportName)
            {
                case "Products Report":
                    ProductsDataGrid.ItemsSource = _reportService.GetProductsReport();
                    ProductsDataGrid.Visibility = Visibility.Visible;
                    break;

                case "Suppliers Report":
                    SuppliersDataGrid.ItemsSource = _reportService.GetSuppliersReport();
                    SuppliersDataGrid.Visibility = Visibility.Visible;
                    break;

                case "Low Stock Products Report":
                    LowStockDataGrid.ItemsSource = _reportService.GetLowStockReport();
                    LowStockDataGrid.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void HideAllReportGrids()
        {
            ProductsDataGrid.Visibility = Visibility.Collapsed;
            SuppliersDataGrid.Visibility = Visibility.Collapsed;
            LowStockDataGrid.Visibility = Visibility.Collapsed;
        }
    }
}
