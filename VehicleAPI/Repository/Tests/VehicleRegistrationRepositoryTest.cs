using AutoMapper;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using NSubstitute;
using System.Net.Sockets;
using Vehicle.DAL.Context;
using Vehicle.DAL.Entities;
using Vehicle.Models.Common;
using Vehicle.Models.Common.Write;
using Vehicle.Models.DTOs;
using Xunit;

namespace Vehicle.Repository.Tests
{
    public class VehicleRegistrationRepositoryTests
    {
        private readonly Mock<IVehicleDbContext> _mockDbContext;
        private readonly IMapper _mapper;

        private readonly List<VehicleMake> _makeData;
        private readonly List<VehicleModel> _modelData;
        private readonly List<VehicleEngine> _engineData;
        private readonly List<VehicleOwner> _ownerData;
        private readonly List<VehicleRegistration> _registrationData;

        public VehicleRegistrationRepositoryTests()
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

            _engineData = new List<VehicleEngine>
            {
                new VehicleEngine { Id = 1, Type = "Petrol", Abrv = "P"},
                new VehicleEngine { Id = 2, Type = "Diesel", Abrv = "D"},
                new VehicleEngine { Id = 3, Type = "Electric", Abrv = "EV"}
            };

            _ownerData = new List<VehicleOwner>
            {
                new VehicleOwner { Id = 1, FirstName = "Branimir", LastName = "Tomašić", DOB = new DateOnly(1997, 1, 20)},
                new VehicleOwner { Id = 2, FirstName = "Željka", LastName = "Dragila", DOB = new DateOnly(1996, 7, 19)},
                new VehicleOwner { Id = 3, FirstName = "Josip", LastName = "Tomašić", DOB = new DateOnly(2001, 5, 14)}
            };

            _registrationData = new List<VehicleRegistration>
            {
                new VehicleRegistration
                {
                    Id = 1,
                    RegistrationNumber = "NA425DG",
                    VehicleModel = _modelData[0],
                    VehicleModelId = 1,
                    VehicleEngine = _engineData[0],
                    VehicleEngineId = 1,
                    VehicleOwner = _ownerData[0],
                    VehicleOwnerId = 1
                },
                new VehicleRegistration
                {
                    Id = 2,
                    RegistrationNumber = "OS268PU",
                    VehicleModel = _modelData[2],
                    VehicleModelId = 3,
                    VehicleEngine = _engineData[1],
                    VehicleEngineId = 2,
                    VehicleOwner = _ownerData[1],
                    VehicleOwnerId = 2
                }
            };

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IVehicleRegistration, VehicleRegistration>()
                    .ForMember(dest => dest.VehicleOwner, opt => opt.Ignore())
                    .ForMember(dest => dest.VehicleModel, opt => opt.Ignore())
                    .ForMember(dest => dest.VehicleEngine, opt => opt.Ignore());
                cfg.CreateMap<VehicleRegistration, VehicleRegistrationDTO>()
                    .ForMember(dest => dest.VehicleModel, opt => opt.MapFrom(src => src.VehicleModel))
                    .ForMember(dest => dest.VehicleEngine, opt => opt.MapFrom(src => src.VehicleEngine))
                    .ForMember(dest => dest.VehicleOwner, opt => opt.MapFrom(src => src.VehicleOwner));

                cfg.CreateMap<VehicleMake, VehicleMakeDTO>();
                cfg.CreateMap<VehicleModel, VehicleModelDTO>();
                cfg.CreateMap<VehicleEngine, VehicleEngineDTO>();
                cfg.CreateMap<VehicleOwner, VehicleOwnerDTO>();
            });
            _mapper = config.CreateMapper();
        }

        private VehicleRegistrationRepository CreateRepository()
        {
            var mockSetR = _registrationData.BuildMockDbSet();
            var mockSetM = _modelData.BuildMockDbSet();
            var mockSetE = _engineData.BuildMockDbSet();
            var mockSetO = _ownerData.BuildMockDbSet();

            _mockDbContext.Setup(c => c.VehicleRegistration).Returns(mockSetR.Object);
            _mockDbContext.Setup(c => c.VehicleModel).Returns(mockSetM.Object);
            _mockDbContext.Setup(c => c.VehicleEngine).Returns(mockSetE.Object);
            _mockDbContext.Setup(c => c.VehicleOwner).Returns(mockSetO.Object);
            
            return new VehicleRegistrationRepository(_mockDbContext.Object, _mapper);
        }

        [Fact]
        public async Task GetRegistrationsAsync_ShouldReturnAllRegistrations()
        {
            var repo = CreateRepository();
            var result = await repo.GetRegistrationsAsync();

            result.Should().HaveCount(_registrationData.Count);
            result.Select(r => r.RegistrationNumber)
                  .Should().BeEquivalentTo(_registrationData.Select(r => r.RegistrationNumber));
        }

        [Fact]
        public async Task GetRegistrationByIdAsync_ShouldReturnCorrectRegistration()
        {
            var result = await CreateRepository().GetRegistrationByIdAsync(2);

            result.Should().ContainSingle(r => r.RegistrationNumber == "OS268PU");
        }

        [Fact]
        public async Task GetRegistrationByNumberAsync_ShouldReturnByNumber()
        {
            var result1 = await CreateRepository().GetRegistrationByNumberAsync("NA425DG");
            result1.Should().ContainSingle(r => r.RegistrationNumber == "NA425DG");
        }

        [Fact]
        public async Task GetRegistrationsByEngineAsync_ShouldReturnMatchingEngine()
        {
            var repo = CreateRepository();

            var resultP = await repo.GetRegistrationsByEngineAsync("Petrol");
            resultP.Should().ContainSingle(r => r.RegistrationNumber == "NA425DG");

        }

        [Fact]
        public async Task GetRegistrationsByModelAsync_ShouldReturnMatchingModel()
        {
            var repo = CreateRepository();

            var resultO = await repo.GetRegistrationsByModelAsync("O");
            resultO.Should().ContainSingle(r => r.RegistrationNumber == "NA425DG");
        }

        [Fact]
        public async Task GetRegistrationsByOwnerAsync_ShouldReturnMatchingOwner()
        {
            var repo = CreateRepository();

            var resultB = await repo.GetRegistrationsByOwnerAsync("Branimir");
            resultB.Should().ContainSingle(r => r.RegistrationNumber == "NA425DG");
        }

        [Fact]
        public async Task InsertRegistrationAsync_AddsWhenAllExist()
        {
            var repo = CreateRepository();

            _mockDbContext.Setup(c => c.VehicleModel.FindAsync(6))
                .ReturnsAsync(_modelData.First(m => m.Id == 6));

            _mockDbContext.Setup(c => c.VehicleEngine.FindAsync(3))
                .ReturnsAsync(_engineData.First(e => e.Id == 3));

            _mockDbContext.Setup(c => c.VehicleOwner.FindAsync(3))
                .ReturnsAsync(_ownerData.First(o => o.Id == 3));

            var newRegMock = new Mock<IVehicleRegistration>();
            newRegMock.Setup(r => r.RegistrationNumber).Returns("OS111OS");

            await repo.InsertRegistrationAsync(newRegMock.Object, 6, 3, 3);
            _mockDbContext.Verify(c => c.VehicleRegistration.Add(It.IsAny<VehicleRegistration>()), Times.Once);
        }

        [Fact]
        public async Task InsertRegistrationAsync_DoesNothingWhenDependencyMissing()
        {
            var repo = CreateRepository();
            var newRegMock = new Mock<IVehicleRegistration>();

            await repo.InsertRegistrationAsync(newRegMock.Object, 99, 1, 1);
            _mockDbContext.Verify(c => c.VehicleRegistration.Add(It.IsAny<VehicleRegistration>()), Times.Never);

            await repo.InsertRegistrationAsync(newRegMock.Object, 1, 99, 1);
            _mockDbContext.Verify(c => c.VehicleRegistration.Add(It.IsAny<VehicleRegistration>()), Times.Never);

            await repo.InsertRegistrationAsync(newRegMock.Object, 1, 1, 99);
            _mockDbContext.Verify(c => c.VehicleRegistration.Add(It.IsAny<VehicleRegistration>()), Times.Never);

            await repo.InsertRegistrationAsync(null, 1, 1, 1);
            _mockDbContext.Verify(c => c.VehicleRegistration.Add(It.IsAny<VehicleRegistration>()), Times.Never);
        }

        [Fact]
        public async Task DeleteRegistrationAsync_RemovesWhenExists()
        {
            var repo = CreateRepository();
            var idToDelete = 1;

            _mockDbContext.Setup(c => c.VehicleRegistration.FindAsync(1)).ReturnsAsync(_registrationData.First(m => m.Id == idToDelete));

            await repo.DeleteRegistrationAsync(idToDelete);
            _mockDbContext.Verify(c => c.VehicleRegistration.Remove(It.IsAny<VehicleRegistration>()), Times.Once);
        }

        [Fact]
        public async Task DeleteRegistrationAsync_DoesNothingWhenNotFound()
        {
            var repo = CreateRepository();
            var idToDelete = 99;

            _mockDbContext.Setup(c => c.VehicleRegistration.FindAsync(99)).ReturnsAsync(_registrationData.FirstOrDefault(m => m.Id == idToDelete));

            await repo.DeleteRegistrationAsync(idToDelete);
            _mockDbContext.Verify(c => c.VehicleRegistration.Remove(It.IsAny<VehicleRegistration>()), Times.Never);
        }

        [Fact]
        public async Task UpdateRegistrationAsync_UpdatesWhenExists()
        {
            var repo = CreateRepository();
            var existing = _registrationData[0];
            _mockDbContext.Setup(c => c.VehicleRegistration.FindAsync(1)).ReturnsAsync(existing);

            //var mockUpdateE = new Mock<IVehicleEngineWrite>();
            //mockUpdateE.Setup(e => e.Id).Returns(1);

            //var mockUpdateM = new Mock<IVehicleModelWrite>();
            //mockUpdateM.Setup(m => m.Id).Returns(1);

            //var mockUpdateO = new Mock<IVehicleOwnerWrite>();
            //mockUpdateO.Setup(o => o.Id).Returns(1);

            var mockUpdate = new Mock<IVehicleRegistrationWrite>();
            mockUpdate.SetupGet(r => r.RegistrationNumber).Returns("NA476NA");
            mockUpdate.SetupGet(r => r.VehicleEngine.Id).Returns(1);
            mockUpdate.SetupGet(r => r.VehicleModel.Id).Returns(1);
            mockUpdate.SetupGet(r => r.VehicleOwner.Id).Returns(1);

            await repo.UpdateRegistrationAsync(1, mockUpdate.Object);
            existing.RegistrationNumber.Should().Be("NA476NA");
        }

        [Fact]
        public async Task UpdateRegistrationAsync_DoesNothingWhenNotFound()
        {
            var repo = CreateRepository();

            _mockDbContext.Setup(c => c.VehicleRegistration.FindAsync(99)).ReturnsAsync((VehicleRegistration)null);

            var mockWrite = new Mock<IVehicleRegistrationWrite>();
            
            await repo.UpdateRegistrationAsync(99, mockWrite.Object);
            _mockDbContext.Verify(c => c.VehicleRegistration.FindAsync(99), Times.Once);
        }
    }
}
