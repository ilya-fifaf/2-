using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace FruitApp
{
    public class Fruit
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Calories { get; set; }

        public override string ToString() => $"{Name} - {Price}р, {Calories} ккал";
    }

    public partial class MainWindow : Window
{
    private string currentFile;
    private List<Fruit> fruits = new List<Fruit>();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void LoadFruits(string filePath)
    {
        currentFile = filePath;
        fruits.Clear();

        if (File.Exists(filePath))
        {
            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(' ');
                if (parts.Length == 3 &&
                    double.TryParse(parts[1], out double price) &&
                    int.TryParse(parts[2], out int calories))
                {
                    fruits.Add(new Fruit { Name = parts[0], Price = price, Calories = calories });
                }
            }
        }
        ApplySorting();
    }

    private void RbCitrus_OnChecked(object sender, RoutedEventArgs e)
    {
        if (rbCitrus.IsChecked == true)
        {
            inputBox.Visibility = Visibility.Visible;
            inputBox.Text = "";
            LoadFruits("citrus.txt");
        }
        else if (rbBerries.IsChecked == true)
        {
            inputBox.Visibility = Visibility.Visible;
            inputBox.Text = "";
            LoadFruits("berries.txt");
        }
        else if (rbTropical.IsChecked == true)
        {
            inputBox.Visibility = Visibility.Visible;
            inputBox.Text = "";
            LoadFruits("tropical.txt");
        }
            
    }
    
        private void Add_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(inputBox.Text))
            {
                errorBlock.Text = "Введите данные в формате: Название Цена Калорийность";
                return;
            }

            var parts = inputBox.Text.Split(' ');
            if (parts.Length != 3 || !double.TryParse(parts[1], out double price) || !int.TryParse(parts[2], out int calories))
            {
                errorBlock.Text = "Неверный формат данных. Формат предоставлен в виде {Название цена калорийность}";
                return;
            }

            string name = parts[0];
            
            if (fruits.Any(f => f.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                errorBlock.Text = "Товар с таким названием уже существует.";
                return;
            }

            var newFruit = new Fruit { Name = name, Price = price, Calories = calories };
            fruits.Add(newFruit);
            SaveFruits();
            ApplySorting();
            errorBlock.Text = "";
            inputBox.Clear();
        }

    private void Dell_OnClick(object sender, RoutedEventArgs e)
    {
        if (fruitList.SelectedItem is Fruit selected)
        {
            fruits.Remove(selected);
            SaveFruits();
            ApplySorting();
        }
    }
    private void FruitList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (fruitList.SelectedItem is Fruit selected)
        {
            inputBox.Text = $"{selected.Name} {selected.Price} {selected.Calories}";
            errorBlock.Text = "";
        }
    }

    private void Edit_Click(object sender, RoutedEventArgs e)
    {
        if (fruitList.SelectedItem is Fruit selected && !string.IsNullOrWhiteSpace(inputBox.Text))
        {
            var parts = inputBox.Text.Split(' ');
            if (parts.Length == 3 &&
                double.TryParse(parts[1], out double price) &&
                int.TryParse(parts[2], out int calories))
            {
                string newName = parts[0];

                
                if (fruits.Any(f => f != selected && f.Name.Equals(newName, StringComparison.OrdinalIgnoreCase)))
                {
                    errorBlock.Text = "Товар с таким названием уже существует.";
                    return;
                }

                
                selected.Name = newName;
                selected.Price = price;
                selected.Calories = calories;

                SaveFruits();
                ApplySorting();
                errorBlock.Text = "";
                inputBox.Clear();
            }
            else
            {
                errorBlock.Text = "Неверный формат данных для редактирования.";
            }
        }
    }


    private void SaveFruits()
    {
        File.WriteAllLines(currentFile, fruits.Select(f => $"{f.Name} {f.Price} {f.Calories}"));
    }

    private void ApplySorting()
    {
        IEnumerable<Fruit> sorted = fruits;

        switch (sortBox.SelectedIndex)
        {
            case 0:
                sorted = fruits.OrderBy(f => f.Price);
                break;
            case 1:
                sorted = fruits.OrderByDescending(f => f.Price);
                break;
            case 2:
                sorted = fruits.OrderBy(f => f.Calories);
                break;
            case 3:
                sorted = fruits.OrderByDescending(f => f.Calories);
                break;
            case 4:
                sorted = fruits.OrderBy(f => f.Name);
                break;
            case 5:
                sorted = fruits.OrderByDescending(f => f.Name);
                break;
        }

        fruitList.ItemsSource = null;
        fruitList.ItemsSource = sorted.ToList();
    }


    private void SortBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ApplySorting();
    }
}


}
