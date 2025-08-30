using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Vehicle.Common;
using Vehicle.DAL.Context;
using Vehicle.Models.Common;
using Vehicle.Models.Common.Write;
using Vehicle.Models.DTOs;
using Vehicle.Models.DTOs.Write;
using Vehicle.Repository;
using Vehicle.Repository.Common;
using Vehicle.Service;
using Vehicle.Service.Common;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("https://localhost:5252");
builder.Services.AddProblemDetails(configure =>
{
    configure.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
    };
});
builder.Services.AddExceptionHandler<ExceptionHandler>();

builder.Services.AddScoped<IVehicleDbContext, VehicleDbContext>();

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

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IVehicleEngineRepository, VehicleEngineRepository>();
builder.Services.AddScoped<IVehicleMakeRepository, VehicleMakeRepository>();
builder.Services.AddScoped<IVehicleModelRepository, VehicleModelRepository>();
builder.Services.AddScoped<IVehicleOwnerRepository, VehicleOwnerRepository>();
builder.Services.AddScoped<IVehicleRegistrationRepository, VehicleRegistrationRepository>();

builder.Services.AddScoped<IVehicleEngineService, VehicleEngineService>();
builder.Services.AddScoped<IVehicleMakeService, VehicleMakeService>();
builder.Services.AddScoped<IVehicleModelService, VehicleModelService>();
builder.Services.AddScoped<IVehicleOwnerService, VehicleOwnerService>();
builder.Services.AddScoped<IVehicleRegistrationService, VehicleRegistrationService>();

builder.Services.AddSingleton<IExceptionHandler, ExceptionHandler>();

builder.Services.AddDbContext<VehicleDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });

});

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AllowFrontend");
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
