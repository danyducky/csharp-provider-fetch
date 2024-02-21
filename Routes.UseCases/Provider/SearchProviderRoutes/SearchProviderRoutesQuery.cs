using MediatR;
using Routes.Infrastructure.Abstractions.Models;

namespace Routes.UseCases.Provider.SearchProviderRoutes;

/// <summary>
/// Search provider routes.
/// </summary>
/// <param name="Data">Request data.</param>
public record SearchProviderRoutesQuery(SearchRequest Data) : IRequest<SearchProviderRoutesDto>;