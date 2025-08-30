using AutoMapper;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using NuGet.Packaging;
using Vehicle.DAL.Context;
using Vehicle.DAL.Entities;
using Vehicle.Models.Common;
using Vehicle.Models.Common.Write;
using Vehicle.Models.DTOs;
using Xunit;

namespace Vehicle.Repository.Tests
{
    public class VehicleModelRepositoryTests
    {
        private readonly Mock<IVehicleDbContext> _mockDbContext;
        private readonly IMapper _mapper;
        private readonly List<VehicleModel> _modelData;
        private readonly List<VehicleMake> _makeData;

        public VehicleModelRepositoryTests()
        {
            _mockDbContext = new Mock<IVehicleDbContext>();

            _makeData = new List<VehicleMake>
            {
                new VehicleMake { Id = 1, Name = "Škoda", Abrv = "š"},
                new VehicleMake { Id = 2, Name = "Suzuki", Abrv = "S"},
                new VehicleMake { Id = 3, Name = "Opel", Abrv = "O"}
            };

            _modelData = new List<VehicleModel>
            {
                new VehicleModel { Id = 1, Name = "Octavia", Abrv = "O", VehicleMakeId = 1, VehicleMake = _makeData[0]},
                new VehicleModel { Id = 2, Name = "Superb", Abrv = "S", VehicleMakeId = 1, VehicleMake = _makeData[0]},
                new VehicleModel { Id = 3, Name = "Vitara", Abrv = "V", VehicleMakeId = 2, VehicleMake = _makeData[1]},
                new VehicleModel { Id = 4, Name = "Astra", Abrv = "A", VehicleMakeId = 3, VehicleMake = _makeData[2]},
                new VehicleModel { Id = 5, Name = "Insignia", Abrv = "I", VehicleMakeId = 3, VehicleMake = _makeData[2]},
                new VehicleModel { Id = 6, Name = "Corsa", Abrv = "C", VehicleMakeId = 3, VehicleMake = _makeData[2]}
            };

            _makeData[0].VehicleModels.AddRange(_modelData.Where(m => m.VehicleMakeId == 1));
            _makeData[1].VehicleModels.AddRange(_modelData.Where(m => m.VehicleMakeId == 2));
            _makeData[2].VehicleModels.AddRange(_modelData.Where(m => m.VehicleMakeId == 3));

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IVehicleModel, VehicleModel>()
                    .ForMember(dest => dest.VehicleRegistrations, opt => opt.Ignore());
                cfg.CreateMap<VehicleModel, VehicleModelDTO>()
                   .ForMember(d => d.VehicleMake, opt => opt.MapFrom(src => src.VehicleMake));
                cfg.CreateMap<VehicleMake, VehicleMakeDTO>();
            });
            _mapper = config.CreateMapper();
        }

        private VehicleModelRepository CreateRepository()
        {
            var mockSet = _modelData.BuildMockDbSet();
            _mockDbContext.Setup(c => c.VehicleModel).Returns(mockSet.Object);
            
            return new VehicleModelRepository(_mockDbContext.Object, _mapper);
        }

        [Fact]
        public async Task GetModelsAsync_ShouldReturnAllModels()
        {
            var repo = CreateRepository();

            var result = await repo.GetModelsAsync();

            result.Should().HaveCount(_modelData.Count);
            result.Select(m => m.Name).Should().BeEquivalentTo(_modelData.Select(m => m.Name));
        }

        [Fact]
        public async Task GetModelByIdAsync_ReturnsCorrectModel()
        {
            var repo = CreateRepository();

            var result = await repo.GetModelByIdAsync(3);

            result.Should().ContainSingle(m => m.Name == "Vitara");
        }

        [Fact]
        public async Task GetModelsByNameAsync_ReturnsMatchingByNameOrAbrv()
        {
            var repo = CreateRepository();

            var byName = await repo.GetModelsByNameAsync("Astra");
            var byAbrv = await repo.GetModelsByNameAsync("S");

            byName.Should().ContainSingle(m => m.Name == "Astra");
            byAbrv.Should().ContainSingle(m => m.Abrv == "S");
        }

        [Fact]
        public async Task GetModelsByMakeAsync_ReturnsCorrectModels()
        {
            var repo = CreateRepository();

            var byName = await repo.GetModelsByMakeAsync("Škoda");
            var byAbrv = await repo.GetModelsByMakeAsync("S");

            byName.Should().HaveCount(2);
            byAbrv.Should().HaveCount(1).And.ContainSingle(m => m.Name == "Vitara");
        }

        [Fact]
        public async Task InsertModelAsync_AddsModelWhenMakeExists()
        {
            var repo = CreateRepository();

            var newModelMock = new Mock<IVehicleModel>();
            newModelMock.Setup(m => m.Name).Returns("Fabia");
            newModelMock.Setup(m => m.Abrv).Returns("F");

            _mockDbContext.Setup(c => c.VehicleMake.FindAsync(1))
                .ReturnsAsync(_makeData.First(m => m.Id == 1));

            await repo.InsertModelAsync(newModelMock.Object, 1);
            _mockDbContext.Verify(c => c.VehicleModel.Add(It.IsAny<VehicleModel>()), Times.Once);
        }

        [Fact]
        public async Task InsertModelAsync_DoesNothingWhenMakeNotFound()
        {
            var repo = CreateRepository();

            var newModelMock = new Mock<IVehicleModel>();

            _mockDbContext.Setup(c => c.VehicleMake.FindAsync(99)).ReturnsAsync(_makeData.FirstOrDefault(m => m.Id == 99));

            await repo.InsertModelAsync(newModelMock.Object, 99);
            _mockDbContext.Verify(c => c.VehicleModel.Add(It.IsAny<VehicleModel>()), Times.Never);
        }

        [Fact]
        public async Task DeleteModelAsync_RemovesModelWhenExists()
        {
            var repo = CreateRepository();

            _mockDbContext.Setup(c => c.VehicleModel.FindAsync(1)).ReturnsAsync(_modelData.First(m => m.Id == 1));

            await repo.DeleteModelAsync(1);
            _mockDbContext.Verify(c => c.VehicleModel.Remove(It.IsAny<VehicleModel>()), Times.Once);
        }

        [Fact]
        public async Task DeleteModelAsync_DoesNothingWhenNotFound()
        {
            var repo = CreateRepository();

            _mockDbContext.Setup(c => c.VehicleModel.FindAsync(99)).ReturnsAsync((VehicleModel)null);

            await repo.DeleteModelAsync(99);
            _mockDbContext.Verify(c => c.VehicleModel.Remove(It.IsAny<VehicleModel>()), Times.Never);
        }

        [Fact]
        public async Task UpdateModelAsync_UpdatesWhenExists()
        {
            var repo = CreateRepository();
            var existing = _modelData.First(m => m.Id == 6);
            _mockDbContext.Setup(c => c.VehicleModel.FindAsync(6)).ReturnsAsync(existing);

            var mockUpdate = new Mock<IVehicleModelWrite>();
            mockUpdate.Setup(m => m.Name).Returns("Zafira");
            mockUpdate.Setup(m => m.Abrv).Returns("Z");
            mockUpdate.Setup(m => m.VehicleMake.Id).Returns(3);

            _mockDbContext.Setup(c => c.VehicleMake.FindAsync(3)).ReturnsAsync(_makeData.First(m => m.Id == 3));

            await repo.UpdateModelAsync(6, mockUpdate.Object);

            existing.Name.Should().Be("Zafira");
            existing.Abrv.Should().Be("Z");
            existing.VehicleMakeId.Should().Be(3);
        }

        [Fact]
        public async Task UpdateModelAsync_IgnoresWhenNotFound()
        {
            var repo = CreateRepository();
            _mockDbContext.Setup(c => c.VehicleModel.FindAsync(99)).ReturnsAsync((VehicleModel)null);

            var mockUpdate = new Mock<IVehicleModelWrite>();

            await repo.UpdateModelAsync(99, mockUpdate.Object);
            _mockDbContext.Verify(c => c.VehicleModel.FindAsync(99), Times.Once);
        }
    }
}
