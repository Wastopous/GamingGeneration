using System.Linq;
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


    private void EntButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var enterwindow = new EnterWindow();
        this.Hide();
        enterwindow.Show();
    }
    

    private void RegButton_OnClick(object? sender, RoutedEventArgs e)
    {
        
    }
    private bool HaveText()
    {
        bool haveText = !string.IsNullOrWhiteSpace(LoginTextBox.Text) &&
                         !string.IsNullOrWhiteSpace(NameTextBox.Text) &&
                         !string.IsNullOrWhiteSpace(SurNameTextBox.Text) &&
                         !string.IsNullOrWhiteSpace(MidNameTextBox.Text);

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
}