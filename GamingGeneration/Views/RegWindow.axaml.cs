using Avalonia;
using Avalonia.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using GamingGeneration.Views;

namespace GamingGeneration.Views;

public partial class RegWindow : Window
{
    public RegWindow()
    {
        InitializeComponent();
    }

    private void RegButton_OnClick(object? sender, RoutedEventArgs e)
    {
        string enteredName = LoginTextBox.Text;
        if (string.IsNullOrWhiteSpace(enteredName) || !IsLetter(enteredName))
        {
            LoginShowError();
            return;
        }
        var mainWindow = new MainWindow(enteredName);
        this.Close();
        mainWindow.Show();
    }

    private bool IsLetter(string input)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(input, @"[a-zA-Zа-яА-Я]+$");
    }

    private void LoginShowError()
    {
        LoginTextBox.Watermark = "НЕКОРРЕКТНЫЙ ВВОД!";
        LoginTextBox.Foreground = Brushes.Red;
    }

    private void LoginTextBox_OnKeyDown(object? sender, KeyEventArgs e)
    {
        LoginTextBox.Foreground = Brushes.White;
    }
}