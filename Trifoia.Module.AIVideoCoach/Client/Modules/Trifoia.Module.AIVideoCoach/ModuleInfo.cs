using Oqtane.Models;
using Oqtane.Modules;

namespace Trifoia.Module.AIVideoCoach
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "AIVideoCoach",
            Description = "Open source edition of the Trifoia AI Video Coach Oqtane Module",
            Version = "1.0.0",
            ServerManagerType = "Trifoia.Module.AIVideoCoach.Manager.AIVideoCoachManager, Trifoia.Module.AIVideoCoach.Server.Oqtane",
            ReleaseVersions = "1.0.0",
            Dependencies = "Trifoia.Module.AIVideoCoach.Shared.Oqtane,MudBlazor",
            PackageName = "Trifoia.AIVideoCoach" 
        };
    }
}
