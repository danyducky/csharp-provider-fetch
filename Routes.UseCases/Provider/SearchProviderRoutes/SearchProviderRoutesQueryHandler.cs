using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Routes.Infrastructure.Abstractions.Interfaces;
using Routes.Infrastructure.Abstractions.Models;

namespace Routes.UseCases.Provider.SearchProviderRoutes;

/// <summary>
/// A handler for <see cref="SearchProviderRoutesQuery"/>.
/// </summary>
internal sealed class SearchProviderRoutesQueryHandler : IRequestHandler<SearchProviderRoutesQuery, SearchProviderRoutesDto>
{
    private const string CacheName = "routes";
    
    private static readonly TimeSpan CacheExpirationMinutes = TimeSpan.FromMinutes(5);
    
    private readonly ISearchService _searchService;
    private readonly IMemoryCache _memoryCache;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="searchService">Search service.</param>
    /// <param name="memoryCache">Memory cache.</param>
    public SearchProviderRoutesQueryHandler(ISearchService searchService, IMemoryCache memoryCache)
    {
        _searchService = searchService;
        _memoryCache = memoryCache;
    }

    /// <inheritdoc />
    public async Task<SearchProviderRoutesDto> Handle(SearchProviderRoutesQuery request, CancellationToken cancellationToken)
    {
        var filters = request.Data.Filters;
        if (filters?.OnlyCached is true)
        {
            var routes = _memoryCache.Get<Route[]>(CacheName) ?? Array.Empty<Route>();

            routes = GetFilteredRoutes(routes, filters);

            return new SearchProviderRoutesDto(routes);
        }
        
        var response = await _searchService.SearchAsync(request.Data, cancellationToken);

        _memoryCache.Set(CacheName, response.Routes, CacheExpirationMinutes);

        if (filters is not null)
        {
            response.Routes = GetFilteredRoutes(response.Routes, filters);
        }

        return new SearchProviderRoutesDto(response.Routes);
    }

    private static Route[] GetFilteredRoutes(IEnumerable<Route> routes, SearchFilters filter)
    {
        if (filter.DestinationDateTime.HasValue)
        {
            routes = routes.Where(route => route.DestinationDateTime == filter.DestinationDateTime);
        }

        if (filter.MaxPrice.HasValue)
        {
            routes = routes.Where(route => route.Price <= filter.MaxPrice);
        }

        if (filter.MinTimeLimit.HasValue)
        {
            routes = routes.Where(route => route.TimeLimit >= filter.MinTimeLimit);
        }

        return routes.ToArray();
    }
}