using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Models;

using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Account;

public partial class SignupViewModel : ObservableRecipient
{
    private readonly INavigationService _navigationService;
    private readonly IRepository<User> _userRepository;

    public IEnumerable<User> Users { get; private set; } = Enumerable.Empty<User>();

    public SignupViewModel(INavigationService navigationService, IRepository<User> userRepository)
    {
        _navigationService = navigationService;
        _userRepository = userRepository;
        OnNavigatedTo(0);
    }

    public async void OnNavigatedTo(object parameter)
    {
        // Load user data list
        var users = await _userRepository.GetAllAsync();
        Users = users;
    }

    public void OnNavigatedFrom()
    {
    }

    public bool SignupAuthentication(string username, string password)
    {
        // Tìm người dùng có username tương ứng
        var user = Users.FirstOrDefault(u => u.Username.Equals(username));
        if (user == null)
        {
            user = new User
            {
                Username = username,
                Password = password
            };
            _userRepository.AddAsync(user);
            return true;
        }
        return false; // Đăng ky thất bại
    }
}

