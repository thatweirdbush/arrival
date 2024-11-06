using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Repositories;
public class QnARepository : Repository<QnA>
{
    public QnARepository()
    {
        _entities.AddRange(
        [
            new() { Id = 1,
                    Question = "Do they serve breakfast?",
                    Answer = "There are breakfast options available.",
                    PropertyId = 1,
                    CustomerId = 1,
                    HostId = 2,
                    CreatedAt = DateTime.Now,
                    Status = QnAStatus.Answered
            },
            new() { Id = 2,
                    Question = "How do i book with breakfast?",
                    Answer = "Dear Guest\r\nWhen you make a booking you can choose included breakfast or only room.\r\nAnd also we have restaurant open 7.00 A.M. - 19.00 P.M.\r\nRegards\r\nMr.Key",
                    PropertyId = 2,
                    CustomerId = 2,
                    HostId = 3,
                    CreatedAt = DateTime.Now,
                    Status = QnAStatus.Answered
            },
            new() { Id = 3,
                    Question = "Can I park there?",
                    Answer = "This is what Lamphuhouse Bangkok - SHA Extra Plus Certified says about parking:\r\nNo parking available.",
                    PropertyId = 3,
                    CustomerId = 3,
                    HostId = 4,
                    CreatedAt = DateTime.Now,
                    Status = QnAStatus.Answered
            },
            new() { Id = 4,
                    Question = "Are there rooms with a hot tub?",
                    Answer = "No we don't have rooms with a hot tub.",
                    PropertyId = 4,
                    CustomerId = 4,
                    HostId = 5,
                    CreatedAt = DateTime.Now,
                    Status = QnAStatus.Answered
            },
            new() { Id = 5,
                    Question = "What are the check-in and check-out times?",
                    Answer = "✓ Check-in from 14:00 ✓ Check-out until 11:30\r\nIf you'd like to request an early or late check-in or check-out, you can make a special request when you book.\r\nNote: Special requests can't be guaranteed. If early or late check-in or check-out is essential to your travel plans, check the cancellation options before booking.",
                    PropertyId = 5,
                    CustomerId = 5,
                    HostId = 1,
                    CreatedAt = DateTime.Now,
                    Status = QnAStatus.Answered
            },
            new() { Id = 6,
                    Question = "Hi there just wondering if  we can smoke on Balcony?",
                    Answer = "Dear Guest\r\nYou can smoke on balcony but in the room not allow.\r\nRegards\r\nMr.Key",
                    PropertyId = 6,
                    CustomerId = 6,
                    HostId = 6,
                    CreatedAt = DateTime.Now,
                    Status = QnAStatus.Answered
            }
        ]);
    }
}
