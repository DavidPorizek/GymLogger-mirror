using GymLogger.Context;
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
            if (_context == null)
            {
                throw new ArgumentNullException(nameof(_context));
            }

            return await _context.Configurations.ToListAsync();
        }
    }
}
