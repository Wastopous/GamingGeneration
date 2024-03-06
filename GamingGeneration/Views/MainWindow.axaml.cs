using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace GamingGeneration.Views;

public partial class MainWindow : Window
{
    private readonly string enteredName;
    public MainWindow(string textEnteredName)
    {
        InitializeComponent();
        enteredName = textEnteredName;
        NameTabItem.Header = enteredName.ToUpper();
        this.Closing += MainWindow_Closing;
        PassGenButton = this.FindControl<Button>("PassGenButton");
        PassTextBox = this.FindControl<TextBox>("PassTextBox");
    }

    private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        var enterWindow = new EnterWindow();
        enterWindow.Show();
    }


    private async void PassGenButton_OnClick(object? sender, RoutedEventArgs e)
    {
        PassGenButton.Background = new SolidColorBrush(Color.Parse("#3083df"));
        PassTextBox.Text = "";
        PassGenButton.Content = "\u27f3  ГЕНЕРАЦИЯ";
        await Task.Delay(TimeSpan.FromSeconds(1));
        string passGen = GeneratePassword();
        PassTextBox.Text = passGen;
        PassGenButton.Content = "\u25b6  СГЕНЕРИРОВАТЬ ПАРОЛЬ";
        PassGenButton.Background = new SolidColorBrush(Color.Parse("#6dd51f"));
    }

    private string GeneratePassword()
    {
        const string upChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string lowChars = "abcdefghijklmnopqrstuvwxyz";
        const string dgChars = "0123456789";
        const string specChars = "!@#$%&*_?";

        StringBuilder pass = new StringBuilder();
        Random r = new Random();

        
        for (int i = 0; i < 8; i++)
        {
            pass.Append(lowChars[r.Next(lowChars.Length)]);
        }
        pass.Append(upChars[r.Next(upChars.Length)]);
        
        for (int i = 0; i < 3; i++)
        {
            pass.Append(dgChars[r.Next(dgChars.Length)]);
        }
        pass.Append(specChars[r.Next(specChars.Length)]);

        return pass.ToString();
    }

    private async void NikGenButton_OnClick(object? sender, RoutedEventArgs e)
    {
        NikGenButton.Background = new SolidColorBrush(Color.Parse("#3083df"));
        NikTextBox.Text = "";
        NikGenButton.Content = "\u27f3  ГЕНЕРАЦИЯ";
        await Task.Delay(TimeSpan.FromSeconds(1));
        NikTextBox.Text = "Дон Симон";
        SaveButton.IsVisible = true;
        NikGenButton.Content = "\u25b6  СГЕНЕРИРОВАТЬ ПАРОЛЬ";
        NikGenButton.Background = new SolidColorBrush(Color.Parse("#6dd51f"));
    }
    

}