using IMS_UI_Assignment.Models;
using System.Windows;
using System.Windows.Controls;

namespace IMS_UI_Assignment.Views.CRUDWindows
{
    public partial class UserActionWin : Window
    {
        public UserModel? User { get; private set; }
        public UserActionWin()
        {
            InitializeComponent();
            User = new UserModel();
        }

        public UserActionWin(UserModel existingUser)
        {
            InitializeComponent();
            User = existingUser; 
            userIdInput.InputTextBox.Text = User.Id.ToString();
            firstNameInput.InputTextBox.Text = User.FName;
            lastNameInput.InputTextBox.Text = User.LName;
            usernameInput.InputTextBox.Text = User.UserName;
            userPasswordInput.InputPasswordBox.Password = User.Password;
            UserRoleComboBox.Text = User.UserRole;

            userIdInput.InputTextBox.IsReadOnly = true;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(firstNameInput.InputTextBox.Text) ||
                string.IsNullOrWhiteSpace(lastNameInput.InputTextBox.Text) ||
                string.IsNullOrWhiteSpace(usernameInput.InputTextBox.Text) ||
                string.IsNullOrWhiteSpace(userPasswordInput.InputPasswordBox.Password) ||
                UserRoleComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all required fields.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if(!int.TryParse(userIdInput.InputTextBox.Text.Trim() ,out int userid))
            {

                MessageBox.Show("User ID must be valid numbers.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            User.Id = userid;
            User.FName = firstNameInput.InputTextBox.Text.Trim();
            User.LName = lastNameInput.InputTextBox.Text.Trim();
            User.UserName = usernameInput.InputTextBox.Text.Trim();
            User.Password = userPasswordInput.InputPasswordBox.Password.Trim();
            User.UserRole = UserRoleComboBox.Text.Trim();

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
