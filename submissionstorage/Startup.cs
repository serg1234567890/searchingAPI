using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using submissionstorage.Entities;
using submissionstorage.Entities.Searching;
using submissionstorage.Stories;
using submissionstorage.Stories.Common;

namespace submissionstorage
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
            services.AddEntityFrameworkContext<CommonContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DevConnection"));
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddEntityFrameworkStoreService<Submission, SubmissionStore>();
            services.AddEntityFrameworkStoreService<Submission_type, SubmissionTypeStore>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .WithOrigins(new[] {
                        "http://localhost:5200",
                        "http://localhost:5100"
                    })
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .Build());
            });
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");

            app.UseMvc();
        }
    }
}
