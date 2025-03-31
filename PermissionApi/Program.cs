using Microsoft.EntityFrameworkCore;
using Nest;
using Confluent.Kafka;
using Serilog;
using Permission.Infrastructure.Adapters;
using Permission.Infrastructure.Context;
using Permission.Infrastructure.Extensions;
using Permission.Domain.Ports;
using Elasticsearch.Net;

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

var elasticUri = config["ElasticSearch:Uri"];
var elasticUsername = config["ElasticSearch:Username"];
var elasticPassword = config["ElasticSearch:Password"];

var connectionSettings = new ConnectionSettings(new Uri(elasticUri))
    .DefaultIndex(config["ElasticSearch:DefaultIndex"])
    .ServerCertificateValidationCallback(CertificateValidations.AllowAll)
    .DisableDirectStreaming()
    .PrettyJson();

if (!string.IsNullOrWhiteSpace(elasticUsername) && !string.IsNullOrWhiteSpace(elasticPassword))
{
    connectionSettings = connectionSettings.BasicAuthentication(elasticUsername, elasticPassword);
}

builder.Services.AddSingleton<IElasticClient>(new ElasticClient(connectionSettings));
builder.Services.AddElasticSearch();

var kafkaConfig = new ProducerConfig
{
    BootstrapServers = config["Kafka:BootstrapServers"]
};

builder.Services.AddSingleton<IProducer<Null, string>>(new ProducerBuilder<Null, string>(kafkaConfig).Build());

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IPermissionElasticRepository, PermissionElasticRepository>();
builder.Services.AddScoped<IPermissionKafkaRepository, PermissionKafkaRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddPersistence(config).AddDomainServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
