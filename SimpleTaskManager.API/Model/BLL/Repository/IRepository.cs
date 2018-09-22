﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SimpleTaskManager.API.Model.BLL.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(string name);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);
    }
}
