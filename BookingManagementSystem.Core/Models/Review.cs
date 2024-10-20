using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BookingManagementSystem.Core.Models;
public class Review : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public override string ToString() => $"Id: {Id}, PropertyId: {PropertyId}," +
        $" UserId: {UserId}, Rating: {Rating}, Comment: {Comment}, CreatedAt: {CreatedAt}";    
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
    public int Rating
    {
        get; set;
    } // From 1 to 5
    public string Comment
    {
        get; set;
    }
    public DateTime CreatedAt
    {
        get; set;
    } = DateTime.Now;
    public DateTime UpdatedAt
    {
        get; set;
    }
}
