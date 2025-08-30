using FluentAssertions;
using Moq;
using Vehicle.Models.Common;
using Vehicle.Models.Common.Write;
using Vehicle.Repository.Common;
using Xunit;

namespace Vehicle.Service.Tests
{
    public class VehicleRegistrationServiceTests
    {
        private readonly Mock<IVehicleRegistrationRepository> _mockRegRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly VehicleRegistrationService _service;

        public VehicleRegistrationServiceTests()
        {
            _mockRegRepo = new Mock<IVehicleRegistrationRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(u => u.RegRepo).Returns(_mockRegRepo.Object);
            _service = new VehicleRegistrationService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetRegistrationsAsync_ShouldReturnAllRegistrations()
        {
            var expected = new List<IVehicleRegistration> { Mock.Of<IVehicleRegistration>() };
            _mockRegRepo.Setup(r => r.GetRegistrationsAsync()).ReturnsAsync(expected);

            var result = await _service.GetRegistrationsAsync();

            result.Should().BeEquivalentTo(expected);
            _mockRegRepo.Verify(r => r.GetRegistrationsAsync(), Times.Once);
        }

        [Fact]
        public async Task GetRegistrationByIdAsync_ShouldReturnMatchingRegistration()
        {
            int id = 1;
            var expected = new List<IVehicleRegistration> { Mock.Of<IVehicleRegistration>() };
            _mockRegRepo.Setup(r => r.GetRegistrationByIdAsync(id)).ReturnsAsync(expected);

            var result = await _service.GetRegistrationByIdAsync(id);

            result.Should().BeEquivalentTo(expected);
            _mockRegRepo.Verify(r => r.GetRegistrationByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetRegistrationByNumberAsync_ShouldReturnMatchingRegistration()
        {
            string number = "ABC-123";
            var expected = new List<IVehicleRegistration> { Mock.Of<IVehicleRegistration>() };
            _mockRegRepo.Setup(r => r.GetRegistrationByNumberAsync(number)).ReturnsAsync(expected);

            var result = await _service.GetRegistrationByNumberAsync(number);

            result.Should().BeEquivalentTo(expected);
            _mockRegRepo.Verify(r => r.GetRegistrationByNumberAsync(number), Times.Once);
        }

        [Fact]
        public async Task GetRegistrationsByEngineAsync_ShouldReturnMatchingRegistrations()
        {
            string engine = "V8";
            var expected = new List<IVehicleRegistration> { Mock.Of<IVehicleRegistration>() };
            _mockRegRepo.Setup(r => r.GetRegistrationsByEngineAsync(engine)).ReturnsAsync(expected);

            var result = await _service.GetRegistrationsByEngineAsync(engine);

            result.Should().BeEquivalentTo(expected);
            _mockRegRepo.Verify(r => r.GetRegistrationsByEngineAsync(engine), Times.Once);
        }

        [Fact]
        public async Task GetRegistrationsByModelAsync_ShouldReturnMatchingRegistrations()
        {
            string model = "Model S";
            var expected = new List<IVehicleRegistration> { Mock.Of<IVehicleRegistration>() };
            _mockRegRepo.Setup(r => r.GetRegistrationsByModelAsync(model)).ReturnsAsync(expected);

            var result = await _service.GetRegistrationsByModelAsync(model);

            result.Should().BeEquivalentTo(expected);
            _mockRegRepo.Verify(r => r.GetRegistrationsByModelAsync(model), Times.Once);
        }

        [Fact]
        public async Task GetRegistrationsByOwnerAsync_ShouldReturnMatchingRegistrations()
        {
            string owner = "Alice";
            var expected = new List<IVehicleRegistration> { Mock.Of<IVehicleRegistration>() };
            _mockRegRepo.Setup(r => r.GetRegistrationsByOwnerAsync(owner)).ReturnsAsync(expected);

            var result = await _service.GetRegistrationsByOwnerAsync(owner);

            result.Should().BeEquivalentTo(expected);
            _mockRegRepo.Verify(r => r.GetRegistrationsByOwnerAsync(owner), Times.Once);
        }

        [Fact]
        public async Task InsertRegistrationAsync_ShouldInsertAndCommit()
        {
            var reg = Mock.Of<IVehicleRegistration>();
            int modelId = 1, engineId = 2, ownerId = 3;

            await _service.InsertRegistrationAsync(reg, modelId, engineId, ownerId);

            _mockRegRepo.Verify(r => r.InsertRegistrationAsync(reg, modelId, engineId, ownerId), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteRegistrationAsync_ShouldDeleteAndCommit()
        {
            int id = 1;

            await _service.DeleteRegistrationAsync(id);

            _mockRegRepo.Verify(r => r.DeleteRegistrationAsync(id), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateRegistrationAsync_ShouldUpdateAndCommit()
        {
            int id = 1;
            var updated = Mock.Of<IVehicleRegistrationWrite>();

            await _service.UpdateRegistrationAsync(id, updated);

            _mockRegRepo.Verify(r => r.UpdateRegistrationAsync(id, updated), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }
    }
}
