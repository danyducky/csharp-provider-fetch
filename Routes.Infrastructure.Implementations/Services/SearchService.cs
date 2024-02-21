using System.Net.Http.Json;
using Routes.Infrastructure.Abstractions.Interfaces;
using Routes.Infrastructure.Abstractions.Models;
using Routes.Infrastructure.Implementations.Settings;

namespace Routes.Infrastructure.Implementations.Services;

/// <summary>
/// Provider search service.
/// </summary>
public class SearchService : ISearchService
{
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="url">Service URL.</param>
    /// <param name="httpClientFactory">HTTP client factory.</param>
    public SearchService(string url, IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri(url);
    }

    /// <inheritdoc />
    public async Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken)
    {
        // Not sure that it will work as expected.
        // TODO: Check with the real provider.
        var response = await _httpClient.PostAsJsonAsync(Segments.SearchSegment, request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException("Something went wrong.");
        }

        var content = await response.Content.ReadFromJsonAsync<SearchResponse>(cancellationToken);
        if (content == null)
        {
            throw new ApplicationException("Content is empty.");
        }

        return content;
    }

    /// <inheritdoc />
    public async Task<bool> IsAvailableAsync(CancellationToken cancellationToken)
        => await _httpClient.GetFromJsonAsync<bool>(Segments.PingSegment, cancellationToken);
}