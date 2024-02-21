using Routes.Infrastructure.Abstractions.Models;

namespace Routes.UseCases.Provider.SearchProviderRoutes;

/// <summary>
/// Search provider routes data transfer object.
/// </summary>
/// <param name="Routes">Provider routes.</param>
public record SearchProviderRoutesDto(IReadOnlyCollection<Route> Routes);