using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagementSystem.Core.Models;

public enum Role
{
    Guest,
    Host,
    Admin
}

public partial class User : INotifyPropertyChanged
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string FullName => $"{FirstName} {LastName}";

    public string ShortBio { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Address { get; set; }

    public string GovernmentId { get; set; }

    public Role Role { get; set; } = Role.Guest;

    public ICollection<string> Languages { get; set; } // Languages can be spoken by the user

    public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();

    public DateTime UpdatedAt { get; set; }

    public int YearsHosting => DateTime.Now.Year - CreatedAt.Year;

    public bool IsEliteHost { get; set; } = false;

    public string Username { get; set; }

    public string Password { get; set; }

    public string PasswordHash { get; set; }

    public virtual ICollection<BadReport> BadReportHandledByAdmins { get; set; } = [];

    public virtual ICollection<BadReport> BadReportUsers { get; set; } = [];

    public virtual ICollection<Booking> Bookings { get; set; } = [];

    public virtual ICollection<Notification> Notifications { get; set; } = [];

    public virtual ICollection<Payment> Payments { get; set; } = [];

    public virtual ICollection<Property> Properties { get; set; } = [];

    public virtual ICollection<QnA> QnACustomers { get; set; } = [];

    public virtual ICollection<QnA> QnAHosts { get; set; } = [];

    public virtual ICollection<Review> Reviews { get; set; } = [];

    public event PropertyChangedEventHandler PropertyChanged;
}
