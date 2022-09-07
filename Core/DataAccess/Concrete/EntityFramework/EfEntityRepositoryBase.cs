using Core.DataAccess.Abstract;
using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using MusicLibrary.DataAccess.Extensions;
using System.Linq.Expressions;

namespace Core.DataAccess.Concrete.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext, new()
    {
        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            using TContext context = new();
            return filter == null
                ? context.Set<TEntity>().ToList()
                : context.Set<TEntity>().Where(filter).ToList();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using TContext context = new();
            return context.Set<TEntity>().FirstOrDefault(filter);
        }

        public void Add(TEntity entity)
        {
            using TContext context = new();
            int id = GetNextId();
            entity.GetType().GetProperties()[0].SetValue(entity, id);
            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Added;
            context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            using TContext context = new();
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            using TContext context = new();
            var deletedEntity = context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            context.SaveChanges();
        }

        public int GetNextId()
        {
            using TContext context = new();
            var result = context.Set<TEntity>().ToList()
                .Select(t => t.GetType().GetProperties()[0]
                .GetValue(t)).LastOrDefault() as int?;

            //var obj = context.Set<TEntity>().LastOrDefault().GetType().GetProperties()[0];
            //var temp = obj.GetValue(obj) as int?;

            return result + 1 ?? 1;
        }

        public void DeleteAll()
        {
            using TContext context = new();
            context.Set<TEntity>().Clear();
            context.SaveChanges();
        }
    }
}
