using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymLogger.Shared.Models;
using MediatR;

namespace GymLogger.UnitTests;

public class ConfigurationHandlerMock : IRequest
{

    public record GetConfigurationsQueryMock() : IRequest<List<Configuration>>;

    public class ConfigurationHandler : IRequestHandler<GetConfigurationsQueryMock, List<Configuration>>
    {
        private readonly List<Configuration> _context;

        public ConfigurationHandler()
        {
            _context = new List<Configuration>()
            {
                new Configuration() { Id = 1 },
                new Configuration() { Id = 2 },
                new Configuration() { Id = 5 }
            };
        }

        public async Task<List<Configuration>> Handle(GetConfigurationsQueryMock query,
            CancellationToken cancellationToken)
        {
            if (_context == null)
            {
                throw new ArgumentNullException(nameof(_context));
            }

            var task = new Task<List<Configuration>>(() => { return _context; });

            return await task;
        }
    }
}
