using System.Windows;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Newtonsoft.Json;

namespace Individual_project;

public partial class Registration : Window
{
    public Registration()
    {
        InitializeComponent();
    }

    string path = "user.json";

    private void Login_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        this.Close();
    }

    private void Registration_Click(object sender, RoutedEventArgs e)
    {
        string name = login.Text;
        string password = Pass.Password;
        DateTime? age = Birthday.SelectedDate;

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password) || age == null)
        {
            MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (age.Value == DateTime.Now || DateTime.Now.Year - age.Value.Year < 18)
        {
            MessageBox.Show("Простите, услугами могут пользоваться только совершеннолетние");
            return;
        }

        // Проверка на уникальность имени
        if (RegistrationLogic.IsNameExists(name))
        {
            MessageBox.Show("Пользователь с таким именем уже существует!", "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        var user = new Person()
        {
            Login = name,
            Password = password,
            Age = DateTime.Now.Year - age.Value.Year,
            Role = "User"
        };

        RegistrationLogic.RegisterUser(user);

        MessageBox.Show("Регистрация успешна!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        this.Close();
    }


    private void Input_Restriction(object sender, TextCompositionEventArgs e)
    {
        e.Handled = Regex.IsMatch(e.Text, "[^a-zA-Z]+");
    }
}

public static class RegistrationLogic
{
    private static string filePath = "users.json";

    public static List<Person> LoadUsers()
    {
        if (!File.Exists(filePath))
            return new List<Person>();

        var json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<List<Person>>(json) ?? new List<Person>();
    }

    public static void SaveUsers(List<Person> users)
    {
        var json = JsonConvert.SerializeObject(users, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public static bool IsNameExists(string name)
    {
        var users = LoadUsers();
        return users.Any(u => u.Login.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public static void RegisterUser(Person user)
    {
        var users = LoadUsers();
        users.Add(user);
        SaveUsers(users);
    }
}

public class Person
{
    public string Login { get; set; }
    public string Password { get; set; }
    public int Age { get; set; }
    public string Role { get; set; } = "User";
}