using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BookingManagementSystem.Core.Models;
public class Payment : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public override string ToString() => $"Payment Id: {Id}, User Id: {UserId}," +
        $" Booking Id: {BookingId}, Amount: {Amount}, Payment Date: {PaymentDate}," +
        $" Payment Method: {PaymentMethod}, Payment Status: {PaymentStatus}";
    public int Id
    {
        get; set;
    }
    public int UserId
    {
        get; set;
    }
    public int BookingId
    {
        get; set;
    }
    public decimal Amount
    {
        get; set;
    }
    public DateTime PaymentDate
    {
        get; set;
    }
    public string PaymentMethod
    {
        get; set;
    } // E.g., Credit Card, Debit Card, PayPal
    public string PaymentStatus
    {
        get; set;
    } // E.g., Pending, Paid, Failed
}
