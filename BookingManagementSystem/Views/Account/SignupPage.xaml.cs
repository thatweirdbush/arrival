using BookingManagementSystem.Helpers;
using BookingManagementSystem.ViewModels.Account;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Windows.Storage;

namespace BookingManagementSystem.Views.Account;

public sealed partial class SignupPage : Page
{
    public SignupViewModel ViewModel
    {
        get;
    }

    private readonly ApplicationDataContainer localSettings;

    public SignupPage()
    {
        ViewModel = App.GetService<SignupViewModel>();
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

    private void Hyperlink_Login_Click(Hyperlink sender, HyperlinkClickEventArgs e)
    {
        // Check if previous page is LoginPage
        if (Frame.BackStackDepth > 0)
        {
            var lastPageType = Frame.BackStack.Last().SourcePageType;
            if (lastPageType == typeof(LoginPage))
            {
                Frame.GoBack();
                return;
            }
        }
        Frame.Navigate(typeof(LoginPage));
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
            contentDialog.Content = "Sign up failed! Invalid username.";
            await contentDialog.ShowAsync();
            return;
        }
        else if (!isPasswordValid)
        {
            contentDialog.Content = "Sign up failed! Invalid password.";
            await contentDialog.ShowAsync();
            return;
        }
        else if (!isPasswordMatch)
        {
            contentDialog.Content = "Sign up failed! Confirm password incorrect.";
            await contentDialog.ShowAsync();
            return;
        }

        if (ViewModel.SignupAuthentication(username, password))
        {
            contentDialog.Content = "Sign up successfully!";
            var result = await contentDialog.ShowAsync();

            // If successful, navigate to LoginPage
            if (isPasswordValid && isPasswordMatch && result == ContentDialogResult.None)
            {
                Frame.Navigate(typeof(LoginPage));
            }
        }
        else
        {
            contentDialog.Content = "Signup failed!";
            await contentDialog.ShowAsync();
        }
    }
}
