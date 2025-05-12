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

namespace BiK;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    int popitka = 10;
    public MainWindow()
    {
        InitializeComponent();
    }
    string pass = generation_password();
    private void proverka(object sender, RoutedEventArgs e)
    {
        int z = 0;
        if (popitka <= 0)
        {
            MessageBox.Show("You lose!");
            Sbros();
            z = 1;
        }
        if (First.Text == pass[0].ToString() && Second.Text == pass[1].ToString() && Three.Text == pass[2].ToString() &&
                Four.Text == pass[3].ToString())
        {
            MessageBox.Show("WINNER!!!!!!!!");
            Sbros();
        }
        else
        {
            popitka -= 1;
            qwe.Text = popitka.ToString();
            int zxc = 0;
            int zxc1 = 0;
            var txt = First.Text + Second.Text + Three.Text + Four.Text;
            for (int i = 0; i < 4; i++)
            {
                if (txt[i] == pass[i])
                {
                    zxc1 += 1;
                    zxc--;
                }
            }
            foreach ( var a in txt )
            {
                if (pass.Contains(a))
                {   
                    zxc += 1;
                }
                
            }
            Try.Text += $"{txt} - {zxc1}Быков {zxc}Коров\n";
            if (z == 1)
            {
                Sbros();
                z = 0;
            }
        }
        
    }

    static string generation_password()
    { 
        var pass = generation().ToString();
        if (pass[0] != pass[1] && pass[0] != pass[2] && pass[0] != pass[3] && pass[1] != pass[2] && pass[1] != pass[3] && pass[2] != pass[3])
        {
            return pass;
        }
        return generation_password();
    }

    static int generation()
    {
        Random random = new Random();
        var pass = random.Next(1000, 10000);
        return pass;
    }

    void Sbros()
    {
        popitka = 11;
        First.SelectedIndex = 0;
        Second.SelectedIndex = 0;
        Three.SelectedIndex = 0;
        Four.SelectedIndex = 0;
        Try.Text = "";
    }
}