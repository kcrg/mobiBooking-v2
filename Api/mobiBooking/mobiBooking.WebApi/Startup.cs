using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using mobiBooking.Data;
using mobiBooking.Repository;
using mobiBooking.Repository.Base;
using mobiBooking.Service;
using mobiBooking.Service.Interfaces;
using mobiBooking.Service.Services;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using IContainer = Autofac.IContainer;

namespace mobiBooking.WebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IContainer ApplicationContainer { get; private set; }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.ConfigureCors();
            services.ConfigureJwtAuthentication(Configuration);

            services.AddMvc();

            services.AddDbContext<MobiBookingDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DebugConnection")) // ReleaseConnection, DebugConnection            
            );

            services.ConfigureSwagger();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

           // services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

            var builder = new ContainerBuilder();
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<ServiceModule>();

            builder.Populate(services);
            ApplicationContainer = builder.Build();
           
            
            //services.AddScoped<IUsersService, UsersService>();
            //services.AddScoped<IAccountService, AccountService>();
            //services.AddScoped<IRoomService, RoomService>();
            //services.AddScoped<IReservationService, ReservationService>();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseCors(b =>
            {
                b.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .AllowCredentials();
            });
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }

    public static class ConfigureServicesExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                        .AllowCredentials();
                    });
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme { In = "header", Description = "Please enter JWT with Bearer into field", Name = "Authorization", Type = "apiKey" });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                { "Bearer", Enumerable.Empty<string>() },
            });
            });
        }

        public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration Configuration)
        {
            IConfigurationSection appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);


            AppSettings appSettings = appSettingsSection.Get<AppSettings>();
            byte[] key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
