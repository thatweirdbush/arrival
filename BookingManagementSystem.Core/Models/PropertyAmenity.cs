using System.ComponentModel;

namespace BookingManagementSystem.Core.Models;
public partial class PropertyAmenity : INotifyPropertyChanged
{
    public int PropertyId { get; set; }

    public int AmenityId { get; set; }

    public int Quantity { get; set; }

    public virtual Amenity Amenity { get; set; }

    public virtual Property Property { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
}
