using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Contracts.Facades;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.Core.Facades;
#nullable enable
public class RentalDetailFacade : IRentalDetailFacade
{
    private readonly IRepository<Property> _propertyRepository;
    private readonly IRepository<Review> _reviewRepository;
    private readonly IRepository<QnA> _qnaRepository;
    private readonly IRepository<BadReport> _badReportRepository;

    public Property? Property { get; private set; }

    public RentalDetailFacade(
        IRepository<Property> propertyRepository,
        IRepository<Review> reviewRepository,
        IRepository<QnA> qnaRepository,
        IRepository<BadReport> badReportRepository)
    {
        _propertyRepository = propertyRepository;
        _reviewRepository = reviewRepository;
        _qnaRepository = qnaRepository;
        _badReportRepository = badReportRepository;
    }

    public async Task<Property?> GetPropertyByIdAsync(int id)
    {
        var query = _propertyRepository.GetQueryable();

        // Inlcude neccessary navigational properties
        query = query.Include(p => p.Country)
                     .Include(p => p.PropertyPolicies)
                     .Include(p => p.QnAs)
                     .Include(p => p.Reviews)
                        .ThenInclude(r => r.User)
                     .Include(p => p.PropertyAmenities)
                        .ThenInclude(pa => pa.Amenity);

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

    public Task<IEnumerable<PropertyAmenity>> GetPropertyAmenitiesAsync()
    {
        return Property != null
            ? Task.FromResult(Property.PropertyAmenities.AsEnumerable())
            : Task.FromResult(Enumerable.Empty<PropertyAmenity>());
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

    public async Task UpdateAsync(Property property)
    {
        await _propertyRepository.UpdateAsync(property);
        await _propertyRepository.SaveChangesAsync();
    }
}
