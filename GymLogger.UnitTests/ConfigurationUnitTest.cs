using GymLogger.API.Controllers;
using GymLogger.API.Handler;
using GymLogger.Context;
using GymLogger.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace GymLogger.UnitTests;

public class ConfigurationUnitTest
{
    public void InitContext(GymLoggerContext context)
    {
        var list = new List<Configuration>()
            {
                new Configuration() { Id = 1 },
                new Configuration() { Id = 2 },
                new Configuration() { Id = 5 }
            };

        context.Configurations.AddRange(list);
        context.SaveChanges();
    }

    [Fact]
    public async void ConfigurationGet_ShouldReturnTrue()
    {
        var mediator = new Mock<IMediator>();
        var optionsBuilder = new DbContextOptionsBuilder<GymLoggerContext>();
        optionsBuilder.UseInMemoryDatabase("GymLoggerContext");
        var context = new GymLoggerContext(optionsBuilder.Options);
        InitContext(context);

        var command = new GetConfigurationsQuery();
        var handler = new ConfigurationHandler(context);
        var res = await handler.Handle(command, new CancellationToken());
        Assert.True(res.Count == 3);
        
    }
}