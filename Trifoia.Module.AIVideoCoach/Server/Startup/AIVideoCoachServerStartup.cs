using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Oqtane.Infrastructure;
using Trifoia.Module.AIVideoCoach.Extensions;
using Trifoia.Module.AIVideoCoach.Repository;
using Trifoia.Module.AIVideoCoach.Services;

namespace Trifoia.Module.AIVideoCoach.Startup
{
    public class AIVideoCoachServerStartup : IServerStartup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // not implemented
        }

        public void ConfigureMvc(IMvcBuilder mvcBuilder)
        {
            // not implemented
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextFactory<AIVideoCoachContext>(opt => { }, ServiceLifetime.Transient);
            //Add ChatBot Configuration and Services
            services.ConfigureClientOptions();
            //Register ChatBot Services
            services.AddTransient<IAIVideoCoachServerService, AIVideoCoachServerService>();
        }
    }
}
