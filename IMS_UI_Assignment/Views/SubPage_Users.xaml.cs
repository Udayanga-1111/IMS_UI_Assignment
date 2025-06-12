using IMS_UI_Assignment.Controllers;
using IMS_UI_Assignment.Models;
using IMS_UI_Assignment.Views.CRUDWindows;
using System.Windows;
using System.Windows.Controls;

namespace IMS_UI_Assignment.Views
{
    /// <summary>
    /// Interaction logic for SubPage_Users.xaml
    /// </summary>
    public partial class SubPage_Users : Page
    {
        private UserController userController;
        private UserModel SelectedUser;
        public SubPage_Users()
        {
            InitializeComponent();
            userController = new UserController();
            LoadUsers();
        }

        private void LoadUsers()
        {
            var userList = userController.GetAllUsers();
            UserDataGrid.ItemsSource = userList;
        }

        private void addUserBtn_Click(object sender, RoutedEventArgs e)
        {
            UserActionWin userActionWin = new UserActionWin();
            if (userActionWin.ShowDialog() == true)
            {
                UserModel? newUser = userActionWin.User;
                if (newUser != null) // Ensure newUser is not null before passing it to CreateUser
                {
                    userController.CreateUser(newUser);
                    LoadUsers();
                }
                else
                {
                    MessageBox.Show("Failed to create user. User data is incomplete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                LoadUsers();
            }
        }

        private void editUserBtn_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedUser == null)
            {
                MessageBox.Show("Please select a user to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var userToEdit = new UserModel
            {
                Id = SelectedUser.Id,
                FName = SelectedUser.FName,
                LName = SelectedUser.LName,
                UserName = SelectedUser.UserName,
                Password = SelectedUser.Password,
                UserRole = SelectedUser.UserRole
            };

            var editActionWin = new UserActionWin(userToEdit);
            editActionWin.userIdInput.InputTextBox.IsReadOnly = true;
            if (editActionWin.ShowDialog() == true)
            {
                if (editActionWin.User != null)
                {
                    userController.UpdateUser(editActionWin.User);
                    LoadUsers();
                }
            }
        }

        private void deleteUserBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Please select a user to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete user {SelectedUser.FName} {SelectedUser.LName}?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes) {
                userController.DeleteUser(SelectedUser.Id);
                LoadUsers();
            }
        }

        private void UserDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            SelectedUser = UserDataGrid.SelectedItem as UserModel;
        }
    }
}
