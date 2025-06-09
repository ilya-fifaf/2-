using System.Windows;
using System.Windows.Controls;

namespace Individual_project;

public partial class RayCApp : UserControl
{
    public RayCApp()
    {
        InitializeComponent();
    }
    
    private void RequestsButton_Click(object sender, RoutedEventArgs e)
    {
        if (App.CurrentUser == null)
        {
            MessageBox.Show("Вы не авторизованы!");
            return;
        }
        var requests = UserService.GetRequests(App.CurrentUser.Login);
        var wnd = new UserRequestsWindow(requests) { Owner = Window.GetWindow(this) };
        wnd.ShowDialog();
    }
    
    private void ProfileButton_Click(object sender, RoutedEventArgs e)
    {
        // Здесь используйте логин текущего пользователя, например, "ilya"
        var currentUser = App.CurrentUser;
        if (currentUser == null)
        {
            MessageBox.Show("Пользователь не найден");
            return;
        }
        var dialog = new UserProfileDialog(currentUser)
        {
            Owner = Window.GetWindow(this)
        };
        if (dialog.ShowDialog() == true)
        {
            MessageBox.Show($"Изменения сохранены!\nНовый логин: {dialog.ResultLogin}");
        }
        // Если отмена — ничего не меняем
    }
}