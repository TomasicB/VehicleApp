using AutoMapper;
using FluentAssertions;
using Moq;
using MockQueryable.Moq;
using Vehicle.DAL.Context;
using Vehicle.DAL.Entities;
using Vehicle.Models.DTOs;
using Xunit;

namespace Vehicle.Repository.Tests
{
    public class VehicleEngineRepositoryTests
    {
        private readonly Mock<IVehicleDbContext> _mockDbContext;
        private readonly IMapper _mapper;
        private readonly List<VehicleEngine> _engineData;

        public VehicleEngineRepositoryTests()
        {
            _mockDbContext = new Mock<IVehicleDbContext>();

            _engineData = new List<VehicleEngine>
            {
                new VehicleEngine { Id = 1, Type = "Petrol", Abrv = "P"},
                new VehicleEngine { Id = 2, Type = "Diesel", Abrv = "D"},
                new VehicleEngine { Id = 3, Type = "Electric", Abrv = "EV"}
            };

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VehicleEngine, VehicleEngineDTO>();
            });
            _mapper = config.CreateMapper();
        }

        private VehicleEngineRepository CreateRepository()
        {
            var mockSet = _engineData.BuildMockDbSet();
            _mockDbContext.Setup(c => c.VehicleEngine).Returns(mockSet.Object);

            return new VehicleEngineRepository(_mockDbContext.Object, _mapper);
        }

        [Fact]
        public async Task GetEngineAsync_ShouldReturnAllEngines()
        {
            // Arrange
            var repo = CreateRepository();

            // Act
            var result = await repo.GetEngineAsync();

            // Assert
            result.Should().HaveCount(3);
        }

        [Fact]
        public async Task GetEngineByIdAsync_ShouldReturnMatchingEngine()
        {
            var repo = CreateRepository();

            var result = await repo.GetEngineByIdAsync(2);

            result.Should().ContainSingle(e => e.Type == "Diesel");
        }

        [Fact]
        public async Task GetEngineByNameAsync_ShouldReturnMatchingEngines_ByTypeOrAbrv()
        {
            var repo = CreateRepository();

            var result1 = await repo.GetEngineByNameAsync("Petrol");
            var result2 = await repo.GetEngineByNameAsync("EV");

            result1.Should().ContainSingle(e => e.Type == "Petrol");
            result2.Should().ContainSingle(e => e.Abrv == "EV");
        }
    }
}
