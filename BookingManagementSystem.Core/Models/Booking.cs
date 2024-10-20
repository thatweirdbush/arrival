using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BookingManagementSystem.Core.Models;
public class Booking : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public override string ToString() => $"Id: {Id}, PropertyId: {PropertyId}, UserId: {UserId}, " +
        $"CheckInDate: {CheckInDate}, CheckOutDate: {CheckOutDate}, TotalPrice: {TotalPrice}, " +
        $"Status: {BookingStatus}, CreatedAt: {CreatedAt}, UpdatedAt: {UpdatedAt}";
    public int Id
    {
        get; set;
    }
    public int PropertyId
    {
        get; set;
    }
    public int UserId
    {
        get; set;
    }
    public DateTime CheckInDate
    {
        get; set;
    }
    public DateTime CheckOutDate
    {
        get; set;
    }
    public int DurationInDays
    {
        get
        {
            // Calculate the duration in days between CheckInDate and CheckOutDate
            var duration = (CheckOutDate - CheckInDate).Days;

            // If the check-in and check-out dates are the same, duration should be 1
            return duration == 0 ? 1 : duration;
        }
    }
    public decimal TotalPrice
    {
        get; set;
    }
    public string BookingStatus
    {
        get; set;
    } // E.g., Pending, Confirmed, Canceled
    public DateTime CreatedAt
    {
        get; set;
    }
    public DateTime UpdatedAt
    {
        get; set;
    }
}
