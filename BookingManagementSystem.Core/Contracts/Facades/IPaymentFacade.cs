using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Contracts.Facades;
public interface IPaymentFacade
{
    Task<Property?> GetPropertyByIdAsync(int id);
    Task<IEnumerable<Review>> GetReviewsAsync();
    Task<IEnumerable<DestinationTypeSymbol>> GetDestinationTypeSymbolsAsync();

}
