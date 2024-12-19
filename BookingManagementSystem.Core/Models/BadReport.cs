using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagementSystem.Core.Models;

public enum ReportStatus
{
    Pending,
    Approved,
    Rejected
}

public enum EntityType
{
    Property,
    User,
    Review
}

public partial class BadReport
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int EntityId { get; set; }

    public DateTime ReportDate { get; set; } = DateTime.Now.ToUniversalTime();

    public string ReportReason { get; set; }

    public string Description { get; set; }

    public ReportStatus Status { get; set; } = ReportStatus.Pending;

    public EntityType EntityType { get; set; } = EntityType.Property;

    public int? HandledByAdminId { get; set; }

    public DateTime? HandledDate { get; set; }

    public string AdminNotes { get; set; }

    public virtual User HandledByAdmin { get; set; }

    public virtual User User { get; set; }
}
