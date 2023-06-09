using Lakeshore.DataInterface.RepositoryFactory;
using Lakeshore.DataInterface.UnitOfWork;
using Lakeshore.Kafka.Client.Implementation;
using Lakeshore.Kafka.Client.Interfaces;
using Lakeshore.Kafka.Client;
using Lakeshore.Service;
using Serilog;
using Microsoft.EntityFrameworkCore;
using Lakeshore.Models.DbEntities;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<ProducerSettings>(configuration.GetSection(nameof(ProducerSettings)));

builder.Host.UseSerilog((hostCtx, logConfig) => logConfig
    .ReadFrom.Configuration(hostCtx.Configuration)
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();
builder.Services.AddScoped<Service>();

builder.Services.AddScoped<IKafkaProducerClient, KafkaProducerClient>();

builder.Services.AddDbContext<EpicorDataContex>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EpicorDataConnectionString"), sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure();
    });
    options.EnableSensitiveDataLogging(false);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddDbContext<EpicorStagingDataContex>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EpicorStagingConnectionString"), sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure();
    });
    options.EnableSensitiveDataLogging(false);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddScoped<IRepositoryFactory<EpicorDataContex>, UnitOfWork<EpicorDataContex>>();
builder.Services.AddScoped<IUnitOfWork<EpicorDataContex>, UnitOfWork<EpicorDataContex>>();

builder.Services.AddScoped<IRepositoryFactory<EpicorStagingDataContex>, UnitOfWork<EpicorStagingDataContex>>();
builder.Services.AddScoped<IUnitOfWork<EpicorStagingDataContex>, UnitOfWork<EpicorStagingDataContex>>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
