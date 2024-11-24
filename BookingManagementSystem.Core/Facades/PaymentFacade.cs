using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Contracts.Facades;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Facades;
public class PaymentFacade : IPaymentFacade
{
    private readonly IRepository<Property> _propertyRepository;
    private readonly IRepository<Review> _reviewRepository;
    private readonly IRepository<DestinationTypeSymbol> _destinationTypeSymbolRepository;

    public PaymentFacade(
        IRepository<Property> propertyRepository, 
        IRepository<Review> reviewRepository, 
        IRepository<DestinationTypeSymbol> destinationTypeSymbolRepository)
    {
        _propertyRepository = propertyRepository;
        _reviewRepository = reviewRepository;
        _destinationTypeSymbolRepository = destinationTypeSymbolRepository;
    }
    public async Task<Property?> GetPropertyByIdAsync(int id) =>
        await _propertyRepository.GetByIdAsync(id);

    public async Task<IEnumerable<Review>> GetReviewsAsync() =>
        await _reviewRepository.GetAllAsync();

    public async Task<IEnumerable<DestinationTypeSymbol>> GetDestinationTypeSymbolsAsync() =>
        await _destinationTypeSymbolRepository.GetAllAsync();
}
