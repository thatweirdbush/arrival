using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagementSystem.Core.Models;

public enum QnAStatus
{
    Pending = 0,
    Approved = 1,
    Rejected = 2,
    Answered = 3
}

public partial class QnA
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Question { get; set; }

    public string Answer { get; set; }

    public int PropertyId { get; set; }

    public int CustomerId { get; set; }

    public int? HostId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();

    public QnAStatus Status { get; set; } = QnAStatus.Pending;

    public bool IsAnswered => Status == QnAStatus.Answered;

    public virtual User Customer { get; set; }

    public virtual User Host { get; set; }

    public virtual Property Property { get; set; }
}
