using Microsoft.EntityFrameworkCore;
using Vehicle.DAL.Context;
using Vehicle.Models.Common;
using Vehicle.Models.Common.Write;
using Vehicle.Repository.Common;
using Vehicle.Repository;
using Vehicle.Models.DTOs;
using Vehicle.Models.DTOs.Write;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IVehicleDbContext, VehicleDbContext>();

builder.Services.AddScoped<IVehicleEngineRepository, VehicleEngineRepository>();
builder.Services.AddScoped<IVehicleMakeRepository, VehicleMakeRepository>();
builder.Services.AddScoped<IVehicleModelRepository, VehicleModelRepository>();
builder.Services.AddScoped<IVehicleOwnerRepository, VehicleOwnerRepository>();
builder.Services.AddScoped<IVehicleRegistrationRepository, VehicleRegistrationRepository>();

builder.Services.AddScoped<IVehicleEngineWrite, VehicleEngineWriteDTO>();
builder.Services.AddScoped<IVehicleMakeWrite, VehicleMakeWriteDTO>();
builder.Services.AddScoped<IVehicleModelWrite, VehicleModelWriteDTO>();
builder.Services.AddScoped<IVehicleOwnerWrite, VehicleOwnerWriteDTO>();
builder.Services.AddScoped<IVehicleRegistrationWrite, VehicleRegistrationWriteDTO>();

builder.Services.AddScoped<IVehicleEngine, VehicleEngineDTO>();
builder.Services.AddScoped<IVehicleMake, VehicleMakeDTO>();
builder.Services.AddScoped<IVehicleModel, VehicleModelDTO>();
builder.Services.AddScoped<IVehicleOwner, VehicleOwnerDTO>();
builder.Services.AddScoped<IVehicleRegistration, VehicleRegistrationDTO>();

builder.Services.AddDbContext<VehicleDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
