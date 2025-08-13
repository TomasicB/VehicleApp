using AutoMapper;
using Vehicle.DAL.Entities;
using Vehicle.Models.DTOs;
using Vehicle.Models.DTOs.Write;

namespace Vehicle.Common.Profiles;

public class VehicleOwnerProfile : Profile
{
    public VehicleOwnerProfile()
    {
        CreateMap<VehicleOwner, VehicleOwnerDTO>()
            .ReverseMap();
        CreateMap<VehicleEngineWriteDTO, VehicleOwner>();

        CreateMap<VehicleRegistration, VehicleRegistrationDTO>();
    }
}
