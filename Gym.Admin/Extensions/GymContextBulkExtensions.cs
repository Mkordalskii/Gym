using Gym.Data.Data;
using Gym.Data.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace Gym.Admin.Extensions
{
    public static class GymContextBulkExtensions
    {
        public static async Task<int> SoftDeleteByIdsAsync<T>(
            this GymContext context,
            DbSet<T> dbSet,
            int[]? ids,
            string? deletedBy) where T : BaseEntity
        {
            if (ids == null || ids.Length == 0)
            {
                return 0;
            }

            var entities = await dbSet.Where(e => ids.Contains(e.Id)).ToListAsync();
            if (entities.Count == 0)
            {
                return 0;
            }

            var now = DateTime.UtcNow;
            var user = deletedBy ?? "Admin";

            foreach (var entity in entities)
            {
                entity.IsActive = false;
                entity.DeletedAt = now;
                entity.DeletedBy = user;
                entity.ModifiedAt = now;
                entity.ModifiedBy = user;
            }

            await context.SaveChangesAsync();
            return entities.Count;
        }
    }
}
