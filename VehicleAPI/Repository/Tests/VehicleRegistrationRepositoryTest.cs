using FluentAssertions;
using NSubstitute;
using Vehicle.DAL.Context;
using Vehicle.Models.Common;
using Vehicle.Models.Common.Write;
using Vehicle.Models.DTOs;
using Vehicle.Repository.Common;
using Xunit;

namespace Project.Repository.Tests.Repositories;

public class VehicleRegistrationRepositoryTests
{
    private readonly IVehicleRegistrationRepository _repoMock;
    private readonly VehicleRegistrationDTO _registration = new VehicleRegistrationDTO 
        {
            RegistrationNumber = "NA-159-TO",
            VehicleEngine = new VehicleEngineDTO{ Type = "Hybrid", Abrv = "H" },
            VehicleOwner = new VehicleOwnerDTO { FirstName = "Josip", LastName = "Tomašić", DOB = new DateOnly(2000, 5, 14) },
            VehicleModel = new VehicleModelDTO
            {
                Name = "Golf 6",
                Abrv = "G6",
                VehicleMake = new VehicleMakeDTO { Name = "Volkswagen", Abrv = "VW" }
            }
        };

    public VehicleRegistrationRepositoryTests()
    {
        _repoMock = Substitute.For<IVehicleRegistrationRepository>();
    }

    [Fact]
    public async Task GetRegistrationAsync_Should_ReturnData()
    {
        IEnumerable<IVehicleRegistration> registration = await _repoMock.GetRegistrationsAsync();
        registration.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetRegistrationByIdAsync_ReturnsData()
    {
        IEnumerable<IVehicleRegistration> registration = await _repoMock.GetRegistrationByIdAsync(1);
        registration.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetRegistrationByIdAsync_ReturnsNull()
    {
        IEnumerable<IVehicleRegistration> registration = await _repoMock.GetRegistrationByIdAsync(100);
        registration.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task GetRegistrationByNameAsync_ReturnsData()
    {
        IEnumerable<IVehicleRegistration> registration = await _repoMock.GetRegistrationByNumberAsync("NA-452_DG");
        registration.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetRegistrationByNameAsync_ReturnsNull()
    {
        var registration = await _repoMock.GetRegistrationByNumberAsync("OS-111-OS");
        registration.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task InsertRegistrationAsync_ReturnsNewRegistrationWithId()
    {
        await _repoMock.Received(1).InsertRegistrationAsync(Arg.Is<IVehicleRegistration>(m => m.RegistrationNumber == _registration.RegistrationNumber),1,1,1);
    }

    [Fact]
    public async Task UpdateRegistrationAsync_RetrunsNewAbrvCorrectly()
    {
        _registration.RegistrationNumber = "NA-156-TO";
        await _repoMock.Received(1).UpdateRegistrationAsync(1, Arg.Is<IVehicleRegistrationWrite>(m => m.RegistrationNumber == _registration.RegistrationNumber));
    }

    [Fact]
    public async Task DeleteRegistrationAsync_ReturnsNullCorrectly()
    {
        await _repoMock.DeleteRegistrationAsync(id: 1);
        //await _repoMock.SaveChangesAsync();

        IEnumerable<IVehicleRegistration> Registration = await _repoMock.GetRegistrationByIdAsync(1);
        Registration.Should().BeEmpty();
    }
}