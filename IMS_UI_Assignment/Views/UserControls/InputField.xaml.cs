using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace IMS_UI_Assignment.Views.UserControls
{
    /// <summary>
    /// Interaction logic for InputField.xaml
    /// </summary>
    public partial class InputField : UserControl, INotifyPropertyChanged
    {
        public InputField()
        {
            placeholder = string.Empty; // Initialize the non-nullable field
            Password = string.Empty; // Initialize the non-nullable field
            Text = string.Empty; // Initialize the non-nullable field
            DataContext = this; // Set the DataContext to the UserControl itself
            InitializeComponent();
        }

        // Creating the Placeholder Property to use in the user Control
        // This property will be used to set the placeholder text in the TextBox
        private string placeholder;
        internal readonly string Password;
        internal readonly string Text;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Placeholder
        {
            get { return placeholder; }
            set
            {
                placeholder = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Placeholder)));
            }
        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(InputTextBox.Text))
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
