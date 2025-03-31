var builder = DistributedApplication.CreateBuilder(args);

var kafka = builder.AddKafka("kafka");

var api = builder.AddProject<Projects.PermissionApi>("permissions-api")
    .WithExternalHttpEndpoints();

var elastic = builder.AddContainer("elasticsearch", "docker.elastic.co/elasticsearch/elasticsearch:8.10.2")
    .WithEndpoint(9200, targetPort: 9200)
    .WithEnvironment("discovery.type", "single-node");

var sqlServer = builder.AddSqlServer("sqlserver", port: 1433, password: builder.AddParameter("sql-password", "sql-password123456789"));

builder.Build().Run();
