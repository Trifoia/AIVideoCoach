using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Infrastructure;
using Oqtane.Repository.Databases.Interfaces;

namespace Trifoia.Module.AIVideoCoach.Repository
{
    public class AIVideoCoachContext : DBContextBase, ITransientService, IMultiDatabase
    {
        public virtual DbSet<Models.AIVideoCoach> AIVideoCoach { get; set; }

        public AIVideoCoachContext(IDBContextDependencies DBContextDependencies) : base(DBContextDependencies)
        {
            // ContextBase handles multi-tenant database connections
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Models.AIVideoCoach>().ToTable(ActiveDatabase.RewriteName("TrifoiaAIVideoCoach"));
        }
    }
}
