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
        Frame.GoBack(); 
    }

    private void btnBackLoginPage_Click(object sender, RoutedEventArgs e)
    {
        Frame.GoBack();
    }

    private async void btnSignUp_Click(object sender, RoutedEventArgs e)
    {
        // Default ContentDialog
        ContentDialog contentDialog = new ContentDialog
        {
            XamlRoot = Content.XamlRoot,
            Title = "Result",
            CloseButtonText = "Ok"
        };
        // Get username & password
        var username = txtUsername.Text;
        var password = passworBoxWithRevealmode.Password;
        var confirmPassword = confirmPassworBoxWithRevealmode.Password;

        // Validate password
        var isUserNameValid = ValidationAccount.IsValidUsername(username);
        var isPasswordValid = ValidationAccount.IsValidPassword(password);
        var isPasswordMatch = ValidationAccount.IsPasswordMatch(password, confirmPassword);
        if (!isUserNameValid)
        {
            contentDialog.Content = "Recover failed! Invalid username.";
        }
        else if (!isPasswordValid)
        {
            contentDialog.Content = "Recover failed! Invalid password.";
        }
        else if (!isPasswordMatch)
        {
            contentDialog.Content = "Recover failed! Confirm password incorrect.";
        }
        else
        {
            contentDialog.Content = "Sign up successfully!";
        }

        // Show dialog
        var result = await contentDialog.ShowAsync();

        // If successful, navigate to LoginPage
        if (isPasswordValid && isPasswordMatch && result == ContentDialogResult.None)
        {
            Frame.Navigate(typeof(LoginPage));
        }
    }
}
