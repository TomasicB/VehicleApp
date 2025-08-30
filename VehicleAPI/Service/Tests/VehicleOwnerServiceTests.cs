using FluentAssertions;
using Moq;
using Vehicle.Models.Common;
using Vehicle.Repository.Common;
using Xunit;

namespace Vehicle.Service.Tests
{
    public class VehicleOwnerServiceTests
    {
        private readonly Mock<IVehicleOwnerRepository> _mockOwnerRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly VehicleOwnerService _service;

        public VehicleOwnerServiceTests()
        {
            _mockOwnerRepo = new Mock<IVehicleOwnerRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(u => u.OwnerRepo).Returns(_mockOwnerRepo.Object);

            _service = new VehicleOwnerService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetOwnersAsync_ShouldReturnAllOwners()
        {
            var owners = new List<IVehicleOwner> { Mock.Of<IVehicleOwner>() };
            _mockOwnerRepo.Setup(r => r.GetOwnersAsync()).ReturnsAsync(owners);

            var result = await _service.GetOwnersAsync();

            result.Should().BeEquivalentTo(owners);
            _mockOwnerRepo.Verify(r => r.GetOwnersAsync(), Times.Once);
        }

        [Fact]
        public async Task GetOwnerByIdAsync_ShouldReturnOwner()
        {
            int id = 1;
            var owners = new List<IVehicleOwner> { Mock.Of<IVehicleOwner>() };
            _mockOwnerRepo.Setup(r => r.GetOwnerByIdAsync(id)).ReturnsAsync(owners);

            var result = await _service.GetOwnerByIdAsync(id);

            result.Should().BeEquivalentTo(owners);
            _mockOwnerRepo.Verify(r => r.GetOwnerByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetOwnersByNameAsync_ShouldReturnMatchingOwners()
        {
            string name = "John";
            var owners = new List<IVehicleOwner> { Mock.Of<IVehicleOwner>() };
            _mockOwnerRepo.Setup(r => r.GetOwnersByNameAsync(name)).ReturnsAsync(owners);

            var result = await _service.GetOwnersByNameAsync(name);

            result.Should().BeEquivalentTo(owners);
            _mockOwnerRepo.Verify(r => r.GetOwnersByNameAsync(name), Times.Once);
        }

        [Fact]
        public async Task InsertOwnerAsync_ShouldInsertAndCommit()
        {
            var owner = Mock.Of<IVehicleOwner>();

            await _service.InsertOwnerAsync(owner);

            _mockOwnerRepo.Verify(r => r.InsertOwnerAsync(owner), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteOwnerAsync_ShouldDeleteAndCommit()
        {
            int id = 1;

            await _service.DeleteOwnerAsync(id);

            _mockOwnerRepo.Verify(r => r.DeleteOwnerAsync(id), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateOwnerAsync_ShouldUpdateAndCommit()
        {
            int id = 1;
            var updatedOwner = Mock.Of<IVehicleOwner>();

            await _service.UpdateOwnerAsync(id, updatedOwner);

            _mockOwnerRepo.Verify(r => r.UpdateOwnerAsync(id, updatedOwner), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }
    }
}
