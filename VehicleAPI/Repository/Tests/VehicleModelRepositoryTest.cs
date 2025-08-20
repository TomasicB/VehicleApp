using FluentAssertions;
using NSubstitute;
using Vehicle.DAL.Context;
using Vehicle.Models.Common;
using Vehicle.Models.Common.Write;
using Vehicle.Models.DTOs;
using Vehicle.Repository.Common;
using Xunit;

namespace Project.Repository.Tests.Repositories;

public class VehicleModelRepositoryTests
{
    private readonly IVehicleModelRepository _repoMock;
    private readonly VehicleModelDTO _model = new VehicleModelDTO { Name = "Golf 6", Abrv = "G6", VehicleMake = new VehicleMakeDTO { Name = "Volkswagen", Abrv = "VW" } };

    public VehicleModelRepositoryTests()
    {
        _repoMock = Substitute.For<IVehicleModelRepository>();
    }

    [Fact]
    public async Task GetModelAsync_Should_ReturnData()
    {
        IEnumerable<IVehicleModel> model = await _repoMock.GetModelsAsync();
        model.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetModelByIdAsync_ReturnsData()
    {
        IEnumerable<IVehicleModel> model = await _repoMock.GetModelByIdAsync(1);
        model.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetModelByIdAsync_ReturnsNull()
    {
        IEnumerable<IVehicleModel> model = await _repoMock.GetModelByIdAsync(100);
        model.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task GetModelByNameAsync_ReturnsData()
    {
        IEnumerable<IVehicleModel> model = await _repoMock.GetModelsByNameAsync("Octavia");
        model.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetModelByNameAsync_ReturnsNull()
    {
        var model = await _repoMock.GetModelsByNameAsync("Fabia");
        model.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task InsertModelAsync_ReturnsNewModelWithId()
    {
        await _repoMock.Received(1).InsertModelAsync(Arg.Is<IVehicleModel>(m => m.Name == _model.Name && m.Abrv == _model.Abrv), 1);
    }

    [Fact]
    public async Task UpdateModelAsync_RetrunsNewAbrvCorrectly()
    {
        _model.Abrv = "V.W.";
        await _repoMock.Received(1).UpdateModelAsync(1, Arg.Is<IVehicleModelWrite>(m => m.Name == _model.Name && m.Abrv == _model.Abrv));
    }

    [Fact]
    public async Task DeleteModelAsync_ReturnsNullCorrectly()
    {
        await _repoMock.DeleteModelAsync(id: 1);
        IEnumerable<IVehicleModel> model = await _repoMock.GetModelByIdAsync(1);
        model.Should().BeEmpty();
    }
}