using IMS_UI_Assignment.Views;
using System.Windows;

namespace IMS_UI_Assignment;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainWinFrame.Navigate(new LoginPage());
    }
}
