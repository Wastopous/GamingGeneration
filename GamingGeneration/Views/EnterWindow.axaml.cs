using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GamingGeneration.Views;

namespace GamingGeneration;

public partial class EnterWindow : ConnWindow
{
    //дальше будет регистрация на различных платформах и вход в них путём всплывающих окон с подробно описанными шагами и каждое окно будет переключаться на следующее 
    #region main
    public EnterWindow()
    {
        InitializeComponent();
    }
    #endregion
    #region sign in

    private void EnterButton_OnClick(object? sender, RoutedEventArgs e)
    {
        string enteredName = NameTextBox.Text;
        if (string.IsNullOrWhiteSpace(enteredName) || !IsLetter(enteredName))
        {
            ShowError();
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

    private void ShowError()
    {
        ErrorLabel.IsVisible = true;
    }
    
    private void NameTextBox_OnTextChanging(object? sender, TextChangingEventArgs e)
    {
        NameTextBox.Watermark = "";
        ErrorLabel.IsVisible = false;
    }

    private void NameTextBox_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
            (e.Key >= Key.OemMinus && e.Key >= Key.OemPlus))
        {
            e.Handled = true;
        }
    }
    #endregion
    #region sign up
    private void RegLabel_OnClick(object? sender, RoutedEventArgs e)
    {
        var regWindow = new RegWindow();
        regWindow.Show();
    }
    #endregion
}