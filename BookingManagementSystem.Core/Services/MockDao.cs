using System.Xml.Linq;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Services;
public class MockDao : IDao
{
    private static List<Amenity> _allAmenities;
    private static List<Booking> _allBookings;
    private static List<FAQ> _allFAQs;
    private static List<Notification> _allNotifications;
    private static List<Payment> _allPayments;
    private static List<Property> _allProperties;
    private static List<PropertyPolicy> _allPropertyPolicies;
    private static List<QnA> _allQnAs;
    private static List<Review> _allReviews;
    private static List<User> _allUsers;

    private static List<Smartphone> _allSmartphones;
    private static List<DestinationTypeSymbol> _allDestinationTypeSymbols;

    public MockDao()
    {
    }

    private static List<Amenity> AllAmenities()
    {
        return new List<Amenity>
        {
            new() { Id = 1,
                    Name = "Free Wi-Fi",
                    Description = "High-speed internet"
            },
            new() { Id = 2,
                    Name = "Free Parking",
                    Description = "On-site parking"
            },
            new() { Id = 3,
                    Name = "Swimming Pool",
                    Description = "Outdoor pool"
            },
            new() { Id = 4,
                    Name = "Gym",
                    Description = "Fitness center"
            },
            new() { Id = 5,
                    Name = "Restaurant",
                    Description = "On-site restaurant"
            }
        };
    }

    private static List<Booking> AllBookings()
    {
        return new List<Booking>
        {
            new() { Id = 1,
                    PropertyId = 1,
                    UserId = 1,
                    CheckInDate = DateTime.Now,
                    CheckOutDate = DateTime.Now.AddDays(3),
                    TotalPrice = 299.9M
            },
            new() { Id = 2,
                    PropertyId = 2,
                    UserId = 2,
                    CheckInDate = DateTime.Now,
                    CheckOutDate = DateTime.Now.AddDays(2),
                    TotalPrice = 199.9M
            },
            new() { Id = 3,
                    PropertyId = 3,
                    UserId = 3,
                    CheckInDate = DateTime.Now,
                    CheckOutDate = DateTime.Now.AddDays(1),
                    TotalPrice = 49.9M
            },
            new() { Id = 4,
                    PropertyId = 4,
                    UserId = 4,
                    CheckInDate = DateTime.Now,
                    CheckOutDate = DateTime.Now.AddDays(4),
                    TotalPrice = 99.9M
            },
            new() { Id = 5,
                    PropertyId = 5,
                    UserId = 5,
                    CheckInDate = DateTime.Now,
                    CheckOutDate = DateTime.Now.AddDays(5),
                    TotalPrice = 69.9M
            }
        };
    }

    private static List<FAQ> AllFAQs()
    {
        return new List<FAQ>
        {
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
        };
    }

    private static List<Notification> AllNotifications()
    {
        return new List<Notification>
        {
            new() { Id = 1,
                    UserId = 1,
                    Message = "Your booking has been confirmed",
                    DateSent = DateTime.Now
            },
            new() { Id = 2,
                    UserId = 2,
                    Message = "Your booking has been canceled",
                    DateSent = DateTime.Now
            },
            new() { Id = 3,
                    UserId = 3,
                    Message = "Your booking is pending", DateSent
                    = DateTime.Now
            },
            new() { Id = 4,
                    UserId = 4,
                    Message = "Your booking has been confirmed",
                    DateSent = DateTime.Now
            },
            new() { Id = 5,
                    UserId = 5,
                    Message = "Your booking has been canceled",
                    DateSent = DateTime.Now
            }
        };
    }

    private static List<Payment> AllPayments()
    {
        return new List<Payment>
        {
            new() { Id = 1,
                    UserId = 1,
                    BookingId = 1,
                    Amount = 299.9M,
                    PaymentDate = DateTime.Now
            },
            new() { Id = 2,
                    UserId = 2,
                    BookingId = 2,
                    Amount = 199.9M,
                    PaymentDate = DateTime.Now
            },
            new() { Id = 3,
                    UserId = 3,
                    BookingId = 3,
                    Amount = 49.9M,
                    PaymentDate = DateTime.Now
            },
            new() { Id = 4,
                    UserId = 4,
                    BookingId = 4,
                    Amount = 99.9M,
                    PaymentDate = DateTime.Now
            },
            new() { Id = 5,
                    UserId = 5,
                    BookingId = 5,
                    Amount = 69.9M,
                    PaymentDate = DateTime.Now
            }
        };
    }

    private static List<Property> AllProperties()
    {
        return new List<Property>
        {
            new()
            {
                Id = 1,
                Name = "Cozy room in Montmartre",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths = new List<string>
                {
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-0.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-1.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-2.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-3.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-4.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-5.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-6.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-7.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-8.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-9.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-10.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-11.jpg"
                },                    
                Location = "Paris, France",
                Capacity = 2,
                PricePerNight = 99.9M,
                IsFavourite = true,
                IsAvailable = true,
                Latitude = 47.6062,
                Longitude = -122.3321,
                IsPriority = true
            },
            new()
            {
                Id = 2,
                Name = "Property 2",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths = new List<string>
                {
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-1.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-0.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-2.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-3.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-4.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-5.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-6.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-7.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-8.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-9.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-10.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-11.jpg"
                },  
                Location = "Location 2",
                Capacity = 4,
                PricePerNight = 199.9M,
                IsFavourite = false,
                IsAvailable = true,
                Latitude = 147.6062,
                Longitude = -22.3321,
                IsPriority = true
            },
            new()
            {
                Id = 3,
                Name = "Property 3",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths = new List<string>
                {
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-2.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-0.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-1.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-3.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-4.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-5.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-6.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-7.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-8.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-9.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-10.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-11.jpg"
                },
                Location = "Location 3",
                Capacity = 6,
                PricePerNight = 299.9M,
                IsFavourite = true,
                IsAvailable = true,
                Latitude = 4.6062,
                Longitude = -1.3321
            },
            new()
            {
                Id = 4,
                Name = "Property 4",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths = new List<string>
                {
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-3.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-0.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-1.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-2.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-4.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-5.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-6.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-7.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-8.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-9.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-10.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-11.jpg"
                },
                Location = "Location 4",
                Capacity = 8,
                PricePerNight = 399.9M,
                IsFavourite = false,
                IsAvailable = true,
                Latitude = 67.6062,
                Longitude = -52.3321,
                IsPriority = true
            },
            new()
            {
                Id = 5,
                Name = "Property 5",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths = new List<string>
                {
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-4.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-0.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-1.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-2.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-3.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-5.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-6.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-7.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-8.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-9.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-10.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-11.jpg"
                },
                Location = "Location 5",
                Capacity = 10,
                PricePerNight = 499.9M,
                IsFavourite = false,
                IsAvailable = true,
                Latitude = 37.6062,
                Longitude = -50.3321
            },
            new()
            {
                Id = 6,
                Name = "Property 6",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths = new List<string>
                {
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-5.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-0.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-1.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-2.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-3.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-4.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-6.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-7.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-8.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-9.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-10.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-11.jpg"
                },
                Location = "Location 6",
                Capacity = 12,
                PricePerNight = 599.9M,
                IsFavourite = true,
                IsAvailable = true,
                Latitude = 57.6062,
                Longitude = -42.3321
            },
            new()
            {
                Id = 7,
                Name = "Property 7",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths = new List<string>
                {
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-6.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-0.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-1.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-2.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-3.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-4.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-5.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-7.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-8.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-9.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-10.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-11.jpg"
                },
                Location = "Location 7",
                Capacity = 14,
                PricePerNight = 699.9M,
                IsFavourite = false,
                IsAvailable = true,
                Latitude = 77.6062,
                Longitude = -32.3321
            },
            new()
            {
                Id = 8,
                Name = "Property 8",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths = new List<string>
                {
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-7.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-0.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-1.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-2.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-3.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-4.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-5.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-6.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-8.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-9.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-10.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-11.jpg"
                },
                Location = "Location 8",
                Capacity = 16,
                PricePerNight = 799.9M,
                IsFavourite = true,
                IsAvailable = true,
                Latitude = 87.6062,
                Longitude = -12.3321
            },
            new()
            {
                Id = 9,
                Name = "Property 9",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths = new List<string>
                {
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-8.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-0.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-1.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-2.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-3.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-4.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-5.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-6.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-7.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-9.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-10.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-11.jpg"
                },
                Location = "Location 9",
                Capacity = 18,
                PricePerNight = 899.9M,
                IsFavourite = false,
                IsAvailable = true,
                Latitude = 97.6062,
                Longitude = -22.3321
            },
            new()
            {
                Id = 10,
                Name = "Property 10",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths = new List<string>
                {
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-9.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-0.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-1.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-2.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-3.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-4.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-5.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-6.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-7.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-8.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-10.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-11.jpg"
                },
                Location = "Location 10",
                Capacity = 20,
                PricePerNight = 999.9M,
                IsFavourite = true,
                IsAvailable = true,
                Latitude = 107.6062,
                Longitude = -32.3321
            },
            new()
            {
                Id = 11,
                Name = "Property 11",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths = new List<string>
                {
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-10.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-0.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-1.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-2.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-3.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-4.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-5.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-6.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-7.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-8.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-9.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-11.jpg"
                },
                Location = "Location 11",
                Capacity = 22,
                PricePerNight = 1099.9M,
                IsFavourite = false,
                IsAvailable = true,
                Latitude = 117.6062,
                Longitude = -42.3321
            },
            new()
            {
                Id = 12,
                Name = "Property 12",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths = new List<string>
                {
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-11.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-0.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-1.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-2.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-3.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-4.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-5.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-6.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-7.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-8.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-9.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-10.jpg"
                },
                Location = "Location 12",
                Capacity = 24,
                PricePerNight = 1199.9M,
                IsFavourite = true,
                IsAvailable = true,
                Latitude = 127.6062,
                Longitude = -52.3321
            },
            new()
            {
                Id = 13,
                Name = "Property 13",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths = new List<string>
                {
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-0.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-1.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-2.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-3.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-4.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-5.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-6.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-7.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-8.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-9.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-10.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-11.jpg"
                },
                Location = "Location 13",
                Capacity = 26,
                PricePerNight = 1299.9M,
                IsFavourite = false,
                IsAvailable = true,
                Latitude = 137.6062,
                Longitude = -62.3321
            },
            new()
            {
                Id = 14,
                Name = "Property 14",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths = new List<string>
                {
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-1.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-0.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-2.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-3.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-4.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-5.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-6.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-7.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-8.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-9.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-10.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-11.jpg"
                },
                Location = "Location 14",
                Capacity = 28,
                PricePerNight = 1399.9M,
                IsFavourite = true,
                IsAvailable = true,
                Latitude = 147.6062,
                Longitude = -72.3321
            },
            new()
            {
                Id = 15,
                Name = "Property 15",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths = new List<string>
                {
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-2.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-0.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-1.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-3.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-4.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-5.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-6.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-7.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-8.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-9.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-10.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-11.jpg"
                },
                Location = "Location 15",
                Capacity = 30,
                PricePerNight = 1499.9M,
                IsFavourite = false,
                IsAvailable = true,
                Latitude = 157.6062,
                Longitude = -82.3321
            }
        };
    }

    private static List<PropertyPolicy> AllPropertyPolicies()
    {
        return new List<PropertyPolicy>
        {
            new() { Id = 1,
                    Name = "No smoking",
                    Description = "Smoking is not allowed",
                    IsMandatory = true
            },
            new() { Id = 2,
                    Name = "No pets",
                    Description = "Pets are not allowed",
                    IsMandatory = true
            },
            new() { Id = 3,
                    Name = "No parties",
                    Description = "Parties are not allowed",
                    IsMandatory = true
            },
            new() { Id = 4,
                    Name = "No loud music",
                    Description = "Loud music is not allowed",
                    IsMandatory = false
            },
            new() { Id = 5,
                    Name = "No outside food",
                    Description = "Outside food is not allowed",
                    IsMandatory = false
            },
            new() { Id = 6,
                    Name = "No outside drinks",
                    Description = "Outside drinks are not allowed",
                    IsMandatory = false
            },
            new() { Id = 7,
                    Name = "No smoking in rooms",
                    Description = "Smoking is not allowed in rooms",
                    IsMandatory = true
            },
            new() { Id = 8,
                    Name = "No pets in rooms",
                    Description = "Pets are not allowed in rooms",
                    IsMandatory = true
            },
            new() { Id = 9,
                    Name = "No parties in rooms",
                    Description = "Parties are not allowed in rooms",
                    IsMandatory = true
            },
            new() { Id = 10,
                     Name = "No loud music in rooms",
                     Description = "Loud music is not allowed in rooms",
                     IsMandatory = false
            }
        };
    }

    private static List<QnA> AllQnAs()
    {
        return new List<QnA>
        {
            new() { Id = 1,
                    Question = "Question 1",
                    Answer = "Answer 1",
                    PropertyId = 1,
                    CustomerId = 1,
                    HostId = 2,
                    CreatedAt = DateTime.Now,
                    Status = QnAStatus.Answered
            },
            new() { Id = 2,
                    Question = "Question 2",
                    Answer = "Answer 2",
                    PropertyId = 2,
                    CustomerId = 2,
                    HostId = 3,
                    CreatedAt = DateTime.Now,
                    Status = QnAStatus.Answered
            },
            new() { Id = 3,
                    Question = "Question 3",
                    Answer = "Answer 3",
                    PropertyId = 3,
                    CustomerId = 3,
                    HostId = 4,
                    CreatedAt = DateTime.Now,
                    Status = QnAStatus.Answered
            },
            new() { Id = 4,
                    Question = "Question 4",
                    Answer = "Answer 4",
                    PropertyId = 4,
                    CustomerId = 4,
                    HostId = 5,
                    CreatedAt = DateTime.Now,
                    Status = QnAStatus.Answered
            },
            new() { Id = 5,
                    Question = "Question 5",
                    Answer = "Answer 5",
                    PropertyId = 5,
                    CustomerId = 5,
                    HostId = 1,
                    CreatedAt = DateTime.Now,
                    Status = QnAStatus.Answered
            }
        };
    }

    private static List<Review> AllReviews()
    {
        return new List<Review>
        {
            new() { Id = 1,
                    PropertyId = 1,
                    UserId = 1,
                    Rating = 5,
                    Comment = "Excellent property",
                    CreatedAt = DateTime.Now
            },
            new() { Id = 2,
                    PropertyId = 2,
                    UserId = 2,
                    Rating = 4,
                    Comment = "Great property",
                    CreatedAt = DateTime.Now
            },
            new() { Id = 3,
                    PropertyId = 3,
                    UserId = 3,
                    Rating = 3,
                    Comment = "Good property",
                    CreatedAt = DateTime.Now
            },
            new() { Id = 4,
                    PropertyId = 4,
                    UserId = 4,
                    Rating = 2,
                    Comment = "Average property",
                    CreatedAt = DateTime.Now
            },
            new() { Id = 5,
                    PropertyId = 5,
                    UserId = 5,
                    Rating = 1,
                    Comment = "Poor property",
                    CreatedAt = DateTime.Now
            }
        };
    }

    private static List<User> AllUsers()
    {
        return new List<User>
        {
            new() { Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    ShortBio = "My name is John Doe",
                    Email = "johndoe@gmail.com",
                    Phone = "123-456-7890",
                    Address = "123 Main St",
                    GovernmentId = "1234567890",
                    Role = Role.Host,
                    CreatedAt = DateTime.Now,
                    Username = "johndoe",
                    Password = "password"
            },
            new() { Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    ShortBio = "My name is Jane Smith",
                    Email = "janesmith@gmail.com",
                    Phone = "234-567-8901",
                    Address = "234 Main St",
                    GovernmentId = "2345678901",
                    Role = Role.Guest,
                    CreatedAt = DateTime.Now,
                    Username = "janesmith",
                    Password = "password"
                },
            new() { Id = 3,
                    FirstName = "Alice",
                    LastName = "Johnson",
                    ShortBio = "My name is Alice Johnson",
                    Email = "alicejs@gmail.com",
                    Phone = "345-678-9012",
                    Address = "345 Main St",
                    GovernmentId = "3456789012",
                    Role = Role.Host,
                    CreatedAt = DateTime.Now,
                    Username = "alicejohnson",
                    Password = "password"
                },
            new() { Id = 4,
                    FirstName = "Bob",
                    LastName = "Brown",
                    ShortBio = "My name is Bob Brown",
                    Email = "bobbrown@gmail.com",
                    Phone = "456-789-0123",
                    Address = "456 Main St",
                    GovernmentId = "4567890123",
                    Role = Role.Guest,
                    CreatedAt = DateTime.Now,
                    Username = "bobbrown",
                    Password = "password"
                },
            new() { Id = 5,
                    FirstName = "Eve",
                    LastName = "White",
                    ShortBio = "My name is Eve White",
                    Email = "evewhite@gmail.com",
                    Phone = "567-890-1234",
                    Address = "567 Main St",
                    GovernmentId = "5678901234",
                    Role = Role.Host,
                    CreatedAt = DateTime.Now,
                    Username = "evewhite",
                    Password = "password"
                },
            new() { Id = 6,
                    FirstName = "Admin",
                    LastName = "Admin",
                    ShortBio = "My name is Admin",
                    Email = "admin@gmail.com",
                    Phone = "678-901-2345",
                    Address = "678 Main St",
                    GovernmentId = "6789012345",
                    Role = Role.Admin,
                    CreatedAt = DateTime.Now,
                    Username = "admin",
                    Password = "password"
                },
            new() { Id = 7,
                    FirstName = "Host",
                    LastName = "Host",
                    ShortBio = "My name is Host",
                    Email = "host@gmail.com",
                    Phone = "789-012-3456",
                    Address = "789 Main St",
                    GovernmentId = "7890123456",
                    Role = Role.Host,
                    CreatedAt = DateTime.Now,
                    Username = "host",
                    Password = "password"
                },
            new() { Id = 8,
                    FirstName = "Guest",
                    LastName = "Guest",
                    ShortBio = "My name is Guest",
                    Email = "guest@gmail.com",
                    Phone = "890-123-4567",
                    Address = "890 Main St",
                    GovernmentId = "8901234567",
                    Role = Role.Guest,
                    CreatedAt = DateTime.Now,
                    Username = "guest",
                    Password = "password"
                },
            new() { Id = 9,
                    FirstName = "Elite",
                    LastName = "Host",
                    ShortBio = "My name is Elite Host",
                    Email = "elite@gmail.com",
                    Phone = "901-234-5678",
                    Address = "901 Main St",
                    GovernmentId = "9012345678",
                    Role = Role.Host,
                    CreatedAt = DateTime.Now,
                    Username = "elitehost",
                    Password = "password"
                },
            new() { Id = 10,
                    FirstName = "Elite Two",
                    LastName = "Host",
                    ShortBio = "My name is Elite Host",
                    Email = "elite2@gmail.com",
                    Phone = "012-345-6789",
                    Address = "012 Main St",
                    GovernmentId = "0123456789",
                    Role = Role.Host,
                    CreatedAt = DateTime.Now,
                    Username = "elitehost2",
                    Password = "password"
                }
        };
    }

    private static List<Smartphone> AllSmartphones()
    {
        return new List<Smartphone>
        {
            new Smartphone(){
            Id = 0,
            Name = "iPhone 16 Pro Max",
            Manufacturer = "Apple",
            Price = 2099,
            ImagePath = "iphone-16-pro-max.png"
            },
            new Smartphone(){
                Id = 1,
                Name = "iPhone 15 Pro Max",
                Manufacturer = "Apple",
                Price = 2999,
                ImagePath = "iphone-15-pro-max-titanium.png"
            },
            new Smartphone(){
                Id = 2,
                Name = "Galaxy S24 Ultra",
                Manufacturer = "Samsung",
                Price = 1999,
                ImagePath = "s24-ultra.png"
            },
            new Smartphone(){
                Id = 3,
                Name = "Xiaomi 14 Ultra",
                Manufacturer = "Xiaomi",
                Price = 1499,
                ImagePath = "xiaomi-14-ultra.png"
            },
            new Smartphone(){
                Id = 4,
                Name = "Pixel 9 Pro",
                Manufacturer = "Google",
                Price = 1799,
                ImagePath = "pixel-9-pro.png"
            },
            new Smartphone(){
                Id = 5,
                Name = "OnePlus 12",
                Manufacturer = "OnePlus",
                Price = 1599,
                ImagePath = "oneplus-12.png"
            },
            new Smartphone(){
                Id = 6,
                Name = "Find X7 Ultra",
                Manufacturer = "OPPO",
                Price = 1699,
                ImagePath = "oppo-find-x7-ultra.png"
            },
            new Smartphone(){
                Id = 7,
                Name = "X100 Pro",
                Manufacturer = "Vivo",
                Price = 1799,
                ImagePath = "vivo-x100-pro.png"
            },
            new Smartphone(){
                Id = 8,
                Name = "iPhone 14",
                Manufacturer = "Apple",
                Price = 1499,
                ImagePath = "iphone-14-pink.png"
            },
            new Smartphone(){
                Id = 9,
                Name = "iPhone 16 Plus",
                Manufacturer = "Apple",
                Price = 1299,
                ImagePath = "iphone-16-plus.png"
            },
            new Smartphone(){
                Id = 10,
                Name = "iPhone 16 Pro Max",
                Manufacturer = "Apple",
                Price = 2099,
                ImagePath = "iphone-16-pro-max.png"
            },
            new Smartphone(){
                Id = 6,
                Name = "Find X7 Ultra",
                Manufacturer = "OPPO",
                Price = 1699,
                ImagePath = "oppo-find-x7-ultra.png"
            },
            new Smartphone(){
                Id = 1,
                Name = "iPhone 15 Pro Max",
                Manufacturer = "Apple",
                Price = 2999,
                ImagePath = "iphone-15-pro-max-titanium.png"
            },
            new Smartphone(){
                Id = 6,
                Name = "Find X7 Ultra",
                Manufacturer = "OPPO",
                Price = 1699,
                ImagePath = "oppo-find-x7-ultra.png"
            },
            new Smartphone(){
                Id = 6,
                Name = "Find X7 Ultra",
                Manufacturer = "OPPO",
                Price = 1699,
                ImagePath = "oppo-find-x7-ultra.png"
            },
            new Smartphone(){
                Id = 1,
                Name = "iPhone 15 Pro Max",
                Manufacturer = "Apple",
                Price = 2999,
                ImagePath = "iphone-15-pro-max-titanium.png"
            },
            new Smartphone(){
                Id = 6,
                Name = "Find X7 Ultra",
                Manufacturer = "OPPO",
                Price = 1699,
                ImagePath = "oppo-find-x7-ultra.png"
            },
            new Smartphone(){
                Id = 1,
                Name = "iPhone 15 Pro Max",
                Manufacturer = "Apple",
                Price = 2999,
                ImagePath = "iphone-15-pro-max-titanium.png"
            },
            new Smartphone(){
                Id = 6,
                Name = "Find X7 Ultra",
                Manufacturer = "OPPO",
                Price = 1699,
                ImagePath = "oppo-find-x7-ultra.png"
            },
            new Smartphone(){
                Id = 6,
                Name = "Find X7 Ultra",
                Manufacturer = "OPPO",
                Price = 1699,
                ImagePath = "oppo-find-x7-ultra.png"
            },
            new Smartphone(){
                Id = 6,
                Name = "Find X7 Ultra",
                Manufacturer = "OPPO",
                Price = 1699,
                ImagePath = "oppo-find-x7-ultra.png"
            },
            new Smartphone(){
                Id = 6,
                Name = "Find X7 Ultra",
                Manufacturer = "OPPO",
                Price = 1699,
                ImagePath = "oppo-find-x7-ultra.png"
            },
            new Smartphone(){
                Id = 6,
                Name = "Find X7 Ultra",
                Manufacturer = "OPPO",
                Price = 1699,
                ImagePath = "oppo-find-x7-ultra.png"
            },
            new Smartphone(){
                Id = 6,
                Name = "Find X7 Ultra",
                Manufacturer = "OPPO",
                Price = 1699,
                ImagePath = "oppo-find-x7-ultra.png"
            },
            new Smartphone(){
                Id = 6,
                Name = "Find X7 Ultra",
                Manufacturer = "OPPO",
                Price = 1699,
                ImagePath = "oppo-find-x7-ultra.png"
            },
            new Smartphone(){
                Id = 6,
                Name = "Find X7 Ultra",
                Manufacturer = "OPPO",
                Price = 1699,
                ImagePath = "oppo-find-x7-ultra.png"
            },
            new Smartphone(){
                Id = 6,
                Name = "Find X7 Ultra",
                Manufacturer = "OPPO",
                Price = 1699,
                ImagePath = "oppo-find-x7-ultra.png"
            },
            new Smartphone(){
                Id = 6,
                Name = "Find X7 Ultra",
                Manufacturer = "OPPO",
                Price = 1699,
                ImagePath = "oppo-find-x7-ultra.png"
            },
            new Smartphone(){
                Id = 6,
                Name = "Find X7 Ultra",
                Manufacturer = "OPPO",
                Price = 1699,
                ImagePath = "oppo-find-x7-ultra.png"
            },
            new Smartphone(){
                Id = 6,
                Name = "Find X7 Ultra",
                Manufacturer = "OPPO",
                Price = 1699,
                ImagePath = "oppo-find-x7-ultra.png"
            },
            new Smartphone(){
                Id = 6,
                Name = "Find X7 Ultra",
                Manufacturer = "OPPO",
                Price = 1699,
                ImagePath = "oppo-find-x7-ultra.png"
            },
            new Smartphone(){
                Id = 6,
                Name = "Find X7 Ultra",
                Manufacturer = "OPPO",
                Price = 1699,
                ImagePath = "oppo-find-x7-ultra.png"
            },
            new Smartphone(){
                Id = 6,
                Name = "Find X7 Ultra",
                Manufacturer = "OPPO",
                Price = 1699,
                ImagePath = "oppo-find-x7-ultra.png"
            },
            new Smartphone(){
                Id = 6,
                Name = "Find X7 Ultra",
                Manufacturer = "OPPO",
                Price = 1699,
                ImagePath = "oppo-find-x7-ultra.png"
            },
            new Smartphone(){
                Id = 6,
                Name = "Find X7 Ultra",
                Manufacturer = "OPPO",
                Price = 1699,
                ImagePath = "oppo-find-x7-ultra.png"
            },
            new Smartphone(){
                Id = 6,
                Name = "Find X7 Ultra",
                Manufacturer = "OPPO",
                Price = 1699,
                ImagePath = "oppo-find-x7-ultra.png"
            },
            new Smartphone(){
                Id = 6,
                Name = "Find X7 Ultra",
                Manufacturer = "OPPO",
                Price = 1699,
                ImagePath = "oppo-find-x7-ultra.png"
            },
            new Smartphone(){
                Id = 6,
                Name = "Find X7 Ultra",
                Manufacturer = "OPPO",
                Price = 1699,
                ImagePath = "oppo-find-x7-ultra.png"
            }
        };
    }

    private static List<DestinationTypeSymbol> AllDestinationTypeSymbols()
    {
        return new List<DestinationTypeSymbol>
        {
            new(){
            Name = "Amazing Pools",
            ImagePath = "amazing-pools.jpg"
            },
            new(){
                Name = "Amazing Views",
                ImagePath = "amazing-views.jpg"
            },
            new(){
                Name = "Beach",
                ImagePath = "beach.jpg"
            },
            new(){
                Name = "Countryside",
                ImagePath = "countryside.jpg"
            },
            new(){
                Name = "Farm",
                ImagePath = "farm.jpg"
            },
            new(){
                Name = "Islands",
                ImagePath = "islands.jpg"
            },
            new(){
                Name = "Lakefront",
                ImagePath = "lakefront.jpg"
            },
            new(){
                Name = "Luxe",
                ImagePath = "luxe.jpg"
            },
            new(){
                Name = "Mansions",
                ImagePath = "mansions.jpg"
            },
            new(){
                Name = "National Parks",
                ImagePath = "national-parks.jpg"
            },
            new(){
                Name = "Room",
                ImagePath = "room.jpg"
            },
            new(){
                Name = "Tiny Homes",
                ImagePath = "tiny-homes.jpg"
            },
            new(){
                Name = "Treehouses",
                ImagePath = "treehouses.jpg"
            },
            new(){
                Name = "Top Cities",
                ImagePath = "top-cities.jpg"
            },
            new(){
                Name = "Trending",
                ImagePath = "trending.jpg"
            },
            new(){
                Name = "Tropical",
                ImagePath = "tropical.jpg"
            }
        };
    }

    public async Task<IEnumerable<Amenity>> GetAmenityListDataAsync()
    {
        _allAmenities ??= AllAmenities();

        await Task.CompletedTask;
        return _allAmenities;
    }

    public async Task<IEnumerable<Booking>> GetBookingListDataAsync()
    {
        _allBookings ??= AllBookings();

        await Task.CompletedTask;
        return _allBookings;
    }

    public async Task<IEnumerable<FAQ>> GetFAQListDataAsync()
    {
        _allFAQs ??= AllFAQs();

        await Task.CompletedTask;
        return _allFAQs;
    }

    public async Task<IEnumerable<Notification>> GetNotificationListDataAsync()
    {
        _allNotifications ??= AllNotifications();

        await Task.CompletedTask;
        return _allNotifications;
    }

    public async Task<IEnumerable<Payment>> GetPaymentListDataAsync()
    {
        _allPayments ??= AllPayments();

        await Task.CompletedTask;
        return _allPayments;
    }

    public async Task<IEnumerable<Property>> GetPropertyListDataAsync()
    {
        _allProperties ??= AllProperties();

        await Task.CompletedTask;
        return _allProperties;
    }

    public async Task<IEnumerable<PropertyPolicy>> GetPropertyPolicyListDataAsync()
    {
        _allPropertyPolicies ??= AllPropertyPolicies();

        await Task.CompletedTask;
        return _allPropertyPolicies;
    }

    public async Task<IEnumerable<QnA>> GetQnAListDataAsync()
    {
        _allQnAs ??= AllQnAs();

        await Task.CompletedTask;
        return _allQnAs;
    }

    public async Task<IEnumerable<Review>> GetReviewListDataAsync()
    {
        _allReviews ??= AllReviews();

        await Task.CompletedTask;
        return _allReviews;
    }

    public async Task<IEnumerable<User>> GetUserListDataAsync()
    {
        _allUsers ??= AllUsers();

        await Task.CompletedTask;
        return _allUsers;
    }

    public async Task<IEnumerable<DestinationTypeSymbol>> GetDestinationTypeSymbolDataAsync()
    {
        _allDestinationTypeSymbols ??= AllDestinationTypeSymbols();

        await Task.CompletedTask;
        return _allDestinationTypeSymbols;
    }

    public async Task<IEnumerable<Smartphone>> GetSmartphoneListDataAsync()
    {
        _allSmartphones ??= AllSmartphones();

        await Task.CompletedTask;
        return _allSmartphones;
    }
}
