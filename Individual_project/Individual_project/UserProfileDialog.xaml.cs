using System.Windows;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Individual_project;
using Newtonsoft.Json;


namespace Individual_project
{
    public partial class UserProfileDialog : Window
    {
        private string _originalLogin;
        private string _originalPassword;

        public string ResultLogin { get; private set; }
        public string ResultPassword { get; private set; }

        public UserProfileDialog(Person user)
        {
            InitializeComponent();
            _originalLogin = user.Login;
            _originalPassword = user.Password;
            LoginBox.Text = user.Login;
            PasswordBox.Password = user.Password;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var newLogin = LoginBox.Text.Trim();
            var newPassword = PasswordBox.Password;
            ErrorText.Text = "";

            if (string.IsNullOrWhiteSpace(newLogin))
            {
                ErrorText.Text = "Логин не может быть пустым";
                return;
            }

            if (newLogin != _originalLogin && !UserService.IsLoginUnique(newLogin, _originalLogin))
            {
                ErrorText.Text = "Такой логин уже существует!";
                return;
            }

            ResultLogin = newLogin;
            ResultPassword = newPassword;

            // Сохраняем изменения в JSON
            UserService.UpdateUser(_originalLogin, newLogin, newPassword);

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            // Восстановим исходные значения (на всякий случай)
            LoginBox.Text = _originalLogin;
            PasswordBox.Password = _originalPassword;
            DialogResult = false;
            Close();
        }
    }
}
public static class UserService
{
    private static string FilePath => "users.json";

    public static List<Person> GetAllUsers()
    {
        if (!File.Exists(FilePath))
            return new List<Person>();
        var json = File.ReadAllText(FilePath);
        return JsonConvert.DeserializeObject<List<Person>>(json);
    }

    public static void SaveAllUsers(List<Person> users)
    {
        var json = JsonConvert.SerializeObject(users, Formatting.Indented);
        File.WriteAllText(FilePath, json);
    }

    public static bool IsLoginUnique(string login, string excludeLogin = null)
    {
        var users = GetAllUsers();
        return !users.Any(u => u.Login == login && u.Login != excludeLogin);
    }

    public static Person GetUser(string login)
    {
        return GetAllUsers().FirstOrDefault(u => u.Login == login);
    }

    public static void UpdateUser(string oldLogin, string newLogin, string newPassword)
    {
        var users = GetAllUsers();
        var user = users.FirstOrDefault(u => u.Login == oldLogin);
        if (user != null)
        {
            user.Login = newLogin;
            user.Password = newPassword;
            SaveAllUsers(users);
        }
    }
}