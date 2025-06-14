using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace IMS_UI_Assignment.Views
{
    public partial class SubPage_dashboard : Page, INotifyPropertyChanged
    {
        private string _productCount;
        private string _lowStock;
        private string _supplierCount;
        private string _systemUsers;
        private readonly string _connectionString = "Data Source=LAPTOP-J2V071AF\\SQLEXPRESS;Initial Catalog=IMS_GameStore;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";


        public string ProductCount
        {
            get { return _productCount; }
            set
            {
                if (_productCount != value)
                {
                    _productCount = value;
                    OnPropertyChanged(nameof(ProductCount));
                }
            }
        }

        public string SupplierCount
        {
            get { return _supplierCount; }
            set
            {
                if (_supplierCount != value)
                {
                    _supplierCount = value;
                    OnPropertyChanged(nameof(SupplierCount));
                }
            }
        }

        public string SystemUsers
        {
            get { return _systemUsers; }
            set
            {
                if (_systemUsers != value)
                {
                    _systemUsers = value;
                    OnPropertyChanged(nameof(SystemUsers));
                }
            }
        }

        public string LowStock
        {
            get { return _lowStock; }
            set
            {
                if (_lowStock != value)
                {
                    _lowStock = value;
                    OnPropertyChanged(nameof(LowStock));
                }
            }
        }

        public SubPage_dashboard()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DashUpdate();
        }

        public void DashUpdate()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM ProductsTable";
                SqlCommand cmd = new SqlCommand(query, conn);
                int count = (int)cmd.ExecuteScalar();
                ProductCount = count.ToString();
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(*) FROM ProductsTable WHERE Quantity <= 10";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();

                    int result = (int)cmd.ExecuteScalar();
                    if (result == null)
                    {
                        LowStock = "0";
                    }
                    else
                    {
                        LowStock = result.ToString();
                    }
                }
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM SupplierTable";
                SqlCommand cmd = new SqlCommand(query, conn);
                int count = (int)cmd.ExecuteScalar();
                SupplierCount = count.ToString();
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Users";
                SqlCommand cmd = new SqlCommand(query, conn);
                int count = (int)cmd.ExecuteScalar();
                SystemUsers = count.ToString();
            }

        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
