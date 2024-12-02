using System.Collections.ObjectModel;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Core.Services;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class PlaceLocationViewModel : BaseStepViewModel
{
    private readonly IPropertyService _propertyService;
    private readonly GeographicNameService _geographicNamesService;
    public Property PropertyOnCreating => _propertyService.PropertyOnCreating;
    public ObservableCollection<CountryInfo> CountryList { get; private set; } = [];
    public double CurrentLatitude { get; set; }
    public double CurrentLongitude { get; set; }
    public CountryInfo SelectedCountry { get; set; }
    public string SelectedStreetAddress { get; set; }
    public string SelectedCityOrDistrict { get; set; }
    public string SelectedStateOrProvince { get; set; }
    public string SelectedPostalCode { get; set; }
    public PlaceLocationViewModel(IPropertyService propertyService, GeographicNameService geographicNamesService)
    {
        _propertyService = propertyService;
        _geographicNamesService = geographicNamesService;

        // Initialize core properties
        CurrentLatitude = PropertyOnCreating.Latitude;
        CurrentLongitude = PropertyOnCreating.Longitude;
        SelectedCountry = PropertyOnCreating.Country;
        SelectedStreetAddress = PropertyOnCreating.StreetAddress;
        SelectedCityOrDistrict = PropertyOnCreating.CityOrDistrict;
        SelectedStateOrProvince = PropertyOnCreating.StateOrProvince;
        SelectedPostalCode = PropertyOnCreating.PostalCode;

        // Load data for UI binding
        _ = LoadCountriesAsync();
    }

    public async Task LoadCountriesAsync()
    {
        var countries = await _geographicNamesService.GetAllCountryInfoAsync();
        foreach (var country in countries)
        {
            CountryList.Add(country);
        }
    }

    public async Task<GeographicName> FindNearbyPlaceAsync() => await _geographicNamesService.FindNearbyPlaceAsync(CurrentLatitude, CurrentLongitude);
    public async Task<List<string>> SearchLocationsAsync(string query) => await _geographicNamesService.SearchLocationsAsync(query);
    public async Task<List<WikipediaSearchResult>> SearchWikipediaAsync(string query, int maxRows) => await _geographicNamesService.SearchWikipediaAsync(query, maxRows);

    public override void SaveProcess()
    {
        PropertyOnCreating.Latitude = CurrentLatitude;
        PropertyOnCreating.Longitude = CurrentLongitude;
        PropertyOnCreating.Country = SelectedCountry;
        PropertyOnCreating.StreetAddress = SelectedStreetAddress;
        PropertyOnCreating.CityOrDistrict = SelectedCityOrDistrict;
        PropertyOnCreating.StateOrProvince = SelectedStateOrProvince;
        PropertyOnCreating.PostalCode = SelectedPostalCode;
    }

    public override void ValidateProcess()
    {
        IsStepCompleted = (CurrentLatitude != 0.0 
                        && CurrentLongitude != 0.0
                        && SelectedCountry != null
                        && !string.IsNullOrWhiteSpace(SelectedStreetAddress)
                        && !string.IsNullOrWhiteSpace(SelectedCityOrDistrict)
                        && !string.IsNullOrWhiteSpace(SelectedStateOrProvince));
    }
}
