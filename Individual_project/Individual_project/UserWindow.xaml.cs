using System.Windows;

namespace Individual_project;

public partial class UserWindow : Window
{
    public UserWindow()
    {
        InitializeComponent();
    }

    private void Tovar1_Click(object sender, RoutedEventArgs e)
    {
        var productName = "CRM-система RayC"; 
        var price = 59000;

        if (App.CurrentUser == null)
        {
            MessageBox.Show("Вы не авторизованы!");
            return;
        }

        UserService.AddRequest(App.CurrentUser.Login, new UserRequest
        {
            ProductName = productName,
            Price = price
        });
        MessageBox.Show("Заявка добавлена в ваши заявки!");
    }
    private void Tovar2_Click(object sender, RoutedEventArgs e)
    {
        var productName = "Корпоративный портал"; 
        var price = 110000;

        if (App.CurrentUser == null)
        {
            MessageBox.Show("Вы не авторизованы!");
            return;
        }

        UserService.AddRequest(App.CurrentUser.Login, new UserRequest
        {
            ProductName = productName,
            Price = price
        });
        MessageBox.Show("Заявка добавлена в ваши заявки!");
    }
    private void Tovar3_Click(object sender, RoutedEventArgs e)
    {
        var productName = "Система онлайн-обучения"; 
        var price = 85000;

        if (App.CurrentUser == null)
        {
            MessageBox.Show("Вы не авторизованы!");
            return;
        }

        UserService.AddRequest(App.CurrentUser.Login, new UserRequest
        {
            ProductName = productName,
            Price = price
        });
        MessageBox.Show("Заявка добавлена в ваши заявки!");
    }
    private void Tovar4_Click(object sender, RoutedEventArgs e)
    {
        var productName = "Автоматизация документооборота"; 
        var price = 49000;

        if (App.CurrentUser == null)
        {
            MessageBox.Show("Вы не авторизованы!");
            return;
        }

        UserService.AddRequest(App.CurrentUser.Login, new UserRequest
        {
            ProductName = productName,
            Price = price
        });
        MessageBox.Show("Заявка добавлена в ваши заявки!");
    }
    private void Tovar5_Click(object sender, RoutedEventArgs e)
    {
        var productName = "Сервис аналитики бизнеса"; 
        var price = 77000;

        if (App.CurrentUser == null)
        {
            MessageBox.Show("Вы не авторизованы!");
            return;
        }

        UserService.AddRequest(App.CurrentUser.Login, new UserRequest
        {
            ProductName = productName,
            Price = price
        });
        MessageBox.Show("Заявка добавлена в ваши заявки!");
    }
}