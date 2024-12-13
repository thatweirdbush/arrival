using System.Collections.ObjectModel;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Core.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BookingManagementSystem.ViewModels.Client;

public partial class ChatViewModel : ObservableRecipient
{
    private readonly ChatBotService _chatBotService;

    [ObservableProperty]
    private string _userInput = string.Empty;

    public ObservableCollection<Message> Messages
    {
        get;
    }

    public ChatViewModel()
    {
        // Khởi tạo dịch vụ bot (API Key cần được cung cấp từ bên ngoài nếu cần bảo mật hơn)
        _chatBotService = new ChatBotService("AIzaSyClVcHqs9my89OZphm4qMCXENEXySyixzg");

        // Danh sách tin nhắn
        Messages = new ObservableCollection<Message>();
        SendMessageAsyncCommand = new RelayCommand(async () => await SendMessageAsync());
    }

    public IRelayCommand SendMessageAsyncCommand
    {
        get;
    }

    [RelayCommand]
    public async Task SendMessageAsync()
    {
        if (string.IsNullOrWhiteSpace(UserInput))
        {
            Messages.Add(new Message
            {
                Content = "Enter the new question, please!",
                IsUserMessage = false
            });
            return;
        }

        // Thêm câu hỏi của người dùng
        Messages.Add(new Message
        {
            Content = UserInput,
            IsUserMessage = true
        });

        var currentInput = UserInput; // Lưu nội dung câu hỏi hiện tại
        UserInput = string.Empty; // Xóa nội dung TextBox

        // Thêm thông báo đang xử lý
        Messages.Add(new Message
        {
            Content = "Processing...",
            IsUserMessage = false
        });

        // Nhận phản hồi từ bot
        var response = await _chatBotService.AskAsync(currentInput);

        // Thay thế thông báo "Processing..." bằng câu trả lời từ bot
        Messages[Messages.Count - 1] = new Message
        {
            Content = response,
            IsUserMessage = false
        };
    }
}
