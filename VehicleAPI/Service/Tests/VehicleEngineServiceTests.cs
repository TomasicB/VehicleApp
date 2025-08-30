using FluentAssertions;
using Moq;
using Vehicle.Models.Common;
using Vehicle.Repository.Common;
using Xunit;

namespace Vehicle.Service.Tests
{
    public class VehicleEngineServiceTests
    {
        private readonly Mock<IVehicleEngineRepository> _mockEngineRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly VehicleEngineService _service;

        public VehicleEngineServiceTests()
        {
            _mockEngineRepo = new Mock<IVehicleEngineRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(u => u.EngineRepo).Returns(_mockEngineRepo.Object);
            _service = new VehicleEngineService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetEngineAsync_ShouldReturnAllEngines()
        {
            // Arrange
            var expectedEngines = new List<IVehicleEngine>
            {
                Mock.Of<IVehicleEngine>(),
                Mock.Of<IVehicleEngine>()
            };

            _mockEngineRepo.Setup(r => r.GetEngineAsync())
                           .ReturnsAsync(expectedEngines);

            // Act
            var result = await _service.GetEngineAsync();

            // Assert
            result.Should().BeEquivalentTo(expectedEngines);
            _mockEngineRepo.Verify(r => r.GetEngineAsync(), Times.Once);
        }

        [Fact]
        public async Task GetEngineByIdAsync_ShouldReturnMatchingEngine()
        {
            // Arrange
            int engineId = 1;
            var expectedEngines = new List<IVehicleEngine>
            {
                Mock.Of<IVehicleEngine>()
            };

            _mockEngineRepo.Setup(r => r.GetEngineByIdAsync(engineId))
                           .ReturnsAsync(expectedEngines);

            // Act
            var result = await _service.GetEngineByIdAsync(engineId);

            // Assert
            result.Should().BeEquivalentTo(expectedEngines);
            _mockEngineRepo.Verify(r => r.GetEngineByIdAsync(engineId), Times.Once);
        }

        [Fact]
        public async Task GetEngineByNameAsync_ShouldReturnMatchingEngines()
        {
            // Arrange
            string engineType = "Diesel";
            var expectedEngines = new List<IVehicleEngine>
            {
                Mock.Of<IVehicleEngine>(),
                Mock.Of<IVehicleEngine>()
            };

            _mockEngineRepo.Setup(r => r.GetEngineByNameAsync(engineType))
                           .ReturnsAsync(expectedEngines);

            // Act
            var result = await _service.GetEngineByNameAsync(engineType);

            // Assert
            result.Should().BeEquivalentTo(expectedEngines);
            _mockEngineRepo.Verify(r => r.GetEngineByNameAsync(engineType), Times.Once);
        }
    }
}