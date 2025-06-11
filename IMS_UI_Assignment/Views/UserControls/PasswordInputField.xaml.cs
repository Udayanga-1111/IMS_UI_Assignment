using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace IMS_UI_Assignment.Views.UserControls
{
    /// <summary>
    /// Interaction logic for PasswordInputField.xaml
    /// </summary>
    public partial class PasswordInputField : UserControl, INotifyPropertyChanged
    {
        private string placeholder;

        public PasswordInputField()
        {
            placeholder = string.Empty; // Initialize the non-nullable field
            DataContext = this; // Set the DataContext to the UserControl itself
            InitializeComponent();
        }

        public string Placeholder
        {
            get { return placeholder; }
            set
            {
                placeholder = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Placeholder)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void InputPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(InputPasswordBox.Password))
            {
                TBPlaceholder.Visibility = Visibility.Visible;
            }
            else
            {
                TBPlaceholder.Visibility = Visibility.Hidden;
            }
        }
    }
}
