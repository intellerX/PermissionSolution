var builder = DistributedApplication.CreateBuilder(args);

var kafka = builder.AddContainer("kafka", "confluentinc/cp-kafka:7.3.2")
    .WithEnvironment("KAFKA_ADVERTISED_LISTENERS", "PLAINTEXT://kafka:9092")
    .WithEnvironment("KAFKA_ZOOKEEPER_CONNECT", "zookeeper:2181")
    .WithEndpoint(9092, targetPort: 9092);

var zookeeper = builder.AddContainer("zookeeper", "confluentinc/cp-zookeeper:7.3.2")
    .WithEnvironment("ZOOKEEPER_CLIENT_PORT", "2181")
    .WithEndpoint(2181, targetPort: 2181);

var sqlServer = builder.AddSqlServer("sqlserver", port: 1433, password: builder.AddParameter("sql-password", "sql-password123456789"));

var api = builder.AddProject<Projects.PermissionApi>("permissions-api")
    .WithExternalHttpEndpoints();

var elastic = builder.AddContainer("elasticsearch", "docker.elastic.co/elasticsearch/elasticsearch:8.10.2")
    .WithEnvironment("discovery.type", "single-node")
    .WithEnvironment("xpack.security.enabled", "false")
    .WithEnvironment("ES_JAVA_OPTS", "-Xms512m -Xmx512m")
    .WithHttpEndpoint(9200, targetPort: 9200);

builder.Build().Run();
