using AutoMapper;
using FluentAssertions;
using Moq;
using MockQueryable.Moq;
using Vehicle.DAL.Context;
using Vehicle.DAL.Entities;
using Vehicle.Models.Common;
using Vehicle.Models.DTOs;
using Xunit;

namespace Vehicle.Repository.Tests
{
    public class VehicleMakeRepositoryTests
    {
        private readonly Mock<IVehicleDbContext> _mockDbContext;
        private readonly IMapper _mapper;
        private readonly List<VehicleMake> _makeData;

        public VehicleMakeRepositoryTests()
        {
            _mockDbContext = new Mock<IVehicleDbContext>();

            _makeData = new List<VehicleMake>
            {
                new VehicleMake { Id = 1, Name = "Škoda", Abrv = "š"},
                new VehicleMake { Id = 2, Name = "Suzuki", Abrv = "S"},
                new VehicleMake { Id = 3, Name = "Opel", Abrv = "O"}
            };

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IVehicleMake, VehicleMake>().ForMember(dest => dest.VehicleModels, opt => opt.Ignore()); 
                cfg.CreateMap<VehicleMake, VehicleMakeDTO>();
            });
            _mapper = config.CreateMapper();
        }

        private VehicleMakeRepository CreateRepository()
        {
            var mockSet = _makeData.BuildMockDbSet();
            _mockDbContext.Setup(c => c.VehicleMake).Returns(mockSet.Object);

            return new VehicleMakeRepository(_mockDbContext.Object, _mapper);
        }

        [Fact]
        public async Task GetMakeAsync_ShouldReturnAllMakes()
        {
            // Arrange
            var repo = CreateRepository();

            // Act
            var result = await repo.GetMakeAsync();

            // Assert
            result.Should().HaveCount(_makeData.Count);
            result.Select(m => m.Name).Should().BeEquivalentTo(_makeData.Select(m => m.Name));
        }

        [Fact]
        public async Task GetMakeByIdAsync_ShouldReturnCorrectMake()
        {
            var repo = CreateRepository();

            var id = 2;
            var result = await repo.GetMakeByIdAsync(id);

            result.Should().ContainSingle();
            result.First().Name.Should().Be("Suzuki");
        }

        [Fact]
        public async Task GetMakesByNameAsync_ShouldReturnMatchingMakes_ByNameOrAbrv()
        {
            var repo = CreateRepository();

            var resultByName = await repo.GetMakesByNameAsync("Škoda");
            var resultByAbrv = await repo.GetMakesByNameAsync("O");

            resultByName.Should().ContainSingle(m => m.Name == "Škoda");
            resultByAbrv.Should().ContainSingle(m => m.Abrv == "O");
        }

        [Fact]
        public async Task InsertMakeAsync_ShouldAddMake_WhenNotNull()
        {
            var repo = CreateRepository();

            var newMakeMock = new Mock<IVehicleMake>();
            newMakeMock.Setup(m => m.Name).Returns("BMW");
            newMakeMock.Setup(m => m.Abrv).Returns("B");

            await repo.InsertMakeAsync(newMakeMock.Object);

            _mockDbContext.Verify(c => c.VehicleMake.Add(It.IsAny<VehicleMake>()), Times.Once);
        }

        [Fact]
        public async Task InsertMakeAsync_ShouldNotAddMake_WhenNull()
        {
            var repo = CreateRepository();

            await repo.InsertMakeAsync(null);

            _mockDbContext.Verify(c => c.VehicleMake.Add(It.IsAny<VehicleMake>()), Times.Never);
        }

        [Fact]
        public async Task DeleteMakeAsync_ShouldRemoveMake_WhenExists()
        {
            var repo = CreateRepository();

            var idToDelete = 1;
            _mockDbContext.Setup(c => c.VehicleMake.FindAsync(idToDelete)).ReturnsAsync(_makeData.First(m => m.Id == idToDelete));

            await repo.DeleteMakeAsync(idToDelete);

            _mockDbContext.Verify(c => c.VehicleMake.Remove(It.IsAny<VehicleMake>()), Times.Once);
        }

        [Fact]
        public async Task DeleteMakeAsync_ShouldNotRemoveMake_WhenNotFound()
        {
            var repo = CreateRepository();

            var idToDelete = 99;
            _mockDbContext.Setup(c => c.VehicleMake.FindAsync(idToDelete)).ReturnsAsync((VehicleMake)null);

            await repo.DeleteMakeAsync(idToDelete);

            _mockDbContext.Verify(c => c.VehicleMake.Remove(It.IsAny<VehicleMake>()), Times.Never);
        }

        [Fact]
        public async Task UpdateMakeAsync_ShouldUpdateMake_WhenExists()
        {
            var repo = CreateRepository();

            var idToUpdate = 2;
            var makeToUpdate = _makeData.First(m => m.Id == idToUpdate);
            _mockDbContext.Setup(c => c.VehicleMake.FindAsync(idToUpdate)).ReturnsAsync(makeToUpdate);

            var mockUpdateMake = new Mock<IVehicleMake>();
            mockUpdateMake.Setup(m => m.Name).Returns("Mazda");
            mockUpdateMake.Setup(m => m.Abrv).Returns("M");

            await repo.UpdateMakeAsync(idToUpdate, mockUpdateMake.Object);

            makeToUpdate.Name.Should().Be("Mazda");
            makeToUpdate.Abrv.Should().Be("M");
        }

        [Fact]
        public async Task UpdateMakeAsync_ShouldDoNothing_WhenNotFound()
        {
            var repo = CreateRepository();

            var idToUpdate = 99;
            _mockDbContext.Setup(c => c.VehicleMake.FindAsync(idToUpdate)).ReturnsAsync((VehicleMake)null);

            var mockUpdateMake = new Mock<IVehicleMake>();

            await repo.UpdateMakeAsync(idToUpdate, mockUpdateMake.Object);

            _mockDbContext.Verify(c => c.VehicleMake.FindAsync(idToUpdate), Times.Once);
        }
    }
}
