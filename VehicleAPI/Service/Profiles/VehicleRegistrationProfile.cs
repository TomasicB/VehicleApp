using AutoMapper;
using Vehicle.DAL.Entities;
using Vehicle.Models.DTOs;
using Vehicle.Models.DTOs.Write;

namespace Vehicle.Service.Profiles;

public class VehicleRegistrationProfile : Profile
{
    public VehicleRegistrationProfile()
    {
        CreateMap<VehicleRegistration, VehicleRegistrationDTO>();
        CreateMap<VehicleRegistrationWriteDTO, VehicleRegistration>();

        CreateMap<VehicleEngine, VehicleEngineDTO>();
        CreateMap<VehicleEngineWriteDTO, VehicleEngine>();

        CreateMap<VehicleModel, VehicleModelDTO>();
        CreateMap<VehicleModelWriteDTO, VehicleModel>();

        CreateMap<VehicleOwner, VehicleOwnerDTO>();
        CreateMap<VehicleOwnerWriteDTO, VehicleOwner>();
    }
}
