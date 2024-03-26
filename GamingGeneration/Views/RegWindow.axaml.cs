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
        this.Close();
    }

    #region sign up
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
                com.Parameters["@name"].Value = NameTextBox.Text ; 
                com.Parameters.Add("@lastname", (MySqlDbType.String)); 
                com.Parameters["@lastname"].Value = SurNameTextBox.Text ; 
                com.Parameters.Add("@birthdate", (MySqlDbType.Date)); 
                com.Parameters["@birthdate"].Value = BirthPicker.SelectedDate ; 
                com.Parameters.Add("@login", (MySqlDbType.String)); 
                com.Parameters["@login"].Value = LoginTextBox.Text ; 
                com.Parameters.Add("@pincode", (MySqlDbType.Int32)); 
                com.Parameters["@pincode"].Value = CodeTextBox.Text ; 
                com.ExecuteNonQuery();
            }
            con.Close();
            this.Close();
        }
    }
    #endregion
}