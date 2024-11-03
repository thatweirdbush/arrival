using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Contracts.Services;

namespace BookingManagementSystem.ViewModels;

public partial class LoginViewModel : ObservableRecipient
{

    private readonly INavigationService _navigationService;
    private readonly IDao _dao;

    public IEnumerable<User> Users { get; private set; } = Enumerable.Empty<User>();

    public User CurrentUser { get; private set; }

    public LoginViewModel(INavigationService navigationService, IDao dao)
    {
        _navigationService = navigationService;
        _dao = dao;
        OnNavigatedTo(_dao);
    }

    public async void OnNavigatedTo(object parameter)
    {
        // Load user data list
        var users = await _dao.GetUserListDataAsync();
        Users = users;
    }

    public void OnNavigatedFrom()
    {
    }

    // Check if user has correct password for account
    public bool LoginAuthentication(string username, string password)
    {
        // Tìm người dùng có username tương ứng
        var user = Users.FirstOrDefault(u => u.Username.Equals(username));

        // Kiểm tra nếu tìm thấy user và password khớp
        if (user != null && user.Password.Equals(password))
        {
            CurrentUser = user;
            return true; // Đăng nhập thành công
        }

        return false; // Đăng nhập thất bại
    }
}
