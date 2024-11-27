using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagementSystem.Core.Contracts.Messengers;
public class ShowDialogMessage
{
    public string Title { get; set; }
    public string Content { get; set; }
}
