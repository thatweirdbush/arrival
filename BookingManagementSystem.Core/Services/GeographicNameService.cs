using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookingManagementSystem.Core.Services;
public class GeographicNameService
{
    private readonly HttpClient _httpClient;
    private const string GeoNamesUsername = "thatweirdbush";
    private const string Language = "en";

    public GeographicNameService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<List<GeographicName>> SearchLocationsAsync(string query, int maxRows = 10)
    {
        if (string.IsNullOrEmpty(query))
        {
            return [];
        }

        var apiUrl = $"http://api.geonames.org/searchJSON?q={query}&maxRows={maxRows}&username={GeoNamesUsername}";

        try
        {
            var response = await _httpClient.GetStringAsync(apiUrl);

            // Deserialize JSON into GeographicNamesResponse
            var searchResult = JsonSerializer.Deserialize<GeographicNamesResponse>(response);

            if (searchResult?.GeographicNames != null && searchResult.GeographicNames.Count != 0)
            {
                // Return the list of GeographicName objects
                return searchResult.GeographicNames;
            }
        }
        catch (JsonException ex)
        {
            Debug.WriteLine($"JSON Parsing Error: {ex.Message}");
        }

        return []; // Return an empty list if no results or an error occurs
    }

    public async Task<List<string>> GeographicNameToStringListAsync(string query, int maxRows = 10)
    {
        var data = await SearchLocationsAsync(query, maxRows);

        // Concatenate the `Name` and `CountryName` of the locations into a list of strings
        return data.Select(location => $"{location.Name}, {location.CountryName}").ToList();
    }

    public async Task<GeographicName> FindNearbyPlaceAsync(double latitude, double longitude)
    {
        var apiUrl = $"http://api.geonames.org/findNearbyPlaceNameJSON?lat={latitude}&lng={longitude}&lang={Language}&username={GeoNamesUsername}";

        try
        {
            var response = await _httpClient.GetStringAsync(apiUrl);
            var result = JsonSerializer.Deserialize<GeographicNamesResponse>(response);

            // Get the nearest place
            var nearestPlace = result?.GeographicNames?.FirstOrDefault();

            // Return the nearest place or null if no results found
            return nearestPlace;
        }
        catch (JsonException ex)
        {
            Debug.WriteLine($"JSON Parsing Error: {ex.Message}");
            return null; // Indicate an error by returning null
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error: {ex.Message}");
            return null; // Handle other unexpected exceptions
        }
    }


    public async Task<List<WikipediaSearchResult>> SearchWikipediaAsync(string query, int maxRows = 10)
    {
        // Check if the query is empty
        if (string.IsNullOrWhiteSpace(query))
        {
            return [];
        }
        var apiUrl = $"http://api.geonames.org/wikipediaSearchJSON?q={Uri.EscapeDataString(query)}&maxRows={maxRows}&username={GeoNamesUsername}";

        try
        {
            var response = await _httpClient.GetStringAsync(apiUrl);
            var result = JsonSerializer.Deserialize<WikipediaSearchResponse>(response);

            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error: {ex.Message}");
            return [];
        }
    }

    public async Task<List<CountryInfo>> GetAllCountryInfoAsync()
    {
        var apiUrl = $"http://api.geonames.org/countryInfoJSON?username={GeoNamesUsername}";

        try
        {
            var response = await _httpClient.GetStringAsync(apiUrl);
            var result = JsonSerializer.Deserialize<CountryInfoResponse>(response);

            return result?.Countries ?? [];
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error: {ex.Message}");
            return [];
        }
    }

    public class GeographicNamesResponse
    {
        [JsonPropertyName("totalResultsCount")]
        public int TotalResultsCount
        {
            get; set;
        }

        [JsonPropertyName("geonames")]
        public List<GeographicName> GeographicNames
        {
            get; set;
        }
    }

    public class WikipediaSearchResponse
    {
        [JsonPropertyName("geonames")]
        public List<WikipediaSearchResult> Results
        {
            get; set;
        }
    }

    public class CountryInfoResponse
    {
        [JsonPropertyName("geonames")]
        public List<CountryInfo> Countries
        {
            get; set;
        }
    }
}

public class GeographicName
{
    [JsonPropertyName("name")]
    public string Name
    {
        get; set;
    }

    [JsonPropertyName("lat")]
    public string Latitude
    {
        get; set;
    }

    [JsonPropertyName("lng")]
    public string Longitude
    {
        get; set;
    }

    [JsonPropertyName("countryName")]
    public string CountryName
    {
        get; set;
    }

    [JsonPropertyName("population")]
    public int Population
    {
        get; set;
    }
}

public class WikipediaSearchResult
{
    [JsonPropertyName("title")]
    public string Title
    {
        get; set;
    }
    [JsonPropertyName("summary")]
    public string Summary
    {
        get; set;
    }
    [JsonPropertyName("wikipediaUrl")]
    public string WikipediaUrl
    {
        get; set;
    }
    [JsonPropertyName("lat")]
    public decimal Latitude
    {
        get; set;
    }
    [JsonPropertyName("lng")]
    public decimal Longitude
    {
        get; set;
    }
}

public class CountryInfo
{
    [JsonPropertyName("continent")]
    public string Continent
    {
        get; set;
    }

    [JsonPropertyName("capital")]
    public string Capital
    {
        get; set;
    }

    [JsonPropertyName("languages")]
    public string Languages
    {
        get; set;
    }

    [Key]
    [JsonPropertyName("geonameId")]
    public int GeoNameId
    {
        get; set;
    }

    [JsonPropertyName("south")]
    public double South
    {
        get; set;
    }

    [JsonPropertyName("isoAlpha3")]
    public string IsoAlpha3
    {
        get; set;
    }

    [JsonPropertyName("north")]
    public double North
    {
        get; set;
    }

    [JsonPropertyName("fipsCode")]
    public string FipsCode
    {
        get; set;
    }

    [JsonPropertyName("population")]
    public string Population
    {
        get; set;
    }

    [JsonPropertyName("east")]
    public double East
    {
        get; set;
    }

    [JsonPropertyName("isoNumeric")]
    public string IsoNumeric
    {
        get; set;
    }

    [JsonPropertyName("areaInSqKm")]
    public string AreaInSqKm
    {
        get; set;
    }

    [JsonPropertyName("countryCode")]
    public string CountryCode
    {
        get; set;
    }

    [JsonPropertyName("west")]
    public double West
    {
        get; set;
    }

    [JsonPropertyName("countryName")]
    public string CountryName
    {
        get; set;
    }

    [JsonPropertyName("postalCodeFormat")]
    public string PostalCodeFormat
    {
        get; set;
    }

    [JsonPropertyName("continentName")]
    public string ContinentName
    {
        get; set;
    }

    [JsonPropertyName("currencyCode")]
    public string CurrencyCode
    {
        get; set;
    }
}