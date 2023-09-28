using HT366.Domain.Entities;
using HT366.Domain.Interfaces;
using HT366.Infrastructure.Persistence;
using HT366.Infrastructure.Persistence.Repositories;
using HT366.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HT366.Infrastructure
{
    public static class InfraDependencyResolver
    {
        public static void Register(IServiceCollection services, IConfiguration Configuration)
        {
            var connectionString = Configuration.GetConnectionString("MyConnectionString");
            var serverVersion = new MySqlServerVersion(ServerVersion.AutoDetect(connectionString));
            services.AddDbContext<ApplicationContext>(opt => opt.UseMySql(connectionString, serverVersion));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ICachingService, CachingService>();
            services.AddScoped<IEmailService, EmailService>();
            //services.AddScoped<IEmailService, EmailService>();
            //services.AddScoped(typeof(ICachingService<>), typeof(CachingService<>));
            services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationContext>();
            //services.AddScoped<IIdentityService, IdentityService>();
            //services.AddScoped<IProductService, ProductService>();
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(Configuration["JWT:key"] ?? ""))
                };
            });

            //TODO use Hangfire or Quartz for config job
            //Hangfire
            /*services.AddHangfire(x =>
            {
                x.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseRedisStorage(Configuration["REDIS:ConnectionString"]);
            });
            services.AddHangfireServer();
            //Config Redis for caching

            */
            services.AddStackExchangeRedisCache(options => { options.Configuration = Configuration["REDIS:ConnectionString"]; });
        }

        //Job Register
        //public static void JobConfigure()
        //{
        //    RecurringJob.AddOrUpdate<Jobs>("g1", job => job.SendGetRequest(), Cron.Minutely());
        //}

        public class MysqlEntityFrameworkDesignTimeServices : IDesignTimeServices
        {
            public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
            {
                serviceCollection.AddEntityFrameworkMySql();
                new EntityFrameworkRelationalDesignServicesBuilder(serviceCollection)
                    .TryAddCoreServices();
            }
        }
    }
}