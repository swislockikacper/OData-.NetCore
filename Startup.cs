using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OData.NetCore.Configuration;
using OData.NetCore.Models;

namespace OData.NetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment appHost)
        {
            Configuration = configuration;
            AppHost = appHost;
        }

        public IConfiguration Configuration { get; }
        private IHostingEnvironment AppHost;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                }); 

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlite($"Data source={AppHost.ContentRootPath}/Database.db");
            });

            services.AddOData();
            services.AddTransient<DatabaseModelBuilder>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DatabaseModelBuilder modelBuilder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc(routeBuilder =>
            {
                routeBuilder.MapODataServiceRoute("ODataRoutes", "odata", modelBuilder.GetEdmModel(app.ApplicationServices));
            });
        }
    }
}
