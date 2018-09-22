using Microsoft.EntityFrameworkCore;
using SimpleTaskManager.API.DAL;
using SimpleTaskManager.API.Model.BLL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleTaskManager.API.Model.DAL.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly STMContext _context;
        protected readonly DbSet<TEntity> Entities;
        public Repository(STMContext context)
        {
            _context = context;
            Entities = _context.Set<TEntity>();
        }

        public void Save() => _context.SaveChanges();

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            Save();
        }

        public TEntity Get(string name)
        {
            return Entities.Find(name);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Entities.ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate);
        }

        public void Add(TEntity entity)
        {
            Entities.Add(entity);
            Save();
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Entities.AddRange(entities);
            Save();
        }
    }
}
