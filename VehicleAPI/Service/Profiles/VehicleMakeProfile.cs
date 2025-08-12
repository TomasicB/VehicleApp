using AutoMapper;
using Vehicle.DAL.Entities;
using Vehicle.Models.DTOs;
using Vehicle.Models.DTOs.Write;

namespace Vehicle.Service.Profiles;

public class VehicleMakeProfile : Profile
{
    public VehicleMakeProfile()
    {
        CreateMap<VehicleMake, VehicleMakeDTO>();
        CreateMap<VehicleMakeWriteDTO, VehicleMake>();

        CreateMap<VehicleModel, VehicleModelDTO>();
    }
}
