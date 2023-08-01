using HT366.Application.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;

namespace HT366.Application
{
    public class AppicationDependencyResolver
    {
        public static void Register(IServiceCollection services, IConfiguration Configuration)
        {
            Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Debug()
           .WriteTo.File("logs/debug/debug_.txt", rollingInterval: RollingInterval.Day)
           .CreateLogger();
            services.AddScoped<IExamService, ExamService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
            services.AddOptions();
            services.AddMediatR(typeof(LibraryEntrypoint).Assembly);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
