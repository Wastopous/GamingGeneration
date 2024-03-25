using System.Linq;
using Avalonia;
using Avalonia.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using GamingGeneration.ViewModels;
using GamingGeneration.Views;
using MySql.Data.MySqlClient;

namespace GamingGeneration.Views;

public partial class RegWindow : ConnWindow
{
    public RegWindow()
    {
        InitializeComponent();

    }


    private void EntButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var enterwindow = new EnterWindow();
        this.Hide();
        enterwindow.Show();
    }
    

    private void RegButton_OnClick(object? sender, RoutedEventArgs e)
    {
        string sql = """
                     insert into user (Name, LastName, BirthDate, Login, PinCode) 
                     VALUES  (@name, @lastname, @birthdate, @login, @pincode)
                     """;
        using (var con = new MySqlConnection(_ConnectionSB.ConnectionString))
        {
            con.Open();
            using (var com = con.CreateCommand())
            {
                com.CommandText = sql;
                com.Parameters.Add("@name", (MySqlDbType.String)); 
                /*com.Parameters["@name"].Value = (NameTextBox.Text as User).Name; */
            }
        }
    }
   /* private bool HaveText()
    {
        bool haveText = !string.IsNullOrWhiteSpace(LoginTextBox.Text) &&
                         !string.IsNullOrWhiteSpace(NameTextBox.Text) &&
                         !string.IsNullOrWhiteSpace(SurNameTextBox.Text) &&
                         !string.IsNullOrWhiteSpace(BirthPicker.ToString());

        if (!haveText)
        {
            RegButton.IsEnabled = false;
        }
        else
        {
            RegButton.IsEnabled = true;
        }

        return haveText;
    }
    */
}