﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Contracts.Facades;
#nullable enable
public interface IRentalDetailFacade
{
    Task<Property?> GetPropertyByIdAsync(int id);
    Task<IEnumerable<Review>> GetReviewsAsync();
    Task<IEnumerable<QnA>> GetQnAsAsync();
    Task<IEnumerable<PropertyAmenity>> GetPropertyAmenitiesAsync();
    Task<IEnumerable<PropertyPolicy>> GetPropertyPoliciesAsync();
    Task AddReviewAsync(Review review);
    Task AddQnAAsync(QnA qna);
    Task AddBadReportAsync(BadReport badReport);
    Task UpdateAsync(Property property);
}
