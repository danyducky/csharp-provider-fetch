using Routes.Infrastructure.Abstractions.Models;

namespace Routes.Infrastructure.Abstractions.Interfaces;

/// <summary>
/// Provider search service.
/// </summary>
public interface ISearchService
{
    /// <summary>
    /// Search data through provider.
    /// </summary>
    /// <param name="request">Request to provider.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Search response.</returns>
    Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken);
    
    /// <summary>
    /// Indicates whether provider is available.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True, if provider is available.</returns>
    Task<bool> IsAvailableAsync(CancellationToken cancellationToken);
}