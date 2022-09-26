using GymLogger.Context;
using GymLogger.Shared.DTOs;
using GymLogger.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GymLogger.API.Handler
{
    public record GetConfigurationsQuery() : IRequest<List<Configuration>>;

    public class ConfigurationHandler : IRequestHandler<GetConfigurationsQuery, List<Configuration>>
    {
        private readonly GymLoggerContext _context;

        public ConfigurationHandler(GymLoggerContext context)
        {
            _context = context;
        }

        public async Task<List<Configuration>> Handle(GetConfigurationsQuery query,
            CancellationToken cancellationToken)
        {
            if (_context.Configurations == null)
            {
                //return NotFound();
            }

            return await _context.Configurations.ToListAsync();
        }
    }
}
