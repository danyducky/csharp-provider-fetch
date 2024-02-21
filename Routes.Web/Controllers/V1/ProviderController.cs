using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Routes.Infrastructure.Abstractions.Models;
using Routes.UseCases.Provider.GetProviderAvailability;
using Routes.UseCases.Provider.SearchProviderRoutes;

namespace Routes.Web.Controllers.V1;

/// <summary>
/// Provider controller.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}")]
public class ProviderController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="mediator">Mediator.</param>
    public ProviderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Search through provider routes.
    /// </summary>
    /// <param name="request">Request data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Result of searching.</returns>
    [HttpPost("search")]
    public async Task<SearchProviderRoutesDto> SearchAsync(SearchRequest request, CancellationToken cancellationToken)
        => await _mediator.Send(new SearchProviderRoutesQuery(request), cancellationToken);

    /// <summary>
    /// Indicates whether provider is available.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True, if provider is available.</returns>
    [HttpGet("ping")]
    public async Task<bool> PingAsync(CancellationToken cancellationToken)
        => await _mediator.Send(new GetProviderAvailabilityQuery(), cancellationToken);
}