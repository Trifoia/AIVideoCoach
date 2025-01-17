using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oqtane.Infrastructure;
using Oqtane.Modules;
using Oqtane.Repository;
using Trifoia.Module.AIVideoCoach.Extensions;
using Trifoia.Module.AIVideoCoach.Repository;
using Trifoia.Module.AIVideoCoach.Services;
using Oqtane.Services;
using OpenAI.Embeddings;
using Microsoft.Extensions.AI;
using Trifoia.Module.AIVideoCoach.Server.Services;
using Trifoia.Module.AIVideoCoach.Shared.Interfaces;

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
            services.AddTransient<IChatClientFactory<IEmbeddingClient>, EmbeddingClientFactory>();
            //Register ChatBot Services
            services.AddTransient<IAIVideoCoachServerService, AIVideoCoachServerService>();
        }
    }
}
