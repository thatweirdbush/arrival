using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Contracts.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Core.Contracts.Repositories;

namespace BookingManagementSystem.ViewModels.Account;
public partial class RecoverPasswordViewModel : ObservableRecipient
{
    private readonly INavigationService _navigationService;
    private readonly IRepository<User> _userRepository;

    public IEnumerable<User> Users { get; private set; } = Enumerable.Empty<User>();

    public RecoverPasswordViewModel(INavigationService navigationService, IRepository<User> userRepository)
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

    public bool RecoverPasswordAuthentication(string username, string password)
    {
        // Tìm người dùng có username tương ứng
        var user = Users.FirstOrDefault(u => u.Username.Equals(username));
        if (user != null)
        {
            user.Password = password;

            return true;
        }
        return false;
    }
}
