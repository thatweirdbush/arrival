using CommunityToolkit.Mvvm.ComponentModel;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Core.Contracts.Repositories;

namespace BookingManagementSystem.ViewModels.Account;

public partial class LoginViewModel : ObservableRecipient
{   
    public static User? CurrentUser { get; private set; }

    public event Action<User>? UserLoggedIn;

    private readonly IRepository<User> _userRepository;

    public LoginViewModel(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    // Check if user has correct password for account
    public async Task<User?> LoginAuthentication(string username, string password)
    {
        CurrentUser = (await _userRepository
            .GetAllAsync(u => u.Username == username && u.Password == password))
            .FirstOrDefault();

        // Notify subscribers that a user has logged in
        if (CurrentUser != null) 
            UserLoggedIn?.Invoke(CurrentUser);

        return CurrentUser;
    }
}
