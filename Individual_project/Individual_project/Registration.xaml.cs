using System.Windows;

namespace Individual_project;

public partial class Registration : Window
{
    public Registration()
    {
        InitializeComponent();
    }

    private void Login_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        this.Close();
    }
}