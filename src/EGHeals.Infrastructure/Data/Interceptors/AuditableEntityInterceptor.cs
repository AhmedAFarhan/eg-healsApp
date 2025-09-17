using Microsoft.EntityFrameworkCore.Diagnostics;
using EGHeals.Application.Contracts.Users;

namespace EGHeals.Infrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor(Func<IUserContext> getUserContext) : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntities(DbContext context)
        {
            if (context is null) return;

            var userContext = getUserContext();

            //foreach (var entity in context.ChangeTracker.Entries<IEntity>())
            //{
            //    if (entity.State == EntityState.Added)
            //    {
            //        entity.Entity.CreatedBy = SystemUserId.Of(userContext.GetUserId());
            //        entity.Entity.CreatedAt = DateTime.Now;
            //    }

            //    if (entity.State == EntityState.Added || entity.State == EntityState.Modified)
            //    {
            //        entity.Entity.LastModifiedBy = SystemUserId.Of(userContext.GetUserId());
            //        entity.Entity.LastModifiedAt = DateTime.Now;
            //    }
            //}
        }
    }
}
