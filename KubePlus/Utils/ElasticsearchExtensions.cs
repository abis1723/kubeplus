using System;
//using KubePlus.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace KubePlus.Utils
{
    public static class ElasticsearchExtensions
    {
        public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = new ConnectionSettings(new Uri(configuration["elasticsearch:url"]));

            var defaultIndex = configuration["elasticsearch:index"];

            if (!string.IsNullOrEmpty(defaultIndex))
                settings = settings.DefaultIndex(defaultIndex);

            // // The authentication options below are set if you have non-null/empty
            // // settings in the configuration.  These are just samples -- there are
            // // other authentication methods available.
            // var apiKeyId = configuration["elasticsearch:apiKeyId"];
            // var apiKey = configuration["elasticsearch:apiKey"];

            // if (!string.IsNullOrEmpty(apiKeyId) && !string.IsNullOrEmpty(apiKey))
            // {
            //     settings = settings.ApiKeyAuthentication(apiKeyId, apiKey);
            // }
            // else
            // {
            //     var basicAuthUser = configuration["ElasticsearchSettings:basicAuthUser"];
            //     var basicAuthPassword = configuration["ElasticsearchSettings:basicAuthPassword"];

            //     if (!string.IsNullOrEmpty(basicAuthUser) && !string.IsNullOrEmpty(basicAuthPassword))
            //         settings = settings.BasicAuthentication(basicAuthUser, basicAuthPassword);
            // }

            var client = new ElasticClient(settings);

            // ElasticClient is thread-safe
            // See https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/lifetimes.html
            services.AddSingleton<IElasticClient>(client);
        }
    }
}