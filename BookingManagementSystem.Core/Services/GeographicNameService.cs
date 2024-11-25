using System;
using System.Collections.Generic;
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

    public GeographicNameService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<List<string>> SearchLocationsAsync(string query, string username)
    {
        if (string.IsNullOrEmpty(query))
        {
            return [];
        }
        var apiUrl = $"http://api.geonames.org/searchJSON?q={query}&maxRows=10&username={username}";
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

    private class GeographicNamesResponse
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

    private class GeographicName
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
}
