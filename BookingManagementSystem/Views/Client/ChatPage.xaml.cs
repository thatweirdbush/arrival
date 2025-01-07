using BookingManagementSystem.ViewModels.Client;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

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

        ViewModel.ScrollToBottomRequested += ChatViewModel_ScrollToBottomRequested;
    }

    private void SuggestedQuestion_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            var question = button.Content;
            var viewModel = DataContext as ChatViewModel;
            viewModel?.SelectSuggestedQuestionCommand.Execute(question);
        }
    }

    // Auto Scroll to bottom when user receive message from bot
    private void ScrollToBottom()
    {
        var scrollViewer = GetScrollViewer(MessagesListView);
        if (scrollViewer != null)
        {
            scrollViewer.ChangeView(null, scrollViewer.ExtentHeight, null);
        }
    }

    // Lấy đối tượng ScrollViewer từ ListView
    private ScrollViewer? GetScrollViewer(DependencyObject obj)
    {
        if (obj is ScrollViewer)
            return obj as ScrollViewer;

        for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
        {
            var child = VisualTreeHelper.GetChild(obj, i);
            var result = GetScrollViewer(child);
            if (result != null)
                return result;
        }
        return null;
    }

    private void ChatViewModel_ScrollToBottomRequested()
    {
        ScrollToBottom();
    }

    private void PromptTextBox_KeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
    {
        if (e.Key == Windows.System.VirtualKey.Enter)
        {
            ViewModel.SendMessageAsyncCommand.Execute(null);
            PromptTextBox.Focus(FocusState.Programmatic);
            e.Handled = true;
        }
    }

    private void PromptTextBox_Loaded(object sender, RoutedEventArgs e)
    {
        PromptTextBox.Focus(FocusState.Programmatic);
    }
}
