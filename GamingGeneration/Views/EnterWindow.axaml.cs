using Avalonia;
using Avalonia.Controls;
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
        var mainWindow = new MainWindow(enteredName);
        this.Hide();
        mainWindow.Show();
    }
}