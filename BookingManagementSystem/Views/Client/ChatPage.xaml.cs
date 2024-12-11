using BookingManagementSystem.Core.Services;
using BookingManagementSystem.ViewModels.Client;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views.Client;

public sealed partial class ChatPage : Page
{
    private readonly ChatBotService _chatBotService;

    public ChatViewModel ViewModel
    {
        get;
    }

    public ChatPage()
    {
        ViewModel = App.GetService<ChatViewModel>();
        InitializeComponent();
        _chatBotService = new ChatBotService("AIzaSyClVcHqs9my89OZphm4qMCXENEXySyixzg");
    }

    private async void AskBotButton_Click(object sender, RoutedEventArgs e)
    {
        var question = QuestionTextBox.Text;

        if (string.IsNullOrWhiteSpace(question))
        {
            ResponseTextBlock.Text = "Enter the new question, please!";
            return;
        }

        ResponseTextBlock.Text = "Processing...";
        var response = await _chatBotService.AskAsync(question);
        ResponseTextBlock.Text = response;
    }
}
