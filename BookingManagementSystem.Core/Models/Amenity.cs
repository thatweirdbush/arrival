using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BookingManagementSystem.Core.Models;
public class Amenity : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public override string ToString() => $"{Name} - {Description} - Is Available: {IsAvailable}";
    public int Id
    {
        get; set;
    }
    public string Name
    {
        get; set;
    }
    public string Description
    {
        get; set;
    }
    // E.g., 1, 2, 3, etc. Default is 1
    public int Quantity
    {
        get; set;
    } = 1;
    public bool IsAvailable
    {
        get; set;
    }
}
