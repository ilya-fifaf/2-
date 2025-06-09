using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Individual_project;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    private void Input_Restriction(object sender, TextCompositionEventArgs e)
    {
        e.Handled = Regex.IsMatch(e.Text, "[^a-zA-Z]+");
    }

    private void Login_Click(object sender, RoutedEventArgs e)
    {
        string name = login.Text;
        string password = Pass.Password;

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
        {
            MessageBox.Show("Пожалуйста, введите логин и пароль!", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        var user = LoginLogic.Authenticate(name, password);

        if (user == null)
        {
            MessageBox.Show("Пользователь с такими данными не найден!", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        App.CurrentUser = UserService.GetUser(name);
        // Открываем окно в зависимости от роли
        if (user.Role == "Admin")
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
        }
        else
        {
            UserWindow userWindow = new UserWindow();
            userWindow.Show();
        }

        
        this.Close();
    }

    private void Registration_Click (object sender, RoutedEventArgs e)
    {
        Registration registration = new Registration();
        registration.Show();
        this.Close();
    }
}

public static class LoginLogic
{
    private static string filePath = "users.json";

    public static Person Authenticate(string name, string password)
    {
        if (!File.Exists(filePath))
            return null;

        var json = File.ReadAllText(filePath);
        var users = JsonConvert.DeserializeObject<List<Person>>(json) ?? new List<Person>();
        return users.FirstOrDefault(u => u.Login == name && u.Password == password);
    }
}
