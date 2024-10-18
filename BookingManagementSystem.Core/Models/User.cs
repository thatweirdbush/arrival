using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagementSystem.Core.Models;
public class User : INotifyPropertyChanged
{
    public string Username
    {
        get; set;
    }
    public string Password
    {
        get; set;
    }

    public event PropertyChangedEventHandler PropertyChanged;

}
