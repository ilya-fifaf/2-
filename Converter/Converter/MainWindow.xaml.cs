using System.Net.Mime;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Converter;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    double area = 0;
    private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !IsTextAllowed(e.Text);
    }

    private static bool IsTextAllowed(string text)
    {
        return text.All(char.IsDigit) || text == ",";
    }
    private void Viches(object sender, RoutedEventArgs e)
    {
        try
        {
            double FirstNum = 0;
            double SecondNum = 0;
            switch (qwe.SelectedIndex)
            {
                case 0:
                    FirstNum = double.Parse(textBoxA.Text);
                    break;
                case 1:
                    FirstNum = double.Parse(textBoxA.Text) * 10;
                    break;
                case 2:
                    FirstNum = double.Parse(textBoxA.Text) * 1000;
                    break;
                case 3:
                    FirstNum = double.Parse(textBoxA.Text) * 1000000;
                    break;
                case 4:
                    FirstNum = double.Parse(textBoxA.Text) * 304.8;
                    break;
                case 5:
                    FirstNum = double.Parse(textBoxA.Text) * 914.4;
                    break;
                case 6:
                    FirstNum = double.Parse(textBoxA.Text) * 25.4;
                    break;
                case 7:
                    FirstNum = double.Parse(textBoxA.Text) * 1609344;
                    break;
            }
            switch (ads.SelectedIndex)
            {
                case 0:
                    SecondNum = double.Parse(textBoxB.Text);
                    break;
                case 1:
                    SecondNum = double.Parse(textBoxB.Text) * 10;
                    break;
                case 2:
                    SecondNum = double.Parse(textBoxB.Text) * 1000;
                    break;
                case 3:
                    SecondNum = double.Parse(textBoxB.Text) * 1000000;
                    break;
                case 4:
                    SecondNum = double.Parse(textBoxB.Text) * 304.8;
                    break;
                case 5:
                    SecondNum = double.Parse(textBoxB.Text) * 914.4;
                    break;
                case 6:
                    SecondNum = double.Parse(textBoxB.Text) * 25.4;
                    break;
                case 7:
                    SecondNum = double.Parse(textBoxB.Text) * 1609344;
                    break;
            }
            area = FirstNum * SecondNum;
            Select();
        }
        catch (Exception exception)
        {
            MessageBox.Show("нельзя");
            textBoxB.Text = "";
            textBoxA.Text = "";
        }
        
    }
    private void Resultbox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Select();
        
    }
    private void Select()
    {
        double area1 = area;
        switch (Resultbox.SelectedIndex)
        {
            case 0:
                area1 = area;
                break;
            case 1:
                area1 = area / 100;
                break;
            case 2:
                area1 = area / 1000000;
                break;
            case 3:
                area1 = area / 1000000000000;
                break;
            case 4:
                area1 = area / 92903.04;
                break;
            case 5:
                area1 = area / 836127.36;
                break;
            case 6:
                area1 = area / 645.16;
                break;
            case 7:
                area1 = area / 2589988110000;
                break;
        } 
        Result.Text = area1.ToString();
    }
}