using System.Collections.Specialized;

namespace BookingManagementSystem.Contracts.Services;

public interface IAppNotificationService
{
    void Initialize();

    bool Show(string payload);

    public bool ShowNotification(string title, string message, string? imageUri = null, List<(string Content, string Arguments)>? buttons = null);

    NameValueCollection ParseArguments(string arguments);

    void Unregister();
}
