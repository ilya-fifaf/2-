using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BD;

    public partial class MainWindow : Window
    {
        private const string FilePath = "database.txt";
        private List<PersonRecord> records = new();

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            // Убедимся, что FilterCombo инициализирован и имеет элементы
            if (FilterCombo != null && FilterCombo.Items.Count > 0)
            {
                FilterCombo.SelectedIndex = 0;
            }
            UpdateGrid();
        }
        


        private void LoadData()
        {
            if (!File.Exists(FilePath)) return;

            var lines = File.ReadAllLines(FilePath);
            records = lines.Select(PersonRecord.Parse).Where(r => r != null).ToList();
        }

        private void SaveData()
        {
            File.WriteAllLines(FilePath, records.Select(r => r.ToTextLine()));
        }

        private void UpdateGrid()
        {
            if (FilterCombo == null || DataGri == null) return;

            string filter = "Все"; // значение по умолчанию
            if (FilterCombo.SelectedItem != null)
            {
                filter = ((ComboBoxItem)FilterCombo.SelectedItem)?.Content?.ToString() ?? "Все";
            }
    
            string search = SearchBox?.Text?.Trim().ToLower() ?? "";

            var filtered = records.Where(r =>
                (filter == "Все" || r.Type == filter) &&
                (r.FullText.ToLower().Contains(search))).ToList();

            DataGri.ItemsSource = filtered;
        }

        private void FilterCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try 
            {
                if (FilterCombo == null || zxc == null) return;
        
                var selectedItem = FilterCombo.SelectedItem as ComboBoxItem;
                if (selectedItem == null) return;
        
                var selected = selectedItem.Content?.ToString();
                if (string.IsNullOrEmpty(selected)) return;

                switch (selected)
                {
                    case "Студент":
                        zxc.Header = "Дата рождения";
                        break;

                    case "Преподаватель":
                        zxc.Header = "Предмет";
                        break;

                    case "Сотрудник":
                        zxc.Header = "Должность";
                        break;

                    default:
                        zxc.Header = "Доп. поле";
                        break;
                }

                UpdateGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при изменении фильтра: {ex.Message}");
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e) => UpdateGrid();

        private void AddTypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = ((ComboBoxItem)AddTypeCombo.SelectedItem)?.Content?.ToString();

            switch (selected)
            {
                case "Студент":
                    ExtraPanel.Visibility = Visibility.Collapsed;
                    DatePanel.Visibility = Visibility.Visible;
                    break;

                case "Преподаватель":
                    ExtraLabel.Text = "Предмет";
                    ExtraPanel.Visibility = Visibility.Visible;
                    DatePanel.Visibility = Visibility.Collapsed;
                    break;

                case "Сотрудник":
                    ExtraLabel.Text = "Должность";
                    ExtraPanel.Visibility = Visibility.Visible;
                    DatePanel.Visibility = Visibility.Collapsed;
                    break;

                default:
                    ExtraLabel.Text = "Поле";
                    ExtraPanel.Visibility = Visibility.Visible;
                    DatePanel.Visibility = Visibility.Collapsed;
                    break;
            }
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            
            string type = ((ComboBoxItem)AddTypeCombo.SelectedItem)?.Content.ToString();
            string extra = type == "Студент" ? BirthDatePicker.SelectedDate?.ToString("yyyy-MM-dd") ?? "" : ExtraFieldBox.Text.Trim();
            string last = LastNameBox.Text.Trim();
            string first = FirstNameBox.Text.Trim();
            string middle = MiddleNameBox.Text.Trim();
            string address = AddressBox.Text.Trim();
            string phone = PhoneBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(extra) || string.IsNullOrWhiteSpace(phone) || 
                !Regex.IsMatch(phone, @"^\+?\d{10,15}$") || string.IsNullOrWhiteSpace(last) || string.IsNullOrWhiteSpace(first) || string.IsNullOrWhiteSpace(middle) || string.IsNullOrWhiteSpace(address) || AddTypeCombo.SelectedIndex == -1)
            {
                MessageBox.Show("Заполните все!!!");
                return;
            }

            var newRecord = new PersonRecord
            {
                Type = type,
                ExtraField = extra,
                LastName = last,
                FirstName = first,
                MiddleName = middle,
                Address = address,
                Phone = phone
            };

            records.Add(newRecord);
            SaveData();
            UpdateGrid();

            // Очистить поля
            ExtraFieldBox.Clear();
            LastNameBox.Clear();
            FirstNameBox.Clear();
            MiddleNameBox.Clear();
            AddressBox.Clear();
            PhoneBox.Clear();
        }
        
        private void PhoneBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            string fullText = textBox.Text.Insert(textBox.SelectionStart, e.Text);
            e.Handled = !Regex.IsMatch(fullText, @"^(\+7|8)\d{0,10}$");
        }


        private void LastNameBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[а-яА-ЯёЁ\-]+$");
        }
    }

    public class PersonRecord
    {
        public string Type { get; set; }
        public string ExtraField { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public string FullText => $"{Type} {ExtraField} {LastName} {FirstName} {MiddleName} {Address} {Phone}";

        public static PersonRecord? Parse(string line)
        {
            try
            {
                var parts = line.Split('|');
                if (parts[0] == "Студент" && parts.Length == 7)
                {
                    return new PersonRecord
                    {
                        Type = parts[0],
                        LastName = parts[1],
                        FirstName = parts[2],
                        MiddleName = parts[3],
                        ExtraField = parts[4],
                        Address = parts[5],
                        Phone = parts[6]
                    };
                }
                else if ((parts[0] == "Преподаватель" || parts[0] == "Сотрудник") && parts.Length == 7) // было parts.Length == 8
                {
                    return new PersonRecord
                    {
                        Type = parts[0],
                        ExtraField = parts[1],
                        LastName = parts[2],
                        FirstName = parts[3],
                        MiddleName = parts[4],
                        Address = parts[5],
                        Phone = parts[6]
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при разборе строки: {line}\n{ex.Message}");
            }
            return null;
        }


        public string ToTextLine()
        {
            return Type switch
            {
                "Студент" => $"{Type}|{LastName}|{FirstName}|{MiddleName}|{ExtraField}|{Address}|{Phone}",
                _ => $"{Type}|{ExtraField}|{LastName}|{FirstName}|{MiddleName}|{Address}|{Phone}"
            };
        }
    }