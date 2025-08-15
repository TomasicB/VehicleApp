namespace Vehicle.Repository.Common
{
    public interface IUnitOfWork : IDisposable
    {
        IVehicleMakeRepository MakeRepo { get; }
        IVehicleModelRepository ModelRepo { get; }
        IVehicleEngineRepository EngineRepo { get; }
        IVehicleOwnerRepository OwnerRepo { get; }
        IVehicleRegistrationRepository RegRepo { get; }

        Task<int> CommitAsync();
    }
}