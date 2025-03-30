using BookingManagementSystem.Helpers;
using BookingManagementSystem.ViewModels.Account;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage;

namespace BookingManagementSystem.Views.Account;

public sealed partial class RecoverPasswordPage : Page
{
    public RecoverPasswordViewModel ViewModel { get; }

    private readonly ApplicationDataContainer localSettings;

    public RecoverPasswordPage()
    {
        ViewModel = App.GetService<RecoverPasswordViewModel>();
        InitializeComponent();

        localSettings = ApplicationData.Current.LocalSettings;
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

    private async void btnRecover_Click(object sender, RoutedEventArgs e)
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
        var isPasswordValid = ValidationAccount.IsValidPassword(password);
        var isPasswordMatch = ValidationAccount.IsPasswordMatch(password, confirmPassword);

        if (string.IsNullOrWhiteSpace(username))
        {
            contentDialog.Content = "Recover failed! Username is required.";
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
            contentDialog.Content = "Recover successfully!";
        }

        
        if (ViewModel.RecoverPasswordAuthentication(username, password))
        {
            // Show dialog
            var result = await contentDialog.ShowAsync();
            // If successful, navigate to LoginPage
            if (!string.IsNullOrWhiteSpace(username)
                && isPasswordValid && isPasswordMatch
                && result == ContentDialogResult.None)
            {
                _ = Frame.Navigate(typeof(LoginPage));
            }
        }
        else
        {
            contentDialog.Content = "Recover failed!";
            await contentDialog.ShowAsync();
        }
    }

    private void btnBackLoginPage_Click(object sender, RoutedEventArgs e)
    {
        Frame.GoBack();
    }
}
