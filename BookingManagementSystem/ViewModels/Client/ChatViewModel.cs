using System.Collections.ObjectModel;
using System.Windows.Input;
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

    public ObservableCollection<string> SuggestedQuestions { get; }

    public RelayCommand SendMessageAsyncCommand { get; }

    public ICommand SelectSuggestedQuestionCommand { get; }

    public ChatViewModel()
    {
        // Khởi tạo dịch vụ bot (API Key cần được cung cấp từ bên ngoài nếu cần bảo mật hơn)
        _chatBotService = new ChatBotService("AIzaSyClVcHqs9my89OZphm4qMCXENEXySyixzg");

        // Danh sách tin nhắn
        Messages = new ObservableCollection<Message>();

        // Câu hỏi gợi ý
        SuggestedQuestions = new ObservableCollection<string>
        {
            "How do I make a booking?",
            "What is your cancellation policy?",
            "Do you have any special offers?",
            "What are your customer support hours?"
        };

        //SendMessageAsyncCommand = new RelayCommand(async () => await SendMessageAsync());
        SendMessageAsyncCommand = new RelayCommand(
            async () => await SendMessageAsync(),
            () => !string.IsNullOrWhiteSpace(UserInput) && !IsSendingMessage
        );

        // Thêm tin nhắn chào mừng từ bot
        Messages.Add(new Message
        {
            Content = "Welcome to Arrival Hotel Booking! I am here to help you with bookings, cancellations, and more. What can I assist you with?",
            IsUserMessage = false,
            Timestamp = DateTime.Now
        });

        SelectSuggestedQuestionCommand = new RelayCommand<string>(OnSelectSuggestedQuestion);
    }

    private void OnSelectSuggestedQuestion(string question)
    {
        // Kiểm tra nếu có tin nhắn nhập tay, nếu có thì lưu vào Messages
        if (!string.IsNullOrEmpty(UserInput))
        {
            Messages.Add(new Message
            {
                Content = UserInput,
                IsUserMessage = true,
                Timestamp = DateTime.Now
            });
        }

        // Gán câu hỏi gợi ý vào UserInput
        UserInput = question;

        // Gọi lệnh gửi câu hỏi tới chatbot
        SendMessageAsyncCommand.Execute(null);
    }


    private async Task SendMessageAsync()
    {
        IsSendingMessage = true;
        SendMessageAsyncCommand.NotifyCanExecuteChanged(); // Vô hiệu hóa button gửi

        try
        {
            // Thêm tin nhắn của người dùng vào danh sách
            var currentInput = UserInput;

            // Nếu có tin nhắn người dùng, gửi nó và thêm vào danh sách
            if (!string.IsNullOrEmpty(currentInput))
            {
                Messages.Add(new Message
                {
                    Content = currentInput,
                    IsUserMessage = true,
                    Timestamp = DateTime.Now
                });
            }

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
        }
        finally
        {
            IsSendingMessage = false;
            SendMessageAsyncCommand.NotifyCanExecuteChanged(); // Kích hoạt lại button gửi
        }
    }
}
