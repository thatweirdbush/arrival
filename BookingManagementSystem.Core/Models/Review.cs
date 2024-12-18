using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BookingManagementSystem.Core.Models;
public partial class Review : INotifyPropertyChanged
{
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
