namespace Routes.Infrastructure.Abstractions.Models;

/// <summary>
/// Search request to provider.
/// </summary>
public record SearchRequest
{
    // Mandatory
    // Start point of route, e.g. Moscow 
    public string Origin { get; set; } = string.Empty;
    
    // Mandatory
    // End point of route, e.g. Sochi
    public string Destination { get; set; } = string.Empty;
    
    // Mandatory
    // Start date of route
    public DateTime OriginDateTime { get; set; }
    
    // Optional
    public SearchFilters? Filters { get; set; }
}