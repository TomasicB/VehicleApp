using AutoMapper;
using FluentAssertions;
using Moq;
using MockQueryable.Moq;
using NSubstitute;
using Vehicle.DAL.Context;
using Vehicle.DAL.Entities;
using Vehicle.Models.Common;
using Vehicle.Models.DTOs;
using Xunit;

namespace Vehicle.Repository.Tests
{
    public class VehicleOwnerRepositoryTests
    {
        private readonly Mock<IVehicleDbContext> _mockDbContext;
        private readonly IMapper _mapper;
        private readonly List<VehicleOwner> _ownerData;

        public VehicleOwnerRepositoryTests()
        {
            _mockDbContext = new Mock<IVehicleDbContext>();

            _ownerData = new List<VehicleOwner>
            {
                new VehicleOwner { Id = 1, FirstName = "Branimir", LastName = "Tomašić", DOB = new DateOnly(1997, 1, 20)},
                new VehicleOwner { Id = 2, FirstName = "Željka", LastName = "Dragila", DOB = new DateOnly(1996, 7, 19)},
                new VehicleOwner { Id = 3, FirstName = "Josip", LastName = "Tomašić", DOB = new DateOnly(2001, 5, 14)}
            };

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IVehicleOwner, VehicleOwner>().ForMember(dest => dest.VehicleRegistrations, opt => opt.Ignore());
                cfg.CreateMap<VehicleOwner, VehicleOwnerDTO>();
            });
            _mapper = config.CreateMapper();
        }

        private VehicleOwnerRepository CreateRepository()
        {
            var mockSet = _ownerData.BuildMockDbSet();
            _mockDbContext.Setup(c => c.VehicleOwner).Returns(mockSet.Object);

            return new VehicleOwnerRepository(_mockDbContext.Object, _mapper);
        }

        [Fact]
        public async Task GetOwnersAsync_ShouldReturnAllOwners()
        {
            var repo = CreateRepository();

            var result = await repo.GetOwnersAsync();

            result.Should().HaveCount(_ownerData.Count);
            result.Select(o => o.FirstName).Should().BeEquivalentTo(_ownerData.Select(o => o.FirstName));
        }

        [Fact]
        public async Task GetOwnerByIdAsync_ShouldReturnMatchingOwner()
        {
            var repo = CreateRepository();

            var result = await repo.GetOwnerByIdAsync(2);

            result.Should().ContainSingle(o => o.FirstName == "Željka");
        }

        [Fact]
        public async Task GetOwnersByNameAsync_ReturnsMatchingByFirstOrLastName()
        {
            var repo = CreateRepository();

            var resultFirstName = await repo.GetOwnersByNameAsync("Branimir");
            var resultLastName = await repo.GetOwnersByNameAsync("Tomašić");

            resultFirstName.Should().OnlyContain(o => o.FirstName == "Branimir");
            resultLastName.Should().HaveCount(2).And.ContainSingle(o => o.FirstName == "Branimir");
        }

        [Fact]
        public async Task InsertOwnerAsync_ShouldAddOwner_WhenProvided()
        {
            var repo = CreateRepository();

            var newOwnerMock = new Mock<IVehicleOwner>();
            newOwnerMock.SetupGet(o => o.FirstName).Returns("Ivan");
            newOwnerMock.SetupGet(o => o.LastName).Returns("Tomašić");
            newOwnerMock.SetupGet(o => o.DOB).Returns(new DateOnly(2004, 1, 20));

            await repo.InsertOwnerAsync(newOwnerMock.Object);

            _mockDbContext.Verify(c => c.VehicleOwner.Add(It.IsAny<VehicleOwner>()), Times.Once);
        }

        [Fact]
        public async Task InsertOwnerAsync_ShouldNotAdd_WhenNull()
        {
            var repo = CreateRepository();

            await repo.InsertOwnerAsync(null);

            _mockDbContext.Verify(c => c.VehicleOwner.Add(It.IsAny<VehicleOwner>()), Times.Never);
        }

        [Fact]
        public async Task DeleteOwnerAsync_ShouldRemove_WhenExists()
        {
            var repo = CreateRepository();

            _mockDbContext.Setup(c => c.VehicleOwner.FindAsync(1))
                .ReturnsAsync(_ownerData.First(o => o.Id == 1));

            await repo.DeleteOwnerAsync(1);

            _mockDbContext.Verify(c => c.VehicleOwner.Remove(It.IsAny<VehicleOwner>()), Times.Once);
        }

        [Fact]
        public async Task DeleteOwnerAsync_ShouldNotRemove_WhenNotFound()
        {
            var repo = CreateRepository();

            _mockDbContext.Setup(c => c.VehicleOwner.FindAsync(99)).ReturnsAsync((VehicleOwner)null);

            await repo.DeleteOwnerAsync(99);

            _mockDbContext.Verify(c => c.VehicleOwner.Remove(It.IsAny<VehicleOwner>()), Times.Never);
        }

        [Fact]
        public async Task UpdateOwnerAsync_ShouldUpdate_WhenExists()
        {
            var repo = CreateRepository();
            var existingOwner = _ownerData.First(o => o.Id == 3);
            _mockDbContext.Setup(c => c.VehicleOwner.FindAsync(3)).ReturnsAsync(existingOwner);

            var updateOwnerMock = new Mock<IVehicleOwner>();
            updateOwnerMock.SetupGet(o => o.FirstName).Returns("JosipN");
            updateOwnerMock.SetupGet(o => o.LastName).Returns("TomašićN");
            updateOwnerMock.SetupGet(o => o.DOB).Returns(new DateOnly(2001, 5, 14));

            await repo.UpdateOwnerAsync(3, updateOwnerMock.Object);

            existingOwner.FirstName.Should().Be("JosipN");
            existingOwner.LastName.Should().Be("TomašićN");
        }

        [Fact]
        public async Task UpdateOwnerAsync_ShouldDoNothing_WhenNotFound()
        {
            var repo = CreateRepository();

            _mockDbContext.Setup(c => c.VehicleOwner.FindAsync(99)).ReturnsAsync((VehicleOwner)null);

            var updateOwnerMock = new Mock<IVehicleOwner>();

            await repo.UpdateOwnerAsync(99, updateOwnerMock.Object);

            _mockDbContext.Verify(c => c.VehicleOwner.FindAsync(99), Times.Once);
        }
    }
}
