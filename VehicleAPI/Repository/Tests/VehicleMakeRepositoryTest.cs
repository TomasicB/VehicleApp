//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using Vehicle.DAL.Context;
//using Vehicle.DAL.Entities;
//using Vehicle.Repository.Common;
//using Xunit;

//namespace Vehicle.Repository.Tests
//{
//    public class VehicleMakeRepositoryTest
//    {
//        private readonly IVehicleMakeRepository _makeRepo;

//        public VehicleMakeRepositoryTest(IVehicleMakeRepository makeRepo)
//        {
//            _makeRepo = makeRepo;
//        }

//        [Fact]
//        public async Task GetMakeByName_ShouldReturnCorrectMake()
//        {
//            // Act
//            var result = await _makeRepo.GetMakeByName("Škoda");

//            // Assert
//            result.Should().NotBeNullOrEmpty();
//            result.Should().ContainSingle()
//                  .Which.Name.Should().Be("Škoda");
//        }
//    }
//}

