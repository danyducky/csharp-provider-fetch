using MediatR;
using Routes.Infrastructure.Abstractions.Interfaces;

namespace Routes.UseCases.Provider.GetProviderAvailability;

/// <summary>
/// A handler for <see cref="GetProviderAvailabilityQuery"/>.
/// </summary>
internal sealed class GetProviderAvailabilityQueryHandler : IRequestHandler<GetProviderAvailabilityQuery, bool>
{
    private readonly ISearchService _searchService;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="searchService">Search service.</param>
    public GetProviderAvailabilityQueryHandler(ISearchService searchService)
    {
        _searchService = searchService;
    }

    /// <inheritdoc />
    public async Task<bool> Handle(GetProviderAvailabilityQuery request, CancellationToken cancellationToken)
        => await _searchService.IsAvailableAsync(cancellationToken);
}