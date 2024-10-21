using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Documents;
using System.Collections.ObjectModel;
using BookingManagementSystem.ViewModels;
using BookingManagementSystem.Helpers;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BookingManagementSystem.Views;


/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SignupPage : Page
{
    public SignupPage()
    {
        InitializeComponent();
    }

    private void RevealModeCheckbox_Changed(object sender, RoutedEventArgs e)
    {
        if (revealModeCheckBox.IsChecked == true)
        {
            passworBoxWithRevealmode.PasswordRevealMode = PasswordRevealMode.Visible;
        }
        else
        {
            passworBoxWithRevealmode.PasswordRevealMode = PasswordRevealMode.Hidden;
        }
    }

    private void Hyperlink_Login_Click(Hyperlink sender, HyperlinkClickEventArgs e)
    {
        Frame.Navigate(typeof(LoginPage)); 
    }

    private async void btnSignUp_Click(object sender, RoutedEventArgs e)
    {
        // Default ContentDialog
        ContentDialog contentDialog = new ContentDialog
        {
            XamlRoot = Content.XamlRoot,
            Title = "Result",
            Content = "Signed up successfully!",
            CloseButtonText = "Ok"
        };
        // Get username & password
        var username = txtUsername.Text;
        var password = passworBoxWithRevealmode.Password;
        var confirmPassword = confirmPassworBoxWithRevealmode.Password;

        if (ValidationAccount.IsValidUsername(username) && ValidationAccount.IsValidPassword(password) && ValidationAccount.IsPasswordMatch(password, confirmPassword))
        {
            var result = await contentDialog.ShowAsync();
            if (result == ContentDialogResult.None)
            {
                // Navigate to MainPage
                Frame.Navigate(typeof(LoginPage));
            }
        }
        else
        {
            contentDialog.Content = "Sign up failed! Invalid username or password.";
            await contentDialog.ShowAsync();
        }
    }
}
