using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Contracts.Facades;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Core.Repositories;

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

    public Task<Property?> GetPropertyByIdAsync(int id)
    {
        return _propertyRepository.GetByIdAsync(id);
    }

    public Task<IEnumerable<Review>> GetReviewsAsync()
    {
        return _reviewRepository.GetAllAsync();
    }

    public Task<IEnumerable<QnA>> GetQnAsAsync()
    {
        return _qnaRepository.GetAllAsync();
    }

    public Task<IEnumerable<DestinationTypeSymbol>> GetDestinationTypeSymbolsAsync()
    {
        return _destinationTypeSymbolRepository.GetAllAsync();
    }

    public Task<IEnumerable<PropertyPolicy>> GetPropertyPoliciesAsync()
    {
        return _propertyPolicyRepository.GetAllAsync();
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
