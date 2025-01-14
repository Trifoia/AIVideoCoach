using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;
using System.Threading.Tasks;

namespace Trifoia.Module.AIVideoCoach.Repository
{
    public class AIVideoCoachRepository : ITransientService
    {
        private readonly IDbContextFactory<AIVideoCoachContext> _factory;

        public AIVideoCoachRepository(IDbContextFactory<AIVideoCoachContext> factory)
        {
            _factory = factory;
        }

        public IEnumerable<Models.AIVideoCoach> GetAIVideoCoachs()
        {
            using var db = _factory.CreateDbContext();
            return db.AIVideoCoach.ToList();
        }

        public Models.AIVideoCoach GetAIVideoCoach(int AIVideoCoachId)
        {
            return GetAIVideoCoach(AIVideoCoachId, true);
        }

        public Models.AIVideoCoach GetAIVideoCoach(int AIVideoCoachId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return db.AIVideoCoach.Find(AIVideoCoachId);
            }
            else
            {
                return db.AIVideoCoach.AsNoTracking().FirstOrDefault(item => item.AIVideoCoachId == AIVideoCoachId);
            }
        }

        public Models.AIVideoCoach AddAIVideoCoach(Models.AIVideoCoach AIVideoCoach)
        {
            using var db = _factory.CreateDbContext();
            db.AIVideoCoach.Add(AIVideoCoach);
            db.SaveChanges();
            return AIVideoCoach;
        }

        public Models.AIVideoCoach UpdateAIVideoCoach(Models.AIVideoCoach AIVideoCoach)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(AIVideoCoach).State = EntityState.Modified;
            db.SaveChanges();
            return AIVideoCoach;
        }

        public void DeleteAIVideoCoach(int AIVideoCoachId)
        {
            using var db = _factory.CreateDbContext();
            Models.AIVideoCoach AIVideoCoach = db.AIVideoCoach.Find(AIVideoCoachId);
            db.AIVideoCoach.Remove(AIVideoCoach);
            db.SaveChanges();
        }


        public async Task<IEnumerable<Models.AIVideoCoach>> GetAIVideoCoachsAsync(int ModuleId)
        {
            using var db = _factory.CreateDbContext();
            return await db.AIVideoCoach.Where(item => item.ModuleId == ModuleId).ToListAsync();
        }

        public async Task<Models.AIVideoCoach> GetAIVideoCoachAsync(int AIVideoCoachId)
        {
            return await GetAIVideoCoachAsync(AIVideoCoachId, true);
        }

        public async Task<Models.AIVideoCoach> GetAIVideoCoachAsync(int AIVideoCoachId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return await db.AIVideoCoach.FindAsync(AIVideoCoachId);
            }
            else
            {
                return await db.AIVideoCoach.AsNoTracking().FirstOrDefaultAsync(item => item.AIVideoCoachId == AIVideoCoachId);
            }
        }

        public async Task<Models.AIVideoCoach> AddAIVideoCoachAsync(Models.AIVideoCoach AIVideoCoach)
        {
            using var db = _factory.CreateDbContext();
            db.AIVideoCoach.Add(AIVideoCoach);
            await db.SaveChangesAsync();
            return AIVideoCoach;
        }

        public async Task<Models.AIVideoCoach> UpdateAIVideoCoachAsync(Models.AIVideoCoach AIVideoCoach)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(AIVideoCoach).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return AIVideoCoach;
        }

        public async Task DeleteAIVideoCoachAsync(int AIVideoCoachId)
        {
            using var db = _factory.CreateDbContext();
            Models.AIVideoCoach AIVideoCoach = db.AIVideoCoach.Find(AIVideoCoachId);
            db.AIVideoCoach.Remove(AIVideoCoach);
            await db.SaveChangesAsync();
        }
    }
}
