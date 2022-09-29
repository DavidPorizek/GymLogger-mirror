using GymLogger.API.Controllers;
using MediatR;
using Moq;

namespace GymLogger.UnitTests;

public class ConfigurationUnitTest
{
    [Fact]
    public void ConfigurationGet_ShouldReturnTrue()
    {
        var mediatorMock = new Mock<IMediator>();
        //var mock = new ConfigurationHandlerMock();
        var controller = new ConfigurationController2(mediatorMock.Object);
        var response = controller.GetConfigurations();
        //Assert.Equal(4, )
    }
}