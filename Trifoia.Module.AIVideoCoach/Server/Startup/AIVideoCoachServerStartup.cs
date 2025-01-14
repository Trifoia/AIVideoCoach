using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Oqtane.Infrastructure;
using Trifoia.Module.AIVideoCoach.Repository;

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
        }
    }
}
