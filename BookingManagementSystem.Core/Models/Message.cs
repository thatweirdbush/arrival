using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagementSystem.Core.Models;
public class Message : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public override string ToString() => base.ToString();

    public string Content
    {
        get; set;
    }
    public bool IsUserMessage
    {
        get; set;
    }

    public DateTime Timestamp
    {
        get; set;
    }
}
