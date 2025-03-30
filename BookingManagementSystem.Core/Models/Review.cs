using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementSystem.Core.Models;
public partial class Review : INotifyPropertyChanged
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public int UserId { get; set; }

    public double Rating { get; set; }

    public string Comment { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();

    public DateTime? UpdatedAt { get; set; }

    public virtual Property Property { get; set; }

    public virtual User User { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
}
