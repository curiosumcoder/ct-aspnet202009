using Microsoft.EntityFrameworkCore;
using Northwind.Store.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Store.Data
{
    public partial class NWContext
    {
        #region Aplicación de cambios general

        /// <summary>
        /// Aplicación de cambios en la fuente de datos.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="root"></param>
        /// <returns></returns>
        public async Task<int> ApplyChanges<TEntity>(TEntity root) where TEntity : class, IObjectWithState
        {
            Set<TEntity>().Add(root);
            CheckForEntitiesWithoutStateInterface(this);
            foreach (var entry in ChangeTracker.Entries<IObjectWithState>())
            {
                IObjectWithState stateInfo = entry.Entity;
                if (stateInfo.State == ModelState.Modified)
                {
                    entry.State = EntityState.Unchanged;
                    foreach (var property in stateInfo.ModifiedProperties)
                    {
                        entry.Property(property).IsModified = true;
                    }
                }
                else
                {
                    entry.State = ConvertState(stateInfo.State);
                }
            }

            return await SaveChangesAsync();
        }

        /// <summary>
        /// Convierte el estado entre el estado personalizado y el estado del Entity Framewok
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private static EntityState ConvertState(ModelState state)
        {
            switch (state)
            {
                case ModelState.Added:
                    return EntityState.Added;
                case ModelState.Modified:
                    return EntityState.Modified;
                case ModelState.Deleted:
                    return EntityState.Deleted;
                default:
                    return EntityState.Unchanged;
            }
        }

        /// <summary>
        /// Se confirma que todos los objetos del modelo implementan la interface IObjectWithState
        /// </summary>
        /// <param name="context"></param>
        private static void CheckForEntitiesWithoutStateInterface(NWContext context)
        {
            var entitiesWithoutState =
            from e in context.ChangeTracker.Entries()
            where !(e.Entity is IObjectWithState)
            select e;
            if (entitiesWithoutState.Any())
            {
                throw new NotSupportedException("Todas la entidades deben implementar IObjectWithState.");
            }
        }
        #endregion
    }
}
