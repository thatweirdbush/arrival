using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;

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

    public async Task<List<string>> SearchLocationsAsync(string query, int maxRows = 10)
    {
        if (string.IsNullOrEmpty(query))
        {
            return [];
        }
        var apiUrl = $"http://api.geonames.org/searchJSON?q={query}&maxRows={maxRows}&username={GeoNamesUsername}";
        var response = await _httpClient.GetStringAsync(apiUrl);

        try
        {
            // Deserialize JSON into GeographicNamesResponse
            var searchResult = JsonSerializer.Deserialize<GeographicNamesResponse>(response);

            if (searchResult?.GeographicNames != null && searchResult.GeographicNames.Count > 0)
            {
                // Returns a list of location names (combining name and country)
                return searchResult.GeographicNames.Select(location =>
                    $"{location.Name}, {location.CountryName}").ToList();
            }
        }
        catch (JsonException ex)
        {
            Debug.WriteLine($"JSON Parsing Error: {ex.Message}");
        }

        return []; // Return an empty list if no results
    }

    public async Task<string> FindNearbyPlaceAsync(double latitude, double longitude)
    {
        var apiUrl = $"http://api.geonames.org/findNearbyPlaceNameJSON?lat={latitude}&lng={longitude}&lang={Language}&username={GeoNamesUsername}";
        var response = await _httpClient.GetStringAsync(apiUrl);

        try
        {
            var result = JsonSerializer.Deserialize<GeographicNamesResponse>(response);

            // Get the nearest place
            var nearestPlace = result?.GeographicNames?.FirstOrDefault();

            if (nearestPlace != null)
            {
                return $"{nearestPlace.Name}, {nearestPlace.CountryName}";
            }
            return Property.DEFAULT_PROPERTY_LOCATION;
        }
        catch (JsonException ex)
        {
            Debug.WriteLine($"JSON Parsing Error: {ex.Message}");
            return "Error parsing JSON response.";
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

            // Return the list of search results
            return result?.Results ?? [];
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

    public class WikipediaSearchResponse
    {
        [JsonPropertyName("geonames")]
        public List<WikipediaSearchResult> Results
        {
            get; set;
        }
    }

}
