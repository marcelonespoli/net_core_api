using AutoMapper;
using Conferences.Infra.CrossCutting.IoC;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Conferences.Service.Api
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
            services.AddOptions();

            services.AddMvc(options =>
            {
                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
                //options.Filters.Add(new ServiceFilterAttribute(typeof(GlobalActionLogger)));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddAutoMapper();

            //services.AddMediatR(typeof(Startup));
            var assembly = AppDomain.CurrentDomain.Load("Conferences.Domain");
            services.AddMediatR(assembly);

            NativeInjectorBootStrapper.RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
