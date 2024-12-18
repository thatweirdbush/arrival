using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BookingManagementSystem.Core.Models;

public enum BookingStatus
{
    Pending,
    Confirmed,
    Canceled
}

public partial class Booking : INotifyPropertyChanged
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public int UserId { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    // If the check-in and check-out dates are the same, duration should be 1
    public int DurationInDays => Math.Max((CheckOutDate - CheckInDate).Days, 1);

    public decimal TotalPrice { get; set; }

    public BookingStatus Status { get; set; } = BookingStatus.Pending;

    public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();

    public DateTime? UpdatedAt { get; set; }
    
    public virtual ICollection<Payment> Payments { get; set; } = [];

    public virtual Property Property { get; set; }

    public virtual User User { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
}
