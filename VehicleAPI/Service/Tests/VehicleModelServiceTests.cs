using FluentAssertions;
using Moq;
using Vehicle.Models.Common;
using Vehicle.Models.Common.Write;
using Vehicle.Repository.Common;
using Xunit;

namespace Vehicle.Service.Tests
{
    public class VehicleModelServiceTests
    {
        private readonly Mock<IVehicleModelRepository> _mockModelRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly VehicleModelService _service;

        public VehicleModelServiceTests()
        {
            _mockModelRepo = new Mock<IVehicleModelRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(u => u.ModelRepo).Returns(_mockModelRepo.Object);

            _service = new VehicleModelService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetModelsAsync_ShouldReturnAllModels()
        {
            // Arrange
            var expectedModels = new List<IVehicleModel>
            {
                Mock.Of<IVehicleModel>(),
                Mock.Of<IVehicleModel>()
            };

            _mockModelRepo.Setup(r => r.GetModelsAsync()).ReturnsAsync(expectedModels);

            // Act
            var result = await _service.GetModelsAsync();

            // Assert
            result.Should().BeEquivalentTo(expectedModels);
            _mockModelRepo.Verify(r => r.GetModelsAsync(), Times.Once);
        }

        [Fact]
        public async Task GetModelByIdAsync_ShouldReturnMatchingModel()
        {
            // Arrange
            int id = 1;
            var expectedModels = new List<IVehicleModel> { Mock.Of<IVehicleModel>() };

            _mockModelRepo.Setup(r => r.GetModelByIdAsync(id)).ReturnsAsync(expectedModels);

            // Act
            var result = await _service.GetModelByIdAsync(id);

            // Assert
            result.Should().BeEquivalentTo(expectedModels);
            _mockModelRepo.Verify(r => r.GetModelByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetModelsByNameAsync_ShouldReturnMatchingModels()
        {
            // Arrange
            string name = "Octavia";
            var expectedModels = new List<IVehicleModel>
            {
                Mock.Of<IVehicleModel>(),
                Mock.Of<IVehicleModel>()
            };

            _mockModelRepo.Setup(r => r.GetModelsByNameAsync(name)).ReturnsAsync(expectedModels);

            // Act
            var result = await _service.GetModelsByNameAsync(name);

            // Assert
            result.Should().BeEquivalentTo(expectedModels);
            _mockModelRepo.Verify(r => r.GetModelsByNameAsync(name), Times.Once);
        }

        [Fact]
        public async Task GetModelsByMakeAsync_ShouldReturnMatchingModels()
        {
            // Arrange
            string make = "Yaris";
            var expectedModels = new List<IVehicleModel>
            {
                Mock.Of<IVehicleModel>(),
                Mock.Of<IVehicleModel>()
            };

            _mockModelRepo.Setup(r => r.GetModelsByMakeAsync(make)).ReturnsAsync(expectedModels);

            // Act
            var result = await _service.GetModelsByMakeAsync(make);

            // Assert
            result.Should().BeEquivalentTo(expectedModels);
            _mockModelRepo.Verify(r => r.GetModelsByMakeAsync(make), Times.Once);
        }

        [Fact]
        public async Task InsertModelAsync_ShouldInsertAndCommit()
        {
            // Arrange
            var model = Mock.Of<IVehicleModel>();
            int makeId = 2;

            // Act
            await _service.InsertModelAsync(model, makeId);

            // Assert
            _mockModelRepo.Verify(r => r.InsertModelAsync(model, makeId), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteModelAsync_ShouldDeleteAndCommit()
        {
            // Arrange
            int id = 3;

            // Act
            await _service.DeleteModelAsync(id);

            // Assert
            _mockModelRepo.Verify(r => r.DeleteModelAsync(id), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateModelAsync_ShouldUpdateAndCommit()
        {
            // Arrange
            int id = 4;
            var updatedModel = Mock.Of<IVehicleModelWrite>();

            // Act
            await _service.UpdateModelAsync(id, updatedModel);

            // Assert
            _mockModelRepo.Verify(r => r.UpdateModelAsync(id, updatedModel), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }
    }
}
