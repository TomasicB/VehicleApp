using FluentAssertions;
using NSubstitute;
using Vehicle.DAL.Context;
using Vehicle.Models.Common;
using Vehicle.Models.DTOs;
using Vehicle.Repository;
using Vehicle.Repository.Common;
using Xunit;

namespace Project.Repository.Tests.Repositories;

public class VehicleMakeRepositoryTests
{
    private readonly IVehicleMakeRepository _repoMock;
    private readonly VehicleMakeDTO _make = new VehicleMakeDTO { Name = "Volkswagen", Abrv = "VW" };

    public VehicleMakeRepositoryTests()
    {
        _repoMock = Substitute.For<IVehicleMakeRepository>();
    }

    [Fact]
    public async Task GetMakeAsync_Should_ReturnData()
    {
        IEnumerable<IVehicleMake> make = await _repoMock.GetMakeAsync();
        make.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetMakeByIdAsync_ReturnsData()
    {
        IEnumerable<IVehicleMake> make = await _repoMock.GetMakeByIdAsync(1);
        make.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetMakeByIdAsync_ReturnsNull()
    {
        IEnumerable<IVehicleMake> make = await _repoMock.GetMakeByIdAsync(100);
        make.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task GetMakeByNameAsync_ReturnsData()
    {
        IEnumerable<IVehicleMake> make = await _repoMock.GetMakesByNameAsync("Škoda");
        make.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetMakeByNameAsync_ReturnsNull()
    {
        var make = await _repoMock.GetMakesByNameAsync("Toyta");
        make.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task InsertMakeAsync_ReturnsNewMakeWithId()
    {
        await _repoMock.Received(1).InsertMakeAsync(Arg.Is<IVehicleMake>(m => m.Name == _make.Name && m.Abrv == _make.Abrv));
    }

    [Fact]
    public async Task UpdateMakeAsync_RetrunsNewAbrvCorrectly()
    {
        _make.Abrv = "V.W.";
        await _repoMock.Received(1).UpdateMakeAsync(1, Arg.Is<IVehicleMake>(m => m.Name == _make.Name && m.Abrv == _make.Abrv));
    }

    [Fact]
    public async Task DeleteMakeAsync_ReturnsNullCorrectly()
    {
        await _repoMock.DeleteMakeAsync(id: 1);

        IEnumerable<IVehicleMake> make = await _repoMock.GetMakeByIdAsync(1);
        make.Should().BeEmpty();
    }
}