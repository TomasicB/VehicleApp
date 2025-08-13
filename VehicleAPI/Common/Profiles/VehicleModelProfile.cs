using AutoMapper;
using Vehicle.DAL.Entities;
using Vehicle.Models.Common;
using Vehicle.Models.DTOs;
using Vehicle.Models.DTOs.Write;

namespace Vehicle.Common.Profiles;

public class VehicleModelProfile : Profile
{
    public VehicleModelProfile()
    {
        CreateMap<VehicleModel, VehicleModelDTO>()
            .ReverseMap();
        CreateMap<VehicleModelWriteDTO, VehicleModel>();

        CreateMap<VehicleMake, VehicleMakeDTO>();
        CreateMap<VehicleMakeWriteDTO, VehicleModel>();

        CreateMap<VehicleRegistration, VehicleRegistrationDTO>();
    }
}
