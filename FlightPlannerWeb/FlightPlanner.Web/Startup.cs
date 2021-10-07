using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using FlightPlanner.Data;
using FlightPlanner.Services;
using FlightPlanner.Services.Validators;
using FlightPlanner.Web.Authentication;
using FlightPlanner.Web.Mappings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FlightPlanner.Web", Version = "v1" });
            });
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            services.AddDbContext<FlightPlannerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("flight-planner")));
            services.AddScoped<IFlightDbContext, FlightPlannerDbContext>();
            services.AddScoped<IDbService, DbService>();
            services.AddScoped<IEntityService<Flight>, EntityService<Flight>>();
            services.AddScoped<IEntityService<Airport>, EntityService<Airport>>();
            services.AddScoped<IDbServiceExtended, DbServiceExtended>();
            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<IValidator, AirportCityValidator>();
            services.AddScoped<IValidator, AirportCodeValidator>();
            services.AddScoped<IValidator, AirportCountryValidator>();
            services.AddScoped<IValidator, ArrivalTimeValidator>();
            services.AddScoped<IValidator, CarrierValidator>();
            services.AddScoped<IValidator, DepartureTimeValidator>();
            services.AddScoped<IValidator, TimeFrameValidator>();
            services.AddScoped<IValidator, AirportCodesEqualityValidator>();
            services.AddScoped<ISearchValidator, SearchValidator>();

            var cfg = AutoMapperConfig.GetConfig();
            services.AddSingleton(typeof(IMapper), cfg);
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlightPlanner.Web v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
