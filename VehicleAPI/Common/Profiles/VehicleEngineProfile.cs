using AutoMapper;
using Vehicle.DAL.Entities;
using Vehicle.Models.DTOs;
using Vehicle.Models.DTOs.Write;

namespace Vehicle.Common.Profiles;

public class VehicleEngineProfile : Profile
{
    public VehicleEngineProfile()
    {
        CreateMap<VehicleEngine, VehicleEngineDTO>()
            .ReverseMap();
        CreateMap<VehicleEngineWriteDTO, VehicleEngine>();

        CreateMap<VehicleRegistration, VehicleRegistrationDTO>();
    }
}
