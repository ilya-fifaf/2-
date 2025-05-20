using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Timer;

public partial class MainWindow : Window
{
    private DispatcherTimer timer;
    private TimeSpan timeLeft;
    private bool isPaused = false;

    public MainWindow()
    {
        InitializeComponent();
        timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += Timer_Tick;
    }

    private void Start_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            int hours = Math.Min(ParseInput(HoursBox.Text), 23);
            int minutes = Math.Min(ParseInput(MinutesBox.Text), 59);
            int seconds = Math.Min(ParseInput(SecondsBox.Text), 59);

            timeLeft = new TimeSpan(hours, minutes, seconds);
            if (timeLeft.TotalSeconds <= 0)
                throw new ArgumentException("Введите положительное время.");

            TimerDisplay.Text = timeLeft.ToString(@"hh\:mm\:ss");
            timer.Start();
            ErrorBlock.Text = "";

            // Блокировка ввода
            StartButton.IsEnabled = false;
            HoursBox.IsEnabled = false;
            MinutesBox.IsEnabled = false;
            SecondsBox.IsEnabled = false;

            // Очистка ввода
            HoursBox.Text = "";
            MinutesBox.Text = "";
            SecondsBox.Text = "";

            isPaused = false;
        }
        catch (Exception ex)
        {
            timer.Stop();
            ErrorBlock.Text = ex.Message;
        }
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        if (timeLeft.TotalSeconds > 0)
        {
            timeLeft = timeLeft.Subtract(TimeSpan.FromSeconds(1));
            TimerDisplay.Text = timeLeft.ToString(@"hh\:mm\:ss");
        }
        else
        {
            timer.Stop();
            TimerDisplay.Text = "00:00:00";
            MessageBox.Show("Таймер завершён!", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
            ResetState();
        }
    }

    private void Pause_Click(object sender, RoutedEventArgs e)
    {
        if (!isPaused)
        {
            timer.Stop();
            isPaused = true;
            PauseButton.Content = "Продолжить";
        }
        else
        {
            timer.Start();
            isPaused = false;
            PauseButton.Content = "Пауза";
        }
    }

    private void Stop_Click(object sender, RoutedEventArgs e)
    {
        timer.Stop();
        timeLeft = TimeSpan.Zero;
        TimerDisplay.Text = "00:00:00";
        ResetState();
    }

    private void ResetState()
    {
        // Сброс блокировки
        StartButton.IsEnabled = true;
        HoursBox.IsEnabled = true;
        MinutesBox.IsEnabled = true;
        SecondsBox.IsEnabled = true;

        PauseButton.Content = "Пауза";
        isPaused = false;
        ErrorBlock.Text = "";
    }

    private int ParseInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return 0;
        if (!int.TryParse(input, out int result) || result < 0)
            throw new ArgumentException("Введите неотрицательное число.");
        return result;
    }

    private void HoursBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !Regex.IsMatch(e.Text, @"^[0-9]+$");
    }
}
