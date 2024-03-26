using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Avalonia;
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
            CheckCodeButton.IsVisible = true;
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
        if (File.Exists("codes"))
        {
            readCodes = File.ReadAllText("codes");
            if (string.IsNullOrEmpty(readCodes))
            {
                readStored = "{}";
            }
        }

        if (JsonSerializer.Deserialize<Dictionary<string, List<Credentials>>>(readStored) is { } us)
        {
            User = us;
        }

        if (!User.ContainsKey(enteredName))
        {
            User[enteredName] = new List<Credentials>();
        }

        if (JsonSerializer.Deserialize<Dictionary<string, string>>(readCodes) is { } codes)
        {
            Codes = codes;
        }

        if (Codes.ContainsKey(enteredName))
        {
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
        string inputText = nikTextBox.Text.ToLower();

        Random r = new Random();
        StringBuilder result = new StringBuilder();

        foreach (char character in inputText)
        {
            switch (character)
            {
                case ' ':
                    int choice_ = r.Next(6);
                    switch (choice_)
                    {
                        case 0:
                            result.Append("_");
                            break;
                        case 1:
                            result.Append("\ud83d\ude3b");
                            break;
                        case 2:
                            result.Append("\ud83e\udd20");
                            break;
                        case 3:
                            result.Append("༻");
                            break;
                        case 4:
                            result.Append("ღ");
                            break;
                        case 5:
                            result.Append("꧂");
                            break;
                    }

                    break;
                case 'а':
                    int choiceA = r.Next(2);
                    switch (choiceA)
                    {
                        case 0:
                            result.Append("A");
                            break;
                        case 1:
                            result.Append("@");
                            break;
                    }

                    break;
                case 'б':
                    int choiceB = r.Next(2);
                    switch (choiceB)
                    {
                        case 0:
                            result.Append("G");
                            break;
                        case 1:
                            result.Append("6");
                            break;
                    }

                    break;
                case 'в':
                    int choiceV = r.Next(2);
                    switch (choiceV)
                    {
                        case 0:
                            result.Append("B");
                            break;
                        case 1:
                            result.Append("BB");
                            break;
                    }

                    break;
                case 'г':
                    int choiceG = r.Next(2);
                    switch (choiceG)
                    {
                        case 0:
                            result.Append("r");
                            break;
                        case 1:
                            result.Append("7");
                            break;
                    }

                    break;
                case 'д':
                    int choiceD = r.Next(2);
                    switch (choiceD)
                    {
                        case 0:
                            result.Append("D");
                            break;
                        case 1:
                            result.Append("ᗪ");
                            break;
                    }

                    break;
                case 'е':
                    int choiceE = r.Next(2);
                    switch (choiceE)
                    {
                        case 0:
                            result.Append("е");
                            break;
                        case 1:
                            result.Append("Ɇ");
                            break;
                    }

                    break;
                case 'ё':
                    int choiceYO = r.Next(2);
                    switch (choiceYO)
                    {
                        case 0:
                            result.Append("e^");
                            break;
                        case 1:
                            result.Append("$");
                            break;
                    }

                    break;
                case 'ж':
                    int choiceGE = r.Next(2);
                    switch (choiceGE)
                    {
                        case 0:
                            result.Append("jk");
                            break;
                    }

                    break;
                case 'з':
                    int choiceZ = r.Next(2);
                    switch (choiceZ)
                    {
                        case 0:
                            result.Append("Z");
                            break;
                        case 1:
                            result.Append("乙\n");
                            break;
                    }

                    break;
                case 'и':
                    int choiceI = r.Next(3);
                    switch (choiceI)
                    {
                        case 0:
                            result.Append("1");
                            break;
                        case 1:
                            result.Append("I");
                            break;
                        case 2:
                            result.Append("\ud835\udd5a");
                            break;
                    }

                    break;
                case 'й':
                    int choiceYi = r.Next(2);
                    switch (choiceYi)
                    {
                        case 0:
                            result.Append("u*");
                            break;
                        case 1:
                            result.Append("Y");
                            break;
                    }

                    break;
                case 'к':
                    int choiceK = r.Next(2);
                    switch (choiceK)
                    {
                        case 0:
                            result.Append("K");
                            break;
                        case 1:
                            result.Append("k");
                            break;
                    }

                    break;
                case 'л':
                    int choiceL = r.Next(2);
                    switch (choiceL)
                    {
                        case 0:
                            result.Append("JI");
                            break;
                        case 1:
                            result.Append("L");
                            break;
                    }

                    break;
                case 'м':
                    int choiceM = r.Next(2);
                    switch (choiceM)
                    {
                        case 0:
                            result.Append("M");
                            break;
                        case 1:
                            result.Append("m");
                            break;
                    }

                    break;
                case 'н':
                    int choiceN = r.Next(2);
                    switch (choiceN)
                    {
                        case 0:
                            result.Append("H");
                            break;
                        case 1:
                            result.Append("I-I");
                            break;
                    }

                    break;
                case 'о':
                    int choiceO = r.Next(3);
                    switch (choiceO)
                    {
                        case 0:
                            result.Append("0");
                            break;
                        case 1:
                            result.Append("O");
                            break;
                        case 2:
                            result.Append("o");
                            break;
                    }

                    break;
                case 'п':
                    int choiceP = r.Next(2);
                    switch (choiceP)
                    {
                        case 0:
                            result.Append("TT");
                            break;
                        case 1:
                            result.Append("n");
                            break;
                    }

                    break;
                case 'р':
                    int choiceR = r.Next(2);
                    switch (choiceR)
                    {
                        case 0:
                            result.Append("P");
                            break;
                        case 1:
                            result.Append("R");
                            break;
                    }

                    break;
                case 'с':
                    int choiceC = r.Next(2);
                    switch (choiceC)
                    {
                        case 0:
                            result.Append("C");
                            break;
                        case 1:
                            result.Append("c");
                            break;
                    }

                    break;
                case 'т':
                    int choiceT = r.Next(2);
                    switch (choiceT)
                    {
                        case 0:
                            result.Append("t");
                            break;
                        case 1:
                            result.Append("T");
                            break;
                    }

                    break;
                case 'у':
                    int choiceU = r.Next(2);
                    switch (choiceU)
                    {
                        case 0:
                            result.Append("y");
                            break;
                        case 1:
                            result.Append("j");
                            break;
                    }

                    break;
                case 'ф':
                    int choiceF = r.Next(2);
                    switch (choiceF)
                    {
                        case 0:
                            result.Append("f");
                            break;
                        case 1:
                            result.Append("F");
                            break;
                    }

                    break;
                case 'х':
                    int choiceX = r.Next(2);
                    switch (choiceX)
                    {
                        case 0:
                            result.Append("X");
                            break;
                        case 1:
                            result.Append("x");
                            break;
                    }

                    break;
                case 'ш':
                    int choiceSHE = r.Next(2);
                    switch (choiceSHE)
                    {
                        case 0:
                            result.Append("lll");
                            break;
                        case 1:
                            result.Append("III");
                            break;
                    }

                    break;
                case 'щ':
                    int choiceSHA = r.Next(2);
                    switch (choiceSHA)
                    {
                        case 0:
                            result.Append("llj");
                            break;
                        case 1:
                            result.Append("IIL");
                            break;
                    }

                    break;
                case 'ъ':
                    int choiceTverd = r.Next(2);
                    switch (choiceTverd)
                    {
                        case 0:
                            result.Append("'b");
                            break;
                        case 1:
                            result.Append("'6");
                            break;
                    }

                    break;
                case 'ь':
                    int choiceMagk = r.Next(2);
                    switch (choiceMagk)
                    {
                        case 0:
                            result.Append("b");
                            break;
                        case 1:
                            result.Append("d");
                            break;
                    }

                    break;
                case 'ы':
                    int choiceYI = r.Next(2);
                    switch (choiceYI)
                    {
                        case 0:
                            result.Append("bl");
                            break;
                        case 1:
                            result.Append("bI");
                            break;
                    }

                    break;
                case 'э':
                    int choiceYE = r.Next(1);
                    switch (choiceYE)
                    {
                        case 0:
                            result.Append("3");
                            break;
                    }

                    break;
                case 'ю':
                    int choiceYU = r.Next(2);
                    switch (choiceYU)
                    {
                        case 0:
                            result.Append("u");
                            break;
                        case 1:
                            result.Append("U");
                            break;
                    }

                    break;
                case 'я':
                    int choiceYA = r.Next(2);
                    switch (choiceYA)
                    {
                        case 0:
                            result.Append("9I");
                            break;
                        case 1:
                            result.Append("R");
                            break;
                    }

                    break;
                default:
                    result.Append(character);
                    break;
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

    private void CodeButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Codes.Add(enteredName, CodeTextBox.Text ?? "");
        CodePanel.IsVisible = false;
        Save();
        CheckCodeButton.IsVisible = true;
    }

    #endregion

    #region design

    private void CheckCodeButton_OnClick(object? sender, RoutedEventArgs e)
    {


        CodeTextBox.Text = "";
        CodeGrid.IsVisible = false;
    }

    private void NickTabItem_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        NickTabItem.FontWeight = FontWeight.ExtraBold;
        NameTabItem.FontWeight = FontWeight.Normal;
        PassTabItem.FontWeight = FontWeight.Normal;
    }

    private void PassTabItem_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        PassTabItem.FontWeight = FontWeight.ExtraBold;
        NameTabItem.FontWeight = FontWeight.Normal;
        NickTabItem.FontWeight = FontWeight.Normal;
    }

    private void NameTabItem_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        CodeGrid.IsVisible = true;
        NameTabItem.FontWeight = FontWeight.ExtraBold;
        NickTabItem.FontWeight = FontWeight.Normal;
        PassTabItem.FontWeight = FontWeight.Normal;
    }

    #endregion
}