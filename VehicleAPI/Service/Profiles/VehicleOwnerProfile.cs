using AutoMapper;
using Vehicle.DAL.Entities;
using Vehicle.Models.DTOs;
using Vehicle.Models.DTOs.Write;

namespace Vehicle.Service.Profiles;

public class VehicleOwnerProfile : Profile
{
    public VehicleOwnerProfile()
    {
        CreateMap<VehicleOwner, VehicleOwnerDTO>();
        CreateMap<VehicleEngineWriteDTO, VehicleOwner>();

        CreateMap<VehicleRegistration, VehicleRegistrationDTO>();
    }
}
