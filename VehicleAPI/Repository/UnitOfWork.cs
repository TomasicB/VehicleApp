using AutoMapper;
using Vehicle.DAL.Context;
using Vehicle.Repository.Common;

namespace Vehicle.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IVehicleDbContext _context;
        private readonly IMapper _mapper;
        public IVehicleMakeRepository MakeRepo { get; }
        public IVehicleModelRepository ModelRepo { get; }
        public IVehicleEngineRepository EngineRepo { get; }
        public IVehicleOwnerRepository OwnerRepo { get; }
        public IVehicleRegistrationRepository RegRepo { get; }

        public UnitOfWork(IVehicleDbContext context, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(context);
            _context = context;
            _mapper = mapper;

            MakeRepo = new VehicleMakeRepository(_context, _mapper);
            ModelRepo = new VehicleModelRepository(_context, _mapper);
            EngineRepo = new VehicleEngineRepository(_context, _mapper);
            OwnerRepo = new VehicleOwnerRepository(_context, _mapper);
            RegRepo = new VehicleRegistrationRepository(_context, _mapper);
        }

        public async Task<int> CommitAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
