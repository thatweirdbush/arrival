using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Contracts.Facades;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Facades;
#nullable enable
public class RentalDetailFacade : IRentalDetailFacade
{
    private readonly IRepository<Property> _propertyRepository;
    private readonly IRepository<Review> _reviewRepository;
    private readonly IRepository<QnA> _qnaRepository;
    private readonly IRepository<DestinationTypeSymbol> _destinationTypeSymbolRepository;
    private readonly IRepository<PropertyPolicy> _propertyPolicyRepository;
    private readonly IRepository<BadReport> _badReportRepository;

    public RentalDetailFacade(
        IRepository<Property> propertyRepository,
        IRepository<Review> reviewRepository,
        IRepository<QnA> qnaRepository,
        IRepository<DestinationTypeSymbol> destinationTypeSymbolRepository,
        IRepository<PropertyPolicy> propertyPolicyRepository,
        IRepository<BadReport> badReportRepository)
    {
        _propertyRepository = propertyRepository;
        _reviewRepository = reviewRepository;
        _qnaRepository = qnaRepository;
        _destinationTypeSymbolRepository = destinationTypeSymbolRepository;
        _propertyPolicyRepository = propertyPolicyRepository;
        _badReportRepository = badReportRepository;
    }

    public async Task<Property?> GetPropertyByIdAsync(int id) =>
        await _propertyRepository.GetByIdAsync(id);

    public async Task<IEnumerable<Review>> GetReviewsAsync() =>
        await _reviewRepository.GetAllAsync();

    public async Task<IEnumerable<QnA>> GetQnAsAsync() =>
        await _qnaRepository.GetAllAsync();

    public async Task<IEnumerable<DestinationTypeSymbol>> GetDestinationTypeSymbolsAsync() =>
        await _destinationTypeSymbolRepository.GetAllAsync();

    public async Task<IEnumerable<PropertyPolicy>> GetPropertyPoliciesAsync() =>
        await _propertyPolicyRepository.GetAllAsync();

    public async Task AddReviewAsync(Review review) =>
        await _reviewRepository.AddAsync(review);

    public async Task AddQnAAsync(QnA qna) =>
        await _qnaRepository.AddAsync(qna);

    public async Task AddBadReportAsync(BadReport badReport) =>
        await _badReportRepository.AddAsync(badReport);
}
