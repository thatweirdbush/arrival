using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagementSystem.Core.Models;
public class Voucher : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public override string ToString() => $"Voucher Id: {Id}, Code: {Code}, Quantity: {Quantity}, " +
        $"Discount Percentage: {DiscountPercentage}%, Discount Amount: {DiscountAmount}, " +
        $"Minimum Order Value: {MinimumOrderValue}, Max Discount Value: {MaxDiscountValue}, " +
        $"Valid From: {ValidFrom}, Valid Until: {ValidUntil}, Is Used: {IsUsed}";

    // Unique identifier of the voucher
    public int Id
    {
        get; set;
    }

    // Unique code of the voucher
    public string Code
    {
        get; set;
    }

    // Quantity of voucher can be use
    public int Quantity
    {
        get; set; 
    }

    // Percentage discount applied by the voucher
    // Nullable if the voucher applies a percentage discount
    public decimal? DiscountPercentage
    {
        get; set;
    } = 0.0m; // Default discount percentage is 0

    // Fixed amount discount applied by the voucher
    // Nullable if the voucher applies a fixed amount discount
    public decimal? DiscountAmount
    {
        get; set;
    } = 0.0m; // Default discount amount is 0

    // Minimum order value required to apply the voucher
    // Nullable if there is no minimum order value
    public decimal? MinimumOrderValue
    {
        get; set;
    }

    // Maximum discount value applicable (e.g., cap the discount at $50 even if percentage discount applies)
    // Nullable if there is no maximum discount value
    public decimal? MaxDiscountValue
    {
        get; set;
    }

    // Date when the voucher becomes valid
    public DateTime ValidFrom
    {
        get; set;
    } = DateTime.Now.ToUniversalTime();   // Default valid from the current date and time

    // Date when the voucher expires
    // Nullable if the voucher does not expire
    public DateTime? ValidUntil
    {
        get; set;
    }

    // Indicates whether the voucher is valid for use
    public bool IsValid => DateTime.Now >= ValidFrom && DateTime.Now <= ValidUntil && !IsUsed;

    // Indicates whether the voucher has been used
    public bool IsUsed
    {
        get; set;
    } = false;    
}
