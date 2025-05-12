using System.Windows;
using System.Windows.Controls;

namespace development_services;

public partial class vxod : Window
{
    public vxod()
    {
        InitializeComponent();
    }

    private void RegisterButton_Click(object sender, RoutedEventArgs e)
    {
        string name = NameTextBox.Text;
        string password = PasswordBox.Password;

        // Проверка на ввод имени и пароля
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
        {
            MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка регистрации", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return;
        }

        // Открываем новое окно и передаем имя пользователя
        SecondWindow secondWindow = new SecondWindow(name);
        secondWindow.Show();

        // Закрываем текущее окно
        this.Close();
    }
}