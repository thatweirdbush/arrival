using System.Security.Cryptography;
using System.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage;
using BookingManagementSystem.ViewModels.Account;
using Microsoft.UI.Xaml.Documents;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Views.Client;

namespace BookingManagementSystem.Views.Account;

public sealed partial class LoginPage : Page
{
    public LoginViewModel ViewModel
    {
        get;
    }

    private readonly ApplicationDataContainer localSettings;

    public LoginPage()
    {
        ViewModel = App.GetService<LoginViewModel>();
        InitializeComponent();

        localSettings = ApplicationData.Current.LocalSettings;

        // Load saved credentials if they exist
        LoadCredentials();
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
        // Save username
        localSettings.Values["Username"] = username;

        // Encrypt password
        var passwordInBytes = Encoding.UTF8.GetBytes(password);
        var entropyInBytes = new byte[20]; // Create entropy (key)
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(entropyInBytes);
        }

        // Encrypt the password using the entropy (key)
        var encryptedPasswordInBytes = ProtectedData.Protect(
            passwordInBytes,
            entropyInBytes,
            DataProtectionScope.CurrentUser);

        // Convert encrypted password and entropy to Base64 for storage
        var encryptedPasswordBase64 = Convert.ToBase64String(encryptedPasswordInBytes);
        var entropyBase64 = Convert.ToBase64String(entropyInBytes);

        // Store encrypted password and entropy
        localSettings.Values["PasswordInBase64"] = encryptedPasswordBase64;
        localSettings.Values["EntropyInBase64"] = entropyBase64;
    }

    // Method to load saved credentials
    public void LoadCredentials()
    {
        if (localSettings.Values.ContainsKey("Username") && localSettings.Values.ContainsKey("PasswordInBase64"))
        {
            // Retrieve username
            var username = localSettings.Values["Username"] as string;

            // Retrieve encrypted password and entropy
            var encryptedPasswordBase64 = localSettings.Values["PasswordInBase64"] as string;
            var entropyBase64 = localSettings.Values["EntropyInBase64"] as string;

            if (!string.IsNullOrEmpty(encryptedPasswordBase64) && !string.IsNullOrEmpty(entropyBase64))
            {
                // Decode Base64 to byte arrays
                var encryptedPasswordInBytes = Convert.FromBase64String(encryptedPasswordBase64);
                var entropyInBytes = Convert.FromBase64String(entropyBase64);

                // Decrypt the password
                var decryptedPasswordInBytes = ProtectedData.Unprotect(
                    encryptedPasswordInBytes,
                    entropyInBytes,
                    DataProtectionScope.CurrentUser);

                var password = Encoding.UTF8.GetString(decryptedPasswordInBytes);

                // Populate the username and password fields
                txtUsername.Text = username;
                passworBoxWithRevealmode.Password = password;
                chkRememberMe.IsChecked = true;
            }
        }
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

        // Check if the user is valid
        if (ViewModel.LoginAuthentication(username, password))
        {
            if (chkRememberMe.IsChecked == true)
            {
                SaveCredentials(username, password);
                contentDialog.Content = "Signed in successfully! Credentials saved.";
            }
            else
            {
                localSettings.Values.Remove("Username");
                localSettings.Values.Remove("PasswordInBase64");
                localSettings.Values.Remove("EntropyInBase64");
            }

            var result = await contentDialog.ShowAsync();
            if (result == ContentDialogResult.None)
            {
                Frame.Navigate(typeof(HomePage));
            }
        }
        else
        {
            contentDialog.Content = "Login failed! Incorrect username or password.";
            await contentDialog.ShowAsync();
        }
    }

    private void Hyperlink_Signup_Click(Hyperlink sender, HyperlinkClickEventArgs e)
    {
        Frame.Navigate(typeof(SignupPage));
    }

    private void Hyperlink_Recover_Click(Hyperlink sender, HyperlinkClickEventArgs e)
    {
        Frame.Navigate(typeof(RecoverPasswordPage));
    }
}
