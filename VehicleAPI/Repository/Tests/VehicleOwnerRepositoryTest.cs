using FluentAssertions;
using NSubstitute;
using Vehicle.DAL.Context;
using Vehicle.Models.Common;
using Vehicle.Models.DTOs;
using Vehicle.Repository.Common;
using Xunit;

namespace Project.Repository.Tests.Repositories;

public class VehicleOwnerRepositoryTests
{
    private readonly IVehicleOwnerRepository _repoMock;
    private readonly VehicleOwnerDTO _owner = new VehicleOwnerDTO { FirstName = "Josip", LastName = "Tomašić", DOB = new DateOnly(2000,5,14)};

    public VehicleOwnerRepositoryTests()
    {
        _repoMock = Substitute.For<IVehicleOwnerRepository>();
    }

    [Fact]
    public async Task GetOwnerAsync_Should_ReturnData()
    {
        IEnumerable<IVehicleOwner> owner = await _repoMock.GetOwnersAsync();
        owner.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetOwnerByIdAsync_ReturnsData()
    {
        IEnumerable<IVehicleOwner> owner = await _repoMock.GetOwnerByIdAsync(1);
        owner.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetOwnerByIdAsync_ReturnsNull()
    {
        IEnumerable<IVehicleOwner> owner = await _repoMock.GetOwnerByIdAsync(100);
        owner.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task GetOwnerByNameAsync_ReturnsData()
    {
        IEnumerable<IVehicleOwner> owner = await _repoMock.GetOwnersByNameAsync("Branimir");
        owner.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetOwnerByNameAsync_ReturnsNull()
    {
        var owner = await _repoMock.GetOwnersByNameAsync("Snnježana");
        owner.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task InsertOwnerAsync_ReturnsNewOwnerWithId()
    {
        await _repoMock.Received(1).InsertOwnerAsync(Arg.Is<IVehicleOwner>(m => m.FirstName == _owner.FirstName && m.LastName == _owner.LastName));
    }

    [Fact]
    public async Task UpdateOwnerAsync_RetrunsNewAbrvCorrectly()
    {
        _owner.FirstName = "Ivan";
        await _repoMock.Received(1).UpdateOwnerAsync(1, Arg.Is<IVehicleOwner>(m => m.FirstName == _owner.FirstName && m.LastName == _owner.LastName));
    }

    [Fact]
    public async Task DeleteOwnerAsync_ReturnsNullCorrectly()
    {
        await _repoMock.DeleteOwnerAsync(id: 1);
        //await _repoMock.SaveChangesAsync();

        IEnumerable<IVehicleOwner> owner = await _repoMock.GetOwnerByIdAsync(1);
        owner.Should().BeEmpty();
    }
}