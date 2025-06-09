using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Individual_project
{
    public partial class AdminWindow : Window
    {
        private List<Product> products;
        private List<Person> users;
        private List<RequestView> currentRequests;

        public AdminWindow()
        {
            InitializeComponent();
            LoadProducts();
            LoadServicesFilter();
        }

        private void LoadProducts()
        {
            products = ProductService.GetAllProducts();
            ProductsList.ItemsSource = products;
        }

        private void LoadServicesFilter()
        {
            ServicesFilterBox.ItemsSource = products.Select(p => p.ProductName).ToList();
        }

        private void ProductsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var prod = ProductsList.SelectedItem as Product;
            if (prod != null)
            {
                ProductNameBox.Text = prod.ProductName;
                ProductPriceBox.Text = prod.Price.ToString();
            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ProductNameBox.Text) || !int.TryParse(ProductPriceBox.Text, out int price))
            {
                MessageBox.Show("Введите корректные данные.");
                return;
            }
            ProductService.AddProduct(new Product { ProductName = ProductNameBox.Text, Price = price });
            LoadProducts();
            LoadServicesFilter();
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            var prod = ProductsList.SelectedItem as Product;
            if (prod == null) return;
            if (string.IsNullOrWhiteSpace(ProductNameBox.Text) || !int.TryParse(ProductPriceBox.Text, out int price))
            {
                MessageBox.Show("Введите корректные данные.");
                return;
            }
            ProductService.UpdateProduct(prod.ProductName, ProductNameBox.Text, price);
            LoadProducts();
            LoadServicesFilter();
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            var prod = ProductsList.SelectedItem as Product;
            if (prod == null) return;
            ProductService.DeleteProduct(prod.ProductName);
            LoadProducts();
            LoadServicesFilter();
        }

        private void ServicesFilterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string product = ServicesFilterBox.SelectedItem as string;
            if (string.IsNullOrEmpty(product)) return;

            users = UserService.GetAllUsers();
            currentRequests = new List<RequestView>();

            foreach (var user in users)
            {
                if (user.Requests == null) continue;
                foreach (var req in user.Requests.Where(r => r.ProductName == product))
                {
                    currentRequests.Add(new RequestView
                    {
                        UserLogin = user.Login,
                        ProductName = req.ProductName,
                        Price = req.Price,
                        Status = req.Status,
                        UserDataRef = user,
                        UserRequestRef = req
                    });
                }
            }
            RequestsList.ItemsSource = currentRequests;
        }

        // Изменение статуса заявки
        private void StatusChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            if (combo?.DataContext is RequestView rv)
            {
                var selectedStatus = (combo.SelectedItem as ComboBoxItem)?.Content?.ToString();
                if (!string.IsNullOrEmpty(selectedStatus))
                {
                    rv.Status = selectedStatus;
                    rv.UserRequestRef.Status = selectedStatus;
                    // Сохраняем изменения
                    UserService.SaveAllUsers(users);
                }
            }
        }
    }

    // Вспомогательный класс для отображения заявок с логином пользователя
    public class RequestView
    {
        public string UserLogin { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public string Status { get; set; }
        public Person UserDataRef { get; set; }
        public UserRequest UserRequestRef { get; set; }
    }
}

public class Product
{
    public string ProductName { get; set; }
    public int Price { get; set; }
}

public static class ProductService
{
    private static string FilePath => "products.json";

    public static List<Product> GetAllProducts()
    {
        if (!File.Exists(FilePath))
            return new List<Product>();
        var json = File.ReadAllText(FilePath);
        return JsonConvert.DeserializeObject<List<Product>>(json);
    }

    public static void SaveAllProducts(List<Product> products)
    {
        var json = JsonConvert.SerializeObject(products, Formatting.Indented);
        File.WriteAllText(FilePath, json);
    }

    public static void UpdateProduct(string oldName, string newName, int newPrice)
    {
        var products = GetAllProducts();
        var prod = products.FirstOrDefault(p => p.ProductName == oldName);
        if (prod != null)
        {
            prod.ProductName = newName;
            prod.Price = newPrice;
            SaveAllProducts(products);
        }
    }

    public static void AddProduct(Product p)
    {
        var products = GetAllProducts();
        products.Add(p);
        SaveAllProducts(products);
    }

    public static void DeleteProduct(string name)
    {
        var products = GetAllProducts();
        var prod = products.FirstOrDefault(p => p.ProductName == name);
        if (prod != null)
        {
            products.Remove(prod);
            SaveAllProducts(products);
        }
    }
}