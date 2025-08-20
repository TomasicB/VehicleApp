using FluentAssertions;
using NSubstitute;
using Vehicle.DAL.Context;
using Vehicle.Models.Common;
using Vehicle.Models.DTOs;
using Vehicle.Repository.Common;
using Xunit;

namespace Project.Repository.Tests.Repositories;

public class VehicleEngineRepositoryTests
{
    private readonly IVehicleEngineRepository _repoMock;
    private readonly VehicleEngineDTO _engine = new VehicleEngineDTO { Type = "Hybrid", Abrv = "H" };

    public VehicleEngineRepositoryTests()
    {
        _repoMock = Substitute.For<IVehicleEngineRepository>();
    }

    [Fact]
    public async Task GetEngineAsync_Should_ReturnData()
    {
        IEnumerable<IVehicleEngine> result = await _repoMock.GetEngineAsync();
        result.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetEngineByIdAsync_ReturnsData()
    {
        IEnumerable<IVehicleEngine> engine = await _repoMock.GetEngineByIdAsync(1);
        engine.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetEngineByIdAsync_ReturnsNull()
    {
        IEnumerable<IVehicleEngine> engine = await _repoMock.GetEngineByIdAsync(100);
        engine.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task GetEngineByNameAsync_ReturnsData()
    {
        IEnumerable<IVehicleEngine> engine = await _repoMock.GetEngineByNameAsync("Petrol");
        engine.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetEngineByNameAsync_ReturnsNull()
    {
        var engine = await _repoMock.GetEngineByNameAsync("x");
        engine.Should().BeNullOrEmpty();
    }
}