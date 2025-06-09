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
using Random;

namespace rand;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public int CountNumbers = 1;
    public int LeftRange = 0;
    public int RightRange = 0;
    public List<int> RandomNumbers = [];
    public MainWindow()
    {
        InitializeComponent();
        Loaded += Window_Loaded;
        ResultTextBlock.Text = "";
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        // Скрытие всех основных панелек
        ElliminationAllNumbersPanel.Visibility = Visibility.Collapsed;
        ListPanelNumbers.Visibility = Visibility.Collapsed;
    }
     
    private void GenerateButton_Clicked(object sender, RoutedEventArgs e)
{
    if (!CheckNumbersAvaliable()) return;

    RandomNumbers.Clear();
    var random = new System.Random();

    var excluded = ElliminationAllNumbersBox.IsChecked == true
        ? ParseTextBoxToNumbers(ElliminationAllNumbersOfBox)
        : new List<int>();

    int targetCount = CountNumbers;

    if (RangeRadioButton.IsChecked == true)
    {
        int from = int.Parse(StartRange.Text);
        int to = int.Parse(FinishRange.Text);
        int left = Math.Min(from, to);
        int right = Math.Max(from, to);

        var available = Enumerable.Range(left, right - left + 1)
            .Except(excluded)
            .ToList();


        if (ElliminationRepeat.IsChecked == true)
        {
            if (available.Count < targetCount)
            {
                targetCount = available.Count;
            }

            for (int i = 0; i < targetCount; i++)
            {
                int number = available[random.Next(available.Count)];
                RandomNumbers.Add(number);
                available.Remove(number);
            }
        }
        else
        {
            for (int i = 0; i < targetCount; i++)
            {
                if (available.Count == 0) break;
                int number = available[random.Next(available.Count)];
                RandomNumbers.Add(number);
            }
        }
    }
    else if (ListRadioButton.IsChecked == true)
    {
        var sourceList = ParseTextBoxToNumbers(ListPanelNumbersOfBox)
                         .Except(excluded)
                         .ToList();

        if (ElliminationRepeat.IsChecked == true)
        {
            if (sourceList.Count < targetCount)
            {
                targetCount = sourceList.Count;
            }

            for (int i = 0; i < targetCount; i++)
            {
                int number = sourceList[random.Next(sourceList.Count)];
                RandomNumbers.Add(number);
                sourceList.Remove(number);
            }
        }
        else
        {
            for (int i = 0; i < targetCount; i++)
            {
                if (sourceList.Count == 0) break;
                int number = sourceList[random.Next(sourceList.Count)];
                RandomNumbers.Add(number);
            }
        }
    }

    ResultTextBlock.Text = "";
    foreach (var nums in RandomNumbers)
    {
        ResultTextBlock.Text += nums.ToString() + ' ';
    }
}


    private bool CheckNumbersAvaliable()
    {
        if ((StartRange.Text.Trim() == "" || FinishRange.Text.Trim() == "") && RangeRadioButton.IsChecked == true)
        {
            MessageBox.Show("Введите числа от и до");
            return false;
        }

        if (ListPanelNumbersOfBox.Text.Trim() == "" && ListRadioButton.IsChecked == true)
        {
            MessageBox.Show("В списке нету не одной цифры");
            return false;
        }
        return true;
    }
    private List<int> ParseTextBoxToNumbers(TextBox box)
    {
        return box.Text
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(s => int.TryParse(s, out int n) ? n : (int?)null)
            .Where(n => n.HasValue)
            .Select(n => n.Value)
            .ToList();
    }

    private void MoveWindow(object sender, MouseButtonEventArgs e)
    {
        DragMove();
    }

    private void CloseWindow(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void Slider_SetCountNumbers(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (sender is Slider slider && NumbersCounter != null)
        {
            CountNumbers = (int)slider.Value;
            NumbersCounter.Text = $"Выбранное количество чисел: {CountNumbers}";
        }
    }

    private void ElliminationNumberBox_Checked(object sender, RoutedEventArgs e)
    {
        ElliminationAllNumbersPanel.Visibility = Visibility.Visible;
    }

    private void ElliminaitonNumberBox_UnChecked(object sender, RoutedEventArgs e)
    {
        ElliminationAllNumbersPanel.Visibility = Visibility.Collapsed;
    }

private void RangeRadioButton_Checked(object sender, RoutedEventArgs e)
{
    // Убедимся, что элементы инициализированы
    if (RangePanel != null)
    {
        RangePanel.Visibility = Visibility.Visible;
    }

    if (ListPanelNumbers != null)
    {
        ListPanelNumbers.Visibility = Visibility.Collapsed;
    }

    if (ListPanelNumbersOfBox != null) // Проверяем и очищаем содержимое списка, если оно есть
    {
        ListPanelNumbersOfBox.Text = string.Empty;
    }
}

private void ListRadioButton_Checked(object sender, RoutedEventArgs e)
{
    // Показываем список и скрываем диапазон
    ListPanelNumbers.Visibility = Visibility.Visible;
    RangePanel.Visibility = Visibility.Collapsed;

    if (StartRange != null && FinishRange != null) // Очищаем содержимое диапазона, если оно есть
    {
        StartRange.Text = string.Empty;
        FinishRange.Text = string.Empty;
    }
}

    private void OnlyInts_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        var box = (TextBox)sender;
        e.Handled = !Regex.IsMatch(e.Text, "^[0-9-]$");

            var text = box.Text + e.Text;
            if (text.Split(' ',StringSplitOptions.RemoveEmptyEntries).Any(x => x.Contains("--") || (x.Length > 1 && x[^1] == '-')))
                e.Handled = true;

    }

    private void CloseSpaceInput_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space)
        {
            e.Handled = true;
        }
    }
}