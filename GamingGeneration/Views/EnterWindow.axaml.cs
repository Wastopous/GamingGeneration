using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GamingGeneration.Views;

namespace GamingGeneration;

public partial class EnterWindow : Window
{
    public EnterWindow()
    {
        InitializeComponent();
    }
    
    private void EnterButton_OnClick(object? sender, RoutedEventArgs e)
    {
        string enteredName = NameTextBox.Text;
        if (string.IsNullOrWhiteSpace(enteredName) || !IsLetter(enteredName))
        {
            ShowError("у вас не может быть такое имя");
            return;
        }
        var mainWindow = new MainWindow(enteredName);
        this.Hide();
        mainWindow.Show();
    }
    private bool IsLetter(string input)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(input, @"[a-zA-Zа-яА-Я]+$");
    }

    private void ShowError(string уваснеможетбытьтакоеимя)
    {
        ErrorLabel.IsVisible = true;
    }

    private void NameTextBox_OnTextChanging(object? sender, TextChangingEventArgs e)
    {
        NameTextBox.Watermark = "";
        ErrorLabel.IsVisible = false;
    }
}