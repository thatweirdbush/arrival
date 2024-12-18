using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Contracts.Facades;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.Core.Facades;
#nullable enable
public class RentalDetailFacade : IRentalDetailFacade
{
    private readonly IRepository<Property> _propertyRepository;
    private readonly IRepository<Review> _reviewRepository;
    private readonly IRepository<QnA> _qnaRepository;
    private readonly IRepository<PropertyPolicy> _propertyPolicyRepository;
    private readonly IRepository<BadReport> _badReportRepository;
    private readonly DestinationTypeSymbolRepository _destinationTypeSymbolRepository;

    public Property? Property { get; private set; }

    public RentalDetailFacade(
        IRepository<Property> propertyRepository,
        IRepository<Review> reviewRepository,
        IRepository<QnA> qnaRepository,
        IRepository<PropertyPolicy> propertyPolicyRepository,
        IRepository<BadReport> badReportRepository,
        DestinationTypeSymbolRepository destinationTypeSymbolRepository)
    {
        _propertyRepository = propertyRepository;
        _reviewRepository = reviewRepository;
        _qnaRepository = qnaRepository;
        _propertyPolicyRepository = propertyPolicyRepository;
        _badReportRepository = badReportRepository;
        _destinationTypeSymbolRepository = destinationTypeSymbolRepository;
    }

    public async Task<Property?> GetPropertyByIdAsync(int id)
    {
        var query = _propertyRepository.GetQueryable();

        // Inlcude neccessary navigational properties
        query = query.Include(p => p.Country)
                     .Include(p => p.PropertyAmenities)
                     .Include(p => p.PropertyPolicies)
                     .Include(p => p.QnAs)
                     .Include(p => p.Reviews)
                     .ThenInclude(r => r.User);

        Property = await query.FirstOrDefaultAsync(p => p.Id == id);
        return Property;
    }

    public Task<IEnumerable<Review>> GetReviewsAsync()
    {
        return Property != null
            ? Task.FromResult(Property.Reviews.AsEnumerable())
            : Task.FromResult(Enumerable.Empty<Review>());
    }

    public Task<IEnumerable<QnA>> GetQnAsAsync()
    {
        return Property != null
            ? Task.FromResult(Property.QnAs.AsEnumerable())
            : Task.FromResult(Enumerable.Empty<QnA>());
    }

    public Task<IEnumerable<Amenity>> GetPropertyAmenitiesAsync()
    {
        return Property != null
            ? Task.FromResult(Property.PropertyAmenities.Select(p => p.Amenity).AsEnumerable())
            : Task.FromResult(Enumerable.Empty<Amenity>());
    }

    public Task<IEnumerable<DestinationTypeSymbol>> GetDestinationTypeSymbolsAsync()
    {
        return Property != null
            ? _destinationTypeSymbolRepository.GetAllAsync(dts => Property.DestinationTypes.Contains(dts.DestinationType))
            : Task.FromResult(Enumerable.Empty<DestinationTypeSymbol>());
    }

    public Task<IEnumerable<PropertyPolicy>> GetPropertyPoliciesAsync()
    {
        return Property != null
            ? Task.FromResult(Property.PropertyPolicies.AsEnumerable())
            : Task.FromResult(Enumerable.Empty<PropertyPolicy>());
    }

    public async Task AddReviewAsync(Review review)
    {
        await _reviewRepository.AddAsync(review);
        await _reviewRepository.SaveChangesAsync();
    }

    public async Task AddQnAAsync(QnA qna)
    {
        await _qnaRepository.AddAsync(qna);
        await _qnaRepository.SaveChangesAsync();
    }

    public async Task AddBadReportAsync(BadReport badReport)
    {
        await _badReportRepository.AddAsync(badReport);
        await _badReportRepository.SaveChangesAsync();
    }
}
