using Microsoft.EntityFrameworkCore;
using Permission.Domain.Ports;
using Permission.Infrastructure.Adapters;
using Permission.Infrastructure.Context;
using Permission.Infrastructure.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

const string serviceName = "permissions-api";
builder.AddServiceDefaults(serviceName);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AppDomain.CurrentDomain.Load("Permission.Aplication")));


builder.Services.AddDbContext<PersistenceContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("database"));
});

builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IPermissionElasticRepository, PermissionElasticRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddPersistence(config).AddDomainServices();

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
