using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BookingManagementSystem.Core.Models;
public class PropertyPolicy : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public override string ToString() => $"{Name} - {Description} - Is Mandatory: {IsMandatory}";
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
    public bool IsMandatory
    {
        get; set;
    }  // True if it's mandatory to follow, False otherwise
}
