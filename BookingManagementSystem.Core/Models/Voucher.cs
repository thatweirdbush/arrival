using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagementSystem.Core.Models;
public partial class Voucher : INotifyPropertyChanged
{
    public int Id { get; set; }

    public string Code { get; set; }

    public int Quantity { get; set; }

    // Nullable when the voucher applies a percentage discount
    public decimal? DiscountPercentage { get; set; } = DefaultDiscountValue;

    // Nullable when the voucher applies a fixed amount discount
    public decimal? DiscountAmount { get; set; } = DefaultDiscountValue;

    // Nullable when there is no minimum order value
    public decimal? MinimumOrderValue { get; set; }

    // Maximum discount value applicable (e.g., cap the discount at $50 even if percentage discount applies)
    // Nullable when there is no maximum discount value
    public decimal? MaxDiscountValue { get; set; }

    public DateTime ValidFrom { get; set; } = DateTime.Now.ToUniversalTime();

    public DateTime? ValidUntil { get; set; } // Nullable when the voucher does not expire

    public bool IsValid => DateTime.Now >= ValidFrom && DateTime.Now <= ValidUntil && !IsUsed;

    public bool IsUsed { get; set; } = false;

    public const decimal DefaultDiscountValue = 0.0m;

    public event PropertyChangedEventHandler PropertyChanged;
}
