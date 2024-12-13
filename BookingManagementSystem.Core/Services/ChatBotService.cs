using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookingManagementSystem.Core.Services;
public class ChatBotService
{
    private readonly HttpClient _httpClient;
    //private const string BaseUrl = "https://api.generativeai.google/v1beta3/models/";
    //private const string Model = "gemini-1.5-flash";

    private readonly string _apiKey;

    private readonly List<string> _context;  // Lưu trữ ngữ cảnh cuộc trò chuyện

    public ChatBotService(string apiKey)
    {
        _httpClient = new HttpClient();
        _apiKey = apiKey;
        _context = new List<string>();
    }

    public async Task<string> AskAsync(string question)
    {
        // Thêm câu hỏi vào ngữ cảnh
        _context.Add("User: " + question);

        var requestBody = new
        {
            contents = new[]
            {
            new
                {
                    parts = new[]
                    {
                        new { text = string.Join("\n", _context) }  // Gửi toàn bộ ngữ cảnh cuộc trò chuyện
                    }
                }
            }
        };

        var jsonContent = JsonSerializer.Serialize(requestBody);
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri($"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={_apiKey}"),
            Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
        };

        try
        {
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<JsonDocument>(jsonResponse);

            // Truy xuất câu trả lời từ JSON
            if (result?.RootElement.TryGetProperty("candidates", out var candidatesProperty) == true)
            {
                var firstCandidate = candidatesProperty[0];
                var contentProperty = firstCandidate.GetProperty("content");
                var partsProperty = contentProperty.GetProperty("parts");
                var answer = partsProperty[0].GetProperty("text").GetString();

                // Thêm câu trả lời vào ngữ cảnh
                _context.Add("Bot: " + answer);

                return answer ?? "Không nhận được phản hồi từ bot.";
            }
            else
            {
                return "Không tìm thấy thông tin cần thiết trong phản hồi từ bot.";
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error: {ex.Message}");
            return "Đã xảy ra lỗi khi giao tiếp với bot.";
        }
    }


    private class ChatBotResponse
    {
        public Candidate[] Candidates
        {
            get; set;
        }
    }

    private class Candidate
    {
        public string Content
        {
            get; set;
        }
    }
}
