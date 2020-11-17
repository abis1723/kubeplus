using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using KubePlus.Utils;
using KubePlus.Data;
using Nest;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using KubePlus.Models;

namespace KubePlus
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            

        //register the client as a singleton
        // services.Add(ServiceDescriptor.Singleton<IElasticClient>(
        //     new ElasticClient(new ConnectionSettings(new Uri("http://localhost:9200")))));
            

            // services.TryAddScoped<IHttpContextAccessor, HttpContextAccessor>();
            services.Configure<Command>(Configuration.GetSection("command"));
            // services.AddScoped<IStudentRepo, KubePlusRepo>;
            services.AddHostedService<ElasticsearchHostedService>();
            services.AddSingleton<IStudentRepo, KubePlusRepo>();
            services.AddElasticsearch(Configuration);
            services.AddControllers();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
