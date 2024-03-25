using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace GamingGeneration.Views;

public partial class ConnWindow : Window
{
    public MySqlConnectionStringBuilder _ConnectionSB;
    public ConnWindow()
    {
        InitializeComponent();
        _ConnectionSB = new MySqlConnectionStringBuilder
        {
            Server = "localhost", 
            Database = "gaminggeneration",
            UserID = "root", 
            Password = "1234",
        };
    }
    
}