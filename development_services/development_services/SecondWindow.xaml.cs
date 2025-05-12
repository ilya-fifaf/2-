using System.Windows;
 
namespace development_services
{
    public partial class SecondWindow : Window
    {
        public SecondWindow(string userName)
        {
            InitializeComponent();

            // Устанавливаем имя пользователя в текстовое поле
            UserNameTextBlock.Text = $"Привет, {userName}!";
        }
    }
}