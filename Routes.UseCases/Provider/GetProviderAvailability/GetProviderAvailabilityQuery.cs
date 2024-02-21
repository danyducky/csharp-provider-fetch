using MediatR;

namespace Routes.UseCases.Provider.GetProviderAvailability;

/// <summary>
/// Gets provider availability status.
/// </summary>
public record GetProviderAvailabilityQuery : IRequest<bool>;