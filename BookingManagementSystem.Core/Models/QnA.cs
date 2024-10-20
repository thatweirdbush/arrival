using System;
using System.Collections.Generic;
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
public class QnA
{
    public int Id
    {
        get; set;
    }
    public string Question
    {
        get; set;
    }
    public string Answer
    {
        get; set;
    }
    public int PropertyId
    {
        get; set;
    }
    public int CustomerId
    {
        get; set;
    }
    public int HostId
    {
        get; set;
    }
    public DateTime CreatedAt
    {
        get; set;
    }
    public QnAStatus Status
    {
        get; set;
    } // 0: Pending, 1: Approved, 2: Rejected, 3: Answered
    public bool IsAnswered => Status == QnAStatus.Answered;
}
