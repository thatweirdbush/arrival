using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementSystem.Core.Models;

public enum FAQCategory
{
    General,
    Booking,
    Payment,
    Pricing,
    Cancellations,
    PropertyPolicies,
}

public partial class FAQ
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Question { get; set; }

    public string Answer { get; set; }

    public FAQCategory FAQCategory { get; set; }
}
