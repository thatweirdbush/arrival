using BookingManagementSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.Core.Repositories;
public class QnARepository : Repository<QnA>
{
    public QnARepository(DbContext context) : base(context)
    {
    }
}
