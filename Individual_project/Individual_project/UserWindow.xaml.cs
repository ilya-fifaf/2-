using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Individual_project;

public static Product GetProductByName(string name)
{
    return GetAllProducts().FirstOrDefault(p => p.ProductName == name);
}

public partial class UserWindow : Window
{
    private List<Product> products;

    public UserWindow()
    {
        InitializeComponent();
        LoadProducts();
    }

    private void LoadProducts()
    {
        products = ProductService.GetAllProducts();
        ProductsPanel.Children.Clear();
        foreach (var product in products)
        {
            ProductsPanel.Children.Add(CreateProductCard(product));
        }
    }

    private UIElement CreateProductCard(Product product)
    {
        var border = new Border
        {
            Margin = new Thickness(8),
            Padding = new Thickness(12),
            CornerRadius = new CornerRadius(12),
            BorderBrush = System.Windows.Media.Brushes.LightGray,
            BorderThickness = new Thickness(1),
            Background = System.Windows.Media.Brushes.White,
            Width = 350,
            Child = new StackPanel
            {
                Children =
                {
                    new TextBlock { Text = product.ProductName, FontWeight = FontWeights.Bold, FontSize = 18 },
                    new TextBlock { Text = $"Цена: {product.Price:N0} ₽", FontWeight = FontWeights.SemiBold, Foreground = System.Windows.Media.Brushes.Green, Margin = new Thickness(0,6,0,0) },
                    new Button
                    {
                        Content = "Заказать",
                        Background = System.Windows.Media.Brushes.SteelBlue,
                        Foreground = System.Windows.Media.Brushes.White,
                        Margin = new Thickness(0,10,0,0),
                        Tag = product.ProductName,
                    }
                }
            }
        };

        ((border.Child as StackPanel).Children[2] as Button).Click += ProductOrder_Click;
        return border;
    }

    private void ProductOrder_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var productName = button.Tag.ToString();
        var product = ProductService.GetProductByName(productName);

        if (App.CurrentUser == null)
        {
            MessageBox.Show("Вы не авторизованы!");
            return;
        }

        UserService.AddRequest(App.CurrentUser.Login, new UserRequest
        {
            ProductName = product.ProductName
        });
        MessageBox.Show("Заявка добавлена в ваши заявки!");
    }

    // Для обновления карточек товаров после изменения цен:
    public void RefreshProducts() => LoadProducts();
}
