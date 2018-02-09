using AspNetCoreOdataTest.Model;
using AutoMapper;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreOdataTest
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
            services.AddOData();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var builder = new ODataConventionModelBuilder(app.ApplicationServices);
            builder.EntitySet<OrderDto>("Orders");

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.MapODataServiceRoute("ODataRoute", "odata", builder.GetEdmModel());

                // Work-around for #1175
                routeBuilder.EnableDependencyInjection();
            });

            Mapper.Initialize(cfg =>
                cfg.CreateMap<BusinessOrder, OrderDto>()
                    .ForMember(o => o.Id, o => o.MapFrom(v => v.OrderId))
                    .ForMember(o => o.Name, o => o.MapFrom(v => v.OrderName))
                    .ForMember(o => o.Quantity, o => o.MapFrom(v => v.OrderQuantity))
                    .ForMember(o => o.Status, o => o.MapFrom(v => v.DebugHelper))
            );

            Mapper.AssertConfigurationIsValid();
        }
    }
}
