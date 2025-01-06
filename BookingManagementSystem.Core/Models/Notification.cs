using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagementSystem.Core.Models;
public partial class Notification : INotifyPropertyChanged
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int UserId { get; set; }

    public string SenderName { get; set; } = DefaultSenderName;

    public string Title { get; set; }

    public string Message { get; set; }

    public string ImagePath { get; set; } = DefaultThumbnail;

    public DateTime DateSent { get; set; } = DateTime.Now.ToUniversalTime();

    public bool IsRead { get; set; } = false;

    public virtual User User { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    public const string DefaultSenderName = "Arrival";
    public const string DefaultThumbnail = "ms-appx:///Assets/homescreen.jpg";

    // Override the equality methods for Contains() method usage
    public override bool Equals(object obj)
    {
        if (obj is not Notification other) return false;
        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}