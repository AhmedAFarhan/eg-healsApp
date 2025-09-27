﻿using BuildingBlocks.DataAccess.Contracts;
using BuildingBlocks.Domain.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EGHeals.Infrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor(Func<IUserContext> getUserContext) : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await UpdateEntities(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private async Task UpdateEntities(DbContext context)
        {
            if (context is ApplicationDbContext db && db.IsSeeding) return;

            var userContext = getUserContext();
            var userId = await userContext.GetUserIdAsync();

            foreach (var entity in context.ChangeTracker.Entries<IEntity>())
            {
                entity.Entity.OwnershipId = SystemUserId.Of(userId == Guid.Empty ? Guid.NewGuid() : userId);

                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedBy = userId;
                    entity.Entity.CreatedAt = DateTime.Now;
                }

                if (entity.State == EntityState.Added || entity.State == EntityState.Modified)
                {
                    entity.Entity.LastModifiedBy = userId;
                    entity.Entity.LastModifiedAt = DateTime.Now;
                }
            }
        }
    }
}
