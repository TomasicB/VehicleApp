using FluentAssertions;
using Moq;
using Vehicle.Models.Common;
using Vehicle.Repository.Common;
using Xunit;

namespace Vehicle.Service.Tests
{
    public class VehicleMakeServiceTests
    {
        private readonly Mock<IVehicleMakeRepository> _mockMakeRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly VehicleMakeService _service;

        public VehicleMakeServiceTests()
        {
            _mockMakeRepo = new Mock<IVehicleMakeRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockUnitOfWork.Setup(u => u.MakeRepo).Returns(_mockMakeRepo.Object);
            _service = new VehicleMakeService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetMakeAsync_ShouldReturnAllMakes()
        {
            // Arrange
            var expectedMakes = new List<IVehicleMake>
            {
                Mock.Of<IVehicleMake>(),
                Mock.Of<IVehicleMake>()
            };

            _mockMakeRepo.Setup(r => r.GetMakeAsync())
                         .ReturnsAsync(expectedMakes);

            // Act
            var result = await _service.GetMakeAsync();

            // Assert
            result.Should().BeEquivalentTo(expectedMakes);
            _mockMakeRepo.Verify(r => r.GetMakeAsync(), Times.Once);
        }

        [Fact]
        public async Task GetMakeByIdAsync_ShouldReturnMatchingMake()
        {
            // Arrange
            int id = 1;
            var expectedMakes = new List<IVehicleMake> { Mock.Of<IVehicleMake>() };

            _mockMakeRepo.Setup(r => r.GetMakeByIdAsync(id))
                         .ReturnsAsync(expectedMakes);

            // Act
            var result = await _service.GetMakeByIdAsync(id);

            // Assert
            result.Should().BeEquivalentTo(expectedMakes);
            _mockMakeRepo.Verify(r => r.GetMakeByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetMakesByNameAsync_ShouldReturnMatchingMakes()
        {
            // Arrange
            string name = "Toyota";
            var expectedMakes = new List<IVehicleMake>
            {
                Mock.Of<IVehicleMake>(),
                Mock.Of<IVehicleMake>()
            };

            _mockMakeRepo.Setup(r => r.GetMakesByNameAsync(name))
                         .ReturnsAsync(expectedMakes);

            // Act
            var result = await _service.GetMakesByNameAsync(name);

            // Assert
            result.Should().BeEquivalentTo(expectedMakes);
            _mockMakeRepo.Verify(r => r.GetMakesByNameAsync(name), Times.Once);
        }

        [Fact]
        public async Task InsertMakeAsync_ShouldInsertAndCommit()
        {
            // Arrange
            var make = Mock.Of<IVehicleMake>();

            // Act
            await _service.InsertMakeAsync(make);

            // Assert
            _mockMakeRepo.Verify(r => r.InsertMakeAsync(make), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteMakeAsync_ShouldDeleteAndCommit()
        {
            // Arrange
            int id = 1;

            // Act
            await _service.DeleteMakeAsync(id);

            // Assert
            _mockMakeRepo.Verify(r => r.DeleteMakeAsync(id), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateMakeAsync_ShouldUpdateAndCommit()
        {
            // Arrange
            int id = 1;
            var updatedMake = Mock.Of<IVehicleMake>();

            // Act
            await _service.UpdateMakeAsync(id, updatedMake);

            // Assert
            _mockMakeRepo.Verify(r => r.UpdateMakeAsync(id, updatedMake), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }
    }
}
