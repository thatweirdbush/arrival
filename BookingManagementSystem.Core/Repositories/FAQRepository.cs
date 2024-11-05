using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Repositories;
public class FAQRepository : Repository<FAQ>
{
    public FAQRepository()
    {
        _entities.AddRange(
        [
            new() { Id = 1,
                    Question = "What is the check-in time?",
                    Answer = "Check-in time is 3:00 PM",
                    FAQCategory = FAQCategory.General
            },
            new() { Id = 2,
                    Question = "What is the check-out time?",
                    Answer = "Check-out time is 11:00 AM",
                    FAQCategory = FAQCategory.General
            },
            new() { Id = 3,
                    Question = "Is breakfast included?",
                    Answer = "Yes, breakfast is included",
                    FAQCategory = FAQCategory.PropertyPolicies
            },
            new() { Id = 4,
                    Question = "Is there a gym?",
                    Answer = "Yes, there is a gym",
                    FAQCategory = FAQCategory.Booking
            },
            new() { Id = 5,
                    Question = "Is there a swimming pool?",
                    Answer = "Yes, there is a swimming pool",
                    FAQCategory = FAQCategory.Booking
            },
            new() { Id = 6,
                    Question = "What is the cancellation policy?",
                    Answer = "Free cancellation up to 24 hours before check-in",
                    FAQCategory = FAQCategory.Cancellations
            },
            new() { Id = 7,
                    Question = "What is the payment policy?",
                    Answer = "Payment is required at the time of booking",
                    FAQCategory = FAQCategory.Payment
            },
            new() { Id = 8,
                    Question = "What is the pet policy?",
                    Answer = "Pets are not allowed",
                    FAQCategory = FAQCategory.PropertyPolicies
            },
            new() { Id = 9,
                    Question = "What is the smoking policy?",
                    Answer = "Smoking is not allowed",
                    FAQCategory = FAQCategory.PropertyPolicies
            },
            new() { Id = 10,
                    Question = "Is there any fee for early check-in?",
                    Answer = "There is no fee for early check-in",
                    FAQCategory = FAQCategory.Pricing
            }
        ]);
    }
}
