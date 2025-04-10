﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementSystem.Core.Models;

public enum PaymentStatus
{
    Pending,
    Paid,
    Failed,
    Refunded
}

public partial class Payment : INotifyPropertyChanged
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int UserId { get; set; }

    public int BookingId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public string PaymentMethod { get; set; } = "Credit Card"; // E.g., Credit Card, Debit Card, PayPal

    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

    public virtual Booking Booking { get; set; }

    public virtual User User { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
}
