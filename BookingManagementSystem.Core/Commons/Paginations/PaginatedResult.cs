using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagementSystem.Core.Commons.Paginations;
public class PaginatedResult<T>
{
    public IEnumerable<T> Items { get; set; } = [];
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
}
