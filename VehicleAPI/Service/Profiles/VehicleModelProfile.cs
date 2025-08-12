using AutoMapper;
using Vehicle.DAL.Entities;
using Vehicle.Models.Common;
using Vehicle.Models.DTOs;
using Vehicle.Models.DTOs.Write;

namespace Vehicle.Service.Profiles;

public class VehicleModelProfile : Profile
{
    public VehicleModelProfile()
    {
        CreateMap<VehicleModel, VehicleModelDTO>();
        CreateMap<VehicleModelWriteDTO, VehicleModel>();

        CreateMap<VehicleMake, VehicleMakeDTO>();
        CreateMap<VehicleMakeWriteDTO, VehicleModel>();

        CreateMap<VehicleRegistration, VehicleRegistrationDTO>();
    }
}
