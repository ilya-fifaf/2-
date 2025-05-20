using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Anketa_v._2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadSavedForms();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[а-яА-ЯёЁ\-]+$");
        }

        private void Phone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            string fullText = textBox.Text.Insert(textBox.SelectionStart, e.Text);
            e.Handled = !Regex.IsMatch(fullText, @"^(\+7|8)\d{0,10}$");
        }

        private bool IsFormValid()
        {
            if (string.IsNullOrWhiteSpace(Family.Text) ||
                string.IsNullOrWhiteSpace(Name.Text) ||
                string.IsNullOrWhiteSpace(LastName.Text) ||
                string.IsNullOrWhiteSpace(Phone.Text) ||
                !Regex.IsMatch(Phone.Text, @"^(\+7|8)\d{10}$") ||
                !Day.SelectedDate.HasValue ||
                Education.SelectedItem == null ||
                string.IsNullOrWhiteSpace(Oplata.Text) ||
                !(RadioButton1.IsChecked == true || RadioButton2.IsChecked == true || RadioButton3.IsChecked == true))
            {
                MessageBox.Show("Пожалуйста, заполните все поля корректно.");
                return false;
            }
            return true;
        }

        private void SaveInfo(object sender, RoutedEventArgs e)
        {
            if (!IsFormValid()) return;

            string dir = "Анкеты";
            Directory.CreateDirectory(dir);
            int count = Directory.GetFiles(dir, "Анкета *.txt").Length + 1;
            string path = Path.Combine(dir, $"Анкета {count}.txt");

            string education = ((ComboBoxItem)Education.SelectedItem)?.Content.ToString() ?? "";
            string workType = RadioButton1.IsChecked == true ? "Офис" :
                RadioButton2.IsChecked == true ? "Удаленка" : "Гибрид";

            string content = $"Фамилия: {Family.Text}\nИмя: {Name.Text}\nОтчество: {LastName.Text}\n" +
                             $"Телефон: {Phone.Text}\nДата рождения: {Day.SelectedDate?.ToShortDateString()}\n" +
                             $"Образование: {education}\nОпыт: {Exp.Value} лет\nНавыки: {(Skills.IsChecked == true ? $"{Skills.Content}": "")}  {(Skills1.IsChecked == true ? $"{Skills1.Content}": "")} {(Skills2.IsChecked == true ? $"{Skills2.Content}": "")}\n" +
                             $"Зарплата: {Oplata.Text}\nТип работы: {workType}";

            File.WriteAllText(path, content);
            ComboBoxAnket.Items.Add($"Анкета {count}");
            MessageBox.Show("Анкета сохранена.");

            ClearForm(); // ← очищаем после сохранения
        }


        private void LoadSavedForms()
        {
            string dir = "Анкеты";
            if (!Directory.Exists(dir)) return;
            foreach (string file in Directory.GetFiles(dir, "Анкета *.txt"))
            {
                ComboBoxAnket.Items.Add(System.IO.Path.GetFileNameWithoutExtension(file));
            }
        }

        private void ComboBoxAnket_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxAnket.SelectedItem is string selected)
            {
                string path = System.IO.Path.Combine("Анкеты", selected + ".txt");
                if (File.Exists(path))
                {
                    TextBoxOutput.Text = File.ReadAllText(path);
                }
            }
        }

        private void Oplata_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[0-9]+$");
        }


        private void ClearForm()
        {
            Family.Text = "";
            Name.Text = "";
            LastName.Text = "";
            Phone.Text = "";
            Day.SelectedDate = null;
            Education.SelectedIndex = -1;
            Exp.Value = 0;
            Skills.IsChecked = false;
            Skills1.IsChecked = false;
            Skills2.IsChecked = false;
            Oplata.Text = "";
            RadioButton1.IsChecked = false;
            RadioButton2.IsChecked = false;
            RadioButton3.IsChecked = false;
            TextBoxOutput.Text = "";
        }

    }
}
