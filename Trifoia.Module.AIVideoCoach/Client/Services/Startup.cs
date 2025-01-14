using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using Oqtane.Services;
using Trifoia.Module.AIVideoCoach.Services;

namespace Trifoia.Module.AIVideoCoach.Client.Services
{
    public class Startup : IClientStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<AIVideoCoachService, AIVideoCoachService>();
            services.AddMudServices();
        }
    }
}
