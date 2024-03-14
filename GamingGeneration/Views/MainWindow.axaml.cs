using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;


namespace GamingGeneration.Views;

public partial class MainWindow : Window
{
    #region filesaving

    public class Credentials
    {
        public string value { get; set; }
        public static string? code { get; set; }
        public bool isPassword { get; set; }
        public bool isCode { get; set; }
    }

    private void Save()
    {
        var serializeCreds = JsonSerializer.Serialize(User);
        File.WriteAllText("savefile", serializeCreds);

        var serializeCodes = JsonSerializer.Serialize(Codes);
        File.WriteAllText("codes", serializeCodes);
    }

    private void Load()
    {
        string readStored = "{}";
        if (File.Exists("savefile"))
        {
            PassGenButton.IsEnabled = true;
            NameTabItem.IsVisible = true;
            CodePanel.IsVisible = true;
            readStored = File.ReadAllText("savefile");
            if (string.IsNullOrEmpty(readStored))
            {
                readStored = "{}";
            }
        }

        string readCodes = "{}";
        if (File.Exists("codes")) {
            readCodes = File.ReadAllText("codes");
            if (string.IsNullOrEmpty(readCodes)) {
                readStored = "{}";
            }
        }

        if (JsonSerializer.Deserialize<Dictionary<string, List<Credentials>>>(readStored) is { } us) {
            User = us;
        }
        
        if (!User.ContainsKey(enteredName))
        {
            User[enteredName] = new List<Credentials>();
        }

        if (JsonSerializer.Deserialize<Dictionary<string, string>>(readCodes) is { } codes) {
            Codes = codes;
        }

        if (Codes.ContainsKey(enteredName)) {
            CodePanel.IsVisible = false;
        }
        DataGrid.ItemsSource = User[enteredName];
    }


    public Dictionary<string, List<Credentials>> User = new Dictionary<string, List<Credentials>>();
    public Dictionary<string, string> Codes = new Dictionary<string, string>();
    private string enteredName;
    private string code => Codes[enteredName];

    #endregion

    #region main

    public MainWindow(string textEnteredName)
    {
        InitializeComponent();
        enteredName = textEnteredName;
        NameTabItem.Header = enteredName.ToUpper();
        Load();
        this.Closing += MainWindow_Closing;
        PassGenButton = this.FindControl<Button>("PassGenButton");
        PassTextBox = this.FindControl<TextBox>("PassTextBox");
    }

    #endregion

    #region generation

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
        PassSavePanel.IsVisible = true;
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
        string inputText = nikTextBox.Text;
        if (string.IsNullOrEmpty(inputText))
        {
            ErrorLabel.IsVisible = true;
            NikSaveEditPanel.IsVisible = false;
        }

        NikGenButton.Background = new SolidColorBrush(Color.Parse("#3083df"));
        NikTextBox.Text = "";
        NikGenButton.Content = "\u27f3  ГЕНЕРАЦИЯ";
        await Task.Delay(TimeSpan.FromSeconds(1));
        string nikGen = GenerateNickName();
        NikTextBox.Text = nikGen;
        NikGenButton.Content = "\u25b6  СГЕНЕРИРОВАТЬ ПАРОЛЬ";
        NikSaveEditPanel.IsVisible = true;
        NikGenButton.Background = new SolidColorBrush(Color.Parse("#6dd51f"));
    }

    private string GenerateNickName()
    {
        string inputText = nikTextBox.Text;

        Random r = new Random();
        StringBuilder result = new StringBuilder();

        foreach (char character in inputText)
        {
            if (char.IsLetter(character))
            {
                result.Append(r.Next(2) == 0 ? char.ToUpper(character) : character);
            }
            else if (char.IsWhiteSpace(character))
            {
                result.Append('_');
            }
        }

        return result.ToString();
    }


    private void NikEditButton_OnClick(object? sender, RoutedEventArgs e)
    {
        NikTextBox.IsReadOnly = false;
    }


    private void NikTextBox_OnTextChanging(object? sender, TextChangingEventArgs e)
    {
        NikSaveEditPanel.IsVisible = true;
        NikGenButton.IsEnabled = true;
        ErrorLabel.IsVisible = false;
    }

    private void NikTextBox_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
            (e.Key == Key.OemMinus && e.Key == Key.OemPlus))
        {
            e.Handled = true;
        }
    }

    #endregion

    #region saving

    private void PassSaveButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (!User.ContainsKey(enteredName))
        {
            User[enteredName] = new List<Credentials>();
        }

        User[enteredName].Add(new Credentials()
        {
            isPassword = true,
            value = PassTextBox.Text
        });
        PassSavePanel.IsVisible = false;
        // CodePanel.IsVisible = true;
        Save();
    }

    private void NikSaveButton_OnClick(object? sender, RoutedEventArgs e)
    {
        PassGenButton.IsEnabled = true;
        NikTextBox.IsReadOnly = true;
        if (!User.ContainsKey(enteredName))
        {
            User[enteredName] = new List<Credentials>();
        }

        User[enteredName].Add(new Credentials() { value = NikTextBox.Text });
        Save();
        NikSaveEditPanel.IsVisible = false;
    }

    #endregion

    #region account

    private void NameTabItem_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        CodeGrid.IsVisible = true;
    }

    private void CodeButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Codes.Add(enteredName, CodeTextBox.Text ?? "");
        CodePanel.IsVisible = false;
        Save();
    }

    #endregion

    private void CheckCodeButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(CodeTextBox.Text)) {
            return;
        }
        if (CodeTextBox.Text != code) {
            return;
        }

        CodeTextBox.Text = "";
        CodeGrid.IsVisible = false;
    }
}