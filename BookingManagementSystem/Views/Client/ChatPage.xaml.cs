using BookingManagementSystem.Core.Services;
using BookingManagementSystem.ViewModels.Client;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace BookingManagementSystem.Views.Client;

public sealed partial class ChatPage : Page
{

    public ChatViewModel ViewModel
    {
        get;
    }

    public ChatPage()
    {
        ViewModel = App.GetService<ChatViewModel>();
        InitializeComponent();

        // Gắn ViewModel vào DataContext của trang
        DataContext = ViewModel;
    }


    private void OnSuggestedQuestionTapped(object sender, TappedRoutedEventArgs e)
    {
        if (sender is TextBlock textBlock)
        {
            var question = textBlock.Text;
            var viewModel = DataContext as ChatViewModel;
            viewModel?.SelectSuggestedQuestionCommand.Execute(question);
        }
    }
}
