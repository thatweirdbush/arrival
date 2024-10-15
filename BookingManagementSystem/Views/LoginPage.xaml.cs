using System.Security.Cryptography;
using System.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage;
using BookingManagementSystem.ViewModels;

namespace BookingManagementSystem.Views;

public sealed partial class LoginPage : Page
{
    public LoginViewModel ViewModel
    {
        get;
    }

    private ApplicationDataContainer localSettings;

    public LoginPage()
    {
        ViewModel = App.GetService<LoginViewModel>();
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

    public void SaveCredentials(string username, string password)
    {
        var passwordInBytes = Encoding.UTF8.GetBytes(password);
        var entropyInBytes = new byte[20];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(entropyInBytes);
        }
        var encryptedPasswordInBytes = ProtectedData.Protect(
            passwordInBytes,
            entropyInBytes,
            DataProtectionScope.CurrentUser);

        var encryptedPasswordBase64 = Convert.ToBase64String(encryptedPasswordInBytes);
        var entropyBase64 = Convert.ToBase64String(entropyInBytes);

        localSettings.Values["PasswordInBase64"] = encryptedPasswordBase64;
        localSettings.Values["EntropyInBase64"] = entropyBase64;
    }

    private async void btnSignIn_Click(object sender, RoutedEventArgs e)
    {
        // Default ContentDialog
        ContentDialog contentDialog = new ContentDialog
        {
            XamlRoot = Content.XamlRoot,
            Title = "Result",
            Content = "Signed in successfully!",
            CloseButtonText = "Ok"
        };

        // Get username & password
        var username = txtUsername.Text;
        var password = passworBoxWithRevealmode.Password;
        
        if (chkRememberMe.IsChecked == true)
        {
            SaveCredentials(username, password);
            contentDialog.Content = "Signed in successfully! Credentials saved.";
        }
        else
        {
            localSettings.Values.Remove("Username");
            localSettings.Values.Remove("Password");
        }

        var result = await contentDialog.ShowAsync();
        if (result == ContentDialogResult.None)
        {
            // Navigate to MainPage
            Frame.Navigate(typeof(MainPage));
        }
    }
}
