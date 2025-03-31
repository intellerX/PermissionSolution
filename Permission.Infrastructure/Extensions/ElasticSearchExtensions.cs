using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permission.Infrastructure.Extensions
{
    public static class ElasticSearchExtensions
    {
        public static IServiceCollection AddElasticSearch(this IServiceCollection services)
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DisableDirectStreaming()
                .PrettyJson()
                .DefaultIndex("permissions");

            var client = new ElasticClient(settings);
            services.AddSingleton<IElasticClient>(client);

            return services;
        }
    }
}
