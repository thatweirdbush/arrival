using System.Collections.ObjectModel;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Core.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BookingManagementSystem.ViewModels.Client;

public partial class ChatViewModel : ObservableRecipient
{
    private readonly ChatBotService _chatBotService;

    private string _userInput = string.Empty;
    public string UserInput
    {
        get => _userInput;
        set
        {
            _userInput = value;
            OnPropertyChanged();
            SendMessageAsyncCommand.NotifyCanExecuteChanged(); // Cập nhật trạng thái Command
        }
    }

    [ObservableProperty]
    private bool _isSendingMessage;

    public ObservableCollection<Message> Messages { get; }

    public RelayCommand SendMessageAsyncCommand { get; }

    public ChatViewModel()
    {
        // Khởi tạo dịch vụ bot (API Key cần được cung cấp từ bên ngoài nếu cần bảo mật hơn)
        _chatBotService = new ChatBotService("AIzaSyClVcHqs9my89OZphm4qMCXENEXySyixzg");

        // Danh sách tin nhắn
        Messages = new ObservableCollection<Message>();

        //SendMessageAsyncCommand = new RelayCommand(async () => await SendMessageAsync());
        SendMessageAsyncCommand = new RelayCommand(
            async () => await SendMessageAsync(),
            () => !string.IsNullOrWhiteSpace(UserInput) && !IsSendingMessage
        );
    }


    private async Task SendMessageAsync()
    {
        IsSendingMessage = true;
        SendMessageAsyncCommand.NotifyCanExecuteChanged(); // Vô hiệu hóa button gửi

        try
        {
            // Thêm tin nhắn của người dùng vào danh sách
            var currentInput = UserInput;
            Messages.Add(new Message
            {
                Content = currentInput,
                IsUserMessage = true,
                Timestamp = DateTime.Now
            });

            UserInput = string.Empty; // Xóa nội dung TextBox sau khi gửi

            // Thêm thông báo "Processing..." vào danh sách tin nhắn
            Messages.Add(new Message
            {
                Content = "Processing...",
                IsUserMessage = false,
                Timestamp = DateTime.Now
            });

            // Gửi câu hỏi tới ChatBotService và nhận câu trả lời
            var response = await _chatBotService.AskAsync(currentInput);

            // Cập nhật tin nhắn cuối cùng bằng phản hồi từ chatbot
            Messages[Messages.Count - 1] = new Message
            {
                Content = response,
                IsUserMessage = false,
                Timestamp = DateTime.Now
            };
        }
        catch (Exception ex)
        {
            // Xử lý lỗi và thông báo tới người dùng
            Messages.Add(new Message
            {
                Content = "Error occurred. Please try again!",
                IsUserMessage = false,
                Timestamp = DateTime.Now
            });
            Console.WriteLine(ex.Message);
        }
        finally
        {
            IsSendingMessage = false;
            SendMessageAsyncCommand.NotifyCanExecuteChanged(); // Kích hoạt lại button gửi
        }
    }
}
