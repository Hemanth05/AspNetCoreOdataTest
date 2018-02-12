using System;
using AspNetCoreOdataTest.Model;
using AutoMapper;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;
using Microsoft.OData.UriParser;

namespace AspNetCoreOdataTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static IServiceProvider Container;

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddOData();
            services.AddSingleton(sp => new ODataUriResolver() { EnableCaseInsensitive = true });

            var provider = services.BuildServiceProvider();
            Container = provider;
            return provider;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var builder = new ODataConventionModelBuilder(app.ApplicationServices);
            builder.EntitySet<OrderDto>("Orders");
            builder.EnableLowerCamelCase();

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.MapODataServiceRoute("ODataRoute", "odata", builder.GetEdmModel());

                //// Work-around for #1175
                routeBuilder.EnableDependencyInjection(containerBuilder =>
                    containerBuilder.AddService<ODataUriResolver>(
                    Microsoft.OData.ServiceLifetime.Singleton,
                    _ => app.ApplicationServices.GetRequiredService<ODataUriResolver>()));
            });

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<OrderEntity, OrderGraphDto>()
                    .ForMember(o => o.Id, o => o.MapFrom(v => v.OrderId))
                    .ForMember(o => o.Name, o => o.MapFrom(v => v.OrderName))
                    .ForMember(o => o.Quantity, o => o.MapFrom(v => v.OrderQuantity))
                    .ForMember(o => o.GraphProperty, o => o.MapFrom(v => v.GraphProperty))
                    .ForMember(o => o.CityAddress, o => o.MapFrom(v => v.Address.City))
                    .ForMember(o => o.DebugMessage, o => o.MapFrom(v => v.DebugMessage));

                cfg.CreateMap<AddressEntity, AddressDto>()
                    .ForMember(o => o.City, o => o.MapFrom(v => v.City));
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}
