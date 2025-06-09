using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Viselica
{
    public partial class MainWindow : Window
    {
        private string WordToGuess; // Загаданное слово
        private char[] CurrentWordState; // Текущее состояние угадываемого слова
        private int Errors = 0; // Количество ошибок
        private List<UIElement> HangmanParts; // Части тела виселицы

        // Список слов на тему еда
        private readonly List<string> FoodWords = new List<string>
        {
            "ЯБЛОКО", "АПЕЛЬСИН", "БАНАН", "ГРУША", "ВИНОГРАД",
            "КЛУБНИКА", "МАНДАРИН", "ПЕРСИК", "АНАНАС", "МАЛИНА",
            "МОРКОВЬ", "КУКУРУЗА", "КАРТОФЕЛЬ", "КАБАЧОК", "ГРИБЫ",
            "ПОМИДОР", "КУРИНЫЙ", "ХЛЕБ", "КРУПА", "МЕД"
        };

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            // Сбрасываем ошибки
            Errors = 0;

            // Выбираем случайное слово из списка
            Random random = new Random();
            WordToGuess = FoodWords[random.Next(FoodWords.Count)];

            // Инициализируем текущее состояние угадываемого слова
            CurrentWordState = new string('□', WordToGuess.Length).ToCharArray();
            WordToGuessTextBlock.Text = new string(CurrentWordState);

            // Инициализируем части тела виселицы
            HangmanParts = new List<UIElement> { Head, Body, LeftArm, RightArm, LeftLeg, RightLeg };
            foreach (var part in HangmanParts)
            {
                part.Visibility = Visibility.Hidden;
            }

            // Генерируем кнопки для букв алфавита
            GenerateAlphabetButtons();
        }

        private void GenerateAlphabetButtons()
        {
            // Находим панель с именем AlphabetPanel
            WrapPanel panel = (WrapPanel)FindName("AlphabetPanel");
            if (panel == null) return; // Дополнительная проверка на null

            panel.Children.Clear();

            // Создаем кнопки для алфавита
            foreach (char letter in "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ")
            {
                Button button = new Button
                {
                    Content = letter,
                    Tag = letter,
                    FontSize = 18,
                    Width = 40,
                    Height = 40,
                    Margin = new Thickness(5)
                };
                button.Click += LetterButton_Click;
                panel.Children.Add(button);
            }
        }

        private void LetterButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton == null) return;

            char guessedLetter = (char)clickedButton.Tag;
            clickedButton.IsEnabled = false; // Деактивируем кнопку

            bool guessedCorrectly = false;

            // Проверяем, есть ли угаданная буква в слове
            for (int i = 0; i < WordToGuess.Length; i++)
            {
                if (WordToGuess[i] == guessedLetter)
                {
                    CurrentWordState[i] = guessedLetter;
                    guessedCorrectly = true;
                }
            }

            if (guessedCorrectly)
            {
                WordToGuessTextBlock.Text = new string(CurrentWordState);
                CheckForWin();
            }
            else
            {
                ShowNextHangmanPart();
                CheckForLose();
            }
        }

        private void ShowNextHangmanPart()
        {
            if (Errors < HangmanParts.Count)
            {
                HangmanParts[Errors].Visibility = Visibility.Visible;
                Errors++;
            }
        }

        private void CheckForWin()
        {
            if (!CurrentWordState.Contains('□'))
            {
                MessageBox.Show($"Поздравляем! Вы выиграли! Загаданное слово: {WordToGuess}", "Победа");
                InitializeGame(); // Перезапуск игры
            }
        }

        private void CheckForLose()
        {
            if (Errors >= HangmanParts.Count)
            {
                MessageBox.Show($"Вы проиграли. Загаданное слово: {WordToGuess}", "Поражение");
                InitializeGame(); // Перезапуск игры
            }
        }
    }
}