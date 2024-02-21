namespace Routes.Infrastructure.Abstractions.Models;

/// <summary>
/// Provider search filters.
/// </summary>
public record SearchFilters
{
    // Optional
    // End date of route
    public DateTime? DestinationDateTime { get; set; }
    
    // Optional
    // Maximum price of route
    public decimal? MaxPrice { get; set; }
    
    // Optional
    // Minimum value of time limit for route
    public DateTime? MinTimeLimit { get; set; }
    
    // Optional
    // Forcibly search in cached data
    public bool? OnlyCached { get; set; }
}